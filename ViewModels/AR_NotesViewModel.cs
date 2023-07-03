using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace AR_Notas.ViewModels
{
    internal class AR_NotesViewModel : ObservableObject, IQueryAttributable
    {
        private Models.AR_Notas _note;

        public AR_NotesViewModel()
        {
            _note = new Models.AR_Notas();
            SaveCommand = new AsyncRelayCommand(Save);
            DeleteCommand = new AsyncRelayCommand(Delete);
        }

        public AR_NotesViewModel(Models.AR_Notas note)
        {
            _note = note;
            SaveCommand = new AsyncRelayCommand(Save);
            DeleteCommand = new AsyncRelayCommand(Delete);
        }

        public string Text
        {
            get => _note.Text;
            set
            {
                if (_note.Text != value)
                {
                    _note.Text = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime Date => _note.Date;

        public string Identifier => _note.Filename;

        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }


        private async Task Save()
        {
            _note.Date = DateTime.Now;
            _note.Save();
            await Shell.Current.GoToAsync($"..?saved={_note.Filename}");
        }

        private async Task Delete()
        {
            _note.Delete();
            await Shell.Current.GoToAsync($"..?deleted={_note.Filename}");
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("load"))
            {
                _note = Models.AR_Notas.Load(query["load"].ToString());
                RefreshProperties();
            }
        }

        public void Reload()
        {
            _note = Models.AR_Notas.Load(_note.Filename);
            RefreshProperties();
        }

        private void RefreshProperties()
        {
            OnPropertyChanged(nameof(Text));
            OnPropertyChanged(nameof(Date));
        }
    }
}
