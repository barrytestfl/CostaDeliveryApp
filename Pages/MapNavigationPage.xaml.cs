using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.Tasks.NetworkAnalysis;
using Esri.ArcGISRuntime.UI;
using Map = Esri.ArcGISRuntime.Mapping.Map;

namespace CostaDeliveryApp.Pages;
[CostaDeliveryApp.Shared.Attributes.Sample(
    name: "Find route",
    category: "Network analysis",
    description: "Display directions for a route between two points.",
    instructions: "For simplicity, the sample comes loaded with a start and end stop. You can tap on the Find Route button to display a route between these stops. Once the route is generated, turn-by-turn directions are shown in a list.",
    tags: new[] { "directions", "driving", "navigation", "network", "network analysis", "route", "routing", "shortest path", "turn-by-turn" })]

public partial class MapNavigationPage : ContentPage
{
    // List of stops on the route ('from' and 'to')
    private List<Stop> _routeStops;

    // Graphics overlay to display stops and the route result
    private GraphicsOverlay _routeGraphicsOverlay;

    // URI for the San Diego route service
    private Uri _sanDiegoRouteServiceUri = new Uri("https://sampleserver6.arcgisonline.com/arcgis/rest/services/NetworkAnalysis/SanDiego/NAServer/Route");

    // URIs for picture marker images
    private Uri _checkedFlagIconUri = new Uri("https://static.arcgis.com/images/Symbols/Transportation/CheckeredFlag.png");

    private Uri _carIconUri = new Uri("https://static.arcgis.com/images/Symbols/Transportation/CarRedFront.png");

    public MapNavigationPage()
	{
		InitializeComponent();
        Initialize();
    }
    private void Initialize()
    {
        Esri.ArcGISRuntime.ArcGISRuntimeEnvironment.ApiKey = "AAPK0f9d0c6e8210408cb98690f0253288e6qrJH7yzYnPs_jO-VZLRG-FDV3NNGu83pRYg9blIfNJsREcI8SuK3Uwkp4oaWpDSU";

        // Define the route stop locations (points)
        MapPoint fromPoint = new MapPoint(-117.15494348793044, 32.706506537686927, SpatialReferences.Wgs84);
        MapPoint toPoint = new MapPoint(-117.14905088669816, 32.735308180609138, SpatialReferences.Wgs84);

        // Create Stop objects with the points and add them to a list of stops
        Stop stop1 = new Stop(fromPoint);
        Stop stop2 = new Stop(toPoint);
        _routeStops = new List<Stop> { stop1, stop2 };

        // Picture marker symbols: from = car, to = checkered flag
        PictureMarkerSymbol carSymbol = new PictureMarkerSymbol(_carIconUri);
        PictureMarkerSymbol flagSymbol = new PictureMarkerSymbol(_checkedFlagIconUri);

        // Add a slight offset (pixels) to the picture symbols.
        carSymbol.OffsetX = -carSymbol.Width / 2;
        carSymbol.OffsetY = -carSymbol.Height / 2;
        flagSymbol.OffsetX = -flagSymbol.Width / 2;
        flagSymbol.OffsetY = -flagSymbol.Height / 2;

        // Set the height and width.
        flagSymbol.Height = 60;
        flagSymbol.Width = 60;
        carSymbol.Height = 60;
        carSymbol.Width = 60;

        // Create graphics for the stops
        Graphic fromGraphic = new Graphic(fromPoint, carSymbol) { ZIndex = 1 };
        Graphic toGraphic = new Graphic(toPoint, flagSymbol) { ZIndex = 1 };

        // Create the graphics overlay and add the stop graphics
        _routeGraphicsOverlay = new GraphicsOverlay();
        _routeGraphicsOverlay.Graphics.Add(fromGraphic);
        _routeGraphicsOverlay.Graphics.Add(toGraphic);

        // Get an Envelope that covers the area of the stops (and a little more)
        Envelope routeStopsExtent = new Envelope(fromPoint, toPoint);
        EnvelopeBuilder envBuilder = new EnvelopeBuilder(routeStopsExtent);
        envBuilder.Expand(1.5);

        // Create a new viewpoint apply it to the map view when the spatial reference changes
        Viewpoint sanDiegoViewpoint = new Viewpoint(envBuilder.ToGeometry());
        MyMapView.SpatialReferenceChanged += (s, e) => MyMapView.SetViewpoint(sanDiegoViewpoint);

        // Add a new Map and the graphics overlay to the map view
        MyMapView.Map = new Map(BasemapStyle.ArcGISStreets);
        MyMapView.GraphicsOverlays.Add(_routeGraphicsOverlay);
    }

    private async void SolveRouteClick(object sender, EventArgs e)
    {
        try
        {
            // Create a new route task using the San Diego route service URI
            RouteTask solveRouteTask = await RouteTask.CreateAsync(_sanDiegoRouteServiceUri);

            // Get the default parameters from the route task (defined with the service)
            RouteParameters routeParams = await solveRouteTask.CreateDefaultParametersAsync();

            // Make some changes to the default parameters
            routeParams.ReturnStops = true;
            routeParams.ReturnDirections = true;

            // Set the list of route stops that were defined at startup
            routeParams.SetStops(_routeStops);

            // Solve for the best route between the stops and store the result
            RouteResult solveRouteResult = await solveRouteTask.SolveRouteAsync(routeParams);

            // Get the first (should be only) route from the result
            Route firstRoute = solveRouteResult.Routes.First();

            // Get the route geometry (polyline)
            Polyline routePolyline = firstRoute.RouteGeometry;

            // Create a thick purple line symbol for the route
            SimpleLineSymbol routeSymbol = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid,System.Drawing.Color.Purple, 8.0);

            // Create a new graphic for the route geometry and add it to the graphics overlay
            Graphic routeGraphic = new Graphic(routePolyline, routeSymbol) { ZIndex = 0 };
            _routeGraphicsOverlay.Graphics.Add(routeGraphic);

            // Get a list of directions for the route and display it in the list box
            DirectionsListBox.ItemsSource = firstRoute.DirectionManeuvers.Select(direction => direction.DirectionText);
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "OK");
        }
    }

    private void ResetClick(object sender, EventArgs e)
    {
        // Clear the list of directions
        DirectionsListBox.ItemsSource = null;

        // Remove the route graphic from the graphics overlay (only line graphic in the collection)
        int graphicsCount = _routeGraphicsOverlay.Graphics.Count;
        for (int i = graphicsCount; i > 0; i--)
        {
            // Get this graphic and see if it has line geometry
            Graphic g = _routeGraphicsOverlay.Graphics[i - 1];
            if (g.Geometry.GeometryType == GeometryType.Polyline)
            {
                // Remove the graphic from the overlay
                _routeGraphicsOverlay.Graphics.Remove(g);
            }
        }
    }

    private void ShowHideDirectionsClicked(object sender, EventArgs e)
    {
        DirectionsGrid.IsVisible = !DirectionsGrid.IsVisible;
    }
    private void Map_MapClicked(object sender, Mapsui.UI.Maui.MapClickedEventArgs e)
    {
        //map.Pins.Add(new Mapsui.UI.Maui.Pin()
        //{
        //    Label="Salam",
        //    Color=  Color.FromRgb(145,25,69),
        //    IsVisible=true,
        //    Type=Mapsui.UI.Maui.PinType.Pin,
        //    Position=e.Point,
        //    Address="hoooo",
            

        //});
       
 
    }
}