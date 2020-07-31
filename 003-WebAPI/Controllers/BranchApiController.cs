using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RentCars
{
	//[EnableCors("*", "*", "*")]
	[RoutePrefix("api")]
	public class BranchApiController : ApiController
    {
		private IBranchRepository branchRepository;
		public BranchApiController(IBranchRepository _branchRepository)
		{
			branchRepository = _branchRepository;
		}
		
		[HttpGet]
		[Route("branches/NameId")]
		public HttpResponseMessage GetAllBranchesNamesIds()
		{
			try
			{
				List<BranchModel> allBranchesNamesIds = branchRepository.GetAllBranchesNamesIds();
				return Request.CreateResponse(HttpStatusCode.OK, allBranchesNamesIds);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[HttpGet]
		[Route("branches")]
		public HttpResponseMessage GetAllBranches()
		{
			try
			{
				List<BranchModel> allBranches = branchRepository.GetAllBranches();
				return Request.CreateResponse(HttpStatusCode.OK, allBranches);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[HttpGet]
		[Route("branches/{id}")]
		public HttpResponseMessage GetOneBranch(int id)
		{
			try
			{
				BranchModel oneBranch = branchRepository.GetOneBranch(id);
				return Request.CreateResponse(HttpStatusCode.OK, oneBranch);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[HttpPost]
		[Route("branches")]
		public HttpResponseMessage AddBranch(BranchModel branchModel)
		{
			try
			{
				if (branchModel == null)
				{
					return Request.CreateResponse(HttpStatusCode.BadRequest, "Data is null.");
				}
				if (!ModelState.IsValid)
				{
					Errors errors = ErrorsHelper.GetErrors(ModelState);
					return Request.CreateResponse(HttpStatusCode.BadRequest, errors);
				}

				BranchModel addedBranch = branchRepository.AddBranch(branchModel);
				return Request.CreateResponse(HttpStatusCode.Created, addedBranch);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[HttpPut]
		[Route("branches/{id}")]
		public HttpResponseMessage UpdateBranch(int id, BranchModel branchModel)
		{
			try
			{
				if (branchModel == null)
				{
					return Request.CreateResponse(HttpStatusCode.BadRequest, "Data is null.");
				}
				if (!ModelState.IsValid)
				{
					Errors errors = ErrorsHelper.GetErrors(ModelState);
					return Request.CreateResponse(HttpStatusCode.BadRequest, errors);
				}

				branchModel.branchID = id;
				BranchModel updatedBranch = branchRepository.UpdateBranch(branchModel);
				return Request.CreateResponse(HttpStatusCode.OK, updatedBranch);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[HttpDelete]
		[Route("branches/{id}")]
		public HttpResponseMessage DeleteBranch(int id)
		{
			try
			{
				int i = branchRepository.DeleteBranch(id);
				return Request.CreateResponse(HttpStatusCode.NoContent);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}
	}
}