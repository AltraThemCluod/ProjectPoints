using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayers.Contract
{
    public abstract class AbstractUser
    {
        public long Id { get; set; } 
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string EmailId { get; set; } 
        public string Address { get; set; } 
        public string DateOfBirth { get; set; } 
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UsersExperiencedetails { get; set; }
        public List<ConvertObjUsersExperiencedetails> UsersExperiencedetailsObj { get; set; }
    }

    public class AbstractUsersExperiencedetails
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Position { get; set; }
        public string CompanyName { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class ConvertObjUsersExperiencedetails
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Position { get; set; }
        public string CompanyName { get; set; }
    }


}
