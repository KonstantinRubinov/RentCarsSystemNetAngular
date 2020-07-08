using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace RentCars
{
	//[EnableCors("*", "*", "*")]
	[RoutePrefix("api")]
	public class CarsApiController : ApiController
    {
		private ICarsRepository carsRepository;
		public CarsApiController(ICarsRepository _carsRepository)
		{
			carsRepository = _carsRepository;
		}

		[HttpGet]
		[Route("cars")]
		public HttpResponseMessage GetAllCars()
		{
			try
			{
				List<CarModel> allCars = carsRepository.GetAllCars();
				return Request.CreateResponse(HttpStatusCode.OK, allCars);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}


		[HttpGet]
		[Route("cars/{number}")]
		public HttpResponseMessage GetOneCar(string number)
		{
			try
			{
				CarModel oneCar = carsRepository.GetOneCar(number);
				return Request.CreateResponse(HttpStatusCode.OK, oneCar);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}


		[HttpPost]
		[Route("cars")]
		public HttpResponseMessage AddCar(CarModel carModel)
		{
			try
			{
				if (carModel == null)
				{
					return Request.CreateResponse(HttpStatusCode.BadRequest, "Data is null.");
				}
				if (!ModelState.IsValid)
				{
					Errors errors = ErrorsHelper.GetErrors(ModelState);
					return Request.CreateResponse(HttpStatusCode.BadRequest, errors);
				}

				CarModel addedCar = carsRepository.AddCar(carModel);
				return Request.CreateResponse(HttpStatusCode.Created, addedCar);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}


		[HttpPut]
		[Route("cars/{number}")]
		public HttpResponseMessage UpdateCar(string number, CarModel carModel)
		{
			try
			{
				if (carModel == null)
				{
					return Request.CreateResponse(HttpStatusCode.BadRequest, "Data is null.");
				}
				if (!ModelState.IsValid)
				{
					Errors errors = ErrorsHelper.GetErrors(ModelState);
					return Request.CreateResponse(HttpStatusCode.BadRequest, errors);
				}

				carModel.carNumber = number;
				CarModel updatedCar = carsRepository.UpdateCar(carModel);
				return Request.CreateResponse(HttpStatusCode.OK, updatedCar);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}


		[HttpDelete]
		[Route("cars/{number}")]
		public HttpResponseMessage DeleteCar(string number)
		{
			try
			{
				string carImage = carsRepository.DeleteCar(number);
				carImage = carImage.Trim();
				CarPictureModel carsPictureModel = carsRepository.GetNumberOfCarWithImage(carImage);
				if (carsPictureModel.numberOfCars == 0)
				{
					string filePath = HttpContext.Current.Server.MapPath("~/assets/images/cars/" + carImage);
					if (File.Exists(filePath))
					{
						File.Delete(filePath);
					}
				}
				return Request.CreateResponse(HttpStatusCode.NoContent);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[HttpPost]
		[Route("cars/file/{number}")]
		public HttpResponseMessage UploadCarImage(string number, string carPic = "")
		{
			try
			{
				CarModel updloadedCar;
				string fileName;

				if (carPic.Equals(""))
				{
					fileName = Guid.NewGuid() + ".png";
				}
				else
				{
					fileName = carPic;
				}
				string filePath = HttpContext.Current.Server.MapPath("~/assets/images/cars/" + fileName);
				using (var fs = new FileStream(filePath, FileMode.Create))
				{
					HttpContext.Current.Request.InputStream.CopyTo(fs);
					updloadedCar = carsRepository.UploadCarImage(number, fileName);
					updloadedCar.carPicture = "assets/images/cars/" + fileName;
				}
				if (updloadedCar != null)
				{
					return Request.CreateResponse(HttpStatusCode.OK, updloadedCar);
				}
				return Request.CreateResponse(HttpStatusCode.ExpectationFailed);

			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[HttpGet]
		[Route("carimages")]
		public HttpResponseMessage GetAllCarImagesAndNumberOfCars()
		{
			try
			{
				List<CarPictureModel> allCarImages = carsRepository.GetAllCarImagesAndNumberOfCars();
				return Request.CreateResponse(HttpStatusCode.OK, allCarImages);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}
	}
}
