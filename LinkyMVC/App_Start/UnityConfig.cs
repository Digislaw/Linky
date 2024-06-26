using AutoMapper;
using Linky.DataAccessLayer.Repositories.Abstract;
using Linky.DataAccessLayer.Repositories.Concrete;
using Linky.Entities;
using Linky.Entities.Identity;
using Linky.Services.Abstract;
using Linky.Services.Concrete;
using LinkyMVC.App_Start;
using LinkyMVC.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Data.Entity;
using System.Web;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace LinkyMVC
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();

            // Context
            container.RegisterType<DbContext, AppDbContext>(new HierarchicalLifetimeManager());

            // Identity
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
            container.RegisterType<UserManager<ApplicationUser>>();
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(x => HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<AccountController>(new InjectionConstructor());

            // Automapper
            var autoMapperConfig = new MapperConfiguration(x => x.AddProfile<AutoMapperBaseConfig>());
            var mapper = autoMapperConfig.CreateMapper();
            autoMapperConfig.AssertConfigurationIsValid();
            container.RegisterInstance(mapper);

            // Repositories
            container.RegisterType<ILinkRepository, LinkRepository>();

            // Services
            container.RegisterType<ILinkService, LinkService>();
            container.RegisterType<IGeolocationService, GeolocationService>();
        }
    }
}