using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Domain;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Forums;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Messages;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Tax;
using Nop.Core.Domain.Vendors;
using Nop.Core.Infrastructure;
using Nop.Services.Authentication;
using Nop.Services.Authentication.External;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Events;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Seo;
using Nop.Services.Stores;
using Nop.Services.Tax;
using Nop.Web.Extensions;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Security;
using Nop.Web.Framework.Security.Captcha;
using Nop.Web.Framework.Security.Honeypot;
using Nop.Web.Models.Common;
using Nop.Web.Models.Customer;
using Nop.Services.Configuration;
using Nop.Plugin.Misc.SMS.Models;
using Nop.Plugin.Misc.SMS.Services;
using WebGrease.Css.Extensions;

namespace Nop.Plugin.Misc.SMS.Controllers
{
    public class SMSController : BasePluginController
    {
        #region Fields
        private readonly ISettingService _settingService;
        private readonly IStoreService _storeService;
        private readonly ISMSService _smsService;
        private readonly SMSSettings _smsSettings;
        private readonly IAuthenticationService _authenticationService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly DateTimeSettings _dateTimeSettings;
        private readonly TaxSettings _taxSettings;
        private readonly ILocalizationService _localizationService;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly IStoreMappingService _storeMappingService;
        private readonly ICustomerService _customerService;
        private readonly ICustomerAttributeParser _customerAttributeParser;
        private readonly ICustomerAttributeService _customerAttributeService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        private readonly ITaxService _taxService;
        private readonly RewardPointsSettings _rewardPointsSettings;
        private readonly CustomerSettings _customerSettings;
        private readonly AddressSettings _addressSettings;
        private readonly ForumSettings _forumSettings;
        private readonly OrderSettings _orderSettings;
        private readonly IAddressService _addressService;
        private readonly ICountryService _countryService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly IOrderService _orderService;
        private readonly IPictureService _pictureService;
        private readonly INewsLetterSubscriptionService _newsLetterSubscriptionService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IOpenAuthenticationService _openAuthenticationService;
        private readonly IDownloadService _downloadService;
        private readonly IWebHelper _webHelper;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IAddressAttributeParser _addressAttributeParser;
        private readonly IAddressAttributeService _addressAttributeService;
        private readonly IAddressAttributeFormatter _addressAttributeFormatter;
        private readonly IReturnRequestService _returnRequestService;
        private readonly IEventPublisher _eventPublisher;
        private readonly MediaSettings _mediaSettings;
        private readonly IWorkflowMessageService _workflowMessageService;
        private readonly LocalizationSettings _localizationSettings;
        private readonly CaptchaSettings _captchaSettings;
        private readonly SecuritySettings _securitySettings;
        private readonly ExternalAuthenticationSettings _externalAuthenticationSettings;
        private readonly StoreInformationSettings _storeInformationSettings;
        private readonly CatalogSettings _catalogSettings;
        private readonly VendorSettings _vendorSettings;
        #endregion

        #region Ctor
        public SMSController(IAuthenticationService authenticationService,
            IDateTimeHelper dateTimeHelper,
            DateTimeSettings dateTimeSettings,
            TaxSettings taxSettings,
            ILocalizationService localizationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStoreMappingService storeMappingService,
            ICustomerService customerService,
            ICustomerAttributeParser customerAttributeParser,
            ICustomerAttributeService customerAttributeService,
            IGenericAttributeService genericAttributeService,
            ICustomerRegistrationService customerRegistrationService,
            ITaxService taxService,
            RewardPointsSettings rewardPointsSettings,
            CustomerSettings customerSettings,
            AddressSettings addressSettings,
            ForumSettings forumSettings,
            OrderSettings orderSettings,
            IAddressService addressService,
            ICountryService countryService,
            IStateProvinceService stateProvinceService,
            IOrderService orderService,
            IPictureService pictureService,
            INewsLetterSubscriptionService newsLetterSubscriptionService,
            IShoppingCartService shoppingCartService,
            IOpenAuthenticationService openAuthenticationService,
            IDownloadService downloadService,
            IWebHelper webHelper,
            ICustomerActivityService customerActivityService,
            IAddressAttributeParser addressAttributeParser,
            IAddressAttributeService addressAttributeService,
            IAddressAttributeFormatter addressAttributeFormatter,
            IReturnRequestService returnRequestService,
            IEventPublisher eventPublisher,
            MediaSettings mediaSettings,
            IWorkflowMessageService workflowMessageService,
            LocalizationSettings localizationSettings,
            CaptchaSettings captchaSettings,
            SecuritySettings securitySettings,
            ExternalAuthenticationSettings externalAuthenticationSettings,
            StoreInformationSettings storeInformationSettings,
            CatalogSettings catalogSettings,
            VendorSettings vendorSettings,
            IStoreService storeService,
            ISettingService settingService,
            ISMSService smsService,
            SMSSettings smsSettings
            )
        {
            this._storeService = storeService;
            this._settingService = settingService;
            this._smsService = smsService;
            this._smsSettings = smsSettings; ;
            this._authenticationService = authenticationService;
            this._dateTimeHelper = dateTimeHelper;
            this._dateTimeSettings = dateTimeSettings;
            this._taxSettings = taxSettings;
            this._localizationService = localizationService;
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._storeMappingService = storeMappingService;
            this._customerService = customerService;
            this._customerAttributeParser = customerAttributeParser;
            this._customerAttributeService = customerAttributeService;
            this._genericAttributeService = genericAttributeService;
            this._customerRegistrationService = customerRegistrationService;
            this._taxService = taxService;
            this._rewardPointsSettings = rewardPointsSettings;
            this._customerSettings = customerSettings;
            this._addressSettings = addressSettings;
            this._forumSettings = forumSettings;
            this._orderSettings = orderSettings;
            this._addressService = addressService;
            this._countryService = countryService;
            this._stateProvinceService = stateProvinceService;
            this._orderService = orderService;
            this._pictureService = pictureService;
            this._newsLetterSubscriptionService = newsLetterSubscriptionService;
            this._shoppingCartService = shoppingCartService;
            this._openAuthenticationService = openAuthenticationService;
            this._downloadService = downloadService;
            this._webHelper = webHelper;
            this._customerActivityService = customerActivityService;
            this._addressAttributeParser = addressAttributeParser;
            this._addressAttributeService = addressAttributeService;
            this._addressAttributeFormatter = addressAttributeFormatter;
            this._returnRequestService = returnRequestService;
            this._eventPublisher = eventPublisher;
            this._mediaSettings = mediaSettings;
            this._workflowMessageService = workflowMessageService;
            this._localizationSettings = localizationSettings;
            this._captchaSettings = captchaSettings;
            this._securitySettings = securitySettings;
            this._externalAuthenticationSettings = externalAuthenticationSettings;
            this._storeInformationSettings = storeInformationSettings;
            this._catalogSettings = catalogSettings;
            this._vendorSettings = vendorSettings;
        }
        #endregion

