using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Clases_Torneo;

namespace WPF_FIXTURE_JUGADOR
{
    /// <summary>
    /// Lógica de interacción para Ventana_Fixture.xaml
    /// </summary>
    public partial class Ventana_Fixture : Window
    {
        public Ventana_Fixture()
        {
            InitializeComponent();
        }
        private List<Fixture> listaFixture = new List<Fixture>();
        private async void btnObtenerDatos_Click(object sender, RoutedEventArgs e)
        {

            List<Fixture> lista = new List<Fixture>();
            dynamic a = await Fixture.ObtenerTodos();
            int i = 0;
            foreach (var x in a.data)
            {
                lista.Add(new Fixture(a.data[i].id_partido, a.data[i].torneo, a.data[i].equipolocal, a.data[i].equipovisitante, a.data[i].fecha));
                i++;
            }
            dgPartido.ItemsSource = lista;

        }

        private async void obtenerEquiposParaCombo()
        {
            List<Fixture> lista = new List<Fixture>();
            dynamic a = await Fixture.ObtenerTodos();
            int i = 0;
            foreach (var x in a.data)
            {
                lista.Add(new Fixture(a.data[i].id_partido, a.data[i].torneo, a.data[i].equipolocal, a.data[i].equipovisitante, a.data[i].fecha));
                i++;
            }
            cmbEquipo.ItemsSource = lista;
        }

        private async void obtenerEquipos()
        {    
            HashSet<string> nombreEquipo = new HashSet<string>();
            string equipolocal;
            string equipovisitante;
            dynamic a = await Fixture.ObtenerTodos();
            int i = 0;
            foreach (var x in a.data)
            {
                listaFixture.Add(new Fixture(a.data[i].id_partido, a.data[i].torneo, a.data[i].equipolocal, a.data[i].equipovisitante, a.data[i].fecha));
                equipolocal = a.data[i].equipolocal;
                equipovisitante = a.data[i].equipovisitante;
                nombreEquipo.Add(equipolocal);
                nombreEquipo.Add(equipovisitante);
                i++;
            }
            cmbEquipo.ItemsSource = nombreEquipo;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            obtenerEquipos();
        }

        private void btnVerPartido_Click(object sender, RoutedEventArgs e)
        {
            if (cmbEquipo.SelectedIndex != -1)
            {
                List<Fixture> lista = new List<Fixture>();

                var equipos = from fix in listaFixture
                              where fix.equipolocal == cmbEquipo.SelectedItem || fix.equipovisitante == cmbEquipo.SelectedItem
                              select fix;

                foreach (var a in equipos)
                {
                    lista.Add(new Fixture(a.id_partido, a.torneo, a.equipolocal, a.equipovisitante, a.fecha));
                }
                dgPartido.ItemsSource = lista;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            listaFixture.Clear();
            obtenerEquipos();
            MessageBox.Show("Lista de Equipos Actualizada");
        }
    }
}
