using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EXAMEN_FINAL_PROGRAMACION.DATA;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EXAMEN_FINAL_PROGRAMACION
{
    public partial class Form1 : Form
    {
        private SoundPlayer player;
        private void LlenarComboBoxTipo()
        {
            string[] tipos = { "Pistola", "Melee", "Rifle de asalto", "Katana", "Espada", "Escopeta", "Arma pesada", "Revólver" };
            comboBoxTipo.Items.AddRange(tipos);
        }

        private CONEXIONSQL conexionSql;
        public Form1()
        {
            InitializeComponent();
            conexionSql = new CONEXIONSQL();
            player = new SoundPlayer("C:/Users/USER/Downloads/examen/cyberpunk.wav");
        }

        private void buttonCargar_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            try
            {
                DataTable armas = conexionSql.LeerArmas();
                dataGridView1.DataSource = armas;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
        }

        private void buttonCrear_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = textBoxNombre.Text;
                string tipo = comboBoxTipo.Text; 
                int daño = int.Parse(textBoxDaño.Text);
                int cadencia = int.Parse(textBoxCadencia.Text);
                float precision = float.Parse(textBoxPresicion.Text);
                string descripcion = textBoxDescripcion.Text;

                conexionSql.Insertar(nombre, tipo, daño, cadencia, precision, descripcion);
                MessageBox.Show("Registro agregado exitosamente.");
                CargarDatos(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el registro: " + ex.Message);
            }
        }

        private void buttonActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Está seguro de que desea actualizar este registro?", "Confirmación", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int id = int.Parse(textBoxID.Text); 
                    string nombre = textBoxNombre.Text;
                    string tipo = comboBoxTipo.Text;
                    int daño = int.Parse(textBoxDaño.Text);
                    int cadencia = int.Parse(textBoxCadencia.Text);
                    float precision = float.Parse(textBoxPresicion.Text);
                    string descripcion = textBoxDescripcion.Text;

                    conexionSql.ActualizarArma(id, nombre, tipo, daño, cadencia, precision, descripcion);
                    MessageBox.Show("Registro actualizado exitosamente.");
                    CargarDatos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el registro: " + ex.Message);
            }


        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(textBoxID.Text); 

                if (MessageBox.Show("¿Está seguro de que desea eliminar este registro?", "Confirmación", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    conexionSql.EliminarArma(id);
                    CargarDatos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el registro: " + ex.Message);
            }


        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(textBoxID.Text); 
                ArmasModels arma = conexionSql.BuscarPorId(id);

                if (arma != null)
                {
                    textBoxNombre.Text = arma.Nombre;
                    comboBoxTipo.Text = arma.Tipo;
                    textBoxDaño.Text = arma.Daño.ToString();
                    textBoxCadencia.Text = arma.Cadencia.ToString();
                    textBoxPresicion.Text = arma.Presicion.ToString();
                    textBoxDescripcion.Text = arma.Descripcion;

                    
                }
                else
                {
                    MessageBox.Show("No se encontró ningún registro con el ID proporcionado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar el registro: " + ex.Message);
            }


        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
          LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            textBoxID.Text = "";
            textBoxNombre.Text = "";
            textBoxDaño.Text = "";
            textBoxCadencia.Text = "";
            textBoxPresicion.Text = "";
            textBoxDescripcion.Text = "";
            
            comboBoxTipo.SelectedIndex = -1;          
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                player.PlayLooping();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al reproducir la música: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        

        
       
        private void buttonReproducir_Click(object sender, EventArgs e)
        {
            try
            {
                player.PlayLooping(); // Reproduce la música en bucle al hacer clic en el botón Reproducir
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al reproducir la musica: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonPausar_Click(object sender, EventArgs e)
        {
            try
            {
                player.Stop(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al detener la musica: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
