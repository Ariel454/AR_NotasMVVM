namespace AR_Notas.Views;

public partial class AR_AllNotes : ContentPage
{
	public AR_AllNotes()
    {
        InitializeComponent();

        BindingContext = new Models.AR_AllNotes();
    }

    protected override void OnAppearing()
    {
        ((Models.AR_AllNotes)BindingContext).LoadNotes();
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AR_NotasPage));
    }

    private async void notesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            // Get the note model
            var note = (Models.AR_Notas)e.CurrentSelection[0];

            // Should navigate to "NotePage?ItemId=path\on\device\XYZ.notes.txt"
            await Shell.Current.GoToAsync($"{nameof(AR_NotasPage)}?{nameof(AR_NotasPage.ItemId)}={note.Filename}");

            // Unselect the UI
            notesCollection.SelectedItem = null;
        }
    }
}