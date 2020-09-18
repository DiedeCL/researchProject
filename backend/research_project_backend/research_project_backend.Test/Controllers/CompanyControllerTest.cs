using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using research_project_backend.Controllers;
using research_project_backend.Data.Domains;
using research_project_backend.Models;
using research_project_backend.Services;
using System.Collections.Generic;

namespace research_project_backend.Test.Controllers
{
    [TestFixture]
    public class CompanyControllerTest
    {
        private readonly Mock<ICompanyData> _companyDataMock;
        private readonly Mock<ICompanyPromotorData> _companyPromoterDataMock;
        private readonly Mock<IContactCompanyData> _contactCompanyDataMock;
        private readonly Mock<IAddressData> _addressDataMock;
        private readonly CompanyController _companyController;

        public CompanyControllerTest()
        {
            _companyDataMock = new Mock<ICompanyData>();
            _companyPromoterDataMock = new Mock<ICompanyPromotorData>();
            _contactCompanyDataMock = new Mock<IContactCompanyData>();
            _addressDataMock = new Mock<IAddressData>();
            _companyController = new CompanyController(_companyDataMock.Object, _companyPromoterDataMock.Object, _contactCompanyDataMock.Object, _addressDataMock.Object);
        }

        [Test]
        public void GetAllCompanies_ShouldReturnAOkObjectResult()
        {
            var result = _companyController.GetAllCompanies();

            Assert.AreEqual(result.GetType(), typeof(OkObjectResult));
        }

        [Test]
        public void GetAllCompanies_ShouldReturnAllRegisteredCompanies()
        {
            _companyDataMock.Setup(x => x.GetAllCompanies())
                .Returns(GetTestCompanies());

            var result = _companyController.GetAllCompanies() as OkObjectResult;

            Assert.IsInstanceOf<IEnumerable<Company>>(result.Value);
        }

        [Test]
        public void GetAllCompanies_ShouldReturnBadRequest_WhenCompanyListIsNull()
        {
            _companyDataMock.Setup(x => x.GetAllCompanies())
                .Returns((IEnumerable<Company>)null);

            var result = _companyController.GetAllCompanies();

            Assert.AreEqual(result.GetType(), typeof(BadRequestResult));
        }

        [Test]
        public void GetCompanyById_ShouldReturnAOkObjectResult_WhenCompanyExists()
        {
            const int companyId = 1;

            var result = GetTestCompanyById(companyId);

            Assert.AreEqual(result.GetType(), typeof(OkObjectResult));
        }

        [Test]
        public void GetCompanyById_ShouldReturnACompany_WhenCompanyExists()
        {
            const int companyId = 1;

            var company = GetTestCompanyById(companyId);
            var result = company as OkObjectResult;

            Assert.IsInstanceOf<Company>(result.Value);
        }

        [Test]
        public void GetCompanyById_ShouldReturnABadRequestResult_WhenNoCompanyIdIsGiven()
        {
            const int companyId = 1;

            var companyInfoModel = SetupCompanyInfoModel(companyId);

            _companyDataMock.Setup(x => x.GetCompanyById(It.IsAny<int>()))
                .Returns((Company)null);

            var result = _companyController.GetCompanyById(companyInfoModel);

            Assert.AreEqual(result.GetType(), typeof(BadRequestResult));
        }

