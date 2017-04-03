using Nop.Core.Domain.Messages;
//NOP 3.824
namespace Nop.Services.Messages
{
    public interface ISMSNotificationService
    {
        string GetPhoneNumber(int customerId); 
        bool SendSMS(string messageTemplate, string fromNumber, string toNumber, string toName,
            string userName, string password, string baseURL, string resource, string countryCode);
        string GetCustomerByPhoneNumber(string phoneNumber);

        string GetActivationCode(int customerId); //BUGFIX 3.803
        void InsertSMSRecord(SMSNotificationRecord smsNotificationRecord); //BUGFIX 3.803
        void UpdateSMSRecord(SMSNotificationRecord smsNotificationRecord); //BUGFIX 3.803
        bool CheckIfPhoneExistsAndActive(int customerId); //BUGFIX 3.803

        SMSNotificationRecord GetByCustomerId(int customerId); //BUGFIX 3.803

    }
}
