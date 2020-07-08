﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;

namespace RentCars
{
	public class MySqlUsersManager : MySqlDataBase, IUsersRepository
	{
		public List<UserModel> GetAllUsers()
		{
			DataTable dt = new DataTable();
			List<UserModel> arrUser = new List<UserModel>();
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(UserStringsMySql.GetAllUsers());
			}
			foreach (DataRow ms in dt.Rows)
			{
				arrUser.Add(UserModel.ToObject(ms));
			}

			return arrUser;
		}

		public UserModel GetOneUserById(string id)
		{
			if (id.Equals(""))
				throw new ArgumentOutOfRangeException();
			DataTable dt = new DataTable();
			UserModel userModel = new UserModel();
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(UserStringsMySql.GetOneUserById(id));
			}
			foreach (DataRow ms in dt.Rows)
			{
				userModel = UserModel.ToObject(ms);
			}

			return userModel;
		}

		public UserModel GetOneUserForMessageById()
		{
			string id = HttpContext.Current.User.Identity.Name;
			DataTable dt = new DataTable();
			UserModel userModel = new UserModel();
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(UserStringsMySql.GetOneUserForMessageById(id));
			}
			foreach (DataRow ms in dt.Rows)
			{
				userModel = UserModel.ToObjectMessage(ms);
			}

			return userModel;
		}



		public UserModel GetOneUserByName(string name)
		{
			if (name.Equals(""))
				throw new ArgumentOutOfRangeException();
			DataTable dt = new DataTable();
			UserModel userModel = new UserModel();
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(UserStringsMySql.GetOneUserByName(name));
			}
			foreach (DataRow ms in dt.Rows)
			{
				userModel = UserModel.ToObject(ms);
			}

			return userModel;
		}

		public UserModel GetOneUserByLogin(string userNickName, string userPassword)
		{
			if (userNickName.Equals(""))
				throw new ArgumentOutOfRangeException();
			if (userPassword.Equals(""))
				throw new ArgumentOutOfRangeException();

			if (!CheckStringFormat.IsBase64String(userPassword))
			{
				userPassword = ComputeHash.ComputeNewHash(userPassword);
			}
			DataTable dt = new DataTable();
			UserModel userModel = new UserModel();
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(UserStringsMySql.GetOneUserByLogin(userNickName, userPassword));
			}
			foreach (DataRow ms in dt.Rows)
			{
				userModel = UserModel.ToObject(ms);
			}

			return userModel;
		}


		public UserModel AddUser(UserModel userModel)
		{
			string orPass = userModel.userPassword;
			if (userModel.userLevel < 1)
			{
				userModel.userLevel = 1;
			}
			userModel.userPassword = ComputeHash.ComputeNewHash(userModel.userPassword);
			DataTable dt = new DataTable();
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(UserStringsMySql.AddUser(userModel));
			}
			foreach (DataRow ms in dt.Rows)
			{
				userModel = UserModel.ToObject(ms);
			}

			if (ComputeHash.ComputeNewHash(orPass).Equals(userModel.userPassword))
			{
				userModel.userPassword = orPass;
			}

			return userModel;
		}

		public UserModel UpdateUser(UserModel userModel)
		{
			string orPass = userModel.userPassword;
			DataTable dt = new DataTable();
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(UserStringsMySql.UpdateUser(userModel));
			}
			foreach (DataRow ms in dt.Rows)
			{
				userModel = UserModel.ToObject(ms);
			}

			if (ComputeHash.ComputeNewHash(orPass).Equals(userModel.userPassword))
			{
				userModel.userPassword = orPass;
			}

			return userModel;
		}

		public int DeleteUser(string id)
		{
			int i = 0;
			using (MySqlCommand command = new MySqlCommand())
			{
				i = ExecuteNonQuery(UserStringsMySql.DeleteUser(id));
			}
			return i;
		}

		public LoginModel ReturnUserByNamePassword(LoginModel checkUser)
		{
			checkUser.userPassword = ComputeHash.ComputeNewHash(checkUser.userPassword);
			if (checkUser==null)
				throw new ArgumentOutOfRangeException();
			DataTable dt = new DataTable();
			LoginModel loginModel = new LoginModel();
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(UserStringsMySql.ReturnUserByNamePassword(checkUser));
			}
			foreach (DataRow ms in dt.Rows)
			{
				loginModel = LoginModel.ToObject(ms);
			}

			return loginModel;
		}


		public LoginModel ReturnUserByNameLevel(string username, int userLevel = 0)
		{
			if (username == null || username.Equals(""))
				throw new ArgumentOutOfRangeException();
			DataTable dt = new DataTable();
			LoginModel loginModel = new LoginModel();
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(UserStringsMySql.ReturnUserByNameLevel(username, userLevel));
			}
			foreach (DataRow ms in dt.Rows)
			{
				loginModel = LoginModel.ToObjectNameLevel(ms);
			}

			return loginModel;
		}

		public bool IsNameTaken(string name)
		{
			if (name.Equals(""))
				throw new ArgumentOutOfRangeException();
			DataTable dt = new DataTable();
			int taken=0;
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(UserStringsMySql.IsNameTaken(name));
			}
			foreach (DataRow ms in dt.Rows)
			{
				taken = int.Parse(ms[0].ToString());
			}

			if (taken == 0) return false;
			else return true;
		}

		public UserModel UploadUserImage(string id, string img)
		{
			DataTable dt = new DataTable();
			UserModel userModel = new UserModel();
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(UserStringsMySql.UploadUserImage(id, img));
			}
			foreach (DataRow ms in dt.Rows)
			{
				userModel = UserModel.ToObject(ms);
			}

			return userModel;
		}
	}
}
