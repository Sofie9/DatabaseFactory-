using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using TestWinForm.Controllers;

namespace TestWinForm
{
    public partial class MainForm : Form
    {
        SelectController selController;

        public MainForm()
        {
            InitializeComponent();

            try
            {
                //создается  контроллер
                selController = new SelectController(dgvMain, clbColumns,"Registr");

                //делается выборка
                selController.ShowData();
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Указанной таблицы не существует.\r\n");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Ошибка подключения к базе данных.\r\n" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            try
            {
                selController.ShowGrouped();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Ошибка подключения к базе данных.\r\n" + ex.Message);
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Указанной таблицы не существует.\r\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btShowAll_Click(object sender, EventArgs e)
        {
            try
            { 
                selController.ShowData();
            }           
            catch (SqlException ex)
            {
                MessageBox.Show("Ошибка подключения к базе данных.\r\n" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void dgvMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
