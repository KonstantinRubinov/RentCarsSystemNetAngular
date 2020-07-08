using RentCars.EntityDataBase;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentCars
{
	public class EntityMessagesManager : EntityBaseManager, IMessagesRepository
	{
		public List<MessageModel> GetAllMessages()
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.MESSAGES.Select(m => new MessageModel
				{
					messageID = m.messageID,
					userID = m.userID,
					userFirstName = m.userFirstName,
					userLastName = m.userLastName,
					userEmail = m.userEmail,
					userMessage = m.userMessage
				}).ToList();
			}
			else
			{
				return DB.GetAllMessages().Select(m => new MessageModel
				{
					messageID = m.messageID,
					userID = m.userID,
					userFirstName = m.userFirstName,
					userLastName = m.userLastName,
					userEmail = m.userEmail,
					userMessage = m.userMessage
				}).ToList();
			}
		}

		public List<MessageModel> GetMessagesByUserId(string userId)
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.MESSAGES.Where(m => m.userID.Equals(userId)).Select(m => new MessageModel
				{
					messageID = m.messageID,
					userID = m.userID,
					userFirstName = m.userFirstName,
					userLastName = m.userLastName,
					userEmail = m.userEmail,
					userMessage = m.userMessage
				}).ToList();
			}
			else
			{
				return DB.GetMessagesByUserId(userId).Select(m => new MessageModel
				{
					messageID = m.messageID,
					userID = m.userID,
					userFirstName = m.userFirstName,
					userLastName = m.userLastName,
					userEmail = m.userEmail,
					userMessage = m.userMessage
				}).ToList();
			}
		}

		public MessageModel GetOneMessageById(int messageId)
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.MESSAGES.Where(m => m.messageID == messageId).Select(m => new MessageModel
				{
					messageID = m.messageID,
					userID = m.userID,
					userFirstName = m.userFirstName,
					userLastName = m.userLastName,
					userEmail = m.userEmail,
					userMessage = m.userMessage
				}).SingleOrDefault();
			}
			else
			{
				return DB.GetOneMessageById(messageId).Select(m => new MessageModel
				{
					messageID = m.messageID,
					userID = m.userID,
					userFirstName = m.userFirstName,
					userLastName = m.userLastName,
					userEmail = m.userEmail,
					userMessage = m.userMessage
				}).SingleOrDefault();
			}
		}

		public MessageModel AddMessage(MessageModel messageModel)
		{
			messageModel.userID = HttpContext.Current.User.Identity.Name;

			if (GlobalVariable.queryType == 0)
			{
				MESSAGE message = new MESSAGE
				{
					messageID = messageModel.messageID,
					userID = messageModel.userID,
					userFirstName = messageModel.userFirstName,
					userLastName = messageModel.userLastName,
					userEmail = messageModel.userEmail,
					userMessage = messageModel.userMessage
				};
				DB.MESSAGES.Add(message);
				DB.SaveChanges();
				return GetOneMessageById(message.messageID);
			}
			else
			{
				return DB.AddMessage(messageModel.userID, messageModel.userFirstName, messageModel.userLastName, messageModel.userEmail, messageModel.userMessage).Select(m => new MessageModel
				{
					messageID = m.messageID,
					userID = m.userID,
					userFirstName = m.userFirstName,
					userLastName = m.userLastName,
					userEmail = m.userEmail,
					userMessage = m.userMessage
				}).SingleOrDefault();
			}
		}

		public MessageModel UpdateMessage(MessageModel messageModel)
		{
			messageModel.userID = HttpContext.Current.User.Identity.Name;

			if (GlobalVariable.queryType == 0)
			{
				MESSAGE message = DB.MESSAGES.Where(m => m.messageID == messageModel.messageID).SingleOrDefault();
				if (message == null)
					return null;
				message.messageID = messageModel.messageID;
				message.userID = messageModel.userID;
				message.userFirstName = messageModel.userFirstName;
				message.userLastName = messageModel.userLastName;
				message.userEmail = messageModel.userEmail;
				message.userMessage = messageModel.userMessage;
				DB.SaveChanges();
				return GetOneMessageById(messageModel.messageID);
			}
			else
			{
				return DB.UpdateMessage(messageModel.messageID, messageModel.userID, messageModel.userFirstName, messageModel.userLastName, messageModel.userEmail, messageModel.userMessage).Select(m => new MessageModel
				{
					messageID = m.messageID,
					userID = m.userID,
					userFirstName = m.userFirstName,
					userLastName = m.userLastName,
					userEmail = m.userEmail,
					userMessage = m.userMessage
				}).SingleOrDefault();
			}
		}

		public int DeleteMessage(int messageId)
		{
			if (GlobalVariable.queryType == 0)
			{
				MESSAGE message = DB.MESSAGES.Where(m => m.messageID == messageId).SingleOrDefault();
				DB.MESSAGES.Attach(message);
				if (message == null)
					return 0;
				DB.MESSAGES.Remove(message);
				DB.SaveChanges();
				return 1;
			}
			else
			{
				return DB.DeleteMessage(messageId);
			}
		}

		public int DeleteMessageByUser(string userId)
		{
			if (GlobalVariable.queryType == 0)
			{
				MESSAGE message = DB.MESSAGES.Where(m => m.userID.Equals(userId)).SingleOrDefault();
				DB.MESSAGES.Attach(message);
				if (message == null)
					return 0;
				DB.MESSAGES.Remove(message);
				DB.SaveChanges();
				return 1;
			}
			else
			{
				return DB.DeleteMessageByUser(userId);
			}
		}
	}
}
