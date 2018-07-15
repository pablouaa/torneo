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
    public class Fixture
    {
        public dynamic id_partido { get; set; }
        public dynamic torneo { get; set; }
        public dynamic equipolocal { get; set; }
        public dynamic equipovisitante { get; set; }
        public dynamic fecha { get; set; }

        public override string ToString()
        {
            return (this.torneo+" "+this.equipolocal+" vs "+equipovisitante);
        }

        public Fixture() { }
        public Fixture(dynamic id_partido, dynamic torneo_id, dynamic equipo_local_id, dynamic equipo_visitante_id, dynamic fecha_nro)
        {
            this.id_partido = id_partido;
            this.torneo = torneo_id;
            this.equipolocal = equipo_local_id;
            this.equipovisitante = equipo_visitante_id;
            this.fecha = fecha_nro;
        }


        public static async Task<dynamic> ObtenerTodos()
        {
            string json = null;
            dynamic datos = null;
            //List<dynamic> lstpersonas = new List<dynamic>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:3000");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage respuesta = await client.GetAsync("/api/fixture");

                if (respuesta.IsSuccessStatusCode)
                {
                    json = respuesta.Content.ReadAsStringAsync().Result.Replace("\\", "").Trim(new char[1] { '"' });
                    //Console.WriteLine("Lo devuelto es " + json);
                    datos = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                    //lstpersonas = await respuesta.Content.ReadAsAsync<List<dynamic>>();
                    //lstpersonas.Add(json);
                }
            }
            return datos;
        }

        public static async Task<dynamic> ObtenerEquipos()
        {
            string json = null;
            dynamic equipo = null;

            using (var team = new HttpClient())
            {
                team.BaseAddress = new Uri("http://localhost:3000");
                team.DefaultRequestHeaders.Accept.Clear();
                team.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage respuesta = await team.GetAsync("/api/equipos");

                if (respuesta.IsSuccessStatusCode)
                {
                    json = respuesta.Content.ReadAsStringAsync().Result.Replace("\\", "").Trim(new char[1] { '"' });
                    equipo = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                }
            }
            return equipo;
        }
    }
}
