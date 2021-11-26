﻿using FundooModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundoManager.Interfaces
{
    public interface INotesManager
    {
        Task<string> AddNewNote(NotesModel notesModel);
        Task<string> DeleteNotes(string Id);
        Task<List<NotesModel>> ShowAllNotes();
        Task<string> UpdateNote(NotesModel notesModel);
        Task<string> ChangeColor(NotesModel notesModel);
        Task<string> CheckArchive(NotesModel notesModel);
        Task<string> CheckPin(NotesModel notesModel);
        Task<string> TrashNote(NotesModel notesModel);
        Task<List<NotesModel>> ArchiveNotes(long Id);
        Task<List<NotesModel>> TrashNotes(long Id);
        Task<string> BackNotes(NotesModel notesModel);
        string AddImage(IFormFile file);
    }
    
    
}