using System.Net;
using System.Web.Mvc;
using Microsoft.Exchange.WebServices.Data;
using CrudApp_Core;
using System;
using Microsoft.AspNet.Identity;
using CrudApp_Core.Interface;
using CrudApp_Web.Models;

namespace CrudApp_Web.Controllers
{
    public class NoteController : Controller
    {

        private readonly INoteRepository _db;


        public NoteController(INoteRepository db)
        {
            _db = db;
        }


        // GET: Note
        [Authorize]
        public ActionResult Index(int? page, Note note)
        {
            var UserId = User.Identity.GetUserId();
            return View(_db.GetNotesByUser(UserId));
        }
        [Authorize]
        // GET: Note/Archiv
        public ActionResult Archiv()
        {
            var UserId = User.Identity.GetUserId();
            return View(_db.GetArchive(UserId));
        }
        [Authorize]
        // GET: Note/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = _db.Get(id.Value);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }
                                                                     
        // GET: Note/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Note/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Title,Text,Autor,Tag")] Note note)
        {
            if (ModelState.IsValid)
            {

                
                note.Email = User.Identity.GetUserName();
                note.Created = DateTime.Now;
                note.UserId = User.Identity.GetUserId();
                _db.Create(note);
                return RedirectToAction("Index");
            }

            return View(note);
        }

        // GET: Note/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Note note = _db.Get(id.Value);

            if (note.UserId != User.Identity.GetUserId())
            {
                return RedirectToAction("Index");
            }

            if (note == null)
            {
                return HttpNotFound();
            }

            return View(note);
        }


        // POST: Note/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Text,Autor,Tag")] Note note)
        {
            Note oldnote = _db.Get(note.Id);
            if (oldnote.UserId!= User.Identity.GetUserId())
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                TryUpdateModel(oldnote);
                
                oldnote.Email = User.Identity.GetUserName();
                oldnote.Created = DateTime.Now;
                oldnote.UserId = User.Identity.GetUserId();
                _db.Edit(oldnote);
                return RedirectToAction("Index");
            }
            return View(note);
        }

        // GET: Note/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = _db.Get(id.Value);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        // POST: Note/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Note note = _db.Get(id);
            if (User.Identity.GetUserId() == note.UserId)
            {
               _db.Delete(id);
            }
            
            return RedirectToAction("Index");
        }
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}


        public ActionResult Set_Archive(int Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = _db.Get(Id);

            if (note.UserId != User.Identity.GetUserId())
            {
                return RedirectToAction("Index");
            }

            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        [HttpPost, ActionName("Set_Archive")]
        public ActionResult Set_ArchiveConfirm(int Id)
        {
            Note note = _db.Get(Id);
            if (note.UserId != User.Identity.GetUserId())
            {
                return RedirectToAction("Index");
            }
            note.Archiv = true;
            _db.Edit(note);
            return RedirectToAction("Index");
        }

        public ActionResult Back(int Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = _db.Get(Id);

            if (note.UserId != User.Identity.GetUserId())
            {
                return RedirectToAction("Index");
            }

            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        [HttpPost, ActionName("Back")]
        public ActionResult BackConfirm(int Id)
        {
            Note note = _db.Get(Id);
            note.Archiv = false;
            _db.Edit(note);
            return RedirectToAction("Index");
        }


         public ActionResult Appointment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Note note = _db.Get(id.Value);

            if (note.UserId != User.Identity.GetUserId())
            {
                return RedirectToAction("Index");
            }

            if (note == null)
            {
                return HttpNotFound();
            }

            return View(note);
        }
                         //hallo
        [HttpPost, ActionName("Appointment")]
        public ActionResult AppointmentConfirm(Note note, LoginViewModel model)
        {
            ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2013);


            service.Credentials = new WebCredentials(note.Email, model.Password);

                       
            service.UseDefaultCredentials = true;

            service.AutodiscoverUrl(note.Email);

            Appointment appointment = new Appointment(service);


            appointment.Subject =  note.Title;
            appointment.Body = new MessageBody(note.Text);
            appointment.Start = Convert.ToDateTime(note.start);
            appointment.End = Convert.ToDateTime(note.end);


            appointment.Save();

            return RedirectToAction("Index");
        }


    }
}
