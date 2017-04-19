﻿using FluentValidation.TestHelper;
using Nop.Core.Domain.Common;
using Nop.Services.Directory;
using Nop.Web.Models.Common;
using Nop.Web.Validators.Common;
using NUnit.Framework;
using Rhino.Mocks;

namespace Nop.Web.MVC.Tests.Public.Validators.Common
{
    [TestFixture]
    public class AddressValidatorTests : BaseValidatorTests
    {
        private IStateProvinceService _stateProvinceService;
        private IDistrictService _districtService; //NOP 3.828

        [SetUp]
        public new void Setup()
        {
            _stateProvinceService = MockRepository.GenerateMock<IStateProvinceService>();
        }

        [Test]
        public void Should_have_error_when_email_is_null_or_empty()
        {
            var validator = new AddressValidator(_localizationService, _stateProvinceService,
                new AddressSettings(),
                _districtService);

            var model = new AddressModel();
            model.Email = null;
            validator.ShouldHaveValidationErrorFor(x => x.Email, model);
            model.Email = "";
            validator.ShouldHaveValidationErrorFor(x => x.Email, model);
        }
        [Test]
        public void Should_have_error_when_email_is_wrong_format()
        {
            var validator = new AddressValidator(_localizationService, _stateProvinceService,
                new AddressSettings(),
                _districtService);

            var model = new AddressModel();
            model.Email = "adminexample.com";
            validator.ShouldHaveValidationErrorFor(x => x.Email, model);
        }
        [Test]
        public void Should_not_have_error_when_email_is_correct_format()
        {
            var validator = new AddressValidator(_localizationService, _stateProvinceService,
                new AddressSettings(),
                _districtService);

            var model = new AddressModel();
            model.Email = "admin@example.com";
            validator.ShouldNotHaveValidationErrorFor(x => x.Email, model);
        }

        [Test]
        public void Should_have_error_when_firstName_is_null_or_empty()
        {
            var validator = new AddressValidator(_localizationService, _stateProvinceService,
                new AddressSettings(),
                _districtService);

            var model = new AddressModel();
            model.FirstName = null;
            validator.ShouldHaveValidationErrorFor(x => x.FirstName, model);
            model.FirstName = "";
            validator.ShouldHaveValidationErrorFor(x => x.FirstName, model);
        }
        [Test]
        public void Should_not_have_error_when_firstName_is_specified()
        {
            var validator = new AddressValidator(_localizationService, _stateProvinceService,
                new AddressSettings(),
                _districtService);

            var model = new AddressModel();
            model.FirstName = "John";
            validator.ShouldNotHaveValidationErrorFor(x => x.FirstName, model);
        }

        [Test]
        public void Should_have_error_when_lastName_is_null_or_empty()
        {
            var validator = new AddressValidator(_localizationService, _stateProvinceService,
                new AddressSettings(),
                _districtService);

            var model = new AddressModel();
            model.LastName = null;
            validator.ShouldHaveValidationErrorFor(x => x.LastName, model);
            model.LastName = "";
            validator.ShouldHaveValidationErrorFor(x => x.LastName, model);
        }
        [Test]
        public void Should_not_have_error_when_lastName_is_specified()
        {
            var validator = new AddressValidator(_localizationService, _stateProvinceService,
                new AddressSettings(),
                _districtService);

            var model = new AddressModel();
            model.LastName = "Smith";
            validator.ShouldNotHaveValidationErrorFor(x => x.LastName, model);
        }

        [Test]
        public void Should_have_error_when_company_is_null_or_empty_based_on_required_setting()
        {
            var model = new AddressModel();

            //required
            var validator = new AddressValidator(_localizationService, _stateProvinceService,
                new AddressSettings
                {
                    CompanyEnabled = true,
                    CompanyRequired = true
                }, _districtService);
            model.Company = null;
            validator.ShouldHaveValidationErrorFor(x => x.Company, model);
            model.Company = "";
            validator.ShouldHaveValidationErrorFor(x => x.Company, model);


            //not required
            validator = new AddressValidator(_localizationService, _stateProvinceService,
                new AddressSettings
                {
                    CompanyEnabled = true,
                    CompanyRequired = false
                }, _districtService);
            model.Company = null;
            validator.ShouldNotHaveValidationErrorFor(x => x.Company, model);
            model.Company = "";
            validator.ShouldNotHaveValidationErrorFor(x => x.Company, model);
        }
        [Test]
        public void Should_not_have_error_when_company_is_specified()
        {
            var validator = new AddressValidator(_localizationService, _stateProvinceService,
                new AddressSettings
                {
                    CompanyEnabled = true
                }, _districtService);

            var model = new AddressModel();
            model.Company = "Company";
            validator.ShouldNotHaveValidationErrorFor(x => x.Company, model);
        }

