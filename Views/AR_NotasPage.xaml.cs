namespace AR_Notas.Views;

public partial class AR_NotasPage : ContentPage
{
    string _fileName = Path.Combine(FileSystem.AppDataDirectory, "notes.txt");
    
    public AR_NotasPage()
	{
        InitializeComponent();
    }
}