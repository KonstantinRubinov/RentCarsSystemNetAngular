using System.Net.Http;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace RentCars
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICarForRentService" in both code and config file together.
	[ServiceContract]
	public interface ICarForRentService
	{

		[OperationContract]
		[WebInvoke(Method = "GET", UriTemplate = "CarsForRent/all/", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage GetAllCarsForRent();

		[OperationContract]
		[WebInvoke(Method = "GET", UriTemplate = "CarsForRent/?getByNumber={getByNumber}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage GetCarsForRentByCarNumber(string getByNumber);

		[OperationContract]
		[WebInvoke(Method = "GET", UriTemplate = "CarsForRent/?getByUserId={getByUserId}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage GetCarsForRentByUserId(string getByUserId);

		[OperationContract]
		[WebInvoke(Method = "GET", UriTemplate = "CarsForRent/?getByRentNumber={getByRentNumber}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage GetOneCarForRentByRentNumber(int getByRentNumber);

		[OperationContract]
		[WebInvoke(Method = "POST", UriTemplate = "CarsForRent/?userName={userName}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage AddCarForRent(CarForRentModel carForRentModel, string userName);

		[OperationContract]
		[WebInvoke(Method = "PUT", UriTemplate = "CarsForRent/?updateByRentNumber={updateByRentNumber}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage UpdateCarForRent(int updateByRentNumber, CarForRentModel carForRentModel);

		[OperationContract]
		[WebInvoke(Method = "DELETE", UriTemplate = "CarsForRent/?deleteByRentNumber={deleteByRentNumber}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage DeleteCarForRent(int deleteByRentNumber);

		[OperationContract]
		[WebInvoke(Method = "DELETE", UriTemplate = "CarsForRent/?deleteByCarNumber={deleteByCarNumber}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage DeleteCarForRent(string deleteByCarNumber);
	}
}
