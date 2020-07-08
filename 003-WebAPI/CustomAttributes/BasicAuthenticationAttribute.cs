//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Security.Principal;
//using System.Text;
//using System.Threading;
//using System.Web;
//using System.Web.Http.Controllers;
//using System.Web.Http.Filters;

//namespace RentCars
//{
//	public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
//	{
//		public override void OnAuthorization(HttpActionContext actionContext)
//		{
//			string username = "";
//			int userLevel = 0;
//			if (actionContext.Request.Headers == null || actionContext.Request.Headers.Authorization == null)
//			{
//				actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
//			}
//			else
//			{
//				string athenticationToken = actionContext.Request.Headers.Authorization.Parameter;
//				string decodedAthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(athenticationToken));
//				string[] usernameLevelArray = decodedAthenticationToken.Split(':');
//				username = usernameLevelArray[0];
//				userLevel = int.Parse(usernameLevelArray[1]);

//				if (UserSecurity.Login(username, userLevel))
//				{
//					Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
//				}
//				else
//				{
//					actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
//				}
//			}
//		}
//	}
//}