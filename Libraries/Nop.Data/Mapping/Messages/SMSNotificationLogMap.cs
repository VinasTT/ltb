using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity.ModelConfiguration;
using Nop.Core.Domain.Messages;
//NOP 3.829
namespace Nop.Data.Mapping.Messages
{
    public class SMSNotificationLogMap : NopEntityTypeConfiguration<SMSNotificationLog>
    {
        public SMSNotificationLogMap()
        {
            ToTable("SMSLog");

            HasKey(m => m.Id);
            //Map the additional properties
            Property(m => m.PhoneNumber);
            Property(m => m.Message);
            Property(m => m.Status);
            Property(m => m.CreatedOnUtc); 

        }
    }
}
