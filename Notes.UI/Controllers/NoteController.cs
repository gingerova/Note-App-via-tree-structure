using Notes.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Notes.UI.Controllers
{
    public class NoteController : Controller
    {
        // GET: Note
        public ActionResult Index()
        {
            IEnumerable<NoteModel> noteList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Note/GetNote").Result;
            noteList = response.Content.ReadAsAsync<IEnumerable<NoteModel>>().Result;
            return View(noteList);
        }
        public ActionResult NodeNotes(int id)
        {
            IEnumerable<NoteModel> noteList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Note/GetNote/"+id.ToString()).Result;
            noteList = response.Content.ReadAsAsync<IEnumerable<NoteModel>>().Result;
            return View(noteList);
        }
        public ActionResult NodeNote(int id)
        {
            NoteModel note= new NoteModel();
            note.NoteId = id;
            return View(note);
        }
        [HttpPost]
        public ActionResult NodeNote(NoteModel note)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Note/PostNote", note).Result;
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            NoteModel note= new NoteModel();
            return View(note);
        }
        [HttpPost]
        public ActionResult Create(NoteModel note)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Note/PostNote", note).Result;
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Note/DeleteNote/" + id.ToString()).Result;
            return RedirectToAction("Index");
        }
    }
}