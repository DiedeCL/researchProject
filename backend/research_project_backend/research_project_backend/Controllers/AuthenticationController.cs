using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using research_project_backend.Data.Domains;
using research_project_backend.Models;
using researchproject.Models;

namespace research_project_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IOptions<TokenSettings> _tokenSettings;

        public AuthenticationController(UserManager<User> userManager, RoleManager<Role> roleManager,
            IPasswordHasher<User> passwordHasher, IOptions<TokenSettings> tokenSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _passwordHasher = passwordHasher;
            _tokenSettings = tokenSettings;
        }

        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> CreateToken([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return Unauthorized();
            if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password)
                != PasswordVerificationResult.Success) return Unauthorized();
            var token = await CreateJwtToken(user);
            var returnModel = new LoginReturnModel
            {
                Email = user.Email,
                Role = await _userManager.GetRolesAsync(user),
                Token = token
            };
            if (user.CompanyId > 0)
            {
                returnModel.CompanyId = user.CompanyId;
            }
               
            return Ok(returnModel);
        }



        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
            };
            
            var role = DetermineRole(user);

            if (role.Equals(Role.Company) && model.CompanyId > 0)
            {
                user.CompanyId = model.CompanyId;
            }
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                
                await EnsureRoleExists(role);
                await _userManager.AddToRoleAsync(user, role);
                return Ok();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return BadRequest(ModelState);
        }


        [Route("registerCompany")]
        public async Task<IActionResult> RegisterCompany([FromBody] RegisterCompanyModel model)
        {
           
            var user = new User
            {
                UserName = model.LoginEmail,
                Email = model.LoginEmail,
            };
            var contactCompany = new ContactCompany
            {
                Email = model.EmailContactPerson,
                JobTitle = model.JobTitleContactPerson,
                PhoneNumber = model.PhoneNumberContactPerson,
                Name = model.FirstNameContactPerson,
                LastName = model.LastNameContactPerson,
            };
            var companyPromoter = new CompanyPromoter
            {
                Email = model.EmailPromoter,
                Name = model.FirstNamePromoter,
                LastName = model.LastNamePromoter,
                PhoneNumber = model.PhoneNumberPromoter,
                JobTitle = model.JobTitlePromoter
            };
            var address = new Address
            {
                Street = model.Street,
                PostalNumber = model.PostalNumber,
                Township = model.Township,
                Number = model.Number
            };

            var company = new Company
            {
                AmountOfITPersonnel = model.AmountOfItEmployees,
                AmountOfPersonnel = model.AmountOfEmployees,
                NameCompany = model.NameCompany,
                ContactCompany = contactCompany,
                Promoter = companyPromoter,
                AddressCompany = address,
                
            };
            try
            {
                var comppanyId = _companyData.AddCompany(company);
                user.CompanyId = comppanyId;

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var role = Role.Company;
                    await EnsureRoleExists(role);
                    await _userManager.AddToRoleAsync(user, role);

                    return Ok();
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
           

        }

        [HttpPost]
        [Route("RegistrationLink")]
        public async Task<IActionResult> SendRegistrationLink([FromBody] RegistrationLinkModel model)
        {
            var code = GenerateRandomString(6);
            var registrationLinkCode = new RegistrationLinkCode()
            {
                Code = code
            };
            _registrationLinkCodeData.AddRegistrationLinkCode(registrationLinkCode);
            var message = new EmailMessage(new string[] { model.Email }, "Registratie mijnStage.be", @"http://localhost:8080/#/registratiestudent/" + code);
            await _emailSender.SendEmailAsync(message);
            return Ok();
        }

        [HttpPost]
        [Route("ValidateRegistrationUrl")]
        public async Task<IActionResult> ValidateRegistrationUrl([FromBody] ValidatePasswordUrlModel validateRegistrationUrlModel)
        {
            var validUrl = _registrationLinkCodeData.ValidateRegistrationLinkCode(validateRegistrationUrlModel.Code);

            if (!validUrl)
                return BadRequest();

            return Ok();
        }
        
        [HttpPost]
        [Route("newPassword")]
        public async Task<IActionResult> NewPassword([FromBody] NewPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return Unauthorized();
            if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password)
                != PasswordVerificationResult.Success) return Unauthorized();
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
            if(result.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("ValidatePasswordResetUrl")]
        public async Task<IActionResult> ValidatePasswordUrl([FromBody] ValidatePasswordUrlModel validatePasswordUrlModel)
        {
            var validUrl =  _passwordUrlCode.ValidatePasswordUrlCode(validatePasswordUrlModel.Code);

            if (!validUrl)
                return BadRequest();

            return Ok();
        }

        [HttpPost]
        [Route("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel resetPasswordModel)
        {
            var user = _passwordUrlCode.getPasswordUrlCodeUser(resetPasswordModel.Code);

            var validUrl = _passwordUrlCode.ValidatePasswordUrlCode(resetPasswordModel.Code);

            if (!validUrl)
                return BadRequest();

            if (user == null)
                return Unauthorized();

            

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, resetPasswordModel.NewPassword);
            if (result.Succeeded)
            {
                _passwordUrlCode.RemovePasswordUrlCode(resetPasswordModel.Code);
                return Ok();
            }

            return BadRequest();
        }


        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel forgotPasswordModel)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
            if (user == null)
                return Unauthorized();

            var code = GenerateRandomString(6);

            var forgottenPasswordUrlCode = new ForgottenPasswordUrlCode
            {
                Code = code,
                user = user
            };

            _passwordUrlCode.AddPasswordUrlCode(forgottenPasswordUrlCode);

            var message = new EmailMessage(new string[] { forgotPasswordModel.Email}, "Reset password", @"http://localhost:8080/?#/wachtwoordResetten/" + code);
            await _emailSender.SendEmailAsync(message);

            return Ok();
        }

        private static string DetermineRole(User user)
        {
            var email = user.Email.ToLower();
            return  email.Equals(("Marijke.Willems@PXL.BE").ToLower()) ? Role.InternshipCoordinator :
                email.EndsWith("@pxl.be") ? Role.Teacher : email.EndsWith("@student.pxl.be") ?
                    Role.Student : Role.Company;
        }

        private async Task EnsureRoleExists(string roleName)
        {
            if (await _roleManager.RoleExistsAsync(roleName)) return;

            await _roleManager.CreateAsync(new Role
            {
                Name = roleName,
                NormalizedName = roleName.ToUpper()
            });
        }

        private async Task<string> CreateJwtToken(User user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            claims.Add(new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.UserName.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email.ToString()));

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var keyBytes = Encoding.UTF8.GetBytes(_tokenSettings.Value.Key);
            var symmetricSecurityKey = new SymmetricSecurityKey(keyBytes);
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _tokenSettings.Value.Issuer,
                _tokenSettings.Value.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(_tokenSettings.Value.ExpirationTimeInMinutes),
                signingCredentials: signingCredentials);

            var encryptedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encryptedToken;
        }

        private string GenerateRandomString(int length)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char letter;

            for (int i = 0; i < length; i++)
            {
                int asciiValue = random.Next(48,123);
                while ((asciiValue > 57 && asciiValue < 65) || (asciiValue > 90 && asciiValue < 97))
                {
                    asciiValue = random.Next(48, 127);
                }
                letter = Convert.ToChar(asciiValue);
                builder.Append(letter);
            }

            return builder.ToString();
        }
    }
}