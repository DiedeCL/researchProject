using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using research_project_backend.Controllers;
using research_project_backend.Data.Domains;
using research_project_backend.Data.Domains.Enums;
using research_project_backend.Models;
using research_project_backend.Services;
using research_project_backend.Services.Email;
using researchproject.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace research_project_backend.Test.Controllers
{
    [TestFixture]
    public class AuthenticationControllerTest
    {
        private AuthenticationController _authenticationController;
        private Mock<UserManager<User>> _usermanager;
        private Mock<RoleManager<Role>> _rolemanager;
        private Mock<IEmailSender> _emailSender;
        private Mock<IPasswordUrlCodeData> _passwordUrlCode;
        private Mock<IRegistrationLinkCodeData> _registrationUrlCode;
        private Mock<IPasswordHasher<User>> _passwordHasher;
        private Mock<ICompanyData> _companyData;
        private IOptions<TokenSettings> _tokenSettings;
        private User user;
        private User newUser;
        private Company company;
        private ValidatePasswordUrlModel validUrl;
        private string role;
        private RegistrationLinkCode registrationLinkCode;

        [SetUp]
        public void Init()
        {
            var store = new Mock<IUserStore<User>>();
            var roleStore = new Mock<IRoleStore<Role>>();
            _usermanager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            _rolemanager = new Mock<RoleManager<Role>>(roleStore.Object, null, null, null, null);
            _emailSender = new Mock<IEmailSender>();
            _passwordUrlCode = new Mock<IPasswordUrlCodeData>();
            _registrationUrlCode = new Mock<IRegistrationLinkCodeData>();
            _passwordHasher = new Mock<IPasswordHasher<User>>();
            _companyData = new Mock<ICompanyData>();

            TokenSettings settings = new TokenSettings 
            {
                Audience = "PXL Audience",
                Issuer = "Hogeschool PXL",
                ExpirationTimeInMinutes = 60,
                Key = "Supersecretkey!!!"
            };
            _tokenSettings = Options.Create<TokenSettings>(settings);
            

            _authenticationController = new AuthenticationController(_usermanager.Object, _rolemanager.Object, _passwordHasher.Object, _tokenSettings, _companyData.Object, _emailSender.Object, _passwordUrlCode.Object, _registrationUrlCode.Object);
            SetUpMocks();
            user = new User
            {
                Email = "test@test.com",
                UserName = "test@test.com",
                PasswordHash = "Test1234"
            };
            validUrl = new ValidatePasswordUrlModel
            {
                Code = "valid"
            };
        }

        [Test]
        public async Task NewPasswordReturnsOkWhenCorrectCredentialsAreGivenAndPasswordMeetsAllRequirments()
        {
            NewPasswordModel newPasswordModel = GetNewPasswordModel(user.Email, user.PasswordHash, "Test12345");
            
            var result = await _authenticationController.NewPassword(newPasswordModel);
            var okResult = result as OkResult;

            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task NewPasswordChangesPasswordWhenCorrectCredentialsAreGivenAndPasswordMeetsAllRequirments()
        {
            string newPassword = "Test12345";
            NewPasswordModel newPasswordModel = GetNewPasswordModel(user.Email, user.PasswordHash, newPassword);
            
            var result = await _authenticationController.NewPassword(newPasswordModel);
            Assert.That(user.PasswordHash, Is.EqualTo(newPassword));
        }

        [Test]
        public async Task NewPasswordReturnsUnauthorizedWhenOldPasswordIsWrong()
        {
            NewPasswordModel newPasswordModel = GetNewPasswordModel(user.Email, "wrongPassword", "doesntmatter");
            

            var result = await _authenticationController.NewPassword(newPasswordModel);

            var unauthorizedResult = result as UnauthorizedResult;
            Assert.IsNotNull(unauthorizedResult);
            Assert.That(unauthorizedResult.StatusCode, Is.EqualTo(401));
        }

        [Test]
        public async Task NewPasswordReturnsUnauthorizedWhenEmailIsNotFound()
        {
            NewPasswordModel newPasswordModel = GetNewPasswordModel("wrong@email.com", "doesntmatter", "doesntmatter");
            

            var result = await _authenticationController.NewPassword(newPasswordModel);

            var unauthorizedResult = result as UnauthorizedResult;
            Assert.IsNotNull(unauthorizedResult);
            Assert.That(unauthorizedResult.StatusCode, Is.EqualTo(401));
        }

        [Test]
        public async Task NewPasswordReturnsBadRequestWhenNewPasswordIsShorterThan8Characters()
        {
            NewPasswordModel newPasswordModel = GetNewPasswordModel(user.Email, user.PasswordHash, "1234567");
            

            var result = await _authenticationController.NewPassword(newPasswordModel);

            var badRequestResult = result as BadRequestResult;
            Assert.IsNotNull(badRequestResult);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
        }

        [Test]
        public async Task ForgotPasswordReturnsUnauthorizedWhenEmailIsNotFound()
        {
            ForgotPasswordModel forgotPasswordModel = GetForgotPasswordModel("nottest@test.com");
            var result = await _authenticationController.ForgotPassword(forgotPasswordModel);

            var unauthorizedResult = result as UnauthorizedResult;
            Assert.IsNotNull(unauthorizedResult);
            Assert.That(unauthorizedResult.StatusCode, Is.EqualTo(401));
        }

        [Test]
        public async Task ForgotPassWordReturnsOkWhenEverythingIsCorrect()
        {
            ForgotPasswordModel forgotPasswordModel = GetForgotPasswordModel(user.Email);
            var result = await _authenticationController.ForgotPassword(forgotPasswordModel);

            var okResult = result as OkResult;
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task ValidatePasswordUrlReturnsOkWhenValidUrl()
        {
            ValidatePasswordUrlModel validatePasswordUrlModel = new ValidatePasswordUrlModel
            {
                Code = validUrl.Code
            };
            var result = await _authenticationController.ValidatePasswordUrl(validatePasswordUrlModel);

            var okResult = result as OkResult;
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task ValidatePasswordUrlReturnsBadRequestWhenUrlIsNotValid()
        {
            ValidatePasswordUrlModel validatePasswordUrlModel = new ValidatePasswordUrlModel
            {
                Code = "notValid"
            };
            var result = await _authenticationController.ValidatePasswordUrl(validatePasswordUrlModel);

            var badRequestResult = result as BadRequestResult;
            Assert.IsNotNull(badRequestResult);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
        }

        [Test]
        public async Task ResetPasswordReturnsBadRequestIfResetUrlIsNotValid()
        {
            ResetPasswordModel resetPasswordModel = GetResetPasswordModel("notvalid", "doesntmatter");
            var result = await _authenticationController.ResetPassword(resetPasswordModel);

            var badRequestResult = result as BadRequestResult;
            Assert.IsNotNull(badRequestResult);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
        }

        [Test]
        public async Task ResetPasswordReturnsBadRequestIfNewPasswordIsTooShort()
        {
            ResetPasswordModel resetPasswordModel = GetResetPasswordModel(validUrl.Code, "1234567");
            var result = await _authenticationController.ResetPassword(resetPasswordModel);

            var badRequestResult = result as BadRequestResult;
            Assert.IsNotNull(badRequestResult);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
        }

        [Test]
        public async Task ResetPasswordReturnsOkWhenEverythingIsCorrect()
        {
            ResetPasswordModel resetPasswordModel = GetResetPasswordModel(validUrl.Code, "12345678");
            var result = await _authenticationController.ResetPassword(resetPasswordModel);

            var okResult = result as OkResult;
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task CreateTokenReturnsUnAuthorizedWhenEmailIsNotFound()
        {
            LoginModel loginModel = GetLoginModel("wrong@email.com", "doesntmatter");

            var result = await _authenticationController.CreateToken(loginModel);

            var unauthorizedResult = result as UnauthorizedResult;
            Assert.IsNotNull(unauthorizedResult);
            Assert.That(unauthorizedResult.StatusCode, Is.EqualTo(401));
        }

        [Test]
        public async Task CreateTokenReturnsUnAuthorizedWhenPasswordIsNotCorrect()
        {
            LoginModel loginModel = GetLoginModel(user.Email, "doesntmatter");

            var result = await _authenticationController.CreateToken(loginModel);

            var unauthorizedResult = result as UnauthorizedResult;
            Assert.IsNotNull(unauthorizedResult);
            Assert.That(unauthorizedResult.StatusCode, Is.EqualTo(401));
        }

        [Test]
        public async Task CreateTokenReturnsOkWhenEverythingIsCorrect()
        {
            LoginModel loginModel = GetLoginModel(user.Email, user.PasswordHash);
            this.role = Role.Teacher.ToString();
            var result = await _authenticationController.CreateToken(loginModel);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task CreateTokenReturnsModelWhenEverythingIsCorrect()
        {
            LoginModel loginModel = GetLoginModel(user.Email, user.PasswordHash);
            this.role = Role.Teacher.ToString();
            var result = await _authenticationController.CreateToken(loginModel);

            var okResult = result as OkObjectResult;
            var returnModel = okResult.Value as LoginReturnModel;

            Assert.IsNotNull(returnModel);
            Assert.IsNotNull(returnModel.Email);
            Assert.IsNotNull(returnModel.Token);
        }

        [Test]
        public async Task CreateTokenReturnModelHasSpecializationWhenRoleIsStudent()
        {
            LoginModel loginModel = GetLoginModel(user.Email, user.PasswordHash);
            this.role = Role.Student.ToString();
            this.user.Specialization = "ApplicationDevelopment";
            var result = await _authenticationController.CreateToken(loginModel);

            var okResult = result as OkObjectResult;
            var returnModel = okResult.Value as LoginReturnModel;

            Assert.IsNotNull(returnModel.Specialization);
        }

        [Test]
        public async Task CreateTokenReturnModelHasCompanyIdWhenUserHasACompanyId()
        {
            LoginModel loginModel = GetLoginModel(user.Email, user.PasswordHash);
            this.role = Role.Company.ToString();
            this.user.CompanyId = 1;
            var result = await _authenticationController.CreateToken(loginModel);

            var okResult = result as OkObjectResult;
            var returnModel = okResult.Value as LoginReturnModel;

            Assert.IsNotNull(returnModel.CompanyId);
        }

        [Test]
        public async Task RegisterReturnsOkWhenEverythingIsCorrect()
        {
            var model = GetRegisterModel("test@pxl.be", "test", "pxl", "Test1234");

            var result = await _authenticationController.Register(model) as OkResult;

            Assert.That(result.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task RegisterCreatesNewUserWhenEverythingIsCorrect()
        {
            string email = "test@pxl.be";
            string firstname = "test";
            string name = "pxl";
            string password = "Test1234";
            var model = GetRegisterModel(email, firstname, name, password);

            await _authenticationController.Register(model);

            Assert.That(newUser.Email , Is.EqualTo(email));
            Assert.That(newUser.FirstName , Is.EqualTo(firstname));
            Assert.That(newUser.Name , Is.EqualTo(name));
            Assert.That(newUser.PasswordHash , Is.EqualTo(password));
        }

        [Test]
        public async Task RegisterUserHasAddressWhenModelHasStudentNumber()
        {
            string studentNumber = "11703072";
            string specialization = "ApplicationDevelopment";
            string street = "straat";
            string postal = "2230";
            string town = "Hasselt";
            string number = "22";
            var model = GetRegisterModel("test@student.pxl.be", "test", "pxl", "Test1234", studentNumber, specialization, street, postal, town, number);

            await _authenticationController.Register(model);

            Assert.That(newUser.StudentNumber , Is.EqualTo(studentNumber));
            Assert.That(newUser.Specialization , Is.EqualTo(specialization));
            Assert.That(newUser.Address.Street , Is.EqualTo(street));
            Assert.That(newUser.Address.PostalNumber , Is.EqualTo(postal));
            Assert.That(newUser.Address.Township , Is.EqualTo(town));
            Assert.That(newUser.Address.Number , Is.EqualTo(number));
        }

        [Test]
        public async Task RegisterHasTeacherNumberWhenIncludedInModel()
        {
            string teacherNumber = "11703072";
            var model = GetRegisterModel("test@pxl.be", "test", "pxl", "Test1234", teacherNumber);

            await _authenticationController.Register(model);

            Assert.That(newUser.TeacherNumber, Is.EqualTo(teacherNumber));
        }

        [Test]
        [TestCase("student@student.pxl.be", "Student")]
        [TestCase("lector@pxl.be", "Teacher")]
        [TestCase("Marijke.Willems@pxl.be", "Internshipcoordinator")]
        [TestCase("bedrijf@bedrijf.be", "Company")]
        public async Task RegisterUserGetsAssignedCorrectRole(string email, string expected)
        {
            var model = GetRegisterModel(email, "test", "pxl", "Test1234");

            await _authenticationController.Register(model);

            Assert.That(this.role, Is.EqualTo(expected));
        }

        [Test]
        public async Task RegisterCompanyCreatesCompanyWithCorrectPropsWhenEverythingIsCorrect()
        {
            string email = "bedrijf@bedrijf.com";
            string password = "Test1234";
            string jobTitle = "dev";
            string phone = "+32412345678";
            string firstName = "bedrijf";
            string name = "leider";
            string street = "straat";
            string postal = "2230";
            string town = "Hasselt";
            string number = "22";
            int employees = 50;
            string companyName = "bedrijf";

            

            var model = GetRegisterCompanyModel(email, password, email, jobTitle, phone, firstName, name, email, jobTitle, phone, firstName, name, street, postal, town, number, employees, employees, companyName);
            await _authenticationController.RegisterCompany(model);

            Assert.That(company.AmountOfITPersonnel, Is.EqualTo(employees));
            Assert.That(company.AmountOfPersonnel, Is.EqualTo(employees));
            Assert.That(company.NameCompany, Is.EqualTo(companyName));
            Assert.That(company.ContactCompany.Email, Is.EqualTo(email));
            Assert.That(company.ContactCompany.JobTitle, Is.EqualTo(jobTitle));
            Assert.That(company.ContactCompany.LastName, Is.EqualTo(name));
            Assert.That(company.ContactCompany.Name, Is.EqualTo(firstName));
            Assert.That(company.ContactCompany.PhoneNumber, Is.EqualTo(phone));
            Assert.That(company.Promoter.Email, Is.EqualTo(email));
            Assert.That(company.Promoter.JobTitle, Is.EqualTo(jobTitle));
            Assert.That(company.Promoter.LastName, Is.EqualTo(name));
            Assert.That(company.Promoter.Name, Is.EqualTo(firstName));
            Assert.That(company.Promoter.PhoneNumber, Is.EqualTo(phone));
            Assert.That(company.AddressCompany.PostalNumber, Is.EqualTo(postal));
            Assert.That(company.AddressCompany.Street, Is.EqualTo(street));
            Assert.That(company.AddressCompany.Township, Is.EqualTo(town));
            Assert.That(company.AddressCompany.Number, Is.EqualTo(number));
        }

        [Test]
        public async Task RegisterCompanyReturnsOkWhenEverythingIsCorrect()
        {
            string email = "bedrijf@bedrijf.com";
            string password = "Test1234";
            string jobTitle = "dev";
            string phone = "+32412345678";
            string firstName = "bedrijf";
            string name = "leider";
            string street = "straat";
            string postal = "2230";
            string town = "Hasselt";
            string number = "22";
            int employees = 50;
            string companyName = "bedrijf";

            var model = GetRegisterCompanyModel(email, password, email, jobTitle, phone, firstName, name, email, jobTitle, phone, firstName, name, street, postal, town, number, employees, employees, companyName);

            var result = await _authenticationController.RegisterCompany(model) as OkResult;

            Assert.That(result.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task RegisterCompanyCreatesNewUserWithCompanyIdWhenEverythingIsCorrect()
        {
            string email = "bedrijf@bedrijf.com";
            string password = "Test1234";
            string jobTitle = "dev";
            string phone = "+32412345678";
            string firstName = "bedrijf";
            string name = "leider";
            string street = "straat";
            string postal = "2230";
            string town = "Hasselt";
            string number = "22";
            int employees = 50;
            string companyName = "bedrijf";

            var model = GetRegisterCompanyModel(email, password, email, jobTitle, phone, firstName, name, email, jobTitle, phone, firstName, name, street, postal, town, number, employees, employees, companyName);

            await _authenticationController.RegisterCompany(model);

            Assert.That(newUser.CompanyId, Is.Not.Null);
        }

        [Test]
        public async Task SendRegistrationLinkReturnsOkWhenEverythingIsCorrect()
        {
            var model = GetRegistrationLinkModel("doesntexist@blabla.be");

            var result = await _authenticationController.SendRegistrationLink(model) as OkResult;

            Assert.That(result.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task SendRegistrationLinkAddsCodeWhenEverythingIsCorrect()
        {
            var model = GetRegistrationLinkModel("doesntexist@blabla.be");

            await _authenticationController.SendRegistrationLink(model);

            Assert.That(registrationLinkCode, Is.Not.Null);
        }

        [Test]
        public async Task ValidateRegistrationUrlReturnsOkWhenUrlIsValid()
        {
            var model = GetRegistrationLinkModel("doesntexist@blabla.be");
            await _authenticationController.SendRegistrationLink(model);

            var validateModel = GetPasswordUrlModel(registrationLinkCode.Code);
            var result = await _authenticationController.ValidateRegistrationUrl(validateModel) as OkResult;

            Assert.That(result.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task ValidateRegistrationUrlReturnsBadRequestWhenUrlIsInValid()
        {
            var model = GetRegistrationLinkModel("doesntexist@blabla.be");
            await _authenticationController.SendRegistrationLink(model);

            var validateModel = GetPasswordUrlModel("invalidbecauselongerthan6chcracters");
            var result = await _authenticationController.ValidateRegistrationUrl(validateModel) as BadRequestResult;

            Assert.That(result.StatusCode, Is.EqualTo(400));
        }

        private async Task<User> GetUserByMail(string email)
        {
            if (email == user.Email)
                return user;

            return null;
        }

        private async Task<string> GenerateResetToken()
        {
            return "randomresettoken";
        }

        private async Task<IdentityResult> ResetPassword(User user, string newPassword)
        {
            if (newPassword.Length > 7)
            {
                user.PasswordHash = newPassword;
                return IdentityResult.Success;
            }
            return IdentityResult.Failed();
        }

        private PasswordVerificationResult CheckPassword(string password, string passwordGiven)
        {
            if (password == passwordGiven)
                return PasswordVerificationResult.Success;
            return PasswordVerificationResult.Failed;
        }

        private async Task<IdentityResult> CreateUser(User toCreate, string password)
        {
            newUser = toCreate;
            newUser.PasswordHash = password;
            return IdentityResult.Success;
        }

        private async Task<IList<string>> GetRoles(User roleUser)
        {
            IList<string> roles = new List<string>();

            roles.Add(this.role);

            return roles;
        }

        private async Task<IList<Claim>> GetClaimList()
        {
            IList<Claim> claims = new List<Claim>();
            return claims;
        }

        private async Task<bool> ReturnTrue()
        {
            return true;
        }

        private async Task<IdentityResult> AddRole(string rolename)
        {
            this.role = rolename;
            return IdentityResult.Success;
        }

        private int AddCompany(Company newCompany)
        {
            this.company = newCompany;
            this.company.CompanyId = 1;
            return this.company.CompanyId;
        }

        private void AddRegistrationCode(RegistrationLinkCode code)
        {
            registrationLinkCode = code;
        }

        private void SetUpMocks()
        {

            _usermanager.Setup(_usermanager => _usermanager.FindByEmailAsync(It.IsAny<string>()))
                .Returns<string>(email => GetUserByMail(email));

            _passwordHasher.Setup(_passwordHasher => _passwordHasher.VerifyHashedPassword(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns<User, string, string>((user, password, modelPassword) => CheckPassword(password, modelPassword));

            _usermanager.Setup(_usermanager => _usermanager.GeneratePasswordResetTokenAsync(It.IsAny<User>()))
                .Returns(GenerateResetToken);

            _usermanager.Setup(_usermanager => _usermanager.ResetPasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns<User, string, string>((user, token, password) => ResetPassword(user, password));
           
            _passwordUrlCode.Setup(_passwordUrlCode => _passwordUrlCode.ValidatePasswordUrlCode(It.IsAny<string>()))
                .Returns<string>(code => code == validUrl.Code);

            _passwordUrlCode.Setup(_passwordUrlCode => _passwordUrlCode.getPasswordUrlCodeUser(It.IsAny<string>()))
                .Returns(this.user);

            _passwordUrlCode.Setup(_passwordUrlCode => _passwordUrlCode.RemovePasswordUrlCode(It.IsAny<string>()))
                .Verifiable();

            _usermanager.Setup(_usermanager => _usermanager.GetRolesAsync(It.IsAny<User>()))
                .Returns<User>(u => GetRoles(u));

            _usermanager.Setup(_usermanager => _usermanager.GetClaimsAsync(It.IsAny<User>()))
                .Returns(GetClaimList);

            _usermanager.Setup(_usermanager => _usermanager.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .Returns<User, string>((createUser, password) => CreateUser(createUser, password));

            _rolemanager.Setup(_rolemanager => _rolemanager.RoleExistsAsync(It.IsAny<string>()))
                .Returns(ReturnTrue);

            _usermanager.Setup(_usermanager => _usermanager.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .Returns<User, string>((toCreate, userrole) => AddRole(userrole));

            _companyData.Setup(_companyData => _companyData.AddCompany(It.IsAny<Company>()))
                .Returns<Company>(newCompany => AddCompany(newCompany));

            _registrationUrlCode.Setup(_registrationUrlCode => _registrationUrlCode.AddRegistrationLinkCode(It.IsAny<RegistrationLinkCode>()))
                .Callback<RegistrationLinkCode>(code => AddRegistrationCode(code));

            _registrationUrlCode.Setup(_registrationUrlCode => _registrationUrlCode.ValidateRegistrationLinkCode(It.IsAny<string>()))
                .Returns<string>(code => code == registrationLinkCode.Code);

               
        }

        private NewPasswordModel GetNewPasswordModel(string email, string password, string newPassword)
        {
            return new NewPasswordModel
            {
                Email = email,
                Password = password,
                NewPassword = newPassword
            };
        }

        private ForgotPasswordModel GetForgotPasswordModel(string email)
        {
            return new ForgotPasswordModel
            {
                Email = email
            };
        }

        private ResetPasswordModel GetResetPasswordModel(string code, string newPassword)
        {
            return new ResetPasswordModel
            {
                Code = code,
                NewPassword = newPassword
            };
        }

        private LoginModel GetLoginModel(string email, string password)
        {
            return new LoginModel
            {
                Email = email,
                Password = password
            };
        }

        private RegisterModel GetRegisterModel(string email, string firstname, string name, string password)
        {
            return new RegisterModel
            {
                Email = email,
                FirstName = firstname,
                Name = name,
                Password = password
            };
        }

        private RegisterModel GetRegisterModel(string email, string firstname, string name, string password, string teacherNumber)
        {
            return new RegisterModel
            {
                Email = email,
                FirstName = firstname,
                Name = name,
                Password = password,
                TeacherNumber = teacherNumber
            };
        }

        private RegisterModel GetRegisterModel(string email, string firstname, string name, string password, string studentnumber, string specialization, string street, string postalnumber, string township, string number)
        {
            return new RegisterModel
            {
                Email = email,
                FirstName = firstname,
                Name = name,
                Password = password,
                StudentNumber = studentnumber,
                Specialization = specialization,
                Street = street,
                PostalNumber = postalnumber,
                Township = township,
                HouseNumber = number
            };
        }

        private RegisterCompanyModel GetRegisterCompanyModel(string loginEmail, string password, string emailContact, string jobTitleContact, string phoneContact, string firstnameContact, string nameContact, string emailPromoter, string jobTitlePromoter, string phonePromoter, string firstnamePromoter, string namePromoter, string street, string postal, string town, string number, int itEmployees, int employees, string companyName)
        {
            return new RegisterCompanyModel
            {
                LoginEmail = loginEmail,
                Password = password,
                EmailContactPerson = emailContact,
                JobTitleContactPerson = jobTitleContact,
                PhoneNumberContactPerson = phoneContact,
                FirstNameContactPerson = firstnameContact,
                LastNameContactPerson = nameContact,
                EmailPromoter = emailPromoter,
                JobTitlePromoter = jobTitlePromoter,
                PhoneNumberPromoter = phonePromoter,
                FirstNamePromoter = firstnamePromoter,
                LastNamePromoter = namePromoter,
                AmountOfItEmployees = itEmployees,
                AmountOfEmployees = employees,
                NameCompany = companyName,
                Number = number,
                PostalNumber = postal,
                Street = street,
                Township = town
            };
        }

        private RegistrationLinkModel GetRegistrationLinkModel(string email)
        {
            return new RegistrationLinkModel
            {
                Email = email
            };
        }

        private ValidatePasswordUrlModel GetPasswordUrlModel(string code)
        {
            return new ValidatePasswordUrlModel
            {
                Code = code
            };
        }
    }
}
