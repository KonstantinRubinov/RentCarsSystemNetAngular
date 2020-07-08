using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Web;

namespace RentCars
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SearchService" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select SearchService.svc or SearchService.svc.cs at the Solution Explorer and start debugging.
	public class SearchService : ISearchService
	{
		private ISearchRepository searchRepository;
		private IFullCarDataRepository fullDataRepository;
		private IPriceRepository priceRepository;

		public SearchService()
		{
			if (GlobalVariable.logicType == 0)
			{
				searchRepository = new EntitySearchManager();
				fullDataRepository = new EntityFullCarDataManager();
				priceRepository = new EntityPriceManager();
			}
			else if (GlobalVariable.logicType == 1)
			{
				searchRepository = new SqlSearchManager();
				fullDataRepository = new SqlFullCarDataManager();
				priceRepository = new SqlPriceManager();
			}
			else if (GlobalVariable.logicType == 2)
			{
				searchRepository = new MySqlSearchManager();
				fullDataRepository = new MySqlFullCarDataManager();
				priceRepository = new MySqlPriceManager();
			}
			else
			{
				searchRepository = new MongoSearchManager();
				fullDataRepository = new MongoFullCarDataManager();
				priceRepository = new MongoPriceManager();
			}
		}

		public HttpResponseMessage GetAllCarsAllData()
		{
			try
			{
				IncomingWebRequestContext woc = WebOperationContext.Current.IncomingRequest;
				var page = woc.Headers["page"];
				var carsNum = woc.Headers["carsNum"];

				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent(JsonConvert.SerializeObject(searchRepository.GetAllCarsBySearch(new SearchModel(), int.Parse(page), int.Parse(carsNum))))
				};
				return hrm;
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				HttpResponseMessage hr = new HttpResponseMessage(HttpStatusCode.InternalServerError)
				{
					Content = new StringContent(errors.ToString())
				};
				return hr;
			}
		}

		public HttpResponseMessage GetAllCarsBySearch(SearchModel searchModel)
		{
			try
			{
				IncomingWebRequestContext woc = WebOperationContext.Current.IncomingRequest;
				var page = woc.Headers["page"];
				var carsNum = woc.Headers["carsNum"];

				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent(JsonConvert.SerializeObject(searchRepository.GetAllCarsBySearch(searchModel, int.Parse(page), int.Parse(carsNum))))
				};
				return hrm;
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				HttpResponseMessage hr = new HttpResponseMessage(HttpStatusCode.InternalServerError)
				{
					Content = new StringContent(errors.ToString())
				};
				return hr;
			}
		}

		public HttpResponseMessage GetCarAllData(string getCarByNumber)
		{
			try
			{
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent(JsonConvert.SerializeObject(fullDataRepository.GetCarAllData(getCarByNumber)))
				};
				return hrm;
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				HttpResponseMessage hr = new HttpResponseMessage(HttpStatusCode.InternalServerError)
				{
					Content = new StringContent(errors.ToString())
				};
				return hr;
			}
		}

		public HttpResponseMessage IsAvaliableByNumber(string carNumber, SearchModel searchModel)
		{
			try
			{
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent(JsonConvert.SerializeObject(priceRepository.CheckIfCarAvaliable(carNumber, (DateTime)searchModel.fromDate, (DateTime)searchModel.toDate)))
				};
				return hrm;
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				HttpResponseMessage hr = new HttpResponseMessage(HttpStatusCode.InternalServerError)
				{
					Content = new StringContent(errors.ToString())
				};
				return hr;
			}
		}
	}
}
