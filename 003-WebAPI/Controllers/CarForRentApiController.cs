using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RentCars
{
	//[EnableCors("*", "*", "*")]
	[RoutePrefix("api")]
	public class CarForRentApiController : ApiController
    {
		private ICarForRentRepository carForRentRepository;
		private IPriceRepository priceRepository;

		public CarForRentApiController(ICarForRentRepository _carForRentRepository, IPriceRepository _priceRepository)
		{
			carForRentRepository = _carForRentRepository;
			priceRepository = _priceRepository;
		}

		[Authorize]
		[AutorizeByRole(AutorizeByRoleAttribute.roles.Admin)]
		[HttpGet]
		[Route("CarsForRent")]
		public HttpResponseMessage GetAllCarsForRent()
		{
			try
			{
				List<CarForRentModel> allCarsForRent = carForRentRepository.GetAllCarsForRent();
				return Request.CreateResponse(HttpStatusCode.OK, allCarsForRent);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[Authorize]
		//[AutorizeByRole(AutorizeByRoleAttribute.roles.Admin)]
		[HttpGet]
		[Route("CarsForRent/{car}")]
		public HttpResponseMessage GetRentByCar(string car)
		{
			try
			{
				List<CarForRentModel> allCarsForRent = carForRentRepository.GetCarsForRentByCarNumber(car);
				return Request.CreateResponse(HttpStatusCode.OK, allCarsForRent);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[Authorize]
		//[AutorizeByRole(AutorizeByRoleAttribute.roles.Admin)]
		[HttpGet]
		[Route("CarsForRent/{userid}")]
		public HttpResponseMessage GetCarsForRentByUserId(string userid)
		{
			try
			{
				List<FullCarDataModel> allCarsForRent = carForRentRepository.GetCarsForRentByUserId(userid);
				return Request.CreateResponse(HttpStatusCode.OK, allCarsForRent);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[Authorize]
		[HttpPost]
		[Route("CarsForRent")]
		public HttpResponseMessage AddCarForRent(CarForRentModel carForRentModel)
		{
			try
			{
				if (carForRentModel == null)
				{
					return Request.CreateResponse(HttpStatusCode.BadRequest, "Data is null.");
				}
				if (!ModelState.IsValid)
				{
					Errors errors = ErrorsHelper.GetErrors(ModelState);
					return Request.CreateResponse(HttpStatusCode.BadRequest, errors);
				}
				CarForRentModel addedCarForRent = carForRentRepository.AddCarForRent(carForRentModel);
				return Request.CreateResponse(HttpStatusCode.Created, addedCarForRent);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[Authorize]
		[AutorizeByRole(AutorizeByRoleAttribute.roles.Manager)]
		[HttpPut]
		[Route("CarsForRent/{number}")]
		public HttpResponseMessage UpdateForRent(int number, CarForRentModel carForRentModel)
		{
			try
			{
				if (carForRentModel == null)
				{
					return Request.CreateResponse(HttpStatusCode.BadRequest, "Data is null.");
				}
				if (!ModelState.IsValid)
				{
					Errors errors = ErrorsHelper.GetErrors(ModelState);
					return Request.CreateResponse(HttpStatusCode.BadRequest, errors);
				}

				carForRentModel.rentNumber = number;
				CarForRentModel updatedCarForRent = carForRentRepository.UpdateCarForRent(carForRentModel);
				return Request.CreateResponse(HttpStatusCode.OK, updatedCarForRent);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[Authorize]
		[AutorizeByRole(AutorizeByRoleAttribute.roles.Admin)]
		[HttpDelete]
		[Route("CarsForRent/'{number}'")]
		public HttpResponseMessage DeleteForRent(string number)
		{
			try
			{
				int i = carForRentRepository.DeleteCarForRent(number);
				return Request.CreateResponse(HttpStatusCode.NoContent);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[Authorize]
		[AutorizeByRole(AutorizeByRoleAttribute.roles.Admin)]
		[HttpDelete]
		[Route("CarsForRent/{number}")]
		public HttpResponseMessage DeleteForRent(int number)
		{
			try
			{
				int i = carForRentRepository.DeleteCarForRent(number);
				return Request.CreateResponse(HttpStatusCode.NoContent);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[Authorize]
		//[AutorizeByRole(AutorizeByRoleAttribute.roles.Admin)]
		[HttpPost]
		[Route("getSumPrice")]
		public HttpResponseMessage GetSumPrice(OrderPriceModel carForPrice)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					Errors errors = ErrorsHelper.GetErrors(ModelState);
					return Request.CreateResponse(HttpStatusCode.BadRequest, errors);
				}

				OrderPriceModel carSumPrice = priceRepository.priceForOrderIfAvaliable(carForPrice);
				return Request.CreateResponse(HttpStatusCode.Created, carSumPrice);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);

				if (ex is DateNotAvaliableException)
				{
					return Request.CreateResponse(HttpStatusCode.Forbidden, errors);
				}

				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}
	}
}