using Dapper;
using DataLayers.Contract;
using EntitiesLayers.Contract;
using EntitiesLayers.V1;
using EnvDTE;
using Newtonsoft.Json;
using ProjectPoint.Common.Paging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayers.V1
{
    public class UserData : AbstractUserData
    {
        public override SuccessResult<AbstractUser> UserUpsert(AbstractUser abstractUser)
        {
            SuccessResult<AbstractUser> User = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractUser.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@FirstName", abstractUser.FirstName, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@LastName", abstractUser.LastName, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@EmailId", abstractUser.EmailId, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Address", abstractUser.Address, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@DateOfBirth", abstractUser.DateOfBirth, dbType: DbType.String, direction: ParameterDirection.Input);
            
            using (SqlConnection con = new SqlConnection(CommonLayers.Configuration.ConnectionString))
            {
                var task = con.QueryMultiple("UserUpsert", param, commandType: CommandType.StoredProcedure);
                User = task.Read<SuccessResult<AbstractUser>>().SingleOrDefault();
                User.Item = task.Read<User>().SingleOrDefault();
            }
            return User;
        }

        public override SuccessResult<AbstractUser> UserSelect(long Id)
        {
            SuccessResult<AbstractUser> User = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction:ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(CommonLayers.Configuration.ConnectionString))
            {
                var task = con.QueryMultiple("UserSelect", param, commandType: CommandType.StoredProcedure);
                User = task.Read<SuccessResult<AbstractUser>>().SingleOrDefault();
                User.Item = task.Read<User>().SingleOrDefault();

                if (User.Item != null && User.Item.UsersExperiencedetails != null)
                {
                    User.Item.UsersExperiencedetailsObj = JsonConvert.DeserializeObject<List<ConvertObjUsersExperiencedetails>>(Convert.ToString(User.Item.UsersExperiencedetails));
                }

            }

            return User;
        }

        public override PagedList<AbstractUser> UserSelectAll(PageParam pageParam, string search)
        {
            PagedList<AbstractUser> User = new PagedList<AbstractUser>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(CommonLayers.Configuration.ConnectionString))
            {
                var task = con.QueryMultiple("UserSelectAll", param, commandType: CommandType.StoredProcedure);
                User.Values.AddRange(task.Read<User>());
                User.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return User;
        }

        public override bool UserDelete(long Id)
        {
            bool result = false;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(CommonLayers.Configuration.ConnectionString))
            {
                var task = con.Query<bool>("UserDelete", param, commandType: CommandType.StoredProcedure);
                result = task.SingleOrDefault<bool>();
            }
            return result;
        }

        public override SuccessResult<AbstractUsersExperiencedetails> UsersExperienceInsert(AbstractUsersExperiencedetails abstractUsersExperiencedetails)
        {
            SuccessResult<AbstractUsersExperiencedetails> UsersExperiencedetails = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractUsersExperiencedetails.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UserId", abstractUsersExperiencedetails.UserId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@FromDate", abstractUsersExperiencedetails.FromDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ToDate", abstractUsersExperiencedetails.ToDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Position", abstractUsersExperiencedetails.Position, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CompanyName", abstractUsersExperiencedetails.CompanyName, dbType: DbType.String, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(CommonLayers.Configuration.ConnectionString))
            {
                var task = con.QueryMultiple("UsersExperienceInsert", param, commandType: CommandType.StoredProcedure);
                UsersExperiencedetails = task.Read<SuccessResult<AbstractUsersExperiencedetails>>().SingleOrDefault();
                UsersExperiencedetails.Item = task.Read<UsersExperiencedetails>().SingleOrDefault();
            }
            return UsersExperiencedetails;
        }
    }
}
