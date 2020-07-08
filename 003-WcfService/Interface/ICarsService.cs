using System.Net.Http;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace RentCars
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICarsService" in both code and config file together.
	[ServiceContract]
	public interface ICarsService
	{
		[OperationContract]
		[WebInvoke(Method = "GET", UriTemplate = "Cars/all/", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage GetAllCars();

		[OperationContract]
		[WebInvoke(Method = "GET", UriTemplate = "Cars/?getByNumber={getByNumber}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage GetOneCar(string getByNumber);

		[OperationContract]
		[WebInvoke(Method = "POST", UriTemplate = "Cars/", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage AddCar(CarModel carModel);

		[OperationContract]
		[WebInvoke(Method = "PUT", UriTemplate = "Cars/?updateByNumber={updateByNumber}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage UpdateCar(string updateByNumber, CarModel carModel);

		[OperationContract]
		[WebInvoke(Method = "DELETE", UriTemplate = "Branches/?deleteByNumber={deleteByNumber}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage DeleteCar(string deleteByNumber);

		[OperationContract]
		[WebInvoke(Method = "POST", UriTemplate = "Cars/image/?carNumber={carNumber}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage UploadCarImage(string carNumber, string carImage);

		[OperationContract]
		[WebInvoke(Method = "GET", UriTemplate = "Cars/imagesAndNumbers/", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage GetAllCarImagesAndNumberOfCars();
	}
}
