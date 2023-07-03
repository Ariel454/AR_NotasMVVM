namespace AR_Notas.Views;

public partial class AR_AllNotes : ContentPage
{
	public AR_AllNotes()
    {
        InitializeComponent();

    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        notesCollection.SelectedItem = null;
    }
}