using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Notes.API.Models;

namespace Notes.API.Controllers
{
    public class NoteController : ApiController
    {
        private NotesEntities db = new NotesEntities();

        // GET: api/Note
        public List<Note> GetNote()
        {
            List<Note> notes = db.Note.Where(x => x.NoteId == null).ToList();
            return notes;
        }

        // GET: api/Note/5
        [ResponseType(typeof(Note))]
        public IHttpActionResult GetNote(int id)
        {
            List<Note> note = db.Note.Where(x => x.NoteId==id).ToList();
            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }

        // PUT: api/Note/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNote(int id, Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != note.NoteId)
            {
                return BadRequest();
            }

            db.Entry(note).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Note
        [ResponseType(typeof(Note))]
        public IHttpActionResult PostNote(Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Note.Add(note);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = note.NoteId }, note);
        }

        // DELETE: api/Note/5
        [ResponseType(typeof(Note))]
        public IHttpActionResult DeleteNote(int id)
        {
            Note note = db.Note.Find(id);
            if (note == null)
            {
                return NotFound();
            }
            db.Note.Remove(note);
            db.SaveChanges();
            List<Note> allNotes = db.Note.Where(x => x.NoteId==id).ToList();
            DeleteNodeNote(allNotes);
            return Ok(note);
        }
        public void DeleteNodeNote(List<Note> allNotes)
        {
            if (allNotes != null)
            {
                foreach (var item in allNotes)
                {
                    Note byNoteId = db.Note.Where(x => x.Id == item.NoteId && item.NoteId != null).FirstOrDefault();
                    if (byNoteId == null)
                    {
                        List<Note> allNewNotes = db.Note.Where(x => x.NoteId == item.Id).ToList();
                        db.Note.Remove(item);
                        db.SaveChanges();
                        DeleteNodeNote(allNewNotes);
                    }
                }
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NoteExists(int id)
        {
            return db.Note.Count(e => e.NoteId == id) > 0;
        }
    }
}