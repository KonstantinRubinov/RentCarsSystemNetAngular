using RentCars.EntityDataBase;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentCars
{
	public class EntityUsersManager : EntityBaseManager, IUsersRepository
	{
		public List<UserModel> GetAllUsers()
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.USERS.Select(u => new UserModel
				{
					userID = u.userID,
					userFirstName = u.userFirstName,
					userLastName = u.userLastName,
					userNickName = u.userNickName,
					userPassword = u.userPassword,
					userEmail = u.userEmail,
					userGender = u.userGender,
					userBirthDate = u.userBirthDate,
					userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
					userLevel = u.userLevel
				}).ToList();
			}
			else
			{
				return DB.GetAllUsers().Select(u => new UserModel
				{
					userID = u.userID,
					userFirstName = u.userFirstName,
					userLastName = u.userLastName,
					userNickName = u.userNickName,
					userPassword = u.userPassword,
					userEmail = u.userEmail,
					userGender = u.userGender,
					userBirthDate = u.userBirthDate,
					userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
					userLevel = u.userLevel
				}).ToList();
			}
		}

		public UserModel GetOneUserById(string id)
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.USERS.Where(u => u.userID.Equals(id)).Select(u => new UserModel
				{
					userID = u.userID,
					userFirstName = u.userFirstName,
					userLastName = u.userLastName,
					userNickName = u.userNickName,
					userPassword = u.userPassword,
					userEmail = u.userEmail,
					userGender = u.userGender,
					userBirthDate = u.userBirthDate,
					userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
					userLevel = u.userLevel
				}).SingleOrDefault();
			}
			else
			{
				return DB.GetOneUserById(id).Select(u => new UserModel
				{
					userID = u.userID,
					userFirstName = u.userFirstName,
					userLastName = u.userLastName,
					userNickName = u.userNickName,
					userPassword = u.userPassword,
					userEmail = u.userEmail,
					userGender = u.userGender,
					userBirthDate = u.userBirthDate,
					userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
					userLevel = u.userLevel
				}).SingleOrDefault();
			}
		}

		public UserModel GetOneUserForMessageById()
		{
			string id = HttpContext.Current.User.Identity.Name;

			if (GlobalVariable.queryType == 0)
			{
				return DB.USERS.Where(u => u.userID.Equals(id)).Select(u => new UserModel
				{
					userFirstName = u.userFirstName,
					userLastName = u.userLastName,
					userEmail = u.userEmail
				}).SingleOrDefault();
			}
			else
			{
				return DB.GetOneUserForMessageById(id).Select(u => new UserModel
				{
					userFirstName = u.userFirstName,
					userLastName = u.userLastName,
					userEmail = u.userEmail
				}).SingleOrDefault();
			}
		}

		public UserModel GetOneUserByName(string name)
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.USERS.Where(u => u.userNickName.Equals(name)).Select(u => new UserModel
				{
					userID = u.userID,
					userFirstName = u.userFirstName,
					userLastName = u.userLastName,
					userNickName = u.userNickName,
					userPassword = u.userPassword,
					userEmail = u.userEmail,
					userGender = u.userGender,
					userBirthDate = u.userBirthDate,
					userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
					userLevel = u.userLevel
				}).SingleOrDefault();
			}
			else
			{
				return DB.GetOneUserByName(name).Select(u => new UserModel
				{
					userID = u.userID,
					userFirstName = u.userFirstName,
					userLastName = u.userLastName,
					userNickName = u.userNickName,
					userPassword = u.userPassword,
					userEmail = u.userEmail,
					userGender = u.userGender,
					userBirthDate = u.userBirthDate,
					userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
					userLevel = u.userLevel
				}).SingleOrDefault();
			}
		}

		public UserModel GetOneUserByLogin(string userNickName, string userPassword)
		{
			if (!CheckStringFormat.IsBase64String(userPassword))
			{
				userPassword = ComputeHash.ComputeNewHash(userPassword);
			}

			if (GlobalVariable.queryType == 0)
			{
				return DB.USERS.Where(u => u.userNickName.Equals(userNickName)).Where(u => u.userPassword.Equals(userPassword)).Select(u => new UserModel
				{
					userID = u.userID,
					userFirstName = u.userFirstName,
					userLastName = u.userLastName,
					userNickName = u.userNickName,
					userPassword = u.userPassword,
					userEmail = u.userEmail,
					userGender = u.userGender,
					userBirthDate = u.userBirthDate,
					userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
					userLevel = u.userLevel
				}).SingleOrDefault();
			}
			else
			{
				return DB.GetOneUserByLogin(userNickName, userPassword).Select(u => new UserModel
				{
					userID = u.userID,
					userFirstName = u.userFirstName,
					userLastName = u.userLastName,
					userNickName = u.userNickName,
					userPassword = u.userPassword,
					userEmail = u.userEmail,
					userGender = u.userGender,
					userBirthDate = u.userBirthDate,
					userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
					userLevel = u.userLevel
				}).SingleOrDefault();
			}
		}

		public UserModel AddUser(UserModel userModel)
		{
			if (userModel.userLevel < 1)
			{
				userModel.userLevel = 1;
			}

			UserModel user;
			if (GlobalVariable.queryType == 0)
			{
				string userPassword2 = ComputeHash.ComputeNewHash(userModel.userPassword);
				USER user2 = new USER
				{
					userID = userModel.userID,
					userFirstName = userModel.userFirstName,
					userLastName = userModel.userLastName,
					userNickName = userModel.userNickName,
					userPassword = userPassword2,
					userEmail = userModel.userEmail,
					userGender = userModel.userGender,
					userBirthDate = userModel.userBirthDate,
					userPicture = userModel.userPicture,
					userLevel = userModel.userLevel,
				};
				DB.USERS.Add(user2);
				DB.SaveChanges();
				user = GetOneUserById(user2.userID);
			}
			else
			{
				user = DB.AddUser(userModel.userID, userModel.userFirstName, userModel.userLastName, userModel.userNickName, userModel.userBirthDate, userModel.userGender, userModel.userEmail, ComputeHash.ComputeNewHash(userModel.userPassword), userModel.userPicture, userModel.userLevel).Select(u => new UserModel
				{
					userID = u.userID,
					userFirstName = u.userFirstName,
					userLastName = u.userLastName,
					userNickName = u.userNickName,
					userPassword = u.userPassword,
					userEmail = u.userEmail,
					userGender = u.userGender,
					userBirthDate = u.userBirthDate,
					userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
					userLevel = u.userLevel
				}).SingleOrDefault();
			}

			if (ComputeHash.ComputeNewHash(userModel.userPassword).Equals(user.userPassword))
			{
				user.userPassword = userModel.userPassword;
			}

			return user;
		}

		public UserModel UpdateUser(UserModel userModel)
		{
			UserModel user;
			if (GlobalVariable.queryType == 0)
			{
				string userPassword2 = ComputeHash.ComputeNewHash(userModel.userPassword);
				USER user2 = DB.USERS.Where(u => u.userID.Equals(userModel.userID)).SingleOrDefault();
				if (user2 == null)
					return null;
				user2.userID = userModel.userID;
				user2.userFirstName = userModel.userFirstName;
				user2.userLastName = userModel.userLastName;
				user2.userNickName = userModel.userNickName;
				user2.userPassword = userPassword2;
				user2.userEmail = userModel.userEmail;
				user2.userGender = userModel.userGender;
				user2.userBirthDate = userModel.userBirthDate;
				user2.userPicture = userModel.userPicture;
				user2.userLevel = userModel.userLevel;
				DB.SaveChanges();
				user = GetOneUserById(user2.userID);
			}
			else
			{
				user = DB.UpdateUser(userModel.userID, userModel.userFirstName, userModel.userLastName, userModel.userNickName, userModel.userBirthDate, userModel.userGender, userModel.userEmail, ComputeHash.ComputeNewHash(userModel.userPassword), userModel.userPicture, userModel.userLevel).Select(u => new UserModel
				{
					userID = u.userID,
					userFirstName = u.userFirstName,
					userLastName = u.userLastName,
					userNickName = u.userNickName,
					userPassword = u.userPassword,
					userEmail = u.userEmail,
					userGender = u.userGender,
					userBirthDate = u.userBirthDate,
					userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
					userLevel = u.userLevel
				}).SingleOrDefault();
			}

			if (ComputeHash.ComputeNewHash(userModel.userPassword).Equals(user.userPassword))
			{
				user.userPassword = userModel.userPassword;
			}
			return user;
		}

		public int DeleteUser(string id)
		{
			if (GlobalVariable.queryType == 0)
			{
				USER user = DB.USERS.Where(u => u.userID.Equals(id)).SingleOrDefault();
				DB.USERS.Attach(user);
				if (user == null)
					return 0;
				DB.USERS.Remove(user);
				DB.SaveChanges();
				return 1;
			}
			else
			{
				return DB.DeleteUser(id);
			}
		}

		public UserModel UploadUserImage(string id, string img)
		{
			if (GlobalVariable.queryType == 0)
			{
				USER user = DB.USERS.Where(u => u.userID.Equals(id)).SingleOrDefault();
				if (user == null)
					return null;
				user.userPicture = img;
				DB.SaveChanges();
				return GetOneUserById(user.userID);
			}
			else
			{
				return DB.UploadUserImage(id, img).Select(u => new UserModel
				{
					userID = u.userID,
					userFirstName = u.userFirstName,
					userLastName = u.userLastName,
					userNickName = u.userNickName,
					userPassword = u.userPassword,
					userEmail = u.userEmail,
					userGender = u.userGender,
					userBirthDate = u.userBirthDate,
					userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
					userLevel = u.userLevel
				}).SingleOrDefault();
			}
		}

		public LoginModel ReturnUserByNameLevel(string username, int userLevel = 0)
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.USERS.Where(u => u.userNickName.Equals(username)).Where(u => u.userLevel == userLevel).Select(u => new LoginModel
				{
					userNickName = u.userNickName,
					userLevel = u.userLevel
				}).SingleOrDefault();
			}
			else
			{
				return DB.ReturnUserByNameLevel(username, userLevel).Select(u => new LoginModel
				{
					userNickName = u.userNickName,
					userLevel = u.userLevel
				}).SingleOrDefault();
			}
		}

		public bool IsNameTaken(string name)
		{
			if (GlobalVariable.queryType == 0)
				return DB.USERS.Any(u => u.userNickName.ToLower().Equals(name.ToLower()));
			else
			{
				if (DB.IsNameTaken(name).Equals(1))
					return true;
				else
					return false;
			}
		}

		public LoginModel ReturnUserByNamePassword(LoginModel checkUser)
		{
			checkUser.userPassword = ComputeHash.ComputeNewHash(checkUser.userPassword);

			if (GlobalVariable.queryType == 0)
			{
				return DB.USERS.Where(u => u.userNickName.Equals(checkUser.userNickName)).Where(u => u.userPassword.Equals(checkUser.userPassword)).Select(u => new LoginModel
				{
					userNickName = u.userNickName,
					userLevel = u.userLevel,
					userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
				}).SingleOrDefault();
			}
			else
			{
				return DB.ReturnUserByNamePassword(checkUser.userNickName, checkUser.userPassword).Select(u => new LoginModel
				{
					userNickName = u.userNickName,
					userLevel = u.userLevel,
					userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
				}).SingleOrDefault();
			}
		}
	}
}