        private static IEnumerable<Company> GetTestCompanies()
        {
            return new List<Company>()
            {
                new Company()
                {
                    AddressCompany = new Address
                    {
                        AssignmentId = 0,
                        AddressId = 1,
                        CompanyId = 1,
                        Number = "15",
                        PostalNumber = "3500",
                        Street = "Elfde-LinieStraat",
                        Township = "Hasselt"
                    },
                    AmountOfITPersonnel = 15,
                    AmountOfPersonnel = 15,
                    CompanyId = 1,
                    ContactCompany = new ContactCompany
                    {
                        Company = null,
                        CompanyId = 1,
                        Email = "email@email.com",
                        Id = 1,
                        JobTitle = "test",
                        LastName = "jimbo",
                        Name = "jimmy",
                        PhoneNumber = "018646137"
                    },
                    InternshipAssignments = null,
                    NameCompany = "PXL",
                    Promoter = new CompanyPromoter
                    {
                        Company = null,
                        CompanyId = 1,
                        Email = "email@email.com",
                        Id = 1,
                        JobTitle = "test",
                        LastName = "jimbo",
                        Name = "jimmy",
                        PhoneNumber = "018646137"
                    },
                    User = null
                },
                new Company()
                {
                    AddressCompany = new Address
                    {
                        AssignmentId = 0,
                        AddressId = 2,
                        CompanyId = 2,
                        Number = "15",
                        PostalNumber = "3500",
                        Street = "Elfde-LinieStraat",
                        Township = "Hasselt"
                    },
                    AmountOfITPersonnel = 15,
                    AmountOfPersonnel = 15,
                    CompanyId = 2,
                    ContactCompany = new ContactCompany
                    {
                        Company = null,
                        CompanyId = 2,
                        Email = "email@email.com",
                        Id = 2,
                        JobTitle = "test",
                        LastName = "jimbo",
                        Name = "jimmy",
                        PhoneNumber = "018646137"
                    },
                    InternshipAssignments = null,
                    NameCompany = "UCLL",
                    Promoter = new CompanyPromoter
                    {
                        Company = null,
                        CompanyId = 2,
                        Email = "email@email.com",
                        Id = 2,
                        JobTitle = "test",
                        LastName = "jimbo",
                        Name = "jimmy",
                        PhoneNumber = "018646137"
                    },
                    User = null
                },
                new Company()
                {
                    AddressCompany = new Address
                    {
                        AssignmentId = 0,
                        AddressId = 3,
                        CompanyId = 3,
                        Number = "15",
                        PostalNumber = "3500",
                        Street = "Elfde-LinieStraat",
                        Township = "Hasselt"
                    },
                AmountOfITPersonnel = 15,
                AmountOfPersonnel = 15,
                CompanyId = 3,
                ContactCompany = new ContactCompany
                {
                    Company = null,
                    CompanyId = 3,
                    Email = "email@email.com",
                    Id = 3,
                    JobTitle = "test",
                    LastName = "jimbo",
                    Name = "jimmy",
                    PhoneNumber = "018646137"
                },
                InternshipAssignments = null,
                NameCompany = "UHasselt",
                Promoter = new CompanyPromoter
                {
                    Company = null,
                    CompanyId = 3,
                    Email = "email@email.com",
                    Id = 3,
                    JobTitle = "test",
                    LastName = "jimbo",
                    Name = "jimmy",
                    PhoneNumber = "018646137"
                },
                User = null
            }
            };
        }

        private IActionResult GetTestCompanyById(int companyId)
        {
            var companyInfoModel = SetupCompanyInfoModel(companyId);
            var address = SetupAddressMock(companyId);
            var contactCompany = SetupContactCompanyMock(companyId);
            var companyPromoter = SetupCompanyPromoterMock(companyId);

            _companyDataMock.Setup(x => x.GetCompanyById(It.IsAny<int>()))
                .Returns(new Company()
                {
                    AddressCompany = address,
                    AmountOfITPersonnel = 15,
                    AmountOfPersonnel = 15,
                    CompanyId = 1,
                    ContactCompany = contactCompany,
                    InternshipAssignments = null,
                    NameCompany = "PXL",
                    Promoter = companyPromoter,
                    User = null
                });

            return _companyController.GetCompanyById(companyInfoModel);
        }

        private static CompanyInfoModel SetupCompanyInfoModel(int companyId)
        {
            return new CompanyInfoModel
            {
                CompanyId = companyId
            };
        }

        private Address SetupAddressMock(int companyId)
        {
            _addressDataMock.Setup(x => x.GetAddressByCompanyId(It.IsAny<int>()))
                .Returns(new Address
                {
                    AssignmentId = 0,
                    AddressId = 1,
                    CompanyId = 1,
                    Number = "15",
                    PostalNumber = "3500",
                    Street = "Elfde-LinieStraat",
                    Township = "Hasselt"
                });

            return _addressDataMock.Object.GetAddressByCompanyId(companyId);
        }

        private CompanyPromoter SetupCompanyPromoterMock(int companyId)
        {
            _companyPromoterDataMock.Setup(x => x.GetCompanyPromoterByCompanyId(It.IsAny<int>()))
                .Returns(new CompanyPromoter
                {
                    Company = null,
                    CompanyId = 1,
                    Email = "email@email.com",
                    Id = 1,
                    JobTitle = "test",
                    LastName = "jimbo",
                    Name = "jimmy",
                    PhoneNumber = "018646137"
                });

            return _companyPromoterDataMock.Object.GetCompanyPromoterByCompanyId(companyId);
        }

        private ContactCompany SetupContactCompanyMock(int companyId)
        {
            _contactCompanyDataMock.Setup(x => x.GetContactCompanyByCompanyId(It.IsAny<int>()))
                .Returns(new ContactCompany
                {
                    Company = null,
                    CompanyId = 1,
                    Email = "email@email.com",
                    Id = 1,
                    JobTitle = "test",
                    LastName = "jimbo",
                    Name = "jimmy",
                    PhoneNumber = "018646137"
                });

            return _contactCompanyDataMock.Object.GetContactCompanyByCompanyId(companyId);
        }
    }
}
