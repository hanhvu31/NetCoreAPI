using Microsoft.AspNetCore.Mvc; // 
using MvcMovie.Models; //
namespace MvcMovie.Controllers
{
    public class Billcontroller : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Bill model)
        {
            if (ModelState.IsValid)

            { ViewBag.Result = $"Sản phẩm: {model.ProductName} - Tổng tiền: {model.TotalPrice:F2} VNĐ"; }
            return View(model);


        }

    }

}