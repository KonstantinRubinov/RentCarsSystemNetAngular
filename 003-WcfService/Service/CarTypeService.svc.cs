using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;

namespace RentCars
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CarTypeService" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select CarTypeService.svc or CarTypeService.svc.cs at the Solution Explorer and start debugging.
	public class CarTypeService : ICarTypeService
	{
		private ICarTypeRepository carTypeRepository;
		public CarTypeService()
		{
			if (GlobalVariable.logicType == 0)
				carTypeRepository = new EntityCarTypeManager();
			else if (GlobalVariable.logicType == 1)
				carTypeRepository = new SqlCarTypeManager();
			else if (GlobalVariable.logicType == 2)
				carTypeRepository = new MySqlCarTypeManager();
			else
				carTypeRepository = new MongoCarTypeManager();
		}

		public HttpResponseMessage GetAllCarTypes()
		{
			try
			{
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent(JsonConvert.SerializeObject(carTypeRepository.GetAllCarTypes()))
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

		public HttpResponseMessage GetOneCarType(int getById)
		{
			try
			{
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent(JsonConvert.SerializeObject(carTypeRepository.GetOneCarType(getById)))
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
		public HttpResponseMessage AddCarType(CarTypeModel carTypeModel)
		{
			try
			{
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.Created)
				{
					Content = new StringContent(JsonConvert.SerializeObject(carTypeRepository.AddCarType(carTypeModel)))
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
		public HttpResponseMessage UpdateCarType(int id, CarTypeModel carTypeModel)
		{
			try
			{
				carTypeModel.carTypeId = id;
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent(JsonConvert.SerializeObject(carTypeRepository.UpdateCarType(carTypeModel)))
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
		public HttpResponseMessage DeleteCarType(string deleteByType)
		{
			try
			{
				int i = carTypeRepository.DeleteCarType(deleteByType);

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
		public HttpResponseMessage DeleteCarType(int deleteById)
		{
			try
			{
				int i = carTypeRepository.DeleteCarType(deleteById);

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
