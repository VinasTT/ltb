using Nop.Core.Domain.Catalog;
using Nop.Plugin.Misc.SMS.Models;
using Nop.Core.Data;
using System.Linq;
using System;
using Nop.Core.Domain.Messages;
using Nop.Services.Messages;
using Nop.Services.Stores;
using Nop.Core;

namespace Nop.Plugin.Misc.SMS.Services
{
    public class SMSService : ISMSService
    {
        private readonly IRepository<SMSRecord> _SMSRepository;
        private readonly IMessageTemplateService _messageTemplateService;
        private readonly IStoreService _storeService;
        private readonly IStoreContext _storeContext;

        public SMSService(IRepository<SMSRecord> SMSRepository,
            IMessageTemplateService messageTemplateService,
            IStoreService storeService,
            IStoreContext storeContext)
        {
            _SMSRepository = SMSRepository;
            this._messageTemplateService = messageTemplateService;
            this._storeService = storeService;
            this._storeContext = storeContext;
        }
        

        public SMSRecord GetByCustomerId(int customerId)
        {
            if (customerId == 0)
                return null;

            var query = from gp in _SMSRepository.Table
                        where gp.CustomerId == customerId
                        orderby gp.Id
                        select gp;
            var record = query.FirstOrDefault();

            return record;
        }

        public virtual void InsertSMSRecord(SMSRecord smsRecord)
        {
            if (smsRecord == null)
                throw new ArgumentNullException("googleProductRecord");

            _SMSRepository.Insert(smsRecord);
        }

        public virtual void UpdateSMSRecord(SMSRecord smsRecord)
        {
            if (smsRecord == null)
                throw new ArgumentNullException("googleProductRecord");

            _SMSRepository.Update(smsRecord);
        }

        public bool SendRegistrationSMS(SMSModel model)
        {
            var store = _storeContext.CurrentStore;

            var messageTemplate = model.MessageTemplate + " " + model.SmsRecordModel.ActivationCode;
            var fromNumber = model.PhoneNumber;
            var toNumber = model.SmsRecordModel.PhoneNumber;
            var toName = model.RegisterModel.FirstName + " " + model.RegisterModel.LastName;
            return SendSMS(messageTemplate,fromNumber,toNumber,toName);
         
        }


        public bool SendSMS(string messageTemplate,string fromNumber,string toNumber,string toName)
        {
            //sms api code
            return true;
        }


        protected virtual MessageTemplate GetActiveMessageTemplate(string messageTemplateName, int storeId)
        {
            var messageTemplate = _messageTemplateService.GetMessageTemplateByName(messageTemplateName, storeId);

            //no template found
            if (messageTemplate == null)
                return null;

            //ensure it's active
            var isActive = messageTemplate.IsActive;
            if (!isActive)
                return null;

            return messageTemplate;
        }
    }
}
