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
    public partial class FrmUsuario : Form
    {
        public FrmUsuario()
        {
            InitializeComponent();
        }

        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
        }
        private void CargarUsuarios()
        {
            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = UsuarioDAL.Listar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario
            {
                Nombre = txtNombre.Text,
                Email = txtEmail.Text
            };
            UsuarioDAL.Agregar(usuario);
            CargarUsuarios();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow != null)
            {
                Usuario usuario = new Usuario
                {
                    Id = (int)dgvUsuarios.CurrentRow.Cells[0].Value,
                    Nombre = txtNombre.Text,
                    Email = txtEmail.Text
                };
                UsuarioDAL.Editar(usuario);
                CargarUsuarios();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow != null)
            {
                int id = (int)dgvUsuarios.CurrentRow.Cells[0].Value;
                UsuarioDAL.Eliminar(id);
                CargarUsuarios();
            }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvUsuarios.CurrentRow != null)
            {
                txtNombre.Text = dgvUsuarios.CurrentRow.Cells[1].Value.ToString();
                txtEmail.Text = dgvUsuarios.CurrentRow.Cells[2].Value.ToString();
            }
        }
    }
}
