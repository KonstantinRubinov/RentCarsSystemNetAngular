using System;
using System.Linq;

namespace RentCars
{
	public class EntityFullCarDataManager : EntityBaseManager, IFullCarDataRepository
	{
		public FullCarDataModel GetCarAllData(string carNumber)
		{
			if (GlobalVariable.queryType == 0)
			{
				var resultQuary = DB.CARS.Where(c => c.carNumber.Equals(carNumber)).Select(c => new FullCarDataModel
				{
					carNumber = c.carNumber,
					carKm = c.carKm,
					carPicture = c.carPicture != null ? "/assets/images/cars/" + c.carPicture : null,
					carInShape = c.carInShape,
					carAvaliable = true,
					carBranchID = c.carBranchID,

					carType = c.ALLCARTYPE.thisCarType,
					carFirm = c.ALLCARTYPE.carFirm,
					carModel = c.ALLCARTYPE.carModel,
					carDayPrice = c.ALLCARTYPE.carDayPrice,
					carLatePrice = c.ALLCARTYPE.carLatePrice,
					carYear = c.ALLCARTYPE.carYear,
					carGear = c.ALLCARTYPE.carGear,

					branchName = c.BRANCH.branchName,
					branchAddress = c.BRANCH.branchAddress,
					branchLat = c.BRANCH.branchLat,
					branchLng = c.BRANCH.branchLng,
				});

				resultQuary.SingleOrDefault().carAvaliable = new EntityPriceManager().CheckIfCarAvaliable(carNumber, DateTime.Now, DateTime.Now);
				return resultQuary.SingleOrDefault();
			}
			else
			{
				return DB.GetCarAllDataBySearch(carNumber).Select(c => new FullCarDataModel
				{
					carNumber = c.carNumber,
					carKm = c.carKm,
					carPicture = c.carPicture != null ? "/assets/images/cars/" + c.carPicture : null,
					carInShape = c.carInShape,
					carAvaliable = new EntityPriceManager().CheckIfCarAvaliable(carNumber, DateTime.Now, DateTime.Now),
					carBranchID = c.carBranchID,

					carType = c.thisCarType,
					carFirm = c.carFirm,
					carModel = c.carModel,
					carDayPrice = c.carDayPrice.Value,
					carLatePrice = c.carLatePrice.Value,
					carYear = c.carYear.Value,
					carGear = c.carGear,

					branchName = c.branchName,
					branchAddress = c.branchAddress,
					branchLat = c.branchLat.Value,
					branchLng = c.branchLng.Value
				}).SingleOrDefault();
			}
		}
	}
}