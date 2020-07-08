using System.Net.Http;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace RentCars
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBranchService" in both code and config file together.
	[ServiceContract]
	public interface IBranchService
	{
		[OperationContract]
		[WebInvoke(Method = "GET", UriTemplate = "Branches/namesIds/", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage GetAllBranchesNamesIds();

		[OperationContract]
		[WebInvoke(Method = "GET", UriTemplate = "Branches/all/", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage GetAllBranches();

		[OperationContract]
		[WebInvoke(Method = "GET", UriTemplate = "Branches/?getByID={getByID}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage GetOneBranch(int getByID);

		[OperationContract]
		[WebInvoke(Method = "POST", UriTemplate = "Branches/", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage AddBranch(BranchModel branchModel);

		[OperationContract]
		[WebInvoke(Method = "PUT", UriTemplate = "Branches/?updateByID={updateByID}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage UpdateBranch(int updateByID, BranchModel branchModel);

		[OperationContract]
		[WebInvoke(Method = "DELETE", UriTemplate = "Branches/?deleteByID={deleteByID}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage DeleteBranch(int deleteByID);
	}
}
