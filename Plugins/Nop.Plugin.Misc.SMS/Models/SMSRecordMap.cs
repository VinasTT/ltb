using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity.ModelConfiguration;

namespace Nop.Plugin.Misc.SMS.Models
{
    public class SMSRecordMap : EntityTypeConfiguration<SMSRecord>
    {
        public SMSRecordMap()
        {
            ToTable("SMS");

            HasKey(m => m.Id);
            //Map the additional properties
            Property(m => m.CustomerId);
            Property(m => m.PhoneNumber);
            Property(m => m.ActivationCode);

        }
    }
}
