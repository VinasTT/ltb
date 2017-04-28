using Nop.Core.Domain.Catalog;
using Nop.Core.Data;
using System.Linq;
using System;
using Nop.Core.Domain.Messages;
using Nop.Services.Messages;
using Nop.Services.Stores;
using Nop.Core;
using RestSharp;

//NOP 3.824
namespace Nop.Services.Messages
{
    public class SMSNotificationService : ISMSNotificationService
    {
        private readonly IRepository<SMSNotificationRecord> _SMSRepository;
        private readonly IMessageTemplateService _messageTemplateService;
        private readonly IStoreService _storeService;
        private readonly IStoreContext _storeContext;
        private readonly IRepository<SMSNotificationLog> _smsLogRepository; //NOP 3.829

        public SMSNotificationService(IRepository<SMSNotificationRecord> SMSRepository,
            IMessageTemplateService messageTemplateService,
            IStoreService storeService,
            IStoreContext storeContext,
            IRepository<SMSNotificationLog> smsLogRepository) //NOP 3.829
        {
            _SMSRepository = SMSRepository;
            this._messageTemplateService = messageTemplateService;
            this._storeService = storeService;
            this._storeContext = storeContext;
            this._smsLogRepository = smsLogRepository; //NOP 3.829
        }
        
       
        public string GetPhoneNumber(int customerId) {
            if (customerId == 0)
                return null;

            var query = from gp in _SMSRepository.Table
                        where gp.CustomerId == customerId
                        orderby gp.Id
                        select gp;
            var record = query.FirstOrDefault();

            if (record != null)
                return record.PhoneNumber;
            else
                return null;

        }

        //BUGFIX 3.803
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

        //BUGFIX 3.803
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

        //BUGFIX 3.803
        public virtual void InsertSMSRecord(SMSNotificationRecord smsNotificationRecord)
        {
            if (smsNotificationRecord == null)
                throw new ArgumentNullException("SMSNotificationRecord");

            _SMSRepository.Insert(smsNotificationRecord);
        }

        //BUGFIX 3.803
        public virtual void UpdateSMSRecord(SMSNotificationRecord smsNotificationRecord)
        {
            if (smsNotificationRecord == null)
                throw new ArgumentNullException("SMSNotificationRecord");

            _SMSRepository.Update(smsNotificationRecord);
        }

        //BUGFIX 3.803
        public SMSNotificationRecord GetByCustomerId(int customerId)
        {
            if (customerId == 0)
                return null;

            var query = from gp in _SMSRepository.Table
                        where gp.CustomerId == customerId
                        orderby gp.Id descending
                        select gp;
            var record = query.FirstOrDefault();

            return record;
        }

        public string GetCustomerByPhoneNumber(string phoneNumber)
        {
            if (phoneNumber == null)
                return null;

            var query = from gp in _SMSRepository.Table
                        where gp.PhoneNumber == phoneNumber
                        orderby gp.Id descending //BUGFIX 3.803
                        select gp;
            var record = query.FirstOrDefault();

            if (record != null)
                return record.PhoneNumber;
            else
                return null;
        }

        //NOP 3.829
        public virtual void InsertSMSLogRecord(SMSNotificationLog smsNotificationLog)
        {
            if (smsNotificationLog == null)
                throw new ArgumentNullException("SMSNotificationLog");

            _smsLogRepository.Insert(smsNotificationLog);
        }

        //NOP 3.829
        public virtual void UpdateSMSLogRecord(SMSNotificationLog smsNotificationLog)
        {
            if (smsNotificationLog == null)
                throw new ArgumentNullException("SMSNotificationLog");

            _smsLogRepository.Update(smsNotificationLog);
        }

        public bool SendSMS(string messageTemplate, string fromNumber, string toNumber, string toName,
            string userName, string password, string baseURL, string resource, string countryCode)
        {
            
            var uP = userName + ":" + password;
            var toNumberWithCountry = countryCode + toNumber;
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(uP);
            var encodedUP = System.Convert.ToBase64String(plainTextBytes);

            var client = new RestClient(baseURL);
            var request = new RestRequest(resource, Method.POST);
            request.RequestFormat = DataFormat.Json;
            // easily add HTTP Headers
            request.AddHeader("Authorization", "Basic " + encodedUP);
            request.AddHeader("Content-Type", "application/json");

            request.AddBody(new SMSNotificationRequest
            {
                from = fromNumber,
                to = toNumberWithCountry,
                text = messageTemplate
            });

            //NOP 3.829
            var SMSNotificationLog = new SMSNotificationLog()
            {
                PhoneNumber = toNumber,
                Message = messageTemplate,
                CreatedOnUtc = DateTime.UtcNow
            };

            InsertSMSLogRecord(SMSNotificationLog);
            //NOP 3.829

            // execute the request
            var response = client.Execute<SMSNotificationResponse>(request);
            var content = response.Data;

            client = new RestClient("https://api.infobip.com/sms/1/reports" + "?" + content.messages[0].messageId);

            request = new RestRequest(Method.GET);
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", "Basic " + encodedUP);

            var reportResponse = client.Execute<SMSNotificationReportResponse>(request);
            var reportContent = reportResponse.Data;

            foreach (var item in reportContent.results)
            {
                SMSNotificationLog.Status = item.status.name; //NOP 3.829
                UpdateSMSLogRecord(SMSNotificationLog); //NOP 3.829
                if (item.status.name == "DELIVERED_TO_HANDSET")
                    return true;
            }

            return false;
        }
    }
}
