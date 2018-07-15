using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Clases_Torneo
{
    public class Jugador
    {

        public int id_jugador { get; set; }
        public string nro_documento { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string url_imagen_perfil { get; set; }
        public int total_tantos_convertidos { get; set; }
        public string fecha_nacimiento { get; set; }

        public Jugador() { }
        public Jugador(int v1, string v2, string v3, string v4, string v5, int v6, string v7)
        {
            this.id_jugador = v1;
            this.nro_documento = v2;
            this.nombres = v3;
            this.apellidos = v4;
            this.fecha_nacimiento = v5;
            this.total_tantos_convertidos = v6;
            this.url_imagen_perfil = v7;
        }

        public static async Task<dynamic> getJugador(int id)
        {
            string json = null;
            dynamic jugador = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:3000");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage respuesta = await client.GetAsync("/api/jugadores/" + id);

                if (respuesta.IsSuccessStatusCode)
                {
                    json = respuesta.Content.ReadAsStringAsync().Result;
                    jugador = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                }
            }
            return jugador;
        }
        public static async Task<dynamic> getJugadorDocumento(String doc)
        {
            string json = null;
            dynamic jugador = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:3000");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage respuesta = await client.GetAsync("/api/jugadores/buscar/" + doc);

                if (respuesta.IsSuccessStatusCode)
                {
                    json = respuesta.Content.ReadAsStringAsync().Result;
                    jugador = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                }
            }
            return jugador;
        }

        public static async Task<dynamic> getJugadores()
        {
            string json = null;
            dynamic jugadores = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:3000");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage respuesta = await client.GetAsync("/api/jugadores/");

                if (respuesta.IsSuccessStatusCode)
                {
                    json = respuesta.Content.ReadAsStringAsync().Result;
                    jugadores = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                }
            }
            return jugadores;
        }

        public static async Task<bool> postJugador(Jugador j)
        {
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:3000");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                JObject jugador = (JObject)JToken.FromObject(j);
                HttpResponseMessage respuesta = await client.PostAsJsonAsync("/api/jugadores", jugador);
                return respuesta.IsSuccessStatusCode;              
            }
        }

        public static async Task<bool> putJugador(Jugador j)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:3000");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                JObject jugador = (JObject)JToken.FromObject(j);
                HttpResponseMessage respuesta = await client.PutAsJsonAsync("/api/jugadores/"+j.id_jugador, jugador);
                return respuesta.IsSuccessStatusCode;
            }
        }

        public static async Task<bool> deleteJugador(Jugador j)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:3000");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage respuesta = await client.DeleteAsync("/api/jugadores/" + j.id_jugador);
                return respuesta.IsSuccessStatusCode;
            }
        }
    }
}