        #region Methods
        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure()
        {

            var model = new SMSModel();
           

            //BUGFIX 3.812
            if (_workContext.WorkingLanguage.Name == "English") //English
            {
                model.ProviderName = _smsSettings.ProviderName;
                model.Active = _smsSettings.Active;
                model.BaseURL = _smsSettings.BaseURL;//NOP 3.821
                model.Resource = _smsSettings.Resource;//NOP 3.821
                model.CountryCode = _smsSettings.CountryCode;//NOP 3.821
                model.PhoneNumber = _smsSettings.PhoneNumber;
                model.UserName = _smsSettings.UserName;
                model.Password = _smsSettings.Password;
                model.AccountSid = _smsSettings.AccountSid;
                model.AuthToken = _smsSettings.AuthToken;
                model.MessageTemplate = _smsSettings.MessageTemplate;
                
            }
            else
            {
                model.ProviderName = _smsSettings.ProviderNameTr;
                model.Active = _smsSettings.ActiveTr;
                model.BaseURL = _smsSettings.BaseURLTr;
                model.Resource = _smsSettings.ResourceTr;
                model.CountryCode = _smsSettings.CountryCodeTr;
                model.PhoneNumber = _smsSettings.PhoneNumberTr;
                model.UserName = _smsSettings.UserNameTr;
                model.Password = _smsSettings.PasswordTr;
                model.AccountSid = _smsSettings.AccountSidTr;
                model.AuthToken = _smsSettings.AuthTokenTr;
                model.MessageTemplate = _smsSettings.MessageTemplateTr;

            }


            return View("~/Plugins/Misc.SMS/Views/Configure.cshtml", model);


        }

        [AdminAuthorize]
        [ChildActionOnly]
        [HttpPost]
        public ActionResult Configure(SMSModel model)
        {
            if (!ModelState.IsValid)
            {
                return Configure();
            }

            //save settings
            //BUGFIX 3.812
            if (_workContext.WorkingLanguage.Name == "English") //English
            {
                _smsSettings.Active = model.Active;
                _smsSettings.BaseURL = model.BaseURL;//NOP 3.821
                _smsSettings.Resource = model.Resource;//NOP 3.821
                _smsSettings.CountryCode = model.CountryCode;//NOP 3.821
                _smsSettings.PhoneNumber = model.PhoneNumber;
                _smsSettings.UserName = model.UserName;
                _smsSettings.Password = model.Password;
                _smsSettings.AccountSid = _smsSettings.AccountSid;
                _smsSettings.AuthToken = _smsSettings.AuthToken;
                _smsSettings.MessageTemplate = _smsSettings.MessageTemplate;

            }
            else
            {
                _smsSettings.ActiveTr = model.Active;
                _smsSettings.BaseURLTr = model.BaseURL;
                _smsSettings.ResourceTr = model.Resource;
                _smsSettings.CountryCodeTr = model.CountryCode;
                _smsSettings.PhoneNumberTr = model.PhoneNumber;
                _smsSettings.UserNameTr = model.UserName;
                _smsSettings.PasswordTr = model.Password;
                _smsSettings.AccountSidTr = _smsSettings.AccountSid;
                _smsSettings.AuthTokenTr = _smsSettings.AuthToken;
                _smsSettings.MessageTemplateTr = _smsSettings.MessageTemplate;
            }
            _settingService.SaveSetting(_smsSettings);

            SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }

