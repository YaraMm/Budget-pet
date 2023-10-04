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

        public void Remove(Guid id)
        {
            _fileService.LoadBuffer();
            var res = -1;
            for(int i = 0; i < _fileService.Notes.Count; i++)
            {
                if (_fileService.Notes[i].Id == id)
                {
                    res = i;
                    break;
                }
            }
            if (res == -1)
            {
                throw new Exception($"Id {id} не найден");
            }
            _fileService.Notes.RemoveAt(res);
            _fileService.SaveChanges();
        }

    }
}
