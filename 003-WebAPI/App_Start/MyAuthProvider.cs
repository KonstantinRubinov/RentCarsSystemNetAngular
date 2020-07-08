using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RentCars
{
	public class MyAuthProvider : OAuthAuthorizationServerProvider
	{
		UserModel userdata;

		public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
		{
			context.Validated();
		}

		public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
		{
			var identity = new ClaimsIdentity(context.Options.AuthenticationType);


			if (GlobalVariable.logicType == 0)
				userdata = new EntityUsersManager().GetOneUserByLogin(context.UserName, context.Password);
			else if (GlobalVariable.logicType == 1)
				userdata = new SqlUsersManager().GetOneUserByLogin(context.UserName, context.Password);
			else if (GlobalVariable.logicType == 2)
				userdata = new MySqlUsersManager().GetOneUserByLogin(context.UserName, context.Password);
			else
				userdata = new MongoUsersManager().GetOneUserByLogin(context.UserName, context.Password);

			if (userdata != null)
			{
				identity.AddClaim(new Claim(ClaimTypes.Role, userdata.userLevel.ToString()));
				identity.AddClaim(new Claim(ClaimTypes.Name, userdata.userID));
				context.Validated(identity);
			}
			else
			{
				Debug.WriteLine("GrantResourceOwnerCredentials from: " + "invalid_grant" + "Provided username and password is incorrect");
				context.SetError("invalid_grant", "Provided username and password is incorrect");
				context.Rejected();
			}
		}

		public override Task TokenEndpoint(OAuthTokenEndpointContext context)
		{
			foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
			{
				context.AdditionalResponseParameters.Add(property.Key, property.Value);
			}
			context.AdditionalResponseParameters.Add("userNickName", userdata.userNickName);
			context.AdditionalResponseParameters.Add("userLevel", userdata.userLevel);
			context.AdditionalResponseParameters.Add("userPicture", userdata.userPicture);

			return Task.FromResult<object>(null);
		}
	}
}