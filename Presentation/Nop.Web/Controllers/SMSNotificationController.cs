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
        public ActionResult ValidatePhone()
        {
            var model = new SMSNotificationRecordModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult ValidatePhone(SMSNotificationRecordModel model)
        {
            if (ModelState.IsValid)
            {
                var smsNotificationRecord = new SMSNotificationRecord();
                smsNotificationRecord.CustomerId = _workContext.CurrentCustomer.Id;
                smsNotificationRecord.ActivationCode = Guid.NewGuid().ToString().GetHashCode().ToString("x");
                smsNotificationRecord.PhoneNumber = model.PhoneNumber;
                smsNotificationRecord.Active = false;
                _smsNotificationService.InsertSMSRecord(smsNotificationRecord);
                if (_smsNotificationService.SendSMS(_smsNotificationSettings.RegisterMessage + " " + smsNotificationRecord.ActivationCode, 
                    _smsNotificationSettings.PhoneNumber,
                    model.PhoneNumber, null, _smsNotificationSettings.UserName,
                    _smsNotificationSettings.Password, _smsNotificationSettings.BaseURL,
                    _smsNotificationSettings.Resource, _smsNotificationSettings.CountryCode))
                {
                    return View("ValidatePhoneResult", model);
                }

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
            model.Result = _localizationService.GetResource("Account.AccountActivation.Activated");
            return View("AccountActivation", model);
        }
    }
}