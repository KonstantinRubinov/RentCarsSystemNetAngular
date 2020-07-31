using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace RentCars
{
	//[EnableCors("*", "*", "*")]
	[RoutePrefix("api")]
	public class UsersApiController : ApiController
	{
		private IUsersRepository usersRepository;
		public UsersApiController(IUsersRepository _usersRepository)
		{
			usersRepository = _usersRepository;
		}

		[Authorize]
		//[AutorizeByRole(AutorizeByRoleAttribute.roles.Admin)]
		[HttpGet]
		[Route("users")]
		public HttpResponseMessage GetAllUsers()
		{
			try
			{
				List<UserModel> allUsers = usersRepository.GetAllUsers();
				return Request.CreateResponse(HttpStatusCode.OK, allUsers);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[Authorize]
		[AutorizeByRole(AutorizeByRoleAttribute.roles.Admin)]
		[HttpGet]
		[Route("users/{id}")]
		public HttpResponseMessage GetOneUser(string id)
		{
			try
			{
				UserModel oneUser = usersRepository.GetOneUserById(id);
				return Request.CreateResponse(HttpStatusCode.OK, oneUser);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[HttpPost]
		[Route("users/check")]
		public HttpResponseMessage ReturnUserByNamePassword(LoginModel loginModel)
		{
			try
			{
				if (loginModel == null)
				{
					return Request.CreateResponse(HttpStatusCode.BadRequest, "Data is null.");
				}
				if (!ModelState.IsValid)
				{
					Errors errors = ErrorsHelper.GetErrors(ModelState);
					return Request.CreateResponse(HttpStatusCode.BadRequest, errors);
				}

				LoginModel checkUser = usersRepository.ReturnUserByNamePassword(loginModel);
				if (checkUser==null)
				{
					Errors errors = ErrorsHelper.GetErrors("Wrong username or password");
					return Request.CreateResponse(HttpStatusCode.Unauthorized, errors);
				}
				return Request.CreateResponse(HttpStatusCode.Created, checkUser);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[HttpPost]
		[Route("users")]
		public HttpResponseMessage AddUser(UserModel userModel)
		{
			try
			{
				if (userModel == null)
				{
					return Request.CreateResponse(HttpStatusCode.BadRequest, "Data is null.");
				}
				if (!ModelState.IsValid)
				{
					Errors errors = ErrorsHelper.GetErrors(ModelState);
					return Request.CreateResponse(HttpStatusCode.BadRequest, errors);
				}
				
				byte[] bytes = Convert.FromBase64String(userModel.userImage);
				string[] extensions = userModel.userPicture.Split('.');
				string extension = extensions[extensions.Length-1];
				string fileName = Guid.NewGuid().ToString();
				string filePath = HttpContext.Current.Server.MapPath("~/assets/images/users/" + fileName + "." + extension);
				using (FileStream binaryFileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
				{
					binaryFileStream.Write(bytes, 0, bytes.Length);
					userModel.userPicture = fileName + "." + extension;
				}
				userModel.userImage = string.Empty;
				UserModel addedUser = usersRepository.AddUser(userModel);
				return Request.CreateResponse(HttpStatusCode.Created, addedUser);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[Authorize]
		[AutorizeByRole(AutorizeByRoleAttribute.roles.Admin)]
		[HttpPut]
		[Route("users/{id}")]
		public HttpResponseMessage UpdateUser(string id, UserModel userModel)
		{
			try
			{
				if (userModel == null)
				{
					return Request.CreateResponse(HttpStatusCode.BadRequest, "Data is null.");
				}
				if (!ModelState.IsValid)
				{
					Errors errors = ErrorsHelper.GetErrors(ModelState);
					return Request.CreateResponse(HttpStatusCode.BadRequest, errors);
				}

				userModel.userID = id;
				UserModel updatedUser = usersRepository.UpdateUser(userModel);
				return Request.CreateResponse(HttpStatusCode.OK, updatedUser);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[Authorize]
		[AutorizeByRole(AutorizeByRoleAttribute.roles.Admin)]
		[HttpDelete]
		[Route("users/{id}")]
		public HttpResponseMessage DeleteUser(string id)
		{
			try
			{
				int i = usersRepository.DeleteUser(id);
				return Request.CreateResponse(HttpStatusCode.NoContent);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}




		//[HttpPost]
		//[Route("products/file/{id}")]
		//public HttpResponseMessage UploadUserImage(string id)
		//{
		//	try
		//	{
		//		UserModel updloadedUser;
		//		string fileName = Guid.NewGuid() + ".png";
		//		string filePath = HttpContext.Current.Server.MapPath("~/assets/images/users/" + fileName);
		//		using (var fs = new FileStream(filePath, FileMode.Create))
		//		{
		//			HttpContext.Current.Request.InputStream.CopyTo(fs);
		//			updloadedUser = usersRepository.UploadUserImage(id, fileName);
		//			updloadedUser.userPicture = "/assets/images/cars/" + fileName;
		//		}
		//		if (updloadedUser != null)
		//		{
		//			return Request.CreateResponse(HttpStatusCode.OK, updloadedUser);
		//		}
		//		return Request.CreateResponse(HttpStatusCode.ExpectationFailed);

		//	}
		//	catch (Exception ex)
		//	{
		//		Errors errors = ErrorsHelper.GetErrors(ex);
		//		return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
		//	}
		//}
	}
}
