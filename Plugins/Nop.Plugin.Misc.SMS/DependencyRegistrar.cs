using Autofac;
using Autofac.Core;
using Nop.Core.Configuration;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Plugin.Misc.SMS.Models;
using Nop.Plugin.Misc.SMS.Services;
using Nop.Web.Framework.Mvc;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.SMS
{
    /// <summary>
    /// Dependency registrar
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="config">Config</param>
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<SMSService>().As<ISMSService>().InstancePerLifetimeScope();

            //data context
            this.RegisterPluginDataContext<SMSObjectContext>(builder, "nop_object_context_sms");
            builder.RegisterType<SMSActionFilter>().As<IFilterProvider>().SingleInstance();
            //override required repository with our custom context
            builder.RegisterType<EfRepository<SMSRecord>>()
                .As<IRepository<SMSRecord>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_sms"))
                .InstancePerLifetimeScope();
        }

        /// <summary>
        /// Order of this dependency registrar implementation
        /// </summary>
        public int Order
        {
            get { return 1; }
        }
    }
}
