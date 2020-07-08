using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RentCars
{
	public class EntityPriceManager : EntityBaseManager, IPriceRepository
	{
		public OrderPriceModel priceForOrderIfAvaliable(OrderPriceModel carForPrice)
		{
			bool isAvaliable = CheckIfCarAvaliable(carForPrice.carNumber, carForPrice.rentStartDate, carForPrice.rentEndDate) == true;
			if (isAvaliable == true)
			{
				FullCarDataModel myCarsForRentModel = GetCarDayPrice(carForPrice.carNumber);

				carForPrice.orderDays = ((carForPrice.rentEndDate - carForPrice.rentStartDate).Days);
				carForPrice.carPrice = PriceLogic.CarPrice(carForPrice.rentStartDate, carForPrice.rentEndDate, myCarsForRentModel.carDayPrice);
			}
			else
			{
				Debug.WriteLine("priceForOrderIfAvaliable DateNotAvaliableException: " + "The Car Is Not Avaliable at this dates");
				throw new DateNotAvaliableException("The Car Is Not Avaliable at this dates");
			}
			return carForPrice;
		}

		public FullCarDataModel GetCarDayPrice(string carNumber)
		{
			var resultQuary = DB.CARS.Where(c => c.carNumber.Equals(carNumber)).Select(c => new FullCarDataModel
			{
				carDayPrice = c.ALLCARTYPE.carDayPrice,
			});

			var resultSP = DB.GetCarDayPriceBySearch(carNumber).Select(carDayPrice1 => new FullCarDataModel
			{
				carDayPrice = carDayPrice1.Value,
			});

			if (GlobalVariable.queryType == 0)
				return resultQuary.FirstOrDefault();
			else
				return resultSP.FirstOrDefault();
		}

		public bool CheckIfCarAvaliable(string carNumber, DateTime fromDate, DateTime toDate)
		{
			List<CarForRentModel> carForRentList = new EntityCarForRentManager().GetCarsForRentByCarNumber(carNumber);
			if (carForRentList != null && carForRentList.Count > 0)
			{
				if (DateTime.Compare((DateTime)toDate, carForRentList[0].rentStartDate) < 0)
				{
					return true;
				}
				if (DateTime.Compare((DateTime)fromDate, carForRentList[carForRentList.Count - 1].rentEndDate) > 0)
				{
					return true;
				}

				for (int i = 0; i < carForRentList.Count - 1; i++)
				{
					if (DateTime.Compare((DateTime)fromDate, carForRentList[i].rentEndDate) > 0 && DateTime.Compare((DateTime)toDate, carForRentList[i + 1].rentStartDate) < 0)
					{
						return true;
					}
				}
				return false;
			}
			else
			{
				return true;
			}
		}
	}
}
