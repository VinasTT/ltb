using Autofac;
using Autofac.Core;
using Nop.Core.Configuration;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Web.Framework.Mvc;
using Nop.Plugin.Misc.District.Services;
using Nop.Plugin.Misc.District.Models;
using System.Web.Mvc;

namespace Nop.Plugin.Widgets.District
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
            builder.RegisterType<DistrictService>().As<IDistrictService>().InstancePerLifetimeScope();
            builder.RegisterType<DistrictActionFilter>().As<IFilterProvider>().SingleInstance();
            //data context
            this.RegisterPluginDataContext<DistrictObjectContext>(builder, "nop_object_context_mvp");

            //override required repository with our custom context
            builder.RegisterType<EfRepository<DistrictRecord>>()
                .As<IRepository<DistrictRecord>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_mvp"))
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
