using Android.Graphics;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Poplection
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherNews : ContentPage
    {
        public WeatherNews()
        {
            InitializeComponent();
            GetWeather();
        }

        async void GetWeather()
        {
            
            var location = await Geolocation.GetLocationAsync(new GeolocationRequest
            {
                DesiredAccuracy = GeolocationAccuracy.Medium,
                Timeout = TimeSpan.FromSeconds(30)
            });

            string coordsLat = location.Latitude.ToString();
            string coordsLon = location.Longitude.ToString();



            string apiKey = "d8243851511b5c688efaec3ba36dcb13";
            string apiBase = "https://api.openweathermap.org/data/2.5/weather?lat="+ coordsLat + "&lon="+ coordsLon + "&appid=";
            string unit = "metric";

            

            string url = apiBase + apiKey + "&units=" + unit;

            var handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            string result = await client.GetStringAsync(url);

            

            var resultObject = JObject.Parse(result);
            string weatherDescription = resultObject["weather"][0] ["description"].ToString();
            string icon = resultObject["weather"][0]["icon"].ToString();
            string temperature = resultObject["main"]["temp"].ToString();
            string placeName = resultObject["name"].ToString();
            string country = resultObject["sys"]["country"].ToString();
            weatherDescription = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(weatherDescription);
            
            string WeatherIconURL = "https://openweathermap.org/img/wn/" + icon + ".png";
            WeatherImageLabel.Source = WeatherIconURL;

            WeatherPlaceLabel.Text = placeName + ", " + country;
            WeatherDescriptionLabel.Text = weatherDescription;
            WeatherTemperatureLabel.Text = temperature + "℃";
        }

    }
}