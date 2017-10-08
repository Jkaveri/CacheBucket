#region

using System.Web.Mvc;
using WebApplication.Data;
using WebApplication.Helpers;
using WebApplication.Models;

#endregion

namespace WebApplication.Controllers
{
    public class HomeController : ApplicationBaseController
    {
        private readonly UserPreferenceHelper _userPreferenceHelper;

        public HomeController(UserPreferenceHelper userPreferenceHelper)
        {
            _userPreferenceHelper = userPreferenceHelper;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Index()
        {
            var userid = this.UserId;
            var model = new HomeViewModel();

            if (userid > 0)
            {
                foreach (var key in SampleData.GetSampleUserPreferenceKeys())
                {
                    var value = _userPreferenceHelper.Get(userid, key);
                    model.UserPreferences.Add(key, value);
                }
            }
            

            return View(model);
        }
    }
}