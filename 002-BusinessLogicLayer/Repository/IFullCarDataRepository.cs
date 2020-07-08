namespace RentCars
{
	public interface IFullCarDataRepository
	{
		FullCarDataModel GetCarAllData(string carNumber);
	}
}
