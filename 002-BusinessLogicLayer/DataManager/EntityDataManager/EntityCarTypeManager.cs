using RentCars.EntityDataBase;
using System.Collections.Generic;
using System.Linq;

namespace RentCars
{
	public class EntityCarTypeManager : EntityBaseManager, ICarTypeRepository
	{
		public List<CarTypeModel> GetAllCarTypesIds()
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.ALLCARTYPES.Select(ct => new CarTypeModel
				{
					carTypeId = ct.carTypeID,
					carType = ct.thisCarType
				}).ToList();
			}
			else
			{
				return DB.GetAllCarTypesIds().Select(ct => new CarTypeModel
				{
					carTypeId = ct.carTypeID,
					carType = ct.thisCarType
				}).ToList();
			}
		}

		public List<CarTypeModel> GetAllCarTypes()
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.ALLCARTYPES.Select(ct => new CarTypeModel
				{
					carTypeId = ct.carTypeID,
					carType = ct.thisCarType,
					carFirm = ct.carFirm,
					carModel = ct.carModel,
					carDayPrice = ct.carDayPrice,
					carLatePrice = ct.carLatePrice,
					carYear = ct.carYear,
					carGear = ct.carGear
				}).ToList();
			}
			else
			{
				return DB.GetAllCarTypes().Select(ct => new CarTypeModel
				{
					carTypeId = ct.carTypeID,
					carType = ct.thisCarType,
					carFirm = ct.carFirm,
					carModel = ct.carModel,
					carDayPrice = ct.carDayPrice,
					carLatePrice = ct.carLatePrice,
					carYear = ct.carYear,
					carGear = ct.carGear
				}).ToList();
			}
		}

		public CarTypeModel GetOneCarType(int typeId)
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.ALLCARTYPES.Where(ct => ct.carTypeID == typeId).Select(ct => new CarTypeModel
				{
					carTypeId = ct.carTypeID,
					carType = ct.thisCarType,
					carFirm = ct.carFirm,
					carModel = ct.carModel,
					carDayPrice = ct.carDayPrice,
					carLatePrice = ct.carLatePrice,
					carYear = ct.carYear,
					carGear = ct.carGear
				}).SingleOrDefault();
			}
			else
			{
				return DB.GetOneCarType(typeId).Select(ct => new CarTypeModel
				{
					carTypeId = ct.carTypeID,
					carType = ct.thisCarType,
					carFirm = ct.carFirm,
					carModel = ct.carModel,
					carDayPrice = ct.carDayPrice,
					carLatePrice = ct.carLatePrice,
					carYear = ct.carYear,
					carGear = ct.carGear
				}).SingleOrDefault();
			}
		}

		public CarTypeModel AddCarType(CarTypeModel carTypeModel)
		{
			if (GlobalVariable.queryType == 0)
			{
				ALLCARTYPE carType = new ALLCARTYPE
				{
					carTypeID = carTypeModel.carTypeId,
					thisCarType = carTypeModel.carType,
					carFirm = carTypeModel.carFirm,
					carModel = carTypeModel.carModel,
					carDayPrice = carTypeModel.carDayPrice,
					carLatePrice = carTypeModel.carLatePrice,
					carYear = carTypeModel.carYear,
					carGear = carTypeModel.carGear
				};
				DB.ALLCARTYPES.Add(carType);
				DB.SaveChanges();
				return GetOneCarType(carType.carTypeID);
			}
			else
			{
				return DB.AddCarType(carTypeModel.carType, carTypeModel.carFirm, carTypeModel.carModel, carTypeModel.carDayPrice, carTypeModel.carLatePrice, carTypeModel.carYear, carTypeModel.carGear).Select(ct => new CarTypeModel
				{
					carTypeId = ct.carTypeID,
					carType = ct.thisCarType,
					carFirm = ct.carFirm,
					carModel = ct.carModel,
					carDayPrice = ct.carDayPrice,
					carLatePrice = ct.carLatePrice,
					carYear = ct.carYear,
					carGear = ct.carGear
				}).SingleOrDefault();
			}
		}

		public CarTypeModel UpdateCarType(CarTypeModel carTypeModel)
		{
			if (GlobalVariable.queryType == 0)
			{
				ALLCARTYPE carType = DB.ALLCARTYPES.Where(ct => ct.carTypeID == carTypeModel.carTypeId).SingleOrDefault();
				if (carType == null)
					return null;
				carType.carTypeID = carTypeModel.carTypeId;
				carType.thisCarType = carTypeModel.carType;
				carType.carFirm = carTypeModel.carFirm;
				carType.carModel = carTypeModel.carModel;
				carType.carDayPrice = carTypeModel.carDayPrice;
				carType.carLatePrice = carTypeModel.carLatePrice;
				carType.carYear = carTypeModel.carYear;
				carType.carGear = carTypeModel.carGear;
				DB.SaveChanges();
				return GetOneCarType(carType.carTypeID);
			}
			else
			{
				return DB.UpdateCarType(carTypeModel.carType, carTypeModel.carFirm, carTypeModel.carModel, carTypeModel.carDayPrice, carTypeModel.carLatePrice, carTypeModel.carYear, carTypeModel.carGear, carTypeModel.carTypeId).Select(ct => new CarTypeModel
				{
					carTypeId = ct.carTypeID,
					carType = ct.thisCarType,
					carFirm = ct.carFirm,
					carModel = ct.carModel,
					carDayPrice = ct.carDayPrice,
					carLatePrice = ct.carLatePrice,
					carYear = ct.carYear,
					carGear = ct.carGear
				}).SingleOrDefault();
			}
		}

		public int DeleteCarType(string type)
		{
			if (GlobalVariable.queryType == 0)
			{
				ALLCARTYPE carType = DB.ALLCARTYPES.Where(ct => ct.thisCarType.Equals(type)).SingleOrDefault();
				DB.ALLCARTYPES.Attach(carType);
				if (carType == null)
					return 0;
				DB.ALLCARTYPES.Remove(carType);
				DB.SaveChanges();
				return 1;
			}
			else
			{
				return DB.DeleteCarTypeByType(type);
			}
		}

		public int DeleteCarType(int carTypeId)
		{
			if (GlobalVariable.queryType == 0)
			{
				ALLCARTYPE carType = DB.ALLCARTYPES.Where(ct => ct.carTypeID == carTypeId).SingleOrDefault();
				DB.ALLCARTYPES.Attach(carType);
				if (carType == null)
					return 0;
				DB.ALLCARTYPES.Remove(carType);
				DB.SaveChanges();
				return 1;
			}
			else
			{
				return DB.DeleteCarTypeById(carTypeId);
			}
		}
	}
}
