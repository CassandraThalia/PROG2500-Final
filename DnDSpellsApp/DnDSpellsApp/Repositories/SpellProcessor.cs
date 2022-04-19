using DnDSpellsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DnDSpellsApp.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DnDSpellsApp.Repositories
{
    public class SpellProcessor
    {
        //Web call to API
        public static async Task<SpellModel> LoadSpell(SpellViewModel spv)
        {
            string spellsUrl = "https://www.dnd5eapi.co/api/spells";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(spellsUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                   using (HttpContent content = response.Content)
                    {
                        var data = await content.ReadAsStringAsync();

                        if(data != null)
                        {
                            //dynamic dynJson = JsonConvert.DeserializeObject(data);
                            dynamic dynJson = JObject.Parse(data);

                            Console.WriteLine(dynJson);
                            var count = dynJson.count;

                            string[] arr = new string[count];
                            int i = 0;

                            //var dataObj = JObject.Parse(data);
                            foreach (var item in dynJson.results)
                            {
                                //spellViewModel.AddSpell(item.results.name);
                                //Console.WriteLine(item.result(i).names);
                                Console.WriteLine(item.name.ToString());

                                //Store name in an array to access when calling other API
                                arr[i] = item.name.ToString();
                                spv.AddSpell(item.name.ToString(), "", "", "");

                                i++;
                            }
                        }
                        else
                        {
                            throw new Exception("Data didn't load");
                        }
                    }
                }
                else
                {
                    //Weren't able to get the data, throw an exception
                    throw new Exception(response.ReasonPhrase);
                }
            }
            //https://alialhaddad.medium.com/how-to-fetch-data-in-c-net-core-ea1ab720e3f9

            string url = "";
            

            //if (index != "acid-arrow")
            //{
            //    url = $"https://www.dnd5eapi.co/api/spells/{ index}";
            //}
            //else
            //{
              url = "https://www.dnd5eapi.co/api/spells/acid-arrow";
            //}

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
