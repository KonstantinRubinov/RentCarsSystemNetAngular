using System.Net.Http;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace RentCars
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISearchService" in both code and config file together.
	[ServiceContract]
	public interface ISearchService
	{
		[OperationContract]
		[WebInvoke(Method = "GET", UriTemplate = "Search/all/", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage GetAllCarsAllData();

		[OperationContract]
		[WebInvoke(Method = "POST", UriTemplate = "Search/allByModel/", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage GetAllCarsBySearch(SearchModel searchModel);
		
		[OperationContract]
		[WebInvoke(Method = "GET", UriTemplate = "Search/?getCarByNumber={getCarByNumber}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage GetCarAllData(string getCarByNumber);

		[OperationContract]
		[WebInvoke(Method = "POST", UriTemplate = "Search/?carNumber={carNumber}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage IsAvaliableByNumber(string carNumber, SearchModel searchModel);
	}
}
