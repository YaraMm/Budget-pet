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

        public void UpdateNote(Note note)
        {
            _fileService.LoadBuffer();
            var listOfNotes = _fileService.Notes;
            int index = -1;
            for(int i = 0; i < listOfNotes.Count; i++)
            {
                if (listOfNotes[i].Id == note.Id) index = i;
            }
            listOfNotes[index] = note.Copy();
            _fileService.SaveChanges();
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
            return _fileService.Notes.Select(x => x.Copy()).ToList();
        }

        public void RemoveNote(Guid id)
        {
            _fileService.LoadBuffer();

            var removedNote = _fileService.Notes.FirstOrDefault(x => x.Id == id);

            if (removedNote == null)
            {
                throw new Exception($"Id {id} не найден");
            }
            _fileService.Notes.Remove(removedNote);
            _fileService.SaveChanges();
        }
        
    }
}
