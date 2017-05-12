using System;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using Nop.Web.Models.Customer;

//BUGFIX 3.803
namespace Nop.Web.Validators.Customer
{
    public partial class SMSNotificationValidator : BaseNopValidator<SMSNotificationRecordModel>
    {
        public SMSNotificationValidator(ILocalizationService localizationService, 
            IStateProvinceService stateProvinceService,
            CustomerSettings customerSettings)
        {
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.Phone.Required"))
                .Matches(@"^\+((\d{2})(\d{3})(\d{3})(\d{2})(\d{2}))$") //BUGFIX 3.813 
                .WithMessage(localizationService.GetResource("Plugins.Misc.SMS.PhoneNumberFormat"));
           
           
        }
    }
}