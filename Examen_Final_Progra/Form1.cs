using Examen_Final_Progra.Data;
using Examen_Final_Progra.Data.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Examen_Final_Progra
{
    public partial class Form1 : Form
    {
        private Heroes Cargar;
        List<Heroes> todos = new List<Heroes>();
        ConexionMySql HRS = new ConexionMySql();        
        ClsCursor cursor1 = new ClsCursor();
        public Form1()
        {
            InitializeComponent();
            Cargar = new Heroes();
        }

        private void buttonPrueba_Click(object sender, EventArgs e)
        {
            if (Cargar.Probar())
            {
                MessageBox.Show("Conexion establecida!");
            }
            else
            {
                MessageBox.Show("No se establecio conexión");
            }
        }

        private void buttonCargar_Click(object sender, EventArgs e)
        {
            todos = HRS.ObtenerPersonajes();
            dataGridViewDatos.DataSource = todos;

            if (todos.Count > 0)
            {
                cursor1.totalRegistros = todos.Count;
                MessageBox.Show("Total De Registros:" + cursor1.totalRegistros);

            }
            else
            {
                MessageBox.Show("No hay registros");

            }

        }

       private void buttonCrear_Click(object sender, EventArgs e)
{
    try
    {
        // Validaciones para asegurarse de que los campos no estén en blanco
        if (string.IsNullOrWhiteSpace(textBoxNombre.Text) ||
            string.IsNullOrWhiteSpace(textBoxAlias.Text) ||
            string.IsNullOrWhiteSpace(textBoxPoder.Text) ||
            string.IsNullOrWhiteSpace(textBoxAfiliacion.Text) ||
            string.IsNullOrWhiteSpace(textBoxNivelPoder.Text))
        {
            throw new Exception("Por favor, complete todos los campos antes de agregar el personaje.");
        }

        // Validación para el campo de nivel de poder
        if (!int.TryParse(textBoxNivelPoder.Text, out int nivelPoder) || nivelPoder < 1 || nivelPoder > 10)
        {
            throw new Exception("Por favor, ingrese un nivel de poder válido (1-10).");
        }

        string nombre = textBoxNombre.Text;
        string alias = textBoxAlias.Text;
        string superpoder = textBoxPoder.Text;
        string afiliacion = textBoxAfiliacion.Text;
        DateTime fechaliveaction = dateTimePickerFechaLiveAction.Value;

        // nuevo objeto para héroes
        Heroes nuevoHeroe = new Heroes(nombre, alias, superpoder, afiliacion, nivelPoder, fechaliveaction);

        // Llamar el método para insertar personaje
        Cargar.InsertarPersonaje(nuevoHeroe);
        MessageBox.Show("Personaje agregado correctamente.");

        // Actualizar la lista
        todos = HRS.ObtenerPersonajes();

        LimpiarCampos();
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error: " + ex.Message);
    }
}
        private void LimpiarCampos()
        {
            textBoxID.Clear();
            textBoxNombre.Clear();
            textBoxAlias.Clear();
            textBoxPoder.Clear();
            textBoxAfiliacion.Clear();
            textBoxNivelPoder.Clear();
            dateTimePickerFechaLiveAction.Value = DateTime.Now;
        }

        private void MostrarRegistroHeroes()
        {
            if (cursor1.current >= 0 && cursor1.current < cursor1.totalRegistros)
                if (cursor1.current >= 0 && cursor1.current < cursor1.totalRegistros)
                {
                    Heroes h = todos[cursor1.current];
                    textBoxID.Text = h.ID.ToString();
                    textBoxNombre.Text = h.Nombre.ToString();
                    textBoxAlias.Text = h.Alias.ToString();
                    textBoxPoder.Text = h.SuperPoder.ToString();
                    textBoxAfiliacion.Text = h.Afiliacion.ToString();
                    textBoxNivelPoder.Text = h.NivelPoder.ToString();
                    dateTimePickerFechaLiveAction.Text = h.FechaLiveAction.ToString();

                   
                    //incrementar el cursor y validar que no se pase del total de registros
                    cursor1.current++;
                    if (cursor1.current >= cursor1.totalRegistros)
                    {
                        cursor1.current = 0;
                        MessageBox.Show("Fin de los registros");
                    }
                }
        }

        private void buttonSiguiente_Click(object sender, EventArgs e)
        {
            MostrarRegistroHeroes();
        }

        private void MostrarRegistroAnterior()
        {
            cursor1.current--;
            if (cursor1.current >= 0 && cursor1.current < cursor1.totalRegistros)
                if (cursor1.current >= 0 && cursor1.current < cursor1.totalRegistros)
                {
                    Heroes h = todos[cursor1.current];
                    textBoxID.Text = h.ID.ToString();
                    textBoxNombre.Text = h.Nombre.ToString();
                    textBoxAlias.Text = h.Alias.ToString();
                    textBoxPoder.Text = h.SuperPoder.ToString();
                    textBoxAfiliacion.Text = h.Afiliacion.ToString();
                    textBoxNivelPoder.Text = h.NivelPoder.ToString();
                    dateTimePickerFechaLiveAction.Text = h.FechaLiveAction.ToString();
                }


                //decrementar el cursor y validar que no se pase del total de registros

                else
                {

                    MessageBox.Show("No hay mas registros anteriores");
                    cursor1.current++;
                }
        }

        private void buttonAnterior_Click(object sender, EventArgs e)
        {
            MostrarRegistroAnterior();
        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            int id;
            if (!int.TryParse(textBoxID.Text, out id))
            {
                MessageBox.Show("Esta Seguro de eliminar el registro");
                MessageBox.Show("Por favor ingrese un ID valido.");
                return;
            }
            DialogResult result = MessageBox.Show("¿Esta seguro de que desea eliminar este personaje?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
           
            if (result == DialogResult.Yes)
            try
            {
                Cargar.ElimiarPersonaje(id);
                MessageBox.Show("Personaje Eliminado Correctamente");
                todos = HRS.ObtenerPersonajes();
                dataGridViewDatos.DataSource = todos;
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar un personaje" +  ex.Message);
            }
        }

        private void buttonActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validación para asegurarse de que los campos no estén en blanco
                if (string.IsNullOrWhiteSpace(textBoxID.Text) ||
                    string.IsNullOrWhiteSpace(textBoxNombre.Text) ||
                    string.IsNullOrWhiteSpace(textBoxAlias.Text) ||
                    string.IsNullOrWhiteSpace(textBoxPoder.Text) ||
                    string.IsNullOrWhiteSpace(textBoxAfiliacion.Text) ||
                    string.IsNullOrWhiteSpace(textBoxNivelPoder.Text))
                {
                    throw new Exception("Por favor, complete todos los campos antes de actualizar el personaje.");
                }

                // Validación para el campo de ID
                if (!int.TryParse(textBoxID.Text, out int id))
                {
                    MessageBox.Show("Ingrese un ID válido.");
                    return;
                }

                // Validación para el campo de nivel de poder
                if (!int.TryParse(textBoxNivelPoder.Text, out int nivelPoder) || nivelPoder < 1 || nivelPoder > 10)
                {
                    MessageBox.Show("Por favor, ingrese un nivel de poder válido (1-10).");
                    return;
                }

                // Obtener valores de los campos
                string nombre = textBoxNombre.Text;
                string alias = textBoxAlias.Text;
                string superpoder = textBoxPoder.Text;
                string afiliacion = textBoxAfiliacion.Text;
                DateTime fechaliveaction = dateTimePickerFechaLiveAction.Value;

                // Crear objeto de héroe actualizado
                Heroes heroeActualizado = new Heroes(id, nombre, alias, superpoder, afiliacion, nivelPoder, fechaliveaction);

                // Cuadro de diálogo para confirmar la actualización
                DialogResult result = MessageBox.Show("¿Está seguro de que desea actualizar este personaje?", "Confirmar actualización", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Llamar método para actualizar el personaje
                    bool actualizado = Cargar.ActualizarPersonaje(heroeActualizado);
                    if (actualizado)
                    {
                        MessageBox.Show("Personaje actualizado correctamente.");
                        todos = HRS.ObtenerPersonajes();
                        dataGridViewDatos.DataSource = todos;
                       
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el personaje con el ID especificado.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
    }