        [Test]
        public void Should_have_error_when_streetaddress_is_null_or_empty_based_on_required_setting()
        {
            var model = new AddressModel();

            //required
            var validator = new AddressValidator(_localizationService, _stateProvinceService,
                new AddressSettings
                {
                    StreetAddressEnabled = true,
                    StreetAddressRequired = true
                }, _districtService);
            model.Address1 = null;
            validator.ShouldHaveValidationErrorFor(x => x.Address1, model);
            model.Address1 = "";
            validator.ShouldHaveValidationErrorFor(x => x.Address1, model);

            //not required
            validator = new AddressValidator(_localizationService, _stateProvinceService,
                new AddressSettings
                {
                    StreetAddressEnabled = true,
                    StreetAddressRequired = false
                }, _districtService);
            model.Address1 = null;
            validator.ShouldNotHaveValidationErrorFor(x => x.Address1, model);
            model.Address1 = "";
            validator.ShouldNotHaveValidationErrorFor(x => x.Address1, model);
        }
        [Test]
        public void Should_not_have_error_when_streetaddress_is_specified()
        {
            var validator = new AddressValidator(_localizationService, _stateProvinceService,
                new AddressSettings
                {
                    StreetAddressEnabled = true
                },_districtService);

            var model = new AddressModel();
            model.Address1 = "Street address";
            validator.ShouldNotHaveValidationErrorFor(x => x.Address1, model);
        }

        [Test]
        public void Should_have_error_when_streetaddress2_is_null_or_empty_based_on_required_setting()
        {
            var model = new AddressModel();

            //required
            var validator = new AddressValidator(_localizationService, _stateProvinceService,
                new AddressSettings
                {
                    StreetAddress2Enabled = true,
                    StreetAddress2Required = true
                },_districtService);
            model.Address2 = null;
            validator.ShouldHaveValidationErrorFor(x => x.Address2, model);
            model.Address2 = "";
            validator.ShouldHaveValidationErrorFor(x => x.Address2, model);

            //not required
            validator = new AddressValidator(_localizationService, _stateProvinceService,
                new AddressSettings
                {
                    StreetAddress2Enabled = true,
                    StreetAddress2Required = false
                }, _districtService);
            model.Address2 = null;
            validator.ShouldNotHaveValidationErrorFor(x => x.Address2, model);
            model.Address2 = "";
            validator.ShouldNotHaveValidationErrorFor(x => x.Address2, model);
        }
        [Test]
        public void Should_not_have_error_when_streetaddress2_is_specified()
        {
            var validator = new AddressValidator(_localizationService, _stateProvinceService,
                new AddressSettings
                {
                    StreetAddress2Enabled = true
                },_districtService);

            var model = new AddressModel();
            model.Address2 = "Street address 2";
            validator.ShouldNotHaveValidationErrorFor(x => x.Address2, model);
        }

        [Test]
        public void Should_have_error_when_zippostalcode_is_null_or_empty_based_on_required_setting()
        {
            var model = new AddressModel();

            //required
            var validator = new AddressValidator(_localizationService, _stateProvinceService,
                new AddressSettings
                {
                    ZipPostalCodeEnabled = true,
                    ZipPostalCodeRequired = true
                }, _districtService);
            model.ZipPostalCode = null;
            validator.ShouldHaveValidationErrorFor(x => x.ZipPostalCode, model);
            model.ZipPostalCode = "";
            validator.ShouldHaveValidationErrorFor(x => x.ZipPostalCode, model);


            //not required
            validator = new AddressValidator(_localizationService, _stateProvinceService,
                new AddressSettings
                {
                    ZipPostalCodeEnabled = true,
                    ZipPostalCodeRequired = false
                }, _districtService);
            model.ZipPostalCode = null;
            validator.ShouldNotHaveValidationErrorFor(x => x.ZipPostalCode, model);
            model.ZipPostalCode = "";
            validator.ShouldNotHaveValidationErrorFor(x => x.ZipPostalCode, model);
        }
        [Test]
        public void Should_not_have_error_when_zippostalcode_is_specified()
        {
            var validator = new AddressValidator(_localizationService, _stateProvinceService,
                new AddressSettings
                {
                    StreetAddress2Enabled = true
                }, _districtService);

            var model = new AddressModel();
            model.ZipPostalCode = "zip";
            validator.ShouldNotHaveValidationErrorFor(x => x.ZipPostalCode, model);
        }

