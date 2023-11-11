using CostaDeliveryApp.Models;
using System.Collections.ObjectModel;

namespace CostaDeliveryApp.Pages;

public partial class StoresPage : ContentPage
{
    public ObservableCollection<Store> Stores { get; set; }
    public List<string> Addresses { get; set; }
    //public string CurrentAddress { get; set; }
    private double height = 0;
    private double width = 0;
    public StoresPage()
	{
		InitializeComponent();
        LoadData();
        BindingContext = this;
    }
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        addressesList.TranslationY = height;
        this.width = width;
        this.height = height;
    }

    private void LoadData()
    {


        Stores = new ObservableCollection<Store>
          {
               new Store
               {
                    Id = 1,
                    Header = "header1.jpg",
                    Logo = "logo1.jpg",
                    Name = "maugly",
                    DeliveryTime = "30 - 40 mins",
                    Minimum = 50,
                    ServiceFee = 10,
                    Rating = 4.5
               },
               new Store
               {
                    Id = 2,
                    Header = "header2.jpg",
                    Logo = "logo2.jpg",
                    Name = "Tacos MAUIciosos",
                    DeliveryTime = "1 - 3 hours",
                    Minimum = 150,
                    ServiceFee = 5,
                    Rating = 4.8
               },
               new Store
               {
                    Id = 1,
                    Header = "header3.jpg",
                    Logo = "logo3.jpg",
                    Name = "Burguermauia",
                    DeliveryTime = "15 - 30 mins",
                    Minimum = 250,
                    ServiceFee = 0,
                    Rating = 4.3
               },
               new Store
               {
                    Id = 1,
                    Header = "header4.jpg",
                    Logo = "logo4.jpg",
                    Name = "Captain MAUI",
                    DeliveryTime = "25 - 40 mins",
                    Minimum = 100,
                    ServiceFee = 25,
                    Rating = 4.6
               },
               new Store
               {
                    Id = 1,
                    Header = "header5.jpg",
                    Logo = "logo5.jpg",
                    Name = "MAUI's",
                    DeliveryTime = "45 - 60 mins",
                    Minimum = 5,
                    ServiceFee = 18,
                    Rating = 4.3
               },
          };
        lstStores.SelectionChanged += lstStores_SelectionChanged;
        lstStores.SelectionMode = SelectionMode.Single;
        lstStores.ItemsSource = Stores;
        Addresses = new List<string>()
          {
               "1888 London Drive",
               "722 Tehran Avenue",
               "3435 Tehran Lane",
               "510 Tehran Drive",
               "1030 Tehran Avenue",
               "3672 Tehran Mine Road",
               "4366 Tehran Lane"
          };
        CurrentAddress.Text = Addresses.FirstOrDefault();
    }

    private void SelectAddress_Clicked(object sender, EventArgs e)
    {
        addressesList.TranslateTo(0, height, 500, Easing.SinIn);
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
       addressesList.TranslateTo(0, 0, 500, Easing.SinOut);
    }

    private void Address_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var selectedAddress = (RadioButton)sender;
        CurrentAddress.Text = selectedAddress.Content.ToString();
    }

    private async void AddAddress_Clicked(object sender, EventArgs e)
    {
        await  Navigation.PushAsync(new AddAddressPage());
         //Navigation.PushAsync(new MapNavigationPage());
       // Navigation.PushAsync(new DisplayRouteLayerPage());
      //drive  Navigation.PushAsync(new NavigateRoutePage());
    }

    private  void lstStores_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
       // await Application.Current.MainPage.Navigation.PushAsync(new StorePage());
    }
}