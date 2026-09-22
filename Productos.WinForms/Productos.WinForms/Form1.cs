using System;
using System.Windows.Forms;

namespace Productos.WinForms
{
    public partial class Form1 : Form
    {
        private readonly ApiService _apiService;

        public Form1()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                var productos = await _apiService.ObtenerProductosAsync();
                dataGridView1.DataSource = productos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al conectar con la API: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var producto = new Producto
                {
                    Nombre = textBox1.Text,
                    Precio = decimal.Parse(textBox2.Text),
                    Stock = int.Parse(textBox3.Text)
                };

                var resultado = await _apiService.CrearProductoAsync(producto);

                if (resultado)
                {
                    MessageBox.Show("Producto guardado correctamente.");

                    var productos = await _apiService.ObtenerProductosAsync();
                    dataGridView1.DataSource = productos;

                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                }
                else
                {
                    MessageBox.Show("No se pudo guardar el producto.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Selecciona un producto.");
                    return;
                }

                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);

                var producto = new Producto
                {
                    Id = id,
                    Nombre = textBox1.Text,
                    Precio = decimal.Parse(textBox2.Text),
                    Stock = int.Parse(textBox3.Text)
                };

                var resultado = await _apiService.ActualizarProductoAsync(id, producto);

                if (resultado)
                {
                    MessageBox.Show("Producto actualizado correctamente.");

                    var productos = await _apiService.ObtenerProductosAsync();
                    dataGridView1.DataSource = productos;
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el producto.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Selecciona un producto.");
                    return;
                }

                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);

                var confirmar = MessageBox.Show(
                    "¿Estás seguro de que deseas eliminar este producto?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmar == DialogResult.Yes)
                {
                    var resultado = await _apiService.EliminarProductoAsync(id);

                    if (resultado)
                    {
                        MessageBox.Show("Producto eliminado correctamente.");

                        var productos = await _apiService.ObtenerProductosAsync();
                        dataGridView1.DataSource = productos;
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el producto.");
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