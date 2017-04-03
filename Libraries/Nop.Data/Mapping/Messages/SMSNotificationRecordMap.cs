using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity.ModelConfiguration;
using Nop.Core.Domain.Messages;

namespace Nop.Data.Mapping.Messages
{
    public class SMSNotificationRecordMap : NopEntityTypeConfiguration<SMSNotificationRecord>
    {
        public SMSNotificationRecordMap()
        {
            ToTable("SMS");

            HasKey(m => m.Id);
            //Map the additional properties
            Property(m => m.CustomerId);
            Property(m => m.PhoneNumber);
            Property(m => m.ActivationCode);
            Property(m => m.Active); //BUGFIX 3.803

        }
    }
}
