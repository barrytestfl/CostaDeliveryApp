namespace CostaDeliveryApp.Pages;

public partial class CreateAccountPage : ContentPage
{
	public CreateAccountPage()
	{
		InitializeComponent();
	}

	private void CreateAccount_Clicked(object sender, EventArgs e)
	{
        //  Navigation.PushAsync(new StoresPage());
        AppShell appShell = new AppShell();
        Application.Current.MainPage = appShell;
    }
}