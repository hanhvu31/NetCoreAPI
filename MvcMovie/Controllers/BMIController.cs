using Microsoft.AspNetCore.Mvc; // 
using MvcMovie.Models; //

namespace MvcMovie.Controllers
{
    public class BMIController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(float weight, float height)
        {
            BMI model = new BMI
            {
                Weight = weight,
                Height = height,
                BMIValue = weight / (height * height)
            };

            if (model.BMIValue < 18.5)
                model.Result = "Gầy";
            else if (model.BMIValue < 24.9)
                model.Result = "Bình thường";
            else if (model.BMIValue < 29.9)
                model.Result = "Thừa cân";
            else
                model.Result = "Béo phì";

            ViewBag.BMI = model;
            return View();
        }
    }
}