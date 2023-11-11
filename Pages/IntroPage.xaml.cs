using CostaDeliveryApp.Models;
using System.Collections.ObjectModel;

namespace CostaDeliveryApp.Pages;

public partial class IntroPage : ContentPage
{
    public ObservableCollection<IntroModel> IntroItems { get; set; }
    public IntroPage()
	{
		InitializeComponent();
        IntroItems = new ObservableCollection<IntroModel>();
        IntroItems.Add(new IntroModel()
        {
            Title = "Simplify your day-to-day",
            Description = "Maintenance Work Orders",
            Image = "report.jpg"
        });
        IntroItems.Add(new IntroModel()
        {
            Title = "Track",
            Description = " Inspection & Corrective Actions",
            Image = "appmaintenance.jpg"
        });
        IntroItems.Add(new IntroModel()
        {
            Title = "Manage ",
            Description = " Equipment and Part Inventory",
            Image = "equipment.jpg"
        });

        IntroItems.Add(new IntroModel()
        {
            Title = "Control Your ",
            Description = " Daily Operations",
            Image = "housekeeper.jpg"
        });
        BindingContext = this;
    }

    private async void Register_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateAccountPage());
    }

    private async void Login_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HomePage());
        
    }
}