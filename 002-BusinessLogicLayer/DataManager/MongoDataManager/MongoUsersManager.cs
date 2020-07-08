using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentCars
{
	public class MongoUsersManager : IUsersRepository
	{
		private readonly IMongoCollection<UserModel> _users;

		public MongoUsersManager()
		{
			var client = new MongoClient(ConnectionStrings.ConnectionString);
			var database = client.GetDatabase(ConnectionStrings.DatabaseName);

			_users = database.GetCollection<UserModel>(ConnectionStrings.UsersCollectionName);
		}

		public List<UserModel> GetAllUsers()
		{
			return _users.Find(user => true).Project(u => new UserModel
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

		public UserModel GetOneUserById(string id)
		{
			return _users.Find(Builders<UserModel>.Filter.Eq(user => user.userID, id)).Project(u => new UserModel
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

		public UserModel GetOneUserByName(string name)
		{
			return _users.Find(Builders<UserModel>.Filter.Eq(user => user.userNickName, name)).Project(u => new UserModel
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

		public UserModel GetOneUserByLogin(string userNickName, string userPassword)
		{
			if (!CheckStringFormat.IsBase64String(userPassword))
			{
				userPassword = ComputeHash.ComputeNewHash(userPassword);
			}

			return (from user in _users.AsQueryable()
					where user.userNickName.Equals(userNickName)
					where user.userPassword.Equals(userPassword)
					select new UserModel
					{
						userID = user.userID,
						userFirstName = user.userFirstName,
						userLastName = user.userLastName,
						userNickName = user.userNickName,
						userPassword = user.userPassword,
						userEmail = user.userEmail,
						userGender = user.userGender,
						userBirthDate = user.userBirthDate,
						userPicture = user.userPicture != null ? "/assets/images/users/" + user.userPicture : null,
						userLevel = user.userLevel
					}).SingleOrDefault();
		}

		public UserModel AddUser(UserModel userModel)
		{
			if (userModel.userLevel < 1)
			{
				userModel.userLevel = 1;
			}

			_users.InsertOne(userModel);
			UserModel user = GetOneUserByLogin(userModel.userNickName, userModel.userPassword);

			if (ComputeHash.ComputeNewHash(userModel.userPassword).Equals(user.userPassword))
			{
				user.userPassword = userModel.userPassword;
			}

			return user;
		}

		public UserModel UpdateUser(UserModel userModel)
		{
			_users.ReplaceOne(u => u.userID.Equals(userModel.userID), userModel);
			UserModel user = GetOneUserByLogin(userModel.userNickName, userModel.userPassword);

			if (ComputeHash.ComputeNewHash(userModel.userPassword).Equals(user.userPassword))
			{
				user.userPassword = userModel.userPassword;
			}
			return user;
		}

		public int DeleteUser(string id)
		{
			_users.DeleteOne(user => user.userID.Equals(id));

			return 1;
		}

		public UserModel UploadUserImage(string id, string img)
		{
			UserModel tmpUserModel = GetOneUserById(id);
			tmpUserModel.userImage = img;

			_users.ReplaceOne(user => user.userID.Equals(id), tmpUserModel);
			tmpUserModel = GetOneUserById(id);

			return tmpUserModel;
		}

		public LoginModel ReturnUserByNameLevel(string username, int userLevel = 0)
		{
			return (from user in _users.AsQueryable()
					where user.userNickName.Equals(username)
					where user.userLevel.Equals(userLevel)
					select new LoginModel
					{
						userNickName = user.userNickName,
						userLevel = user.userLevel
					}).SingleOrDefault();
		}

		public bool IsNameTaken(string name)
		{
			if (name.Equals(string.Empty) || name.Equals(""))
				throw new ArgumentOutOfRangeException();

			return _users.Find<UserModel>(user => user.userNickName.Equals(name)).Any();
		}

		public LoginModel ReturnUserByNamePassword(LoginModel checkUser)
		{
			checkUser.userPassword = ComputeHash.ComputeNewHash(checkUser.userPassword);

			return (from user in _users.AsQueryable()
					where user.userNickName.Equals(checkUser.userNickName)
					where user.userPassword.Equals(checkUser.userPassword)
					select new LoginModel
					{
						userNickName = user.userNickName,
						userLevel = user.userLevel,
						userPicture = user.userPicture != null ? "/assets/images/users/" + user.userPicture : null
					}).SingleOrDefault();
		}


		public UserModel GetOneUserForMessageById()
		{
			string id = HttpContext.Current.User.Identity.Name;

			return _users.Find(Builders<UserModel>.Filter.Eq(user => user.userID, id)).Project(u => new UserModel
			{
				userFirstName = u.userFirstName,
				userLastName = u.userLastName,
				userEmail = u.userEmail
			}).SingleOrDefault();
		}
	}
}
