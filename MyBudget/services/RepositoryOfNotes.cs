using MyBudget.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudget.services
{
    internal class RepositoryOfNotes
    {
        private readonly FileService _fileService;

        public RepositoryOfNotes()
        {
            _fileService = new FileService();
        }

        public void Save(Note note)
        {
            _fileService.LoadBuffer();
            note.Date = DateTime.Now;
            note.Id = Guid.NewGuid();
            _fileService.Notes.Add(note);
            _fileService.SaveChanges(); 
        }
        public List<Note> GetNotes()
        {
            _fileService.LoadBuffer();
            return _fileService.Notes;
        }
        
    }
}