        public ActionResult Register()
        {
            //check whether registration is allowed
            if (_customerSettings.UserRegistrationType == UserRegistrationType.Disabled)
                return RedirectToRoute("RegisterResult", new { resultId = (int)UserRegistrationType.Disabled });

            var model = new RegisterModel();
            PrepareCustomerRegisterModel(model, false);
            //enable newsletter by default
            model.Newsletter = _customerSettings.NewsletterTickedByDefault;

            var smsModel = new SMSModel();
            smsModel.RegisterModel = model;
            smsModel.Active = _workContext.WorkingLanguage.Name == "English" ? _smsSettings.Active : _smsSettings.ActiveTr; //BUGFIX 3.812
            var smsRecordModel = new SMSRecord();
            smsModel.SmsRecordModel = smsRecordModel;

            return View("~/Plugins/Misc.SMS/Views/Register.cshtml", smsModel);
        }

        [HttpPost]
        [CaptchaValidator]
        [HoneypotValidator]
        [PublicAntiForgery]
        [ValidateInput(false)]
        //available even when navigation is not allowed
        [PublicStoreAllowNavigation(true)]
        public ActionResult RegisterPOST(SMSModel model, string returnUrl, bool captchaValid, FormCollection form)
        {

            //check whether registration is allowed
            if (_customerSettings.UserRegistrationType == UserRegistrationType.Disabled)
                return RedirectToRoute("RegisterResult", new { resultId = (int)UserRegistrationType.Disabled });

            if (_workContext.CurrentCustomer.IsRegistered())
            {
                //Already registered customer. 
                _authenticationService.SignOut();

                //Save a new record
                _workContext.CurrentCustomer = _customerService.InsertGuestCustomer();
            }
            var customer = _workContext.CurrentCustomer;

            //custom customer attributes
            var customerAttributesXml = ParseCustomCustomerAttributes(form);
            var customerAttributeWarnings = _customerAttributeParser.GetAttributeWarnings(customerAttributesXml);
            foreach (var error in customerAttributeWarnings)
            {
                ModelState.AddModelError("", error);
            }

            //validate CAPTCHA
            if (_captchaSettings.Enabled && _captchaSettings.ShowOnRegistrationPage && !captchaValid)
            {
                ModelState.AddModelError("", _captchaSettings.GetWrongCaptchaMessage(_localizationService));
            }

            if (ModelState.IsValid)
            {
                if (_customerSettings.UsernamesEnabled && model.RegisterModel.Username != null)
                {
                    model.RegisterModel.Username = model.RegisterModel.Username.Trim();
                }

                bool isApproved = _customerSettings.UserRegistrationType == UserRegistrationType.Standard;
                var registrationRequest = new CustomerRegistrationRequest(customer,
                    model.RegisterModel.Email,
                    _customerSettings.UsernamesEnabled ? model.RegisterModel.Username : model.RegisterModel.Email,
                    model.RegisterModel.Password,
                    _customerSettings.DefaultPasswordFormat,
                    _storeContext.CurrentStore.Id,
                    isApproved,
                    model.SmsRecordModel.PhoneNumber); //BUGFIX 3.802
                var registrationResult = _customerRegistrationService.RegisterCustomer(registrationRequest);
                if (registrationResult.Success)
                {
                    //properties
                    if (_dateTimeSettings.AllowCustomersToSetTimeZone)
                    {
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.TimeZoneId, model.RegisterModel.TimeZoneId);
                    }
                    //VAT number
                    if (_taxSettings.EuVatEnabled)
                    {
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.VatNumber, model.RegisterModel.VatNumber);

                        string vatName;
                        string vatAddress;
                        var vatNumberStatus = _taxService.GetVatNumberStatus(model.RegisterModel.VatNumber, out vatName, out vatAddress);
                        _genericAttributeService.SaveAttribute(customer,
                            SystemCustomerAttributeNames.VatNumberStatusId,
                            (int)vatNumberStatus);
                        //send VAT number admin notification
                        if (!String.IsNullOrEmpty(model.RegisterModel.VatNumber) && _taxSettings.EuVatEmailAdminWhenNewVatSubmitted)
                            _workflowMessageService.SendNewVatSubmittedStoreOwnerNotification(customer, model.RegisterModel.VatNumber, vatAddress, _localizationSettings.DefaultAdminLanguageId);

                    }

                    //form fields
                    if (_customerSettings.GenderEnabled)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Gender, model.RegisterModel.Gender);
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.FirstName, model.RegisterModel.FirstName);
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.LastName, model.RegisterModel.LastName);
                    if (_customerSettings.DateOfBirthEnabled)
                    {
                        DateTime? dateOfBirth = model.RegisterModel.ParseDateOfBirth();
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.DateOfBirth, dateOfBirth);
                    }
                    if (_customerSettings.CompanyEnabled)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Company, model.RegisterModel.Company);
                    if (_customerSettings.StreetAddressEnabled)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.StreetAddress, model.RegisterModel.StreetAddress);
                    if (_customerSettings.StreetAddress2Enabled)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.StreetAddress2, model.RegisterModel.StreetAddress2);
                    if (_customerSettings.ZipPostalCodeEnabled)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.ZipPostalCode, model.RegisterModel.ZipPostalCode);
                    if (_customerSettings.CityEnabled)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.City, model.RegisterModel.City);
                    if (_customerSettings.CountryEnabled)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.CountryId, model.RegisterModel.CountryId);
                    if (_customerSettings.CountryEnabled && _customerSettings.StateProvinceEnabled)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.StateProvinceId, model.RegisterModel.StateProvinceId);
                    if (_customerSettings.PhoneEnabled)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Phone, model.RegisterModel.Phone);
                    if (_customerSettings.FaxEnabled)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Fax, model.RegisterModel.Fax);

                    //newsletter
                    if (_customerSettings.NewsletterEnabled)
                    {
                        //save newsletter value
                        var newsletter = _newsLetterSubscriptionService.GetNewsLetterSubscriptionByEmailAndStoreId(model.RegisterModel.Email, _storeContext.CurrentStore.Id);
                        if (newsletter != null)
                        {
                            if (model.RegisterModel.Newsletter)
                            {
                                newsletter.Active = true;
                                _newsLetterSubscriptionService.UpdateNewsLetterSubscription(newsletter);
                            }
                            //else
                            //{
                            //When registering, not checking the newsletter check box should not take an existing email address off of the subscription list.
                            //_newsLetterSubscriptionService.DeleteNewsLetterSubscription(newsletter);
                            //}
                        }
                        else
                        {
                            if (model.RegisterModel.Newsletter)
                            {
                                _newsLetterSubscriptionService.InsertNewsLetterSubscription(new NewsLetterSubscription
                                {
                                    NewsLetterSubscriptionGuid = Guid.NewGuid(),
                                    Email = model.RegisterModel.Email,
                                    Active = true,
                                    StoreId = _storeContext.CurrentStore.Id,
                                    CreatedOnUtc = DateTime.UtcNow
                                });
                            }
                        }
                    }

                    //save customer attributes
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.CustomCustomerAttributes, customerAttributesXml);

                    //login customer now
                    if (isApproved)
                        _authenticationService.SignIn(customer, true);

                    //associated with external account (if possible)
                    TryAssociateAccountWithExternalAccount(customer);

                    //insert default address (if possible)
                    var defaultAddress = new Address
                    {
                        FirstName = customer.GetAttribute<string>(SystemCustomerAttributeNames.FirstName),
                        LastName = customer.GetAttribute<string>(SystemCustomerAttributeNames.LastName),
                        Email = customer.Email,
                        Company = customer.GetAttribute<string>(SystemCustomerAttributeNames.Company),
                        CountryId = customer.GetAttribute<int>(SystemCustomerAttributeNames.CountryId) > 0 ?
                            (int?)customer.GetAttribute<int>(SystemCustomerAttributeNames.CountryId) : null,
                        StateProvinceId = customer.GetAttribute<int>(SystemCustomerAttributeNames.StateProvinceId) > 0 ?
                            (int?)customer.GetAttribute<int>(SystemCustomerAttributeNames.StateProvinceId) : null,
                        City = customer.GetAttribute<string>(SystemCustomerAttributeNames.City),
                        Address1 = customer.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress),
                        Address2 = customer.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress2),
                        ZipPostalCode = customer.GetAttribute<string>(SystemCustomerAttributeNames.ZipPostalCode),
                        PhoneNumber = customer.GetAttribute<string>(SystemCustomerAttributeNames.Phone),
                        FaxNumber = customer.GetAttribute<string>(SystemCustomerAttributeNames.Fax),
                        CreatedOnUtc = customer.CreatedOnUtc
                    };
                    if (this._addressService.IsAddressValid(defaultAddress))
                    {
                        //some validation
                        if (defaultAddress.CountryId == 0)
                            defaultAddress.CountryId = null;
                        if (defaultAddress.StateProvinceId == 0)
                            defaultAddress.StateProvinceId = null;
                        //set default address
                        customer.Addresses.Add(defaultAddress);
                        customer.BillingAddress = defaultAddress;
                        customer.ShippingAddress = defaultAddress;
                        _customerService.UpdateCustomer(customer);
                    }

                    //notifications
                    if (_customerSettings.NotifyNewCustomerRegistration)
                        _workflowMessageService.SendCustomerRegisteredNotificationMessage(customer, _localizationSettings.DefaultAdminLanguageId);

                    //raise event       
                    _eventPublisher.Publish(new CustomerRegisteredEvent(customer));

                    if (_smsSettings.Active && registrationResult.Success) //BUGFIX 3.801
                    {
                        if (_workContext.WorkingLanguage.Name == "English")
                        {
                            model.BaseURL = _smsSettings.BaseURL;//NOP 3.821
                            model.Resource = _smsSettings.Resource;//NOP 3.821
                            model.CountryCode = _smsSettings.CountryCode;//NOP 3.821
                            model.UserName = _smsSettings.UserName;//NOP 3.821
                            model.Password = _smsSettings.Password;//NOP 3.821
                            model.MessageTemplate = _smsSettings.MessageTemplate;//NOP 3.821
                        }
                        else //BUGFIX 3.812
                        {
                            model.BaseURL = _smsSettings.BaseURLTr;
                            model.Resource = _smsSettings.ResourceTr;
                            model.CountryCode = _smsSettings.CountryCodeTr;
                            model.UserName = _smsSettings.UserNameTr;
                            model.Password = _smsSettings.PasswordTr;
                            model.MessageTemplate = _smsSettings.MessageTemplateTr;
                        }

                        model.SmsRecordModel.CustomerId = _customerService.GetCustomerByEmail(model.RegisterModel.Email).Id;
                        model.SmsRecordModel.ActivationCode = Guid.NewGuid().ToString().GetHashCode().ToString("x");
                        model.SmsRecordModel.Active = false; //BUGFIX 3.803
                        _smsService.InsertSMSRecord(model.SmsRecordModel);
                        if (_smsService.SendRegistrationSMS(model))
                        {
                            return View("~/Plugins/Misc.SMS/Views/RegisterResultSMS.cshtml", model.SmsRecordModel);
                        }
                        else
                        {
                            //NOP 3.829
                            ModelState.AddModelError("", _localizationService.GetResource("Account.Validatephone.Wrongnumber"));
                            PrepareCustomerRegisterModel(model.RegisterModel, true, customerAttributesXml);
                            return View("~/Plugins/Misc.SMS/Views/Register.cshtml", model);
                        }

                    }
                    switch (_customerSettings.UserRegistrationType)
                    {
                        case UserRegistrationType.EmailValidation:
                            {
                                //email validation message
                                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.AccountActivationToken, Guid.NewGuid().ToString());
                                _workflowMessageService.SendCustomerEmailValidationMessage(customer, _workContext.WorkingLanguage.Id);

                                //result
                                return RedirectToRoute("RegisterResult", new { resultId = (int)UserRegistrationType.EmailValidation });
                            }
                        case UserRegistrationType.AdminApproval:
                            {
                                return RedirectToRoute("RegisterResult", new { resultId = (int)UserRegistrationType.AdminApproval });
                            }
                        case UserRegistrationType.Standard:
                            {
                                //send customer welcome message
                                _workflowMessageService.SendCustomerWelcomeMessage(customer, _workContext.WorkingLanguage.Id);

                                var redirectUrl = Url.RouteUrl("RegisterResult", new { resultId = (int)UserRegistrationType.Standard });
                                if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                                    redirectUrl = _webHelper.ModifyQueryString(redirectUrl, "returnurl=" + HttpUtility.UrlEncode(returnUrl), null);
                                return Redirect(redirectUrl);
                            }
                        default:
                            {
                                return RedirectToRoute("HomePage");
                            }
                    }
                }

                //errors
                foreach (var error in registrationResult.Errors)
                    ModelState.AddModelError("", error);
            }

            //If we got this far, something failed, redisplay form
            PrepareCustomerRegisterModel(model.RegisterModel, true, customerAttributesXml);
            return View("~/Plugins/Misc.SMS/Views/Register.cshtml", model);
        }


        //available even when navigation is not allowed
        [PublicStoreAllowNavigation(true)]
        [HttpPost]
        public ActionResult RegisterResultSMS(SMSRecord smsRecordModel)
        {
            //send sms code
            return View("~/Plugins/Misc.SMS/Views/RegisterResultSMS.cshtml", smsRecordModel);
        }

        [NopHttpsRequirement(SslRequirement.Yes)]
        //available even when navigation is not allowed
        [PublicStoreAllowNavigation(true)]
        [HttpPost]
        public ActionResult AccountActivation(string token, int customerId)
        {
            var customer = _customerService.GetCustomerById(customerId);
            if (customer == null)
                return RedirectToRoute("HomePage");

            var smsRecord = _smsService.GetByCustomerId(customerId);
            var cToken = smsRecord.ActivationCode;

            if (String.IsNullOrEmpty(cToken))
                return RedirectToRoute("HomePage");

            if (!cToken.Equals(token, StringComparison.InvariantCultureIgnoreCase))
                return RedirectToRoute("HomePage");

            //BUGFIX 3.803
            smsRecord.Active = true;
            _smsService.UpdateSMSRecord(smsRecord);

            //activate user account
            customer.Active = true;
            _customerService.UpdateCustomer(customer);
            //_genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.AccountActivationToken, "");
            //send welcome message
            _workflowMessageService.SendCustomerWelcomeMessage(customer, _workContext.WorkingLanguage.Id);

            var model = new AccountActivationModel();
            model.Result = _localizationService.GetResource("Account.AccountActivation.Activated");
            return View("~/Plugins/Misc.SMS/Views/AccountActivation.cshtml", model);
        }



        public ActionResult ValidatePhone(string referrer = null)
        {
            var model = new SMSModel();
            model.Referrer = referrer;
            return View("~/Plugins/Misc.SMS/Views/ValidatePhone.cshtml", model);
        }

        [HttpPost]
        public ActionResult ValidatePhone(SMSModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = EngineContext.Current.Resolve<IWorkContext>().CurrentCustomer;
                var customerId = customer.Id;
                var activationCode = Guid.NewGuid().ToString().GetHashCode().ToString("x");
                var phoneNumber = model.SmsRecordModel.PhoneNumber;
                var active = false;

                var currentPhoneNumber = _smsService.GetPhoneNumber(_workContext.CurrentCustomer.Id);

                if (currentPhoneNumber == null)
                {
                    var smsRecord = new SMSRecord();
                    smsRecord.CustomerId = customerId;
                    smsRecord.ActivationCode = activationCode;
                    smsRecord.PhoneNumber = phoneNumber;
                    smsRecord.Active = active;
                    _smsService.InsertSMSRecord(smsRecord);
                }
                else
                {
                    var smsRecord = _smsService.GetByCustomerId(customerId);
                    smsRecord.CustomerId = customerId;
                    smsRecord.ActivationCode = activationCode;
                    smsRecord.PhoneNumber = phoneNumber;
                    smsRecord.Active = active;
                    _smsService.UpdateSMSRecord(smsRecord);
                }

                if (_workContext.WorkingLanguage.Name == "English")
                {
                    model.BaseURL = _smsSettings.BaseURL;
                    model.Resource = _smsSettings.Resource;
                    model.UserName = _smsSettings.UserName;
                    model.Password = _smsSettings.Password;
                    model.MessageTemplate = _smsSettings.MessageTemplate;
                }
                else 
                {
                    model.BaseURL = _smsSettings.BaseURLTr;
                    model.Resource = _smsSettings.ResourceTr;
                    model.UserName = _smsSettings.UserNameTr;
                    model.Password = _smsSettings.PasswordTr;
                    model.MessageTemplate = _smsSettings.MessageTemplateTr;
                }

                if (_smsService.SendSMS(model.MessageTemplate + " " + activationCode,
                    model.PhoneNumber,
                    model.SmsRecordModel.PhoneNumber, null, model.UserName,
                    model.Password, model.BaseURL,
                    model.Resource))
                {
                    return View("~/Plugins/Misc.SMS/Views/ValidatePhoneResult.cshtml", model);
                }
                else
                {
                    ModelState.AddModelError("", _localizationService.GetResource("Account.Validatephone.Wrongnumber"));
                }
            }
            return View("~/Plugins/Misc.SMS/Views/ValidatePhone.cshtml", model);
        }


        public ActionResult AccountValidation(string token, string referrer)
        {
            var customer = EngineContext.Current.Resolve<IWorkContext>().CurrentCustomer;
            var customerId = customer.Id;

            var cToken = _smsService.GetActivationCode(customerId);

            if (String.IsNullOrEmpty(cToken))
                return RedirectToRoute("HomePage");

            if (!cToken.Equals(token, StringComparison.InvariantCultureIgnoreCase))
                return RedirectToRoute("HomePage");

            var smsRecord = _smsService.GetByCustomerId(customerId);
            smsRecord.Active = true;
            _smsService.UpdateSMSRecord(smsRecord);

            var model = new AccountActivationModel();

            model.CustomProperties.Add("Referrer", referrer);

            model.Result = _localizationService.GetResource("Account.ValidatePhone.Validated");
            return View("~/Plugins/Misc.SMS/Views/AccountValidation.cshtml", model);
        }
        #endregion

        #region Utilities
        [NonAction]
        protected virtual void TryAssociateAccountWithExternalAccount(Customer customer)
        {
            var parameters = ExternalAuthorizerHelper.RetrieveParametersFromRoundTrip(true);
            if (parameters == null)
                return;

            if (_openAuthenticationService.AccountExists(parameters))
                return;

            _openAuthenticationService.AssociateExternalAccountWithUser(customer, parameters);
        }


        [NonAction]
        protected virtual IList<CustomerAttributeModel> PrepareCustomCustomerAttributes(Customer customer,
            string overrideAttributesXml = "")
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            var result = new List<CustomerAttributeModel>();

            var customerAttributes = _customerAttributeService.GetAllCustomerAttributes();
            foreach (var attribute in customerAttributes)
            {
                var attributeModel = new CustomerAttributeModel
                {
                    Id = attribute.Id,
                    Name = attribute.GetLocalized(x => x.Name),
                    IsRequired = attribute.IsRequired,
                    AttributeControlType = attribute.AttributeControlType,
                };

                if (attribute.ShouldHaveValues())
                {
                    //values
                    var attributeValues = _customerAttributeService.GetCustomerAttributeValues(attribute.Id);
                    foreach (var attributeValue in attributeValues)
                    {
                        var valueModel = new CustomerAttributeValueModel
                        {
                            Id = attributeValue.Id,
                            Name = attributeValue.GetLocalized(x => x.Name),
                            IsPreSelected = attributeValue.IsPreSelected
                        };
                        attributeModel.Values.Add(valueModel);
                    }
                }

                //set already selected attributes
                var selectedAttributesXml = !String.IsNullOrEmpty(overrideAttributesXml) ?
                    overrideAttributesXml :
                    customer.GetAttribute<string>(SystemCustomerAttributeNames.CustomCustomerAttributes, _genericAttributeService);
                switch (attribute.AttributeControlType)
                {
                    case AttributeControlType.DropdownList:
                    case AttributeControlType.RadioList:
                    case AttributeControlType.Checkboxes:
                        {
                            if (!String.IsNullOrEmpty(selectedAttributesXml))
                            {
                                //clear default selection
                                foreach (var item in attributeModel.Values)
                                    item.IsPreSelected = false;

                                //select new values
                                var selectedValues = _customerAttributeParser.ParseCustomerAttributeValues(selectedAttributesXml);
                                foreach (var attributeValue in selectedValues)
                                    foreach (var item in attributeModel.Values)
                                        if (attributeValue.Id == item.Id)
                                            item.IsPreSelected = true;
                            }
                        }
                        break;
                    case AttributeControlType.ReadonlyCheckboxes:
                        {
                            //do nothing
                            //values are already pre-set
                        }
                        break;
                    case AttributeControlType.TextBox:
                    case AttributeControlType.MultilineTextbox:
                        {
                            if (!String.IsNullOrEmpty(selectedAttributesXml))
                            {
                                var enteredText = _customerAttributeParser.ParseValues(selectedAttributesXml, attribute.Id);
                                if (enteredText.Any())
                                    attributeModel.DefaultValue = enteredText[0];
                            }
                        }
                        break;
                    case AttributeControlType.ColorSquares:
                    case AttributeControlType.ImageSquares:
                    case AttributeControlType.Datepicker:
                    case AttributeControlType.FileUpload:
                    default:
                        //not supported attribute control types
                        break;
                }

                result.Add(attributeModel);
            }


            return result;
        }


        [NonAction]
        protected virtual void PrepareCustomerRegisterModel(RegisterModel model, bool excludeProperties,
           string overrideCustomCustomerAttributesXml = "")
        {
            if (model == null)
                throw new ArgumentNullException("model");

            model.AllowCustomersToSetTimeZone = _dateTimeSettings.AllowCustomersToSetTimeZone;
            foreach (var tzi in _dateTimeHelper.GetSystemTimeZones())
                model.AvailableTimeZones.Add(new SelectListItem { Text = tzi.DisplayName, Value = tzi.Id, Selected = (excludeProperties ? tzi.Id == model.TimeZoneId : tzi.Id == _dateTimeHelper.CurrentTimeZone.Id) });

            model.DisplayVatNumber = _taxSettings.EuVatEnabled;
            //form fields
            model.GenderEnabled = _customerSettings.GenderEnabled;
            model.DateOfBirthEnabled = _customerSettings.DateOfBirthEnabled;
            model.DateOfBirthRequired = _customerSettings.DateOfBirthRequired;
            model.CompanyEnabled = _customerSettings.CompanyEnabled;
            model.CompanyRequired = _customerSettings.CompanyRequired;
            model.StreetAddressEnabled = _customerSettings.StreetAddressEnabled;
            model.StreetAddressRequired = _customerSettings.StreetAddressRequired;
            model.StreetAddress2Enabled = _customerSettings.StreetAddress2Enabled;
            model.StreetAddress2Required = _customerSettings.StreetAddress2Required;
            model.ZipPostalCodeEnabled = _customerSettings.ZipPostalCodeEnabled;
            model.ZipPostalCodeRequired = _customerSettings.ZipPostalCodeRequired;
            model.CityEnabled = _customerSettings.CityEnabled;
            model.CityRequired = _customerSettings.CityRequired;
            model.CountryEnabled = _customerSettings.CountryEnabled;
            model.CountryRequired = _customerSettings.CountryRequired;
            model.StateProvinceEnabled = _customerSettings.StateProvinceEnabled;
            model.StateProvinceRequired = _customerSettings.StateProvinceRequired;
            model.PhoneEnabled = _customerSettings.PhoneEnabled;
            model.PhoneRequired = _customerSettings.PhoneRequired;
            model.FaxEnabled = _customerSettings.FaxEnabled;
            model.FaxRequired = _customerSettings.FaxRequired;
            model.NewsletterEnabled = _customerSettings.NewsletterEnabled;
            model.AcceptPrivacyPolicyEnabled = _customerSettings.AcceptPrivacyPolicyEnabled;
            model.UsernamesEnabled = _customerSettings.UsernamesEnabled;
            model.CheckUsernameAvailabilityEnabled = _customerSettings.CheckUsernameAvailabilityEnabled;
            model.HoneypotEnabled = _securitySettings.HoneypotEnabled;
            model.DisplayCaptcha = _captchaSettings.Enabled && _captchaSettings.ShowOnRegistrationPage;
            model.EnteringEmailTwice = _customerSettings.EnteringEmailTwice;

            //countries and states
            if (_customerSettings.CountryEnabled)
            {
                model.AvailableCountries.Add(new SelectListItem { Text = _localizationService.GetResource("Address.SelectCountry"), Value = "0" });

                foreach (var c in _countryService.GetAllCountries(_workContext.WorkingLanguage.Id))
                {
                    model.AvailableCountries.Add(new SelectListItem
                    {
                        Text = c.GetLocalized(x => x.Name),
                        Value = c.Id.ToString(),
                        Selected = c.Id == model.CountryId
                    });
                }

                if (_customerSettings.StateProvinceEnabled)
                {
                    //states
                    var states = _stateProvinceService.GetStateProvincesByCountryId(model.CountryId, _workContext.WorkingLanguage.Id).ToList();
                    if (states.Any())
                    {
                        model.AvailableStates.Add(new SelectListItem { Text = _localizationService.GetResource("Address.SelectState"), Value = "0" });

                        foreach (var s in states)
                        {
                            model.AvailableStates.Add(new SelectListItem { Text = s.GetLocalized(x => x.Name), Value = s.Id.ToString(), Selected = (s.Id == model.StateProvinceId) });
                        }
                    }
                    else
                    {
                        bool anyCountrySelected = model.AvailableCountries.Any(x => x.Selected);

                        model.AvailableStates.Add(new SelectListItem
                        {
                            Text = _localizationService.GetResource(anyCountrySelected ? "Address.OtherNonUS" : "Address.SelectState"),
                            Value = "0"
                        });
                    }

                }
            }

            //custom customer attributes
            var customAttributes = PrepareCustomCustomerAttributes(_workContext.CurrentCustomer, overrideCustomCustomerAttributesXml);
            customAttributes.ForEach(model.CustomerAttributes.Add);
        }

        [NonAction]
        protected virtual string ParseCustomCustomerAttributes(FormCollection form)
        {
            if (form == null)
                throw new ArgumentNullException("form");

            string attributesXml = "";
            var attributes = _customerAttributeService.GetAllCustomerAttributes();
            foreach (var attribute in attributes)
            {
                string controlId = string.Format("customer_attribute_{0}", attribute.Id);
                switch (attribute.AttributeControlType)
                {
                    case AttributeControlType.DropdownList:
                    case AttributeControlType.RadioList:
                        {
                            var ctrlAttributes = form[controlId];
                            if (!String.IsNullOrEmpty(ctrlAttributes))
                            {
                                int selectedAttributeId = int.Parse(ctrlAttributes);
                                if (selectedAttributeId > 0)
                                    attributesXml = _customerAttributeParser.AddCustomerAttribute(attributesXml,
                                        attribute, selectedAttributeId.ToString());
                            }
                        }
                        break;
                    case AttributeControlType.Checkboxes:
                        {
                            var cblAttributes = form[controlId];
                            if (!String.IsNullOrEmpty(cblAttributes))
                            {
                                foreach (var item in cblAttributes.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                                {
                                    int selectedAttributeId = int.Parse(item);
                                    if (selectedAttributeId > 0)
                                        attributesXml = _customerAttributeParser.AddCustomerAttribute(attributesXml,
                                            attribute, selectedAttributeId.ToString());
                                }
                            }
                        }
                        break;
                    case AttributeControlType.ReadonlyCheckboxes:
                        {
                            //load read-only (already server-side selected) values
                            var attributeValues = _customerAttributeService.GetCustomerAttributeValues(attribute.Id);
                            foreach (var selectedAttributeId in attributeValues
                                .Where(v => v.IsPreSelected)
                                .Select(v => v.Id)
                                .ToList())
                            {
                                attributesXml = _customerAttributeParser.AddCustomerAttribute(attributesXml,
                                            attribute, selectedAttributeId.ToString());
                            }
                        }
                        break;
                    case AttributeControlType.TextBox:
                    case AttributeControlType.MultilineTextbox:
                        {
                            var ctrlAttributes = form[controlId];
                            if (!String.IsNullOrEmpty(ctrlAttributes))
                            {
                                string enteredText = ctrlAttributes.Trim();
                                attributesXml = _customerAttributeParser.AddCustomerAttribute(attributesXml,
                                    attribute, enteredText);
                            }
                        }
                        break;
                    case AttributeControlType.Datepicker:
                    case AttributeControlType.ColorSquares:
                    case AttributeControlType.ImageSquares:
                    case AttributeControlType.FileUpload:
                    //not supported customer attributes
                    default:
                        break;
                }
            }

            return attributesXml;
        }
        #endregion
    }
}
