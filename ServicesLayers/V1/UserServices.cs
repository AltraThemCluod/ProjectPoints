using DataLayers.Contract;
using EntitiesLayers.Contract;
using ProjectPoint.Common.Paging;
using ServicesLayers.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayers.V1
{
    public class UserServices : AbstractUserServices
    {
        private AbstractUserData abstractUserData;

        public UserServices(AbstractUserData abstractUserData)
        {
            this.abstractUserData = abstractUserData; 
        }

        public override SuccessResult<AbstractUser> UserSelect(long Id)
        {
            return this.abstractUserData.UserSelect(Id);
        }
        public override SuccessResult<AbstractUser> UserUpsert(AbstractUser abstractUser)
        {
            return this.abstractUserData.UserUpsert(abstractUser);
        }
        public override PagedList<AbstractUser> UserSelectAll(PageParam pageParam, string search)
        {
            return this.abstractUserData.UserSelectAll(pageParam, search);
        }
        public override bool UserDelete(long Id)
        {
            return this.abstractUserData.UserDelete(Id);
        }
        public override SuccessResult<AbstractUsersExperiencedetails> UsersExperienceInsert(AbstractUsersExperiencedetails abstractUsersExperiencedetails)
        {
            return this.abstractUserData.UsersExperienceInsert(abstractUsersExperiencedetails);
        }
    }
}
