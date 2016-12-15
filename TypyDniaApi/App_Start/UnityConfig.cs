using Microsoft.Practices.Unity;
using System.Web.Http;
using TypyDniaApi.Model.DataSource.Contexts;
using TypyDniaApi.Model.Repostiories;
using TypyDniaApi.Model.Services;
using Unity.WebApi;

namespace TypyDniaApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            
            container.RegisterType<IWhoScoredService, WhoScoredService>();
            container.RegisterType<IMatchDetailsRepository, MatchDetailsRepository>();

            container.RegisterType<IDayRepository, DayRepository>();
            container.RegisterType<ITableRepository, TableRepository>();
            container.RegisterType<IContext, TypyDniaContext>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}