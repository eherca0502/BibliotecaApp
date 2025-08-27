using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BibliotecaApp
{
    public partial class FormLibros : Form
    {
        public FormLibros()
        {
            InitializeComponent();
        }

        private void FormLibros_Load(object sender, EventArgs e)
        {
            CargarLibros();
        }
        private void CargarLibros()
        {
            dgvLibros.DataSource = null;
            dgvLibros.DataSource = LibroDAL.Listar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Libro libro = new Libro
            {
                Titulo = txtTitulo.Text,
                Autor = txtAutor.Text,
                Anio = int.Parse(txtAño.Text),
                Disponible = chkDisponible.Checked
            };
            LibroDAL.Agregar(libro);
            CargarLibros();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvLibros.CurrentRow != null)
            {
                Libro libro = new Libro
                {
                    Id = (int)dgvLibros.CurrentRow.Cells[0].Value,
                    Titulo = txtTitulo.Text,
                    Autor = txtAutor.Text,
                    Anio = int.Parse(txtAño.Text),
                    Disponible = chkDisponible.Checked
                };
                LibroDAL.Editar(libro);
                CargarLibros();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvLibros.CurrentRow != null)
            {
                int id = (int)dgvLibros.CurrentRow.Cells[0].Value;
                LibroDAL.Eliminar(id);
                CargarLibros();
            }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            CargarLibros();
        }

        private void dgvLibros_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvLibros.CurrentRow != null)
            {
                txtTitulo.Text = dgvLibros.CurrentRow.Cells[1].Value.ToString();
                txtAutor.Text = dgvLibros.CurrentRow.Cells[2].Value.ToString();
                txtAño.Text = dgvLibros.CurrentRow.Cells[3].Value.ToString();
                chkDisponible.Checked = (bool)dgvLibros.CurrentRow.Cells[4].Value;
            }
        }
    }
}
