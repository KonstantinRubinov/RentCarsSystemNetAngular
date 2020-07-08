using RentCars.EntityDataBase;
using System.Collections.Generic;
using System.Linq;

namespace RentCars
{
	public class EntityCarsManager : EntityBaseManager, ICarsRepository
	{
		public List<CarModel> GetAllCars()
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.CARS.Select(c => new CarModel
				{
					carNumber = c.carNumber,
					carTypeID = c.carTypeID,
					carKm = c.carKm,
					carPicture = c.carPicture != null ? "/assets/images/cars/" + c.carPicture : null,
					carInShape = c.carInShape,
					carAvaliable = c.carAvaliable,
					carBranchID = c.carBranchID
				}).ToList();
			}
			else
			{
				return DB.GetAllCars().Select(c => new CarModel
				{
					carNumber = c.carNumber,
					carTypeID = c.carTypeID,
					carKm = c.carKm,
					carPicture = c.carPicture != null ? "/assets/images/cars/" + c.carPicture : null,
					carInShape = c.carInShape,
					carAvaliable = c.carAvaliable,
					carBranchID = c.carBranchID
				}).ToList();
			}
		}

		public CarModel GetOneCar(string num)
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.CARS.Where(c => c.carNumber.Equals(num)).Select(c => new CarModel
				{
					carNumber = c.carNumber,
					carTypeID = c.carTypeID,
					carKm = c.carKm,
					carPicture = c.carPicture != null ? "/assets/images/cars/" + c.carPicture : null,
					carInShape = c.carInShape,
					carAvaliable = c.carAvaliable,
					carBranchID = c.carBranchID
				}).SingleOrDefault();
			}
			else
			{
				return DB.GetOneCarByNumber(num).Select(c => new CarModel
				{
					carNumber = c.carNumber,
					carTypeID = c.carTypeID,
					carKm = c.carKm,
					carPicture = c.carPicture != null ? "/assets/images/cars/" + c.carPicture : null,
					carInShape = c.carInShape,
					carAvaliable = c.carAvaliable,
					carBranchID = c.carBranchID
				}).SingleOrDefault();
			}
		}

		public List<CarPictureModel> GetAllCarImagesAndNumberOfCars()
		{
			if (GlobalVariable.queryType == 0)
			{
				return (from car in DB.CARS
						group car by car.carPicture into pictureGroup
						select new CarPictureModel()
						{
							carPictureLink = "assets/images/cars/" + pictureGroup.Key,
							carPictureName = pictureGroup.Key,
							numberOfCars = pictureGroup.Select(c => c.carNumber).Count()
						}).ToList();
			}
			else
			{
				return DB.GetAllCarImagesAndNumberOfCars().Select(c => new CarPictureModel
				{
					carPictureLink = "assets/images/cars/" + c.carPicture,
					carPictureName = c.carPicture,
					numberOfCars = c.Column1.Value

				}).ToList();
			}
		}


		public CarPictureModel GetNumberOfCarWithImage(string pictureName)
		{
			if (GlobalVariable.queryType == 0)
			{
				return (from car in DB.CARS
						where car.carPicture.Equals(pictureName)
						group car by car.carPicture into pictureGroup
						select new CarPictureModel()
						{
							carPictureLink = "assets/images/cars/" + pictureGroup.Key,
							carPictureName = pictureGroup.Key,
							numberOfCars = pictureGroup.Select(c => c.carNumber).Count()
						}).FirstOrDefault();
			}
			else
			{
				return DB.GetNumberOfCarWithImage(pictureName).Select(c => new CarPictureModel
				{
					carPictureLink = "assets/images/cars/" + c.carPicture,
					carPictureName = c.carPicture,
					numberOfCars = c.Column1.Value

				}).FirstOrDefault();
			}
		}

		public CarModel AddCar(CarModel carModel)
		{
			if (GlobalVariable.queryType == 0)
			{
				CAR car = new CAR
				{
					carNumber = carModel.carNumber,
					carTypeID = carModel.carTypeID,
					carKm = carModel.carKm,
					carPicture = carModel.carPicture,
					carInShape = carModel.carInShape,
					carAvaliable = carModel.carAvaliable,
					carBranchID = carModel.carBranchID
				};
				DB.CARS.Add(car);
				DB.SaveChanges();
				return GetOneCar(car.carNumber);
			}
			else
			{
				return DB.AddCar(carModel.carKm, carModel.carPicture, carModel.carInShape, carModel.carAvaliable, carModel.carNumber, carModel.carBranchID, carModel.carTypeID).Select(c => new CarModel
				{
					carNumber = c.carNumber,
					carTypeID = c.carTypeID,
					carKm = c.carKm,
					carPicture = c.carPicture != null ? "/assets/images/cars/" + c.carPicture : null,
					carInShape = c.carInShape,
					carAvaliable = c.carAvaliable,
					carBranchID = c.carBranchID
				}).SingleOrDefault();
			}
		}

		public CarModel UpdateCar(CarModel carModel)
		{
			if (GlobalVariable.queryType == 0)
			{
				CAR car = DB.CARS.Where(c => c.carNumber.Equals(carModel.carNumber)).SingleOrDefault();
				if (car == null)
					return null;
				car.carNumber = carModel.carNumber;
				car.carTypeID = carModel.carTypeID;
				car.carKm = carModel.carKm;
				car.carPicture = carModel.carPicture;
				car.carInShape = carModel.carInShape;
				car.carAvaliable = carModel.carAvaliable;
				car.carBranchID = carModel.carBranchID;
				DB.SaveChanges();
				return GetOneCar(car.carNumber);
			}
			else
			{
				return DB.UpdateCar(carModel.carKm, carModel.carPicture, carModel.carInShape, carModel.carAvaliable, carModel.carNumber, carModel.carBranchID, carModel.carTypeID).Select(c => new CarModel
				{
					carNumber = c.carNumber,
					carTypeID = c.carTypeID,
					carKm = c.carKm,
					carPicture = c.carPicture != null ? "/assets/images/cars/" + c.carPicture : null,
					carInShape = c.carInShape,
					carAvaliable = c.carAvaliable,
					carBranchID = c.carBranchID
				}).SingleOrDefault();
			}
		}

		public string DeleteCar(string num)
		{
			if (GlobalVariable.queryType == 0)
			{
				CAR car = DB.CARS.Where(c => c.carPicture.Equals(num)).SingleOrDefault();
				DB.CARS.Attach(car);
				if (car == null) return "";
				string str = car.carPicture;
				DB.CARS.Remove(car);
				DB.SaveChanges();
				return str;
			}
			else
			{
				return DB.DeleteCar(num).ToString();
			}
		}

		public CarModel UploadCarImage(string carNumber, string img)
		{
			CarModel carModel = GetOneCar(carNumber);
			carModel.carPicture = img;

			if (GlobalVariable.queryType == 0)
			{
				CAR car = DB.CARS.Where(c => c.carNumber.Equals(carNumber)).SingleOrDefault();
				if (car == null)
					return null;
				car.carPicture = img;
				DB.SaveChanges();
				return GetOneCar(car.carNumber);
			}
			else
			{
				return DB.UpdateCar(carModel.carKm, carModel.carPicture, carModel.carInShape, carModel.carAvaliable, carModel.carNumber, carModel.carBranchID, carModel.carTypeID).Select(c => new CarModel
				{
					carNumber = c.carNumber,
					carTypeID = c.carTypeID,
					carKm = c.carKm,
					carPicture = c.carPicture != null ? "/assets/images/cars/" + c.carPicture : null,
					carInShape = c.carInShape,
					carAvaliable = c.carAvaliable,
					carBranchID = c.carBranchID
				}).SingleOrDefault();
			}
		}
	}
}
