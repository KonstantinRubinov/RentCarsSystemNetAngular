using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RentCars
{
	//[EnableCors("*", "*", "*")]
	[RoutePrefix("api")]
	public class CarTypeApiController : ApiController
    {
		private ICarTypeRepository carTypeRepository;
		public CarTypeApiController(ICarTypeRepository _carTypeRepository)
		{
			carTypeRepository = _carTypeRepository;
		}

		[HttpGet]
		[Route("CarTypes")]
		public HttpResponseMessage GetAllCarTypes()
		{
			try
			{
				List<CarTypeModel> allCarTypes = carTypeRepository.GetAllCarTypes();
				return Request.CreateResponse(HttpStatusCode.OK, allCarTypes);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[HttpGet]
		[Route("CarTypes/{id}")]
		public HttpResponseMessage GetOneCarType(int id)
		{
			try
			{
				CarTypeModel oneCarType = carTypeRepository.GetOneCarType(id);
				return Request.CreateResponse(HttpStatusCode.OK, oneCarType);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[Authorize]
		[AutorizeByRole(AutorizeByRoleAttribute.roles.Admin)]
		[HttpPost]
		[Route("CarTypes")]
		public HttpResponseMessage AddCarType(CarTypeModel carTypeModel)
		{
			try
			{
				if (carTypeModel == null)
				{
					return Request.CreateResponse(HttpStatusCode.BadRequest, "Data is null.");
				}
				if (!ModelState.IsValid)
				{
					Errors errors = ErrorsHelper.GetErrors(ModelState);
					return Request.CreateResponse(HttpStatusCode.BadRequest, errors);
				}

				CarTypeModel addedCarType = carTypeRepository.AddCarType(carTypeModel);
				return Request.CreateResponse(HttpStatusCode.Created, addedCarType);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[Authorize]
		[AutorizeByRole(AutorizeByRoleAttribute.roles.Admin)]
		[HttpPut]
		[Route("CarTypes/{type}")]
		public HttpResponseMessage UpdateCarType(int id, CarTypeModel carTypeModel)
		{
			try
			{
				if (carTypeModel == null)
				{
					return Request.CreateResponse(HttpStatusCode.BadRequest, "Data is null.");
				}
				if (!ModelState.IsValid)
				{
					Errors errors = ErrorsHelper.GetErrors(ModelState);
					return Request.CreateResponse(HttpStatusCode.BadRequest, errors);
				}

				carTypeModel.carTypeId = id;
				CarTypeModel updatedCarType = carTypeRepository.UpdateCarType(carTypeModel);
				return Request.CreateResponse(HttpStatusCode.OK, updatedCarType);
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
		[Route("CarTypes/{type}")]
		public HttpResponseMessage DeleteCarType(string type)
		{
			try
			{
				int i = carTypeRepository.DeleteCarType(type);
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
		[Route("CarTypes/{id}")]
		public HttpResponseMessage DeleteCarType(int id)
		{
			try
			{
				int i = carTypeRepository.DeleteCarType(id);
				return Request.CreateResponse(HttpStatusCode.NoContent);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}
	}
}