        [Test]
        public void Should_have_error_when_city_is_null_or_empty_based_on_required_setting()
        {
            var model = new AddressModel();

            //required
            var validator = new AddressValidator(_localizationService, _stateProvinceService,
                new AddressSettings
                {
                    CityEnabled = true,
                    CityRequired = true
                }, _districtService);
            model.City = null;
            validator.ShouldHaveValidationErrorFor(x => x.City, model);
            model.City = "";
            validator.ShouldHaveValidationErrorFor(x => x.City, model);


            //not required
            validator = new AddressValidator(_localizationService, _stateProvinceService,
                new AddressSettings
                {
                    CityEnabled = true,
                    CityRequired = false
                }, _districtService);
            model.City = null;
            validator.ShouldNotHaveValidationErrorFor(x => x.City, model);
            model.City = "";
            validator.ShouldNotHaveValidationErrorFor(x => x.City, model);
        }
        [Test]
        public void Should_not_have_error_when_city_is_specified()
        {
            var validator = new AddressValidator(_localizationService, _stateProvinceService,
                new AddressSettings
                {
                    CityEnabled = true
                }, _districtService);

            var model = new AddressModel();
            model.City = "City";
            validator.ShouldNotHaveValidationErrorFor(x => x.City, model);
        }

        [Test]
        public void Should_have_error_when_phone_is_null_or_empty_based_on_required_setting()
        {
            var model = new AddressModel();

            //required
            var validator = new AddressValidator(_localizationService, _stateProvinceService,
                new AddressSettings
                {
                    PhoneEnabled = true,
                    PhoneRequired = true
                }, _districtService);
            model.PhoneNumber = null;
            validator.ShouldHaveValidationErrorFor(x => x.PhoneNumber, model);
            model.PhoneNumber = "";
            validator.ShouldHaveValidationErrorFor(x => x.PhoneNumber, model);

            //not required
            validator = new AddressValidator(_localizationService, _stateProvinceService,
                new AddressSettings
                {
                    PhoneEnabled = true,
                    PhoneRequired = false
                }, _districtService);
            model.PhoneNumber = null;
            validator.ShouldNotHaveValidationErrorFor(x => x.PhoneNumber, model);
            model.PhoneNumber = "";
            validator.ShouldNotHaveValidationErrorFor(x => x.PhoneNumber, model);
        }
        [Test]
        public void Should_not_have_error_when_phone_is_specified()
        {
            var validator = new AddressValidator(_localizationService, _stateProvinceService,
                new AddressSettings
                {
                    PhoneEnabled = true
                }, _districtService);

            var model = new AddressModel();
            model.PhoneNumber = "Phone";
            validator.ShouldNotHaveValidationErrorFor(x => x.PhoneNumber, model);
        }

        [Test]
        public void Should_have_error_when_fax_is_null_or_empty_based_on_required_setting()
        {
            var model = new AddressModel();

            //required
            var validator = new AddressValidator(_localizationService, _stateProvinceService,
                new AddressSettings
                {
                    FaxEnabled = true,
                    FaxRequired = true
                }, _districtService);
            model.FaxNumber = null;
            validator.ShouldHaveValidationErrorFor(x => x.FaxNumber, model);
            model.FaxNumber = "";
            validator.ShouldHaveValidationErrorFor(x => x.FaxNumber, model);


            //not required
            validator = new AddressValidator(_localizationService, _stateProvinceService,
                new AddressSettings
                {
                    FaxEnabled = true,
                    FaxRequired = false
                }, _districtService);
            model.FaxNumber = null;
            validator.ShouldNotHaveValidationErrorFor(x => x.FaxNumber, model);
            model.FaxNumber = "";
            validator.ShouldNotHaveValidationErrorFor(x => x.FaxNumber, model);
        }
        [Test]
        public void Should_not_have_error_when_fax_is_specified()
        {
            var validator = new AddressValidator(_localizationService, _stateProvinceService,
                new AddressSettings
                {
                    FaxEnabled = true
                }, _districtService);

            var model = new AddressModel();
            model.FaxNumber = "Fax";
            validator.ShouldNotHaveValidationErrorFor(x => x.FaxNumber, model);
        }
    }
}
