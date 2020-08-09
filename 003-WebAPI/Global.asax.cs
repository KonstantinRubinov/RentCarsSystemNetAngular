using SimpleInjector;
using System.Web.Http;
using System.Web.Mvc;

namespace RentCars
{
    public class WebApiApplication : System.Web.HttpApplication
    {
		private void ConfigureApi()
		{
			var container = new Container();
			container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

			if (GlobalVariable.logicType == 0)
			{
				container.Register<IUsersRepository, EntityUsersManager>();
				container.Register<IBranchRepository, EntityBranchManager>();
				container.Register<ICarForRentRepository, EntityCarForRentManager>();
				container.Register<ICarsRepository, EntityCarsManager>();
				container.Register<ICarTypeRepository, EntityCarTypeManager>();
				container.Register<IFullCarDataRepository, EntityFullCarDataManager>();
				container.Register<IMessagesRepository, EntityMessagesManager>();
				container.Register<IRoleRepository, EntityRoleManager>();
				container.Register<ISearchRepository, EntitySearchManager>();
				container.Register<IPriceRepository, EntityPriceManager>();
			}
			else if (GlobalVariable.logicType == 1)
			{
				// configure DI for application services
				container.Register<IUsersRepository, SqlUsersManager>();
				container.Register<IBranchRepository, SqlBranchManager>();
				container.Register<ICarForRentRepository, SqlCarForRentManager>();
				container.Register<ICarsRepository, SqlCarsManager>();
				container.Register<ICarTypeRepository, SqlCarTypeManager>();
				container.Register<IFullCarDataRepository, SqlFullCarDataManager>();
				container.Register<IMessagesRepository, SqlMessagesManager>();
				container.Register<IRoleRepository, SqlRoleManager>();
				container.Register<ISearchRepository, SqlSearchManager>();
				container.Register<IPriceRepository, SqlPriceManager>();
			}
			else if (GlobalVariable.logicType == 2)
			{
				// configure DI for application services
				container.Register<IUsersRepository, MySqlUsersManager>();
				container.Register<IBranchRepository, MySqlBranchManager>();
				container.Register<ICarForRentRepository, MySqlCarForRentManager>();
				container.Register<ICarsRepository, MySqlCarsManager>();
				container.Register<ICarTypeRepository, MySqlCarTypeManager>();
				container.Register<IFullCarDataRepository, MySqlFullCarDataManager>();
				container.Register<IMessagesRepository, MySqlMessagesManager>();
				container.Register<IRoleRepository, MySqlRoleManager>();
				container.Register<ISearchRepository, MySqlSearchManager>();
				container.Register<IPriceRepository, MySqlPriceManager>();
			}
			else
			{
				// configure DI for application services
				container.Register<IUsersRepository, MongoUsersManager>();
				container.Register<IBranchRepository, MongoBranchManager>();
				container.Register<ICarForRentRepository, MongoCarForRentManager>();
				container.Register<ICarsRepository, MongoCarsManager>();
				container.Register<ICarTypeRepository, MongoCarTypeManager>();
				container.Register<IFullCarDataRepository, MongoFullCarDataManager>();
				container.Register<IMessagesRepository, MongoMessagesManager>();
				container.Register<IRoleRepository, MongoRoleManager>();
				container.Register<ISearchRepository, MongoSearchManager>();
				container.Register<IPriceRepository, MongoPriceManager>();
			}
			container.Verify();
			DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
			GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorDependencyResolver(container);
		}

		protected void Application_Start()
        {
			AreaRegistration.RegisterAllAreas();
			ConfigureApi();

			GlobalConfiguration.Configuration.MessageHandlers.Add(new MessageLoggingHandler());
			GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
