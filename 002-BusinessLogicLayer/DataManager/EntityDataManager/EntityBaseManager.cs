using RentCars.EntityDataBase;
using System;

namespace RentCars
{
	public class EntityBaseManager : IDisposable
	{
		protected RentCarEntities DB = new RentCarEntities();

		public void Dispose()
		{
			DB.Dispose();
		}
	}
}
