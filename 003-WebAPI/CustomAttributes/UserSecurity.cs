using System.Diagnostics;

namespace RentCars
{
	public class UserSecurity
	{
		public static bool Login(string username, int userLevel = 0)
		{
			LoginModel user;

			if (GlobalVariable.logicType == 0)
				user = new EntityUsersManager().ReturnUserByNameLevel(username, userLevel);
			else if (GlobalVariable.logicType == 1)
				user = new SqlUsersManager().ReturnUserByNameLevel(username, userLevel);
			else if (GlobalVariable.logicType == 2)
				user = new MySqlUsersManager().ReturnUserByNameLevel(username, userLevel);
			else
				user = new MongoUsersManager().ReturnUserByNameLevel(username, userLevel);

			if (user != null)
			{
				Debug.WriteLine("Login: " + user.ToString() + "!=null");
				Debug.WriteLine("Login: userLevel is: " + user.userLevel);
				if (user.userLevel > 0)
				{
					return true;
				}
				else
				{
					return false;
				}

			}
			else
			{
				Debug.WriteLine("Login: user=null");
				return false;
			}
			
		}
	}
}