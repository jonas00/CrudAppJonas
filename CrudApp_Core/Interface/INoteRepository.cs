using System.Collections.Generic;

namespace CrudApp_Core.Interface
{
    public interface INoteRepository
    {
        List<Note> GetNotesByUser(string UserId);
        List<Note> GetArchive(string UserId);
        Note Get(int Id);
        void Create(Note note);
        void Edit(Note note);
        void Delete(int id);
                   
    }
}
