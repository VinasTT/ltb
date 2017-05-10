using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Core.Domain.Messages;
using Nop.Core;
using Nop.Services.Messages;
using Nop.Web.Models.Customer;
using Nop.Services.Localization;

//BUGFIX 3.803
namespace Nop.Web.Controllers
{
    public class SMSNotificationController : BasePublicController
    {

        private readonly IWorkContext _workContext;
        private readonly ISMSNotificationService _smsNotificationService; 
        private readonly SMSNotificationSettings _smsNotificationSettings;
        private readonly ILocalizationService _localizationService;

        public SMSNotificationController(IWorkContext workContext,
            ISMSNotificationService smsNotificationService,
            SMSNotificationSettings smsNotificationSettings,
            ILocalizationService localizationService)
        {
            this._workContext = workContext;
            this._smsNotificationService = smsNotificationService;
            this._smsNotificationSettings = smsNotificationSettings;
            this._localizationService = localizationService;
        }
        // GET: SMSNotification
        public ActionResult ValidatePhone(string referrer = null) //NOP 3.825
        {
            var model = new SMSNotificationRecordModel();
            TempData["Referrer"] = referrer;
            return View(model);
        }

        [HttpPost]
        public ActionResult ValidatePhone(SMSNotificationRecordModel model)
        {
            TempData.Keep();
            if (ModelState.IsValid)
            {
                //NOP 3.825
                var customerId = _workContext.CurrentCustomer.Id;
                var activationCode = Guid.NewGuid().ToString().GetHashCode().ToString("x");
                var phoneNumber = model.PhoneNumber;
                var active = false;
                
                var currentPhoneNumber = _smsNotificationService.GetPhoneNumber(_workContext.CurrentCustomer.Id);

                if (currentPhoneNumber == null)
                {
                    var smsNotificationRecord = new SMSNotificationRecord();
                    smsNotificationRecord.CustomerId = customerId;
                    smsNotificationRecord.ActivationCode = activationCode;
                    smsNotificationRecord.PhoneNumber = phoneNumber;
                    smsNotificationRecord.Active = active;
                    _smsNotificationService.InsertSMSRecord(smsNotificationRecord);
                }
                else
                {
                    var smsNotificationRecord = _smsNotificationService.GetByCustomerId(customerId);
                    smsNotificationRecord.CustomerId = customerId;
                    smsNotificationRecord.ActivationCode = activationCode;
                    smsNotificationRecord.PhoneNumber = phoneNumber;
                    smsNotificationRecord.Active = active;
                    _smsNotificationService.UpdateSMSRecord(smsNotificationRecord);
                }
                    
                
                if (_smsNotificationService.SendSMS(_smsNotificationSettings.RegisterMessage + " " + activationCode, 
                    _smsNotificationSettings.PhoneNumber,
                    model.PhoneNumber, null, _smsNotificationSettings.UserName,
                    _smsNotificationSettings.Password, _smsNotificationSettings.BaseURL,
                    _smsNotificationSettings.Resource, _smsNotificationSettings.CountryCode))
                {
                    return View("ValidatePhoneResult", model);
                }
                else
                {
                    ModelState.AddModelError("", _localizationService.GetResource("Account.Validatephone.Wrongnumber")); //NOP 3.829
                }
                //NOP 3.825
            }
            return View("ValidatePhone", model);
        }


        public ActionResult AccountActivation(string token)
        {
            var customerId = _workContext.CurrentCustomer.Id;
           
            var cToken = _smsNotificationService.GetActivationCode(customerId);
            
            if (String.IsNullOrEmpty(cToken))
                return RedirectToRoute("HomePage");

            if (!cToken.Equals(token, StringComparison.InvariantCultureIgnoreCase))
                return RedirectToRoute("HomePage");

            var smsRecord = _smsNotificationService.GetByCustomerId(customerId);
            smsRecord.Active = true;
            _smsNotificationService.UpdateSMSRecord(smsRecord);

            var model = new AccountActivationModel();
            //BUGFIX 3.811
            var referrer = TempData["Referrer"];
            model.CustomProperties.Add("Referrer", referrer);

            model.Result = _localizationService.GetResource("Account.ValidatePhone.Validated"); //NOP 3.825
            return View("AccountActivation", model);
        }
    }
}