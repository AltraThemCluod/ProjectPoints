using DataLayers.Contract;
using EntitiesLayers.Contract;
using ProjectPoint.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayers.Contract
{
    public abstract class AbstractUserServices
    {
        public abstract SuccessResult<AbstractUser> UserUpsert(AbstractUser abstractUser);
        public abstract SuccessResult<AbstractUser> UserSelect(long Id);
        public abstract PagedList<AbstractUser> UserSelectAll(PageParam pageParam, string search);
        public abstract bool UserDelete(long Id);
        public abstract SuccessResult<AbstractUsersExperiencedetails> UsersExperienceInsert(AbstractUsersExperiencedetails abstractUsersExperiencedetails);
    }
}
