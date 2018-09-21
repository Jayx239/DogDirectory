using DogDirectory.Models.Data;
using DogDirectory.Models.Service;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DogDirectory.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() : base()
        {
            _dogService = new DogService();
        }

        DogService _dogService;

        public ActionResult Index()
        {
            return View();
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

        public ActionResult BreedList()
        {
            try
            {
                List<Breed> breedList = _dogService.GetBreedList();
                if (breedList == null || breedList.Count < 1)
                    ViewBag.Errors = new List<string>() { "Unable to retreive dog list" };
                return View(breedList);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Unable to retreive BreedList\n{0}", ex.StackTrace);
                ViewBag.Errors = new List<string>() { "Unable to retreive dog list" };
                return View();
            }
        }

        public ActionResult RandomImage(String name)
        {
            try
            {
                Image image = _dogService.GetImage(name);

                if (image.SourceString == "")
                {
                    ViewBag.Errors = new List<string>() { "Unable to retreive dog image" };
                }

                return View(image);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Unable to retreive BreedList\n{0}", ex.StackTrace);
                ViewBag.Errors = new List<string>() { "Unable to retreive dog image" };
                return View();
            }
        }

        public ActionResult DogDirectory2()
        {
            return View();
        }

        public ActionResult API2GetDogList()
        {

            JsonResult response = new JsonResult();
            response.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            try
            {
                List<Breed> breedList = _dogService.GetBreedList();
                if (breedList == null || breedList.Count == 0)
                {
                    Console.WriteLine("Error: Unable to retreive BreedList");
                    Response.StatusCode = 404;
                }
                else
                {
                    response.Data = breedList;
                }

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Unable to retreive BreedList\n{0}", ex.StackTrace);
                Response.StatusCode = 404;
                return response;
            }
        }

        [HttpPost]
        public ActionResult API2GetDogImage(string name)
        {
            JsonResult response = new JsonResult();
            response.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                Image image = _dogService.GetImage(name);

                if (image.SourceString == "")
                {
                    Console.WriteLine("Error: Unable to retreive Dog Image");
                    Response.StatusCode = 404;
                }
                else
                {
                    response.Data = image.SourceString;
                }
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Unable to retreive Dog Image\n{0}", ex.StackTrace);
                Response.StatusCode = 404;
                return response;
            }
        }
    }
}