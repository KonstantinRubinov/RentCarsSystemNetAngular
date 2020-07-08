using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;

[assembly: OwinStartup(typeof(RentCars.Startup))]

namespace RentCars
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			if (GlobalVariable.logicType == 3)
			{
				new IfMongoDb().AddMongoData();
			}
			
			app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

			var myProvider = new MyAuthProvider();
			OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
			{
				AllowInsecureHttp = true,
				TokenEndpointPath = new PathString("/token"),
				AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
				Provider = myProvider
			};
			app.UseOAuthAuthorizationServer(options);
			app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());


			HttpConfiguration config = new HttpConfiguration();
			WebApiConfig.Register(config);
		}
	}
}
