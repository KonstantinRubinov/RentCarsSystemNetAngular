using System.Collections.Generic;

namespace RentCars
{
	public interface IBranchRepository
	{
		List<BranchModel> GetAllBranchesNamesIds();
		List<BranchModel> GetAllBranches();
		BranchModel GetOneBranch(int branchID);
		BranchModel AddBranch(BranchModel branchModel);
		BranchModel UpdateBranch(BranchModel branchModel);
		int DeleteBranch(int branchID);
	}
}
