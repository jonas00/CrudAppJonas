using System;
using CrudApp_Core.Interface;
using CrudApp.Infrastructure;
using CrudApp_Core;
using System.Collections.Generic;
using System.Linq;

namespace CrudApp_Infrastructure
{
    public class NoteRepository : INoteRepository
    {
        NoteContext db = new NoteContext();
    

        public List<Note> GetNotesByUser(string UserId)
        {

           return db.Note.Where(n => n.Archiv == false && n.UserId == UserId).ToList();
        }

        public List<Note> GetArchive(string UserId)
        {
            return db.Note.Where(n => n.Archiv == true && n.UserId == UserId).ToList();
        }


        public void Create(Note note)
        {
            db.Note.Add(note);
            note.Created = DateTime.Now;
            db.SaveChanges();
        }

        public void Edit(Note note)
        {
            //db.Entry(note).State = System.Data.Entity.EntityState.Modified;
            note.Created = DateTime.Now;
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Note note = db.Note.Find(id);
            db.Note.Remove(note);
            db.SaveChanges();
        }

        public Note Get(int Id)
        {
            
            Note note = db.Note.Find(Id);
           
            return note;
        }
    }
}
