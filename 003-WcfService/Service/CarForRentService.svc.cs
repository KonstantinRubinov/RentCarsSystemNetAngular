using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;

namespace RentCars
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CarForRentService" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select CarForRentService.svc or CarForRentService.svc.cs at the Solution Explorer and start debugging.
	public class CarForRentService : ICarForRentService
	{
		private ICarForRentRepository carForRentRepository;
		public CarForRentService()
		{
			if (GlobalVariable.logicType == 0)
				carForRentRepository = new EntityCarForRentManager();
			else if (GlobalVariable.logicType == 1)
				carForRentRepository = new SqlCarForRentManager();
			else if (GlobalVariable.logicType == 2)
				carForRentRepository = new MySqlCarForRentManager();
			else
				carForRentRepository = new MongoCarForRentManager();
		}

		//[BasicAuthentication]
		//[AutorizeByRole(AutorizeByRoleAttribute.roles.Admin)]
		public HttpResponseMessage GetAllCarsForRent()
		{
			try
			{
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent(JsonConvert.SerializeObject(carForRentRepository.GetAllCarsForRent()))
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

		//[BasicAuthentication]
		//[AutorizeByRole(AutorizeByRoleAttribute.roles.Admin)]
		public HttpResponseMessage GetCarsForRentByCarNumber(string carNumber)
		{
			try
			{
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent(JsonConvert.SerializeObject(carForRentRepository.GetCarsForRentByCarNumber(carNumber)))
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

		//[BasicAuthentication]
		//[AutorizeByRole(AutorizeByRoleAttribute.roles.Admin)]
		public HttpResponseMessage GetCarsForRentByUserId(string userId)
		{
			try
			{
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent(JsonConvert.SerializeObject(carForRentRepository.GetCarsForRentByUserId(userId)))
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

		public HttpResponseMessage GetOneCarForRentByRentNumber(int rentNumber)
		{
			try
			{
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent(JsonConvert.SerializeObject(carForRentRepository.GetOneCarForRentByRentNumber(rentNumber)))
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

		//[BasicAuthentication]
		//[AutorizeByRole(AutorizeByRoleAttribute.roles.Admin)]
		public HttpResponseMessage AddCarForRent(CarForRentModel carForRentModel, string userName)
		{
			try
			{
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.Created)
				{
					Content = new StringContent(JsonConvert.SerializeObject(carForRentRepository.AddCarForRent(carForRentModel)))
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

		//[BasicAuthentication]
		//[AutorizeByRole(AutorizeByRoleAttribute.roles.Manager)]
		public HttpResponseMessage UpdateCarForRent(int updateByRentNumber, CarForRentModel carForRentModel)
		{
			try
			{
				carForRentModel.rentNumber = updateByRentNumber;
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent(JsonConvert.SerializeObject(carForRentRepository.UpdateCarForRent(carForRentModel)))
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

		//[BasicAuthentication]
		//[AutorizeByRole(AutorizeByRoleAttribute.roles.Admin)]
		public HttpResponseMessage DeleteCarForRent(int rentNumber)
		{
			try
			{
				int i = carForRentRepository.DeleteCarForRent(rentNumber);

				if (i > 0)
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

		//[BasicAuthentication]
		//[AutorizeByRole(AutorizeByRoleAttribute.roles.Admin)]
		public HttpResponseMessage DeleteCarForRent(string carNumber)
		{
			try
			{
				int i = carForRentRepository.DeleteCarForRent(carNumber);

				if (i > 0)
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
	}
}
