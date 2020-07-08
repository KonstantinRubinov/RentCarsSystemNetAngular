using System;
using System.Collections.Generic;
using System.Linq;

namespace RentCars
{
	public class EntitySearchManager : EntityBaseManager, ISearchRepository
	{
		public SearchReturnModel GetAllCarsBySearch(SearchModel searchModel, int page = 0, int carsNum = 0)
		{
			var resultQuary =
			from cars in DB.CARS
			join carTypes in DB.ALLCARTYPES on cars.carTypeID equals carTypes.carTypeID
			join carBranches in DB.BRANCHES on cars.carBranchID equals carBranches.branchID
			where ((searchModel.freeSearch != null && !searchModel.freeSearch.Equals("") && (cars.carNumber.Contains(searchModel.freeSearch) || carTypes.carFirm.Contains(searchModel.freeSearch) || carTypes.carModel.Contains(searchModel.freeSearch))) || (searchModel.freeSearch==null || searchModel.freeSearch.Equals("")))
			
			where ((searchModel.company != null && !searchModel.company.Equals("") && carTypes.carFirm.Equals(searchModel.company)) || (searchModel.company == null || searchModel.company.Equals("")))
			where ((searchModel.carType != null && !searchModel.carType.Equals("") && carTypes.thisCarType.Equals(searchModel.carType)) || (searchModel.carType == null || searchModel.carType.Equals("")))
			where ((searchModel.gear != null && !searchModel.gear.Equals("") && carTypes.carGear.Equals(searchModel.gear)) || (searchModel.gear == null || searchModel.gear.Equals("")))
			where ((searchModel.year != 0 && carTypes.carYear.Equals(searchModel.year)) || (searchModel.year == 0))

			select new FullCarDataModel
			{
				carNumber = cars.carNumber,
				carKm = cars.carKm,
				carPicture = cars.carPicture != null ? "/assets/images/cars/" + cars.carPicture : null,
				carInShape = cars.carInShape,
				carAvaliable = cars.carAvaliable,
				carBranchID = cars.carBranchID,

				carType = carTypes.thisCarType,
				carFirm = carTypes.carFirm,
				carModel = carTypes.carModel,
				carDayPrice = carTypes.carDayPrice,
				carLatePrice = carTypes.carLatePrice,
				carYear = carTypes.carYear,
				carGear = carTypes.carGear,

				branchName = carBranches.branchName,
				branchAddress = carBranches.branchAddress,
				branchLat = carBranches.branchLat,
				branchLng = carBranches.branchLng
			};

			var total = resultQuary.Count();
			resultQuary = resultQuary.OrderBy(c => c.carNumber).Skip(page * carsNum).Take(carsNum);


			var resultSP = DB.GetAllCarsBySearch(carsNum * page, carsNum, searchModel.freeSearch, searchModel.company, searchModel.carType, searchModel.gear, searchModel.year).Select(c => new FullCarDataModel
			{
				carNumber = c.carNumber,
				carKm = c.carKm,
				carPicture = c.carPicture != null ? "/assets/images/cars/" + c.carPicture : null,
				carInShape = c.carInShape,
				carAvaliable = c.carAvaliable,
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
				branchLng = c.branchLng.Value,

				numerOfCars = c.TotalRows.Value
			});


			if (GlobalVariable.queryType > 0)
			{
				resultQuary = resultSP.AsQueryable();
			}

			List<FullCarDataModel> fullCars = new List<FullCarDataModel>();

			foreach (FullCarDataModel fullCar in resultQuary)
			{
				if (searchModel.fromDate != null && searchModel.toDate != null)
				{
					fullCar.carAvaliable = new EntityPriceManager().CheckIfCarAvaliable(fullCar.carNumber, (DateTime)searchModel.fromDate, (DateTime)searchModel.toDate);
					if (fullCar.carAvaliable)
					{
						fullCar.carPrice = PriceLogic.CarPrice(searchModel.fromDate, searchModel.toDate, fullCar.carDayPrice);
						fullCars.Add(fullCar);
					}
				}
				else
				{
					fullCars.Add(fullCar);
				}
			}

			SearchReturnModel searchReturnModel = new SearchReturnModel();

			if (GlobalVariable.queryType == 0)
			{
				searchReturnModel.fullCarsData = fullCars;
				searchReturnModel.fullCarsDataLenth = total;
			}
			else
			{
				searchReturnModel.fullCarsData = fullCars;
				if (searchReturnModel.fullCarsData != null && searchReturnModel.fullCarsData.Count > 0)
				{
					searchReturnModel.fullCarsDataLenth = searchReturnModel.fullCarsData[0].numerOfCars;
				}
				else
				{
					searchReturnModel.fullCarsDataLenth = 0;
				}
			}



			searchReturnModel.fullCarsDataPage = page;
			return searchReturnModel;
		}
	}
}