using DnDSpellsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DnDSpellsApp.Repositories
{
    public class SpellProcessor
    {
        //Web call to API
        public static async Task<SpellModel> LoadSpell(string index = "acid-arrow")
        {
            string url = "";

            if(index != "acid-arrow")
            {
                url = $"https://www.dnd5eapi.co/api/spells/{ index}";
            }
            else
            {
                url = "https://www.dnd5eapi.co/api/spells/acid-arrow";
            }

            //Open up new call to web-browser/client, wait for response from call
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                //If the call is successful do something with it, read data that has come back
                if (response.IsSuccessStatusCode)
                {
                    //take Json and convert it to Model Type given, maps over anything that matches
                    SpellModel spell = await response.Content.ReadAsAsync<SpellModel>();

                    return spell;
                }
                else
                {
                    //Weren't able to get the data, throw an exception
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
