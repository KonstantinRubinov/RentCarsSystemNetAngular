using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace RentCars
{
	public class AutorizeByRoleAttribute : AuthorizationFilterAttribute
	{
		public enum roles
		{
			Guest = 0,
			User = 1,
			Manager = 2,
			Admin = 4
		}

		private roles role;
		private roles value;
		public string Role { get; set; }

		public AutorizeByRoleAttribute(roles role2)
		{
			role = role2;
		}

		public override void OnAuthorization(HttpActionContext actionContext)
		{
			string username = "";
			int userLevel = 0;
			if (actionContext.Request.Headers == null || actionContext.Request.Headers.Authorization == null)
			{
				actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
			}
			else
			{
				string id = HttpContext.Current.User.Identity.Name;

				UserModel userModel;

				if (GlobalVariable.logicType == 0)
					userModel = new EntityUsersManager().GetOneUserById(id);
				else if (GlobalVariable.logicType == 1)
					userModel = new SqlUsersManager().GetOneUserById(id);
				else if (GlobalVariable.logicType == 2)
					userModel = new MySqlUsersManager().GetOneUserById(id);
				else
					userModel = new MongoUsersManager().GetOneUserById(id);

				username = userModel.userNickName;
				userLevel = userModel.userLevel;
				value = (roles)userLevel;

				if (UserSecurity.Login(username, userLevel))
				{
					if (value >= role)
					{
						Debug.WriteLine("OnAuthorization: " + value + ">=" + role);
						Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
					}
					else
					{
						Debug.WriteLine("OnAuthorization: " + value + "<" + role);
						actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
					}
				}
				else
				{
					Debug.WriteLine("OnAuthorization: " + username + " or " + userLevel + " is wrong");
					actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
				}
			}
		}
	}
}