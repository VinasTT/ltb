using Nop.Core.Domain.Catalog;
using Nop.Plugin.Misc.SMS.Models;
using Nop.Core.Data;
using System.Linq;
using System;
using Nop.Services.Messages;
using Nop.Services.Stores;
using Nop.Core;
using RestSharp;

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
                        orderby gp.Id descending //BUGFIX 3.803
                        select gp;
            var record = query.FirstOrDefault();

            return record;
        }

        public virtual void InsertSMSRecord(SMSRecord smsRecord)
        {
            if (smsRecord == null)
                throw new ArgumentNullException("SMSRecord");

            _SMSRepository.Insert(smsRecord);
        }

        public virtual void UpdateSMSRecord(SMSRecord smsRecord)
        {
            if (smsRecord == null)
                throw new ArgumentNullException("SMSRecord");

            _SMSRepository.Update(smsRecord);
        }

        public bool SendRegistrationSMS(SMSModel model)
        {
            var store = _storeContext.CurrentStore;

            var messageTemplate = model.MessageTemplate + " " + model.SmsRecordModel.ActivationCode;
            var fromNumber = model.PhoneNumber;
            var toNumber = model.SmsRecordModel.PhoneNumber;//BUGFIX 3.812
            var toName = model.RegisterModel.FirstName + " " + model.RegisterModel.LastName;
            var userName = model.UserName;
            var password = model.Password;
            var baseURL = model.BaseURL;//NOP 3.821
            var resource = model.Resource;//NOP 3.821
            return SendSMS(messageTemplate,fromNumber,toNumber,toName,
                userName,password,baseURL,resource);//NOP 3.821
         
        }


        public bool SendSMS(string messageTemplate,string fromNumber,string toNumber,string toName,
            string userName,string password, string baseURL, string resource) //NOP 3.821
        {
            //NOP 3.821
            var uP = userName + ":" + password;
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(uP);
            var encodedUP = System.Convert.ToBase64String(plainTextBytes);

            var client = new RestClient(baseURL);
            var request = new RestRequest(resource, Method.POST);
            request.RequestFormat = DataFormat.Json;
            // easily add HTTP Headers
            request.AddHeader("Authorization", "Basic " + encodedUP);
            request.AddHeader("Content-Type", "application/json");

            request.AddBody(new SMSMessageModel
            {
                from = fromNumber,
                to = toNumber,
                text = messageTemplate
            });


            // execute the request
            var response = client.Execute<SMSResponseModel>(request);
            var content = response.Data; 

            foreach (var item in content.messages)
            {
                if (item.status.name == "PENDING_ENROUTE")
                    return true;
            }
            
            return false;
            //NOP 3.821
        }

        //NOP 3.824
        public string GetPhoneNumber(int customerId) {
            if (customerId == 0)
                return null;

            var query = from gp in _SMSRepository.Table
                        where gp.CustomerId == customerId
                        orderby gp.Id descending //BUGFIX 3.803
                        select gp;
            var record = query.FirstOrDefault();

            return record.PhoneNumber;

        }
        
        public bool CheckIfPhoneExistsAndActive(int customerId)
        {
            if (customerId == 0)
                return false;

            var query = from gp in _SMSRepository.Table
                        where gp.CustomerId == customerId
                            && gp.Active == true
                        orderby gp.Id descending
                        select gp;
            var record = query.FirstOrDefault();

            if (record != null)
                return true;
            else
                return false;
        }
        
        public string GetActivationCode(int customerId)
        {
            if (customerId == 0)
                return null;

            var query = from gp in _SMSRepository.Table
                        where gp.CustomerId == customerId
                        orderby gp.Id descending
                        select gp;
            var record = query.FirstOrDefault();

            if (record != null)
                return record.ActivationCode;
            else
                return null;

        }

        public string GetCustomerByPhoneNumber(string phoneNumber)
        {
            if (phoneNumber == null)
                return null;

            var query = from gp in _SMSRepository.Table
                        where gp.PhoneNumber == phoneNumber
                        orderby gp.Id descending
                        select gp;
            var record = query.FirstOrDefault();

            if (record != null)
                return record.PhoneNumber;
            else
                return null;
        }
    }
}
