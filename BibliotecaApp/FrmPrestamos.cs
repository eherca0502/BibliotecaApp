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
    public partial class FrmPrestamos : Form
    {
        public FrmPrestamos()
        {
            InitializeComponent();
        }

        private void FrmPrestamos_Load(object sender, EventArgs e)
        {
            CargarPrestamos();
            CargarCombos();
        }
        private void CargarCombos()
        {

            cmbLibros.DataSource = LibroDAL.Listar().FindAll(l => l.Disponible);
            cmbLibros.DisplayMember = "Titulo";
            cmbLibros.ValueMember = "Id";


            cmbUsuarios.DataSource = UsuarioDAL.Listar();
            cmbUsuarios.DisplayMember = "Nombre";
            cmbUsuarios.ValueMember = "Id";
        }

        private void CargarPrestamos()
        {
            dgvPrestamos.DataSource = null;
            dgvPrestamos.DataSource = PrestamoDAL.Listar();
        }

        private void btnPrestar_Click(object sender, EventArgs e)
        {
            int libroId = (int)cmbLibros.SelectedValue;
            int usuarioId = (int)cmbUsuarios.SelectedValue;
            PrestamoDAL.Agregar(libroId, usuarioId);
            CargarPrestamos();
            CargarCombos();
        }

        private void btnDevolver_Click(object sender, EventArgs e)
        {
            if (dgvPrestamos.CurrentRow != null)
            {
                int prestamoId = (int)dgvPrestamos.CurrentRow.Cells["Id"].Value;
                int libroId = (int)dgvPrestamos.CurrentRow.Cells["LibroId"].Value;
                PrestamoDAL.Devolver(prestamoId, libroId);
                CargarPrestamos();
                CargarCombos();
            }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            CargarPrestamos();
        }
    }
}
