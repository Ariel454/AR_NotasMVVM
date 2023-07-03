namespace AR_Notas;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(Views.AR_NotasPage), typeof(Views.AR_NotasPage));
    }
}
