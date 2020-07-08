using System.Net.Http;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace RentCars
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMessageService" in both code and config file together.
	[ServiceContract]
	public interface IMessageService
	{
		[OperationContract]
		[WebInvoke(Method = "GET", UriTemplate = "messages/all/", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage GetAllMessages();

		[OperationContract]
		[WebInvoke(Method = "GET", UriTemplate = "messages/?getByUserID={getByUserID}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage GetMessagesByUserId(string getByUserID);

		[OperationContract]
		[WebInvoke(Method = "GET", UriTemplate = "messages/?getByID={getByID}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage GetOneMessageById(int getByID);

		[OperationContract]
		[WebInvoke(Method = "POST", UriTemplate = "messages/", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage AddMessage(MessageModel messageModel);

		[OperationContract]
		[WebInvoke(Method = "PUT", UriTemplate = "messages/?messageID={messageID}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage UpdateMessage(int messageID, MessageModel messageModel);

		[OperationContract]
		[WebInvoke(Method = "DELETE", UriTemplate = "messages/?deleteByMessageID={deleteByMessageID}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage DeleteMessage(int deleteByMessageID);

		[OperationContract]
		[WebInvoke(Method = "DELETE", UriTemplate = "messages/?deleteByUserID={deleteByUserID}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage DeleteMessageByUser(string deleteByUserID);
	}
}
