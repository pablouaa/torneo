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
    /// Lógica de interacción para Ventana_Jugador.xaml
    /// </summary>
    public partial class Ventana_Jugador : Window
    {
        public Ventana_Jugador()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cargarTabla();
            limpiar();
        }
        private void renderView(dynamic w)
        {
            txtNombre.Text = w.data.nombres;
            txtApellido.Text = w.data.apellidos;
            txtFecha.Text = w.data.fecha_nacimiento;
            txtGoles.Text = w.data.total_tantos_convertidos;
            txtDocumento.Text = w.data.nro_documento;
            imgFoto.Source = w.data.url_imagen_perfil;
        }

        private void limpiar()
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtFecha.Text = string.Empty;
            txtGoles.Text = string.Empty;
            txtDocumento.Text = string.Empty;
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = new Uri("http://localhost:3000/foto/tu.jpg", UriKind.Absolute);
            bi3.EndInit();
            imgFoto.Source = bi3;
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }

        private async void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (validarCampos())
            {
                Jugador j = new Jugador();
                j.nombres = txtNombre.Text;
                j.apellidos = txtApellido.Text;
                j.fecha_nacimiento = txtFecha.Text;
                j.total_tantos_convertidos = int.Parse(txtGoles.Text);
                j.nro_documento = txtDocumento.Text;
                j.url_imagen_perfil = "http://localhost:3000/foto/tu.jpg";

                await Jugador.postJugador(j);
                cargarTabla();
            }
        }

        private async void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (dgGrilla.SelectedIndex != -1)
            {
                if (validarCampos())
                {
                    Jugador j = this.dgGrilla.SelectedItem as Jugador;
                    j.nombres = txtNombre.Text;
                    j.apellidos = txtApellido.Text;
                    j.fecha_nacimiento = txtFecha.Text;
                    j.total_tantos_convertidos = int.Parse(txtGoles.Text);
                    j.nro_documento = txtDocumento.Text;
                    // j.url_imagen_perfil = "http://localhost:3000/foto/tu.jpg";
                    await Jugador.putJugador(j);
                    cargarTabla();
                }
            }
            else
                MessageBox.Show("Debe seleccionar primeramente el jugador a modificar");
        }

        private async void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dgGrilla.SelectedIndex != -1)
            {
                Jugador j = this.dgGrilla.SelectedItem as Jugador;
                await Jugador.deleteJugador(j);
                cargarTabla();
            }
            else
                MessageBox.Show("Debe seleccionar primeramente un jugador a eliminar");
        }

        private async void btnCambiarImagen_Click(object sender, RoutedEventArgs e)
        {
            if (dgGrilla.SelectedIndex != -1)
            {
                Jugador j = this.dgGrilla.SelectedItem as Jugador;
                j.url_imagen_perfil = "http://localhost:3000/foto/Elias2.jpg";
                await Jugador.putJugador(j);
                cargarTabla();
            }
        }

        private async void cargarTabla()
        {
            List<Jugador> lista = new List<Jugador>();

            dynamic j = await Jugador.getJugadores();
            int i = 0;
            foreach (var x in j.data)
            {
                lista.Add(new Jugador
                  (
                    (int)j.data[i].id_jugador,
                    (String)j.data[i].nro_documento,
                    (String)j.data[i].nombres,
                    (String)j.data[i].apellidos,
                    (String)j.data[i].fecha_nacimiento,
                    (int)j.data[i].total_tantos_convertidos,
                    (String)j.data[i].url_imagen_perfil
                   )
                  );
                i++;
            }
            dgGrilla.ItemsSource = lista;
            limpiar();
        }

        private void SetJugador(Jugador j)
        {
            if (j != null)
            {
                txtNombre.Text = j.nombres;
                txtApellido.Text = j.apellidos;
                txtFecha.Text = j.fecha_nacimiento;
                txtGoles.Text = Convert.ToString(j.total_tantos_convertidos);
                txtDocumento.Text = j.nro_documento;
                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = new Uri(j.url_imagen_perfil, UriKind.Absolute);
                bi3.EndInit();
                imgFoto.Source = bi3;
            }
        }
        private void dgGrilla_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgGrilla.SelectedIndex != -1)
            {
                Jugador jSelec = this.dgGrilla.SelectedItem as Jugador;
                SetJugador(jSelec);
            }
        }

        private async void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (txtDocumento.Text != string.Empty)
            {
                string nroDocumento = txtDocumento.Text;
                dynamic j = await Jugador.getJugadorDocumento(nroDocumento);
                if (j != null)
                {
                    renderView(j);
                }
                else
                    MessageBox.Show("No se enctro el jugador con Nro. Documento " + nroDocumento);
            }
            else
                MessageBox.Show("Debe completar el campo Número de Documento");
        }

        private bool validarCampos()
        {
            if (
                txtNombre.Text == string.Empty ||
                txtApellido.Text == string.Empty ||
                txtFecha.Text == string.Empty ||
                txtGoles.Text == string.Empty ||
                txtDocumento.Text == string.Empty
                )
            {
                MessageBox.Show("Debe llenar todo los campos necesarios");
                return false;
            }
            if (fechaValida(txtFecha.Text) == false)
            {
                MessageBox.Show("Fecha Incorrecta, debe modificarla");
                return false;
            }
            else return true;
        }

        private bool fechaValida(string inValue)
        {
            bool bValid;
            try
            {
                DateTime myDT = DateTime.Parse(inValue);
                bValid = true;
            }
            catch (Exception e)
            {
                bValid = false;
            }

            return bValid;
        }
    }
}
