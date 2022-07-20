using DataLayers.Contract;
using DataTables.Mvc;
using EntitiesLayers.Contract;
using EntitiesLayers.V1;
using Newtonsoft.Json;
using ProjectPoint.Common.Paging;
using ServicesLayers.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectPoint.Controllers
{
    public class UsersController : Controller
    {
        private readonly AbstractUserServices abstractUserServices = null;
        public UsersController(AbstractUserServices abstractUserServices)
        {
            this.abstractUserServices = abstractUserServices;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(string ri = "MA==")
        {
            ViewBag.Id = ri;
            return View();
        }

        [HttpPost]
        public ActionResult CreateUserDetails(int Id, string FirstName, string LastName, string EmailId, string Address, string DateOfBirth,string UsersExperienceDetailsData)
        {
            User user = new User();
            user.Id = Id;
            user.FirstName = FirstName;
            user.LastName = LastName;
            user.EmailId = EmailId;
            user.Address = Address;
            user.DateOfBirth = DateOfBirth;
            var result = abstractUserServices.UserUpsert(user);

            if (UsersExperienceDetailsData != null && UsersExperienceDetailsData != "")
            {
                user.UsersExperiencedetailsObj = JsonConvert.DeserializeObject<List<ConvertObjUsersExperiencedetails>>(Convert.ToString(UsersExperienceDetailsData));

                for (int i = 0; i < user.UsersExperiencedetailsObj.Count; i++)
                {
                    UsersExperiencedetails usersExperiencedetails = new UsersExperiencedetails();
                    usersExperiencedetails.UserId = result.Item.Id;
                    usersExperiencedetails.FromDate = user.UsersExperiencedetailsObj[i].FromDate;
                    usersExperiencedetails.ToDate = user.UsersExperiencedetailsObj[i].ToDate;
                    usersExperiencedetails.Position = user.UsersExperiencedetailsObj[i].Position;
                    usersExperiencedetails.CompanyName = user.UsersExperiencedetailsObj[i].CompanyName;
                    abstractUserServices.UsersExperienceInsert(usersExperiencedetails);
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult UserDetails(long Id)
        {
            var result = abstractUserServices.UserSelect(Id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult DeletedUserData(long Id = 0)
        {
            var result = abstractUserServices.UserDelete(Id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ManageUsers([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;
                string search = Convert.ToString(requestModel.Search.Value);
                var response = abstractUserServices.UserSelectAll(pageParam, search);

                totalRecord = (int)response.TotalRecords;
                filteredRecord = (int)response.TotalRecords;

                return Json(new DataTablesResponse(requestModel.Draw, response.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
        }
    }
}