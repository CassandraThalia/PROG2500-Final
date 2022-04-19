using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DnDSpellsApp.Repositories
{
    public static class ApiHelper
    {
        //Want to open up Http CLient once per application, hence why it's static
        public static HttpClient ApiClient { get; set; }
        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri("https://www.dnd5eapi.co/api/spells");
            //ApiClient.BaseAddress = new Uri("https://www.dnd5eapi.co/");
            ApiClient.DefaultRequestHeaders.Accept.Clear();

            //Adds a header saying to give me json so we can parse into application
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}

//https://www.youtube.com/watch?v=aWePkE2ReGw