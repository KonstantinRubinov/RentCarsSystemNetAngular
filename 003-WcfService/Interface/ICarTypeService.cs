using System.Net.Http;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace RentCars
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICarTypeService" in both code and config file together.
	[ServiceContract]
	public interface ICarTypeService
	{
		[OperationContract]
		[WebInvoke(Method = "GET", UriTemplate = "CarTypes/all", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage GetAllCarTypes();

		[OperationContract]
		[WebInvoke(Method = "GET", UriTemplate = "CarTypes/?getById={getById}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage GetOneCarType(int getById);

		[OperationContract]
		[WebInvoke(Method = "POST", UriTemplate = "CarTypes/", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage AddCarType(CarTypeModel carTypeModel);

		[OperationContract]
		[WebInvoke(Method = "PUT", UriTemplate = "CarTypes/?typeId={typeId}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage UpdateCarType(int typeId, CarTypeModel carTypeModel);

		[OperationContract]
		[WebInvoke(Method = "DELETE", UriTemplate = "CarTypes/?deleteByType={deleteByType}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage DeleteCarType(string deleteByType);

		[OperationContract]
		[WebInvoke(Method = "DELETE", UriTemplate = "CarTypes/?deleteById={deleteById}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage DeleteCarType(int deleteById);
	}
}
