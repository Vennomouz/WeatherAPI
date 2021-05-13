using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace WeatherAPI.Classes
{
    public class Metodos
    {
        private HttpClient client = new HttpClient();

        public Metodos()
        {
            client.BaseAddress = new Uri("http://api.openweathermap.org");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<dynamic> metodoGetAsync(string name)
        {
            //var name = "Guarulhos";
            HttpResponseMessage response = await client.GetAsync("/data/2.5/weather?q=" + name + "&appid=dd67e93c0fcabc25447b381cdae09ec1");

            string dados = await response.Content.ReadAsStringAsync();
            dynamic dadosJSON = JsonConvert.DeserializeObject(dados);
            return dadosJSON;

            //return dados;

        }

        //public dynamic retornaDados()
        //{
        //    var dadosTask = metodoGetAsync().Result;
        //    dynamic dadosJSON = JsonConvert.DeserializeObject(dadosTask);
        //    return dadosJSON;
        //}



    }
}

