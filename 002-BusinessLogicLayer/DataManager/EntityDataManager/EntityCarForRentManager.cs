using RentCars.EntityDataBase;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentCars
{
	public class EntityCarForRentManager : EntityBaseManager, ICarForRentRepository
	{
		public List<CarForRentModel> GetAllCarsForRent()
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.CARFORRENTS.OrderBy(cfr => cfr.rentStartDate).Select(cfr => new CarForRentModel
				{
					rentNumber = cfr.rentNumber,
					carNumber = cfr.carNumber,
					userID = cfr.userID,
					rentStartDate = cfr.rentStartDate,
					rentEndDate = cfr.rentEndDate,
					rentRealEndDate = cfr.rentRealEndDate
				}).ToList();
			}
			else
			{
				return DB.GetAllCarsForRent().Select(cfr => new CarForRentModel
				{
					rentNumber = cfr.rentNumber,
					carNumber = cfr.carNumber,
					userID = cfr.userID,
					rentStartDate = cfr.rentStartDate,
					rentEndDate = cfr.rentEndDate,
					rentRealEndDate = cfr.rentRealEndDate
				}).ToList();
			}
		}

		public List<CarForRentModel> GetCarsForRentByCarNumber(string carNumber)
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.CARFORRENTS.Where(cfr => cfr.carNumber.Equals(carNumber)).OrderBy(cfr => cfr.rentStartDate).Select(cfr => new CarForRentModel
				{
					rentNumber = cfr.rentNumber,
					carNumber = cfr.carNumber,
					userID = cfr.userID,
					rentStartDate = cfr.rentStartDate,
					rentEndDate = cfr.rentEndDate,
					rentRealEndDate = cfr.rentRealEndDate
				}).ToList();
			}
			else
			{
				return DB.GetAllCarsForRentByCarNumber(carNumber).Select(cfr => new CarForRentModel
				{
					rentNumber = cfr.rentNumber,
					carNumber = cfr.carNumber,
					userID = cfr.userID,
					rentStartDate = cfr.rentStartDate,
					rentEndDate = cfr.rentEndDate,
					rentRealEndDate = cfr.rentRealEndDate
				}).ToList();
			}
		}

		public List<FullCarDataModel> GetCarsForRentByUserId(string userId)
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.CARFORRENTS.Where(cfr => cfr.userID.Equals(userId)).OrderBy(cfr => cfr.rentStartDate).Select(cfr => new FullCarDataModel
				{
					carNumber = cfr.CAR.carNumber,
					carKm = cfr.CAR.carKm,
					carPicture = cfr.CAR.carPicture != null ? "/assets/images/cars/" + cfr.CAR.carPicture : null,
					carInShape = cfr.CAR.carInShape,
					carAvaliable = cfr.CAR.carAvaliable,
					carBranchID = cfr.CAR.carBranchID,

					carType = cfr.CAR.ALLCARTYPE.thisCarType,
					carFirm = cfr.CAR.ALLCARTYPE.carFirm,
					carModel = cfr.CAR.ALLCARTYPE.carModel,
					carDayPrice = cfr.CAR.ALLCARTYPE.carDayPrice,
					carLatePrice = cfr.CAR.ALLCARTYPE.carLatePrice,
					carYear = cfr.CAR.ALLCARTYPE.carYear,
					carGear = cfr.CAR.ALLCARTYPE.carGear,

					branchName = cfr.CAR.BRANCH.branchName,
					branchAddress = cfr.CAR.BRANCH.branchAddress,
					branchLat = cfr.CAR.BRANCH.branchLat,
					branchLng = cfr.CAR.BRANCH.branchLng
				}).ToList();
			}
			else
			{
				return DB.GetAllCarsForRentByUserId(userId).Select(cfr => new FullCarDataModel
				{
					carNumber = cfr.carNumber,
					carKm = cfr.carKm,
					carPicture = cfr.carPicture != null ? "/assets/images/cars/" + cfr.carPicture : null,
					carInShape = cfr.carInShape,
					carAvaliable = cfr.carAvaliable,
					carBranchID = cfr.carBranchID,

					carType = cfr.thisCarType,
					carFirm = cfr.carFirm,
					carModel = cfr.carModel,
					carDayPrice = cfr.carDayPrice.Value,
					carLatePrice = cfr.carLatePrice.Value,
					carYear = cfr.carYear.Value,
					carGear = cfr.carGear,

					branchName = cfr.branchName,
					branchAddress = cfr.branchAddress,
					branchLat = cfr.branchLat.Value,
					branchLng = cfr.branchLng.Value
				}).ToList();
			}
		}

		public CarForRentModel GetOneCarForRentByRentNumber(int rentNumber)
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.CARFORRENTS.Where(cfr => cfr.rentNumber == rentNumber).Select(cfr => new CarForRentModel
				{
					rentNumber = cfr.rentNumber,
					carNumber = cfr.carNumber,
					userID = cfr.userID,
					rentStartDate = cfr.rentStartDate,
					rentEndDate = cfr.rentEndDate,
					rentRealEndDate = cfr.rentRealEndDate
				}).SingleOrDefault();
			}
			else
			{
				return DB.GetOneCarForRentByRentNumber(rentNumber).Select(cfr => new CarForRentModel
				{
					rentNumber = cfr.rentNumber,
					carNumber = cfr.carNumber,
					userID = cfr.userID,
					rentStartDate = cfr.rentStartDate,
					rentEndDate = cfr.rentEndDate,
					rentRealEndDate = cfr.rentRealEndDate
				}).SingleOrDefault();
			}
		}

		public CarForRentModel AddCarForRent(CarForRentModel carForRentModel)
		{
			string id = HttpContext.Current.User.Identity.Name;
			carForRentModel.userID = id;

			if (carForRentModel.rentRealEndDate == null)
				carForRentModel.rentRealEndDate = carForRentModel.rentStartDate.AddDays(-36);

			if (GlobalVariable.queryType == 0)
			{
				CARFORRENT carForRent = new CARFORRENT
				{
					carNumber = carForRentModel.carNumber,
					userID = carForRentModel.userID,
					rentStartDate = carForRentModel.rentStartDate,
					rentEndDate = carForRentModel.rentEndDate,
					rentRealEndDate = carForRentModel.rentRealEndDate
				};
				DB.CARFORRENTS.Add(carForRent);
				DB.SaveChanges();
				return GetOneCarForRentByRentNumber(carForRent.rentNumber);
			}
			else
			{
				return DB.AddCarForRent(carForRentModel.rentStartDate, carForRentModel.rentEndDate, carForRentModel.rentRealEndDate, carForRentModel.userID, carForRentModel.carNumber).Select(cfr => new CarForRentModel
				{
					rentNumber = cfr.rentNumber,
					carNumber = cfr.carNumber,
					userID = cfr.userID,
					rentStartDate = cfr.rentStartDate,
					rentEndDate = cfr.rentEndDate,
					rentRealEndDate = cfr.rentRealEndDate
				}).SingleOrDefault();
			}
		}

		public CarForRentModel UpdateCarForRent(CarForRentModel carForRentModel)
		{
			string id = HttpContext.Current.User.Identity.Name;
			carForRentModel.userID = id;

			if (carForRentModel.rentRealEndDate == null)
				carForRentModel.rentRealEndDate = carForRentModel.rentStartDate.AddDays(-36);

			if (GlobalVariable.queryType == 0)
			{
				CARFORRENT carForRent = DB.CARFORRENTS.Where(cfr => cfr.rentNumber == carForRentModel.rentNumber).SingleOrDefault();
				if (carForRent == null)
					return null;
				carForRent.carNumber = carForRentModel.carNumber;
				carForRent.userID = carForRentModel.userID;
				carForRent.rentStartDate = carForRentModel.rentStartDate;
				carForRent.rentEndDate = carForRentModel.rentEndDate;
				carForRent.rentRealEndDate = carForRentModel.rentRealEndDate;
				DB.SaveChanges();
				return GetOneCarForRentByRentNumber(carForRent.rentNumber);
			}
			else
			{
				return DB.UpdateCarForRent(carForRentModel.rentStartDate, carForRentModel.rentEndDate, carForRentModel.rentRealEndDate, carForRentModel.userID, carForRentModel.carNumber, carForRentModel.rentNumber).Select(cfr => new CarForRentModel
				{
					rentNumber = cfr.rentNumber,
					carNumber = cfr.carNumber,
					userID = cfr.userID,
					rentStartDate = cfr.rentStartDate,
					rentEndDate = cfr.rentEndDate,
					rentRealEndDate = cfr.rentRealEndDate
				}).SingleOrDefault();
			}
		}

		public int DeleteCarForRent(int rentNumber)
		{
			if (GlobalVariable.queryType == 0)
			{
				CARFORRENT carForRent = DB.CARFORRENTS.Where(cfr => cfr.rentNumber == rentNumber).SingleOrDefault();
				DB.CARFORRENTS.Attach(carForRent);
				if (carForRent == null)
					return 0;
				DB.CARFORRENTS.Remove(carForRent);
				DB.SaveChanges();
				return 1;
			}
			else
			{
				return DB.DeleteCarForRentByRent(rentNumber);
			}
		}

		public int DeleteCarForRent(string carNumber)
		{
			if (GlobalVariable.queryType == 0)
			{
				CARFORRENT carForRent = DB.CARFORRENTS.Where(cfr => cfr.carNumber.Equals(carNumber)).SingleOrDefault();
				DB.CARFORRENTS.Attach(carForRent);
				if (carForRent == null)
					return 0;
				DB.CARFORRENTS.Remove(carForRent);
				DB.SaveChanges();
				return 1;
			}
			else
			{
				return DB.DeleteCarForRentByCar(carNumber);
			}
		}
	}
}
