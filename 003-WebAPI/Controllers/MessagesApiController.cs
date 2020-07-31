using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RentCars
{
	//[EnableCors("*", "*", "*")]
	[RoutePrefix("api")]
	public class MessagesApiController : ApiController
    {
		private IMessagesRepository messagesRepository;
		private IUsersRepository usersRepository;

		public MessagesApiController(IMessagesRepository _messagesRepository, IUsersRepository _usersRepository)
		{
			messagesRepository= _messagesRepository;
			usersRepository= _usersRepository;
		}

		[HttpGet]
		[Route("messages")]
		public HttpResponseMessage GetAllMessages()
		{
			try
			{
				List<MessageModel> messages = messagesRepository.GetAllMessages();
				return Request.CreateResponse(HttpStatusCode.OK, messages);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[HttpGet]
		[Route("messages/{userId}")]
		public HttpResponseMessage GetMessagesByUserId(string userId)
		{
			try
			{
				List<MessageModel> messages = messagesRepository.GetMessagesByUserId(userId);
				return Request.CreateResponse(HttpStatusCode.OK, messages);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[HttpGet]
		[Route("messages/{messageId}")]
		public HttpResponseMessage GetOneMessageById(int messageId)
		{
			try
			{
				MessageModel message = messagesRepository.GetOneMessageById(messageId);
				return Request.CreateResponse(HttpStatusCode.OK, message);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}


		[HttpPost]
		[Route("messages")]
		public HttpResponseMessage AddMessage(MessageModel messageModel)
		{
			try
			{
				if (messageModel == null)
				{
					return Request.CreateResponse(HttpStatusCode.BadRequest, "Data is null.");
				}
				if (!ModelState.IsValid)
				{
					Errors errors = ErrorsHelper.GetErrors(ModelState);
					return Request.CreateResponse(HttpStatusCode.BadRequest, errors);
				}

				MessageModel message = messagesRepository.AddMessage(messageModel);
				return Request.CreateResponse(HttpStatusCode.Created, message);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}


		[HttpPut]
		[Route("messages/{messageID}")]
		public HttpResponseMessage UpdateMessage(int messageID, MessageModel messageModel)
		{
			try
			{
				if (messageModel == null)
				{
					return Request.CreateResponse(HttpStatusCode.BadRequest, "Data is null.");
				}
				if (!ModelState.IsValid)
				{
					Errors errors = ErrorsHelper.GetErrors(ModelState);
					return Request.CreateResponse(HttpStatusCode.BadRequest, errors);
				}
				messageModel.messageID = messageID;
				MessageModel message = messagesRepository.UpdateMessage(messageModel);
				return Request.CreateResponse(HttpStatusCode.OK, message);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[HttpDelete]
		[Route("messages/{messageId}")]
		public HttpResponseMessage DeleteMessage(int messageId)
		{
			try
			{
				int i = messagesRepository.DeleteMessage(messageId);
				return Request.CreateResponse(HttpStatusCode.NoContent);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[HttpDelete]
		[Route("messages/{userId}")]
		public HttpResponseMessage DeleteMessageByUser(string userId)
		{
			try
			{
				int i = messagesRepository.DeleteMessageByUser(userId);
				return Request.CreateResponse(HttpStatusCode.NoContent);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}



		[Authorize]
		[HttpGet]
		[Route("messages/userForMessage")]
		public HttpResponseMessage GetUserForMessage()
		{
			try
			{
				UserModel oneUser = usersRepository.GetOneUserForMessageById();
				return Request.CreateResponse(HttpStatusCode.OK, oneUser);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}
	}
}
