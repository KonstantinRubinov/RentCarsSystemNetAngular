using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;

namespace RentCars
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CarsService" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select CarsService.svc or CarsService.svc.cs at the Solution Explorer and start debugging.
	public class CarsService : ICarsService
	{
		private ICarsRepository carsRepository;
		public CarsService()
		{
			if (GlobalVariable.logicType == 0)
				carsRepository = new EntityCarsManager();
			else if (GlobalVariable.logicType == 1)
				carsRepository = new SqlCarsManager();
			else if (GlobalVariable.logicType == 2)
				carsRepository = new MySqlCarsManager();
			else
				carsRepository = new MongoCarsManager();
		}

		public HttpResponseMessage GetAllCars()
		{
			try
			{
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent(JsonConvert.SerializeObject(carsRepository.GetAllCars()))
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

		public HttpResponseMessage GetOneCar(string getByNumber)
		{
			try
			{
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent(JsonConvert.SerializeObject(carsRepository.GetOneCar(getByNumber)))
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

		public HttpResponseMessage AddCar(CarModel carModel)
		{
			try
			{
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.Created)
				{
					Content = new StringContent(JsonConvert.SerializeObject(carsRepository.AddCar(carModel)))
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

		public HttpResponseMessage UpdateCar(string updateByNumber, CarModel carModel)
		{
			try
			{
				carModel.carNumber = updateByNumber;
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent(JsonConvert.SerializeObject(carsRepository.UpdateCar(carModel)))
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

		public HttpResponseMessage DeleteCar(string deleteByNumber)
		{
			try
			{
				string carImage = carsRepository.DeleteCar(deleteByNumber);
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

				if (!carImage.Equals("") && !carImage.Equals(String.Empty) && carImage!=null)
				{
					HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.NoContent)
					{
					};
					return hrm;
				}
				HttpResponseMessage hr = new HttpResponseMessage(HttpStatusCode.InternalServerError)
				{
				};
				return hr;
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

		public HttpResponseMessage UploadCarImage(string carNumber, string carImage)
		{
			try
			{
				CarModel updloadedCar;
				string fileName;

				if (carImage.Equals(""))
				{
					fileName = Guid.NewGuid() + ".png";
				}
				else
				{
					fileName = carImage;
				}
				string filePath = HttpContext.Current.Server.MapPath("~/assets/images/cars/" + fileName);
				using (var fs = new FileStream(filePath, FileMode.Create))
				{
					HttpContext.Current.Request.InputStream.CopyTo(fs);
					updloadedCar = carsRepository.UploadCarImage(carNumber, fileName);
					updloadedCar.carPicture = "assets/images/cars/" + fileName;
				}

				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent(JsonConvert.SerializeObject(updloadedCar))
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

		public HttpResponseMessage GetAllCarImagesAndNumberOfCars()
		{
			try
			{
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent(JsonConvert.SerializeObject(carsRepository.GetAllCarImagesAndNumberOfCars()))
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
