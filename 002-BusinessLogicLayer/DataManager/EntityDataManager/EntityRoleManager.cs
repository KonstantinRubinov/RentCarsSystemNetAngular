using RentCars.EntityDataBase;
using System.Collections.Generic;
using System.Linq;

namespace RentCars
{
	public class EntityRoleManager : EntityBaseManager, IRoleRepository
	{
		public List<RoleModel> GetAllRoles()
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.ROLES.Select(r => new RoleModel
				{
					userLevel = r.userLevel,
					userRole = r.userRole
				}).ToList();
			}
			else
			{
				return DB.GetAllRoles().Select(r => new RoleModel
				{
					userLevel = r.userLevel,
					userRole = r.userRole
				}).ToList();
			}
		}

		public RoleModel GetOneRole(int userLevel)
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.ROLES.Where(r => r.userLevel == userLevel).Select(r => new RoleModel
				{
					userLevel = r.userLevel,
					userRole = r.userRole
				}).SingleOrDefault();
			}
			else
			{
				return DB.GetOneRole(userLevel).Select(r => new RoleModel
				{
					userLevel = r.userLevel,
					userRole = r.userRole
				}).SingleOrDefault();
			}
		}

		public RoleModel GetOneRole(string userRole)
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.ROLES.Where(r => r.userRole.Equals(userRole)).Select(r => new RoleModel
				{
					userLevel = r.userLevel,
					userRole = r.userRole
				}).SingleOrDefault();
			}
			else
			{
				return DB.GetOneRoleByRole(userRole).Select(r => new RoleModel
				{
					userLevel = r.userLevel,
					userRole = r.userRole
				}).SingleOrDefault();
			}
		}

		public RoleModel AddRole(RoleModel roleModel)
		{
			if (GlobalVariable.queryType == 0)
			{
				ROLE role = new ROLE
				{
					userLevel = roleModel.userLevel,
					userRole = roleModel.userRole
				};
				DB.ROLES.Add(role);
				DB.SaveChanges();
				return GetOneRole(roleModel.userLevel);
			}
			else
			{
				return DB.AddRole(roleModel.userLevel, roleModel.userRole).Select(r => new RoleModel
				{
					userLevel = r.userLevel,
					userRole = r.userRole
				}).SingleOrDefault();
			}
		}

		public RoleModel UpdateRole(RoleModel roleModel)
		{
			if (GlobalVariable.queryType == 0)
			{
				ROLE role = DB.ROLES.Where(r => r.userLevel == roleModel.userLevel).SingleOrDefault();
				if (role == null)
					return null;
				role.userLevel = roleModel.userLevel;
				role.userRole = roleModel.userRole;
				DB.SaveChanges();
				return GetOneRole(roleModel.userLevel);
			}
			else
			{
				return DB.UpdateRole(roleModel.userLevel, roleModel.userRole).Select(r => new RoleModel
				{
					userLevel = r.userLevel,
					userRole = r.userRole
				}).SingleOrDefault();
			}
		}

		public int DeleteRole(string userRole)
		{
			if (GlobalVariable.queryType == 0)
			{
				ROLE role = DB.ROLES.Where(r => r.userRole.Equals(userRole)).SingleOrDefault();
				DB.ROLES.Attach(role);
				if (role == null)
					return 0;
				DB.ROLES.Remove(role);
				DB.SaveChanges();
				return 1;
			}
			else
			{
				return DB.DeleteRoleByRole(userRole);
			}
		}

		public int DeleteRole(int userLevel)
		{
			if (GlobalVariable.queryType == 0)
			{
				ROLE role = DB.ROLES.Where(r => r.userLevel == userLevel).SingleOrDefault();
				DB.ROLES.Attach(role);
				if (role == null)
					return 0;
				DB.ROLES.Remove(role);
				DB.SaveChanges();
				return 1;
			}
			else
			{
				return DB.DeleteRole(userLevel);
			}
		}
	}
}
