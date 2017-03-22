using Nop.Core.Domain.Catalog;
using Nop.Plugin.Misc.SMS.Models;

namespace Nop.Plugin.Misc.SMS.Services
{
    public interface ISMSService
    {
        /// <summary>
        /// Logs the specified record.
        /// </summary>
        /// <param name="record">The record.</param>

        SMSRecord GetByCustomerId(int customerId);

        void UpdateSMSRecord(SMSRecord smsRecord);

        void InsertSMSRecord(SMSRecord smsRecord);

        bool SendRegistrationSMS(SMSModel model);

        bool SendSMS(string messageTemplate, string fromNumber, string toNumber, string toName);
    }
}
