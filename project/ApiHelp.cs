using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using CountryDB;
using CityDB;

namespace ApiHelp
{
    public class ApiH
    {
        private static string countryApi = "https://gist.githubusercontent.com/keeguon/2310008/raw/bdc2ce1c1e3f28f9cab5b4393c7549f38361be4e/countries.json";
        private static string cityApi = "https://raw.githubusercontent.com/lutangar/cities.json/master/cities.json";
        public static List<Country> countries = new List<Country>();
        public static List<City> cities = new List<City>();
        public static void GetAllCountries()
        {
            string response = GetResponse(countryApi);
            countries = JsonConvert.DeserializeObject<List<Country>>(response);
        }
        public static void GetAllCities()
        {
            string response = GetResponse(cityApi);
            cities = JsonConvert.DeserializeObject<List<City>>(response);
        }
        private static string GetResponse(string url)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            string response;
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }
            return response;
        }
    }
}
