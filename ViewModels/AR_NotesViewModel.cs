using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using AR_Notas.Models;
using System.Collections.ObjectModel;

namespace AR_Notas.ViewModels
{
    internal class AR_NotesViewModel : IQueryAttributable
    {
        public ObservableCollection<ViewModels.AR_NoteViewModel> AllNotes { get; }
        public ICommand NewCommand { get; }
        public ICommand SelectNoteCommand { get; }

        public AR_NotesViewModel()
        {
            AllNotes = new ObservableCollection<ViewModels.AR_NoteViewModel>(Models.AR_Notas.LoadAll().Select(n => new AR_NoteViewModel(n)));
            NewCommand = new AsyncRelayCommand(NewNoteAsync);
            SelectNoteCommand = new AsyncRelayCommand<ViewModels.AR_NoteViewModel>(SelectNoteAsync);
        }

        private async Task NewNoteAsync()
        {
            await Shell.Current.GoToAsync(nameof(Views.AR_NotasPage));
        }

        private async Task SelectNoteAsync(ViewModels.AR_NoteViewModel note)
        {
            if (note != null)
                await Shell.Current.GoToAsync($"{nameof(Views.AR_NotasPage)}?load={note.Identifier}");
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("deleted"))
            {
                string noteId = query["deleted"].ToString();
                AR_NoteViewModel matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

                // If note exists, delete it
                if (matchedNote != null)
                    AllNotes.Remove(matchedNote);
            }
            else if (query.ContainsKey("saved"))
            {
                string noteId = query["saved"].ToString();
                AR_NoteViewModel matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

                // If note is found, update it
                if (matchedNote != null)
                    matchedNote.Reload();

                // If note isn't found, it's new; add it.
                else
                    AllNotes.Add(new AR_NoteViewModel(Models.AR_Notas.Load(noteId)));
            }
        }

    }
}
