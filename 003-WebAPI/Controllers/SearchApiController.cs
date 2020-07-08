using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RentCars
{
	//[EnableCors("*", "*", "*")]
	[RoutePrefix("api")]
	public class SearchApiController : ApiController
    {
		private ISearchRepository searchRepository;
		private IFullCarDataRepository fullCarDataRepository;
		private IPriceRepository priceRepository;

		public SearchApiController(ISearchRepository _searchRepository, IFullCarDataRepository _fullCarDataRepository, IPriceRepository _priceRepository)
		{
			searchRepository = _searchRepository;
			fullCarDataRepository = _fullCarDataRepository;
			priceRepository = _priceRepository;
		}

		[HttpPost]
		[Route("search")]
		public HttpResponseMessage GetAllCarsBySearch(SearchModel searchModel)
		{
			var coll = Request.Headers;
			IEnumerable<string> headerValues = coll.GetValues("page");
			var page = headerValues.FirstOrDefault();
			headerValues = coll.GetValues("carsNum");
			var carsNum = headerValues.FirstOrDefault();

			try
			{
				SearchReturnModel searchReturnModel = searchRepository.GetAllCarsBySearch(searchModel, int.Parse(page), int.Parse(carsNum));
				return Request.CreateResponse(HttpStatusCode.OK, searchReturnModel);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[HttpPost]
		[Route("search/{carnumber}")]
		public HttpResponseMessage IsAvaliableByNumber(string carnumber, SearchModel searchModel)
		{
			try
			{
				bool isAvaliable = priceRepository.CheckIfCarAvaliable(carnumber, (DateTime)searchModel.fromDate, (DateTime)searchModel.toDate);
				return Request.CreateResponse(HttpStatusCode.OK, isAvaliable);

			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}


		[HttpGet]
		[Route("fullCarData")]
		public HttpResponseMessage GetAllCarsAllData()
		{
			var coll = Request.Headers;
			IEnumerable<string> headerValues = coll.GetValues("page");
			var page = headerValues.FirstOrDefault();
			headerValues = coll.GetValues("carsNum");
			var carsNum = headerValues.FirstOrDefault();

			try
			{
				SearchReturnModel searchReturnModel = searchRepository.GetAllCarsBySearch(new SearchModel(), int.Parse(page), int.Parse(carsNum));
				return Request.CreateResponse(HttpStatusCode.OK, searchReturnModel);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[HttpGet]
		[Route("fullCarData/{number}")]
		public HttpResponseMessage GetCarAllData(string number)
		{
			try
			{
				FullCarDataModel oneFullCarDataModel = fullCarDataRepository.GetCarAllData(number);
				return Request.CreateResponse(HttpStatusCode.OK, oneFullCarDataModel);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}
	}
}
