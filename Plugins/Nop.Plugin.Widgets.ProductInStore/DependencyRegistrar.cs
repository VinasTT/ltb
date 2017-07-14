using Autofac;
using Autofac.Core;
using Nop.Core.Configuration;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Web.Framework.Mvc;
using Nop.Plugin.Widgets.ProductInStore.Models;
using System.Web.Mvc;

namespace Nop.Plugin.Widgets.ProductInStore
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
            
            //data context
            this.RegisterPluginDataContext<ProductInStoreObjectContext>(builder, "nop_object_context_ProductInStore");

            //override required repository with our custom context
            builder.RegisterType<EfRepository<ProductInStoreRecord>>()
                .As<IRepository<ProductInStoreRecord>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_ProductInStore"))
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
