using Autofac;
using Autofac.Core;
using Nop.Core.Configuration;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Web.Framework.Mvc;
using Nop.Plugin.Misc.StoreList.Services;
using Nop.Plugin.Misc.StoreList.Models;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.StoreList
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
            builder.RegisterType<StoreListService>().As<IStoreListService>().InstancePerLifetimeScope();
            //data context
            this.RegisterPluginDataContext<StoreListObjectContext>(builder, "nop_object_context_storelist");

            //override required repository with our custom context
            builder.RegisterType<EfRepository<StoreListRecord>>()
                .As<IRepository<StoreListRecord>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_storelist"))
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
