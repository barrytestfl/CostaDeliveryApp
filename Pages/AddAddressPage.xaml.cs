 
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using System.Linq;
using System.Net.NetworkInformation; 

namespace CostaDeliveryApp.Pages;

public partial class AddAddressPage : ContentPage
{
    private int CountClicked = 0;
    
    public AddAddressPage()
    {
       
        InitializeComponent();
       // ShowAlert();
    }

    private async  void ShowAlert()
    {
        await DisplayAlert("Source address", $"Please select your location", "Ok");
      
    }

    private void SaveAddress_Clicked(object sender, EventArgs e)
	{
		  Navigation.PopAsync();
	}

    private async void map_MapClicked(object sender, Microsoft.Maui.Controls.Maps.MapClickedEventArgs e)
    {
        CountClicked++;

        System.Diagnostics.Debug.WriteLine($"MapClick: {e.Location.Latitude}, {e.Location.Longitude}");
        if (CountClicked==1)
        {
           
            Pin boardwalkPin = new Pin
            {
                Location = new Location(e.Location.Latitude, e.Location.Longitude),
                Label = "Test ",
                Address = "IRan",
                Type = PinType.Place,
            };
            boardwalkPin.MarkerClicked += OnMarkerClickedAsync;
            map.Pins.Add(boardwalkPin);
           // await DisplayAlert("Source address", $"Please select your location", "Ok");
           
        }
        else if (CountClicked==2) 
        {
            
            Pin wharfPin = new Pin
            {
                Location = new Location(e.Location.Latitude, e.Location.Longitude),
                Label = "Samadi",
                Address = "Saman ",
                Type = PinType.SavedPin,
                
                
            };
            wharfPin.InfoWindowClicked += OnInfoWindowClickedAsync;
            map.Pins.Add(wharfPin);
            var routes = map.Pins;
          //  Distance distance4 = Distance.BetweenPositions(dis[0].Location, dis[1].Location);

            map.MapElements.Clear();
          
            
           

            var polyline = new Polyline
            {
                StrokeColor = Colors.Red,
                StrokeWidth = 12,
            };

            foreach (var route in routes)
            {
                polyline.Geopath.Add(route.Location);
            }

            map.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    routes.FirstOrDefault().Location, Distance.FromMiles(0.5)));
            map.MapElements.Add(polyline);

        }
        else
        {
            map.Pins.Clear();
            CountClicked = 0;
            if (DeviceInfo.Current.Platform == DevicePlatform.Android)
            {
                // opens the 'task chooser' so the user can pick Maps, Chrome or other mapping app
                await Launcher.OpenAsync("http://maps.google.com/?daddr=San+Francisco,+CA&saddr=Mountain+View");
            }
        }

    }
    private async void OnMarkerClickedAsync(object sender, PinClickedEventArgs e)
    {
        e.HideInfoWindow = true;
        string pinName = ((Pin)sender).Label;
        await DisplayAlert("Pin Clicked", $"{pinName} was clicked.", "Ok");
    }

    private async void OnInfoWindowClickedAsync(object sender, PinClickedEventArgs e)
    {
        string pinName = ((Pin)sender).Label;
        await DisplayAlert("Info Window Clicked", $"The info window was clicked for {pinName}.", "Ok");
    }
}