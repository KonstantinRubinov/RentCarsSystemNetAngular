using RentCars.EntityDataBase;
using System.Collections.Generic;
using System.Linq;

namespace RentCars
{
	public class EntityBranchManager : EntityBaseManager, IBranchRepository
	{
		public List<BranchModel> GetAllBranchesNamesIds()
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.BRANCHES.Select(b => new BranchModel
				{
					branchID = b.branchID,
					branchName = b.branchName
				}).ToList();
			}
			else
			{
				return DB.GetAllBranchesNamesIds().Select(b => new BranchModel
				{
					branchID = b.branchID,
					branchName = b.branchName
				}).ToList();
			}
		}

		public List<BranchModel> GetAllBranches()
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.BRANCHES.Select(b => new BranchModel
				{
					branchID = b.branchID,
					branchName = b.branchName,
					branchAddress = b.branchAddress,
					branchLat = b.branchLat,
					branchLng = b.branchLng
				}).ToList();
			}
			else
			{
				return DB.GetAllBranches().Select(b => new BranchModel
				{
					branchID = b.branchID,
					branchName = b.branchName,
					branchAddress = b.branchAddress,
					branchLat = b.branchLat,
					branchLng = b.branchLng
				}).ToList();
			}
		}

		public BranchModel GetOneBranch(int branchID)
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.BRANCHES.Where(b => b.branchID == branchID).Select(b => new BranchModel
				{
					branchID = b.branchID,
					branchName = b.branchName,
					branchAddress = b.branchAddress,
					branchLat = b.branchLat,
					branchLng = b.branchLng
				}).SingleOrDefault();
			}
			else
			{
				return DB.GetOneBranchById(branchID).Select(b => new BranchModel
				{
					branchID = b.branchID,
					branchName = b.branchName,
					branchAddress = b.branchAddress,
					branchLat = b.branchLat,
					branchLng = b.branchLng
				}).SingleOrDefault();
			}
		}

		public BranchModel AddBranch(BranchModel branchModel)
		{
			if (GlobalVariable.queryType == 0)
			{
				BRANCH branch = new BRANCH
				{
					branchID = branchModel.branchID,
					branchName = branchModel.branchName,
					branchAddress = branchModel.branchAddress,
					branchLat = branchModel.branchLat,
					branchLng = branchModel.branchLng
				};
				DB.BRANCHES.Add(branch);
				DB.SaveChanges();
				return GetOneBranch(branchModel.branchID);
			}
			else
			{
				return DB.AddBranch(branchModel.branchAddress, branchModel.branchLat, branchModel.branchLng, branchModel.branchName).Select(b => new BranchModel
				{
					branchID = b.branchID,
					branchName = b.branchName,
					branchAddress = b.branchAddress,
					branchLat = b.branchLat,
					branchLng = b.branchLng
				}).SingleOrDefault();
			}
		}

		public BranchModel UpdateBranch(BranchModel branchModel)
		{
			if (GlobalVariable.queryType == 0)
			{
				BRANCH branch = DB.BRANCHES.Where(b => b.branchID == branchModel.branchID).SingleOrDefault();
				if (branch == null)
					return null;
				branch.branchName = branchModel.branchName;
				branch.branchID = branchModel.branchID;
				branch.branchAddress = branchModel.branchAddress;
				branch.branchLat = branchModel.branchLat;
				branch.branchLng = branchModel.branchLng;
				DB.SaveChanges();
				return GetOneBranch(branchModel.branchID);
			}
			else
			{
				return DB.UpdateBranch(branchModel.branchAddress, branchModel.branchLat, branchModel.branchLng, branchModel.branchName, branchModel.branchID).Select(b => new BranchModel
				{
					branchID = b.branchID,
					branchName = b.branchName,
					branchAddress = b.branchAddress,
					branchLat = b.branchLat,
					branchLng = b.branchLng
				}).SingleOrDefault();
			}
		}

		public int DeleteBranch(int branchID)
		{
			if (GlobalVariable.queryType == 0)
			{
				BRANCH branch = DB.BRANCHES.Where(b => b.branchID == branchID).SingleOrDefault();
				DB.BRANCHES.Attach(branch);
				if (branch == null)
					return 0;
				DB.BRANCHES.Remove(branch);
				DB.SaveChanges();
				return 1;
			}
			else
			{
				return DB.DeleteBranch(branchID);
			}
		}
	}
}