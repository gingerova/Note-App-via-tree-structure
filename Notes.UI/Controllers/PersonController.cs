using Notes.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Notes.UI.Controllers
{
    public class PersonController : Controller
    {
        // GET: Person
        public ActionResult Index()
        {
            IEnumerable<PersonModel> personList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Person").Result;
            personList = response.Content.ReadAsAsync<IEnumerable<PersonModel>>().Result;
            return View(personList);
        }
    }
}