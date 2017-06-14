using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TestWinForm.Models;

namespace TestWinForm.Controllers
{
    // ����������
    class SelectController
    {// ������
        Model Model { get; set; }
        // ����
        DataGridView Grid { get; set; }
        // CheckedListBox
        CheckedListBox clbColumns { get; set; }


        public SelectController(DataGridView grid, CheckedListBox clbColumns, string table  )
        {                  
            Model = new Model(table);
            Grid = grid;
            this.clbColumns = clbColumns;

            //�������  �������, � ������� ����� ��������  (����� ID � ���� ���������)
            var cols = Model.ColumnNames.Where(c => c.ToLower() != "id");
            cols = cols.Take(cols.Count() - 2);
            clbColumns.Items.AddRange(cols.ToArray());
        }
        // �������� �������, ����������� ����
        public void ShowGrouped()
        {   //��������� �������
            var cols = new List<string>();
                foreach (object item in clbColumns.CheckedItems)
                cols.Add(item.ToString());                    
            //��������������� ������
            var table = Model.GetGrouped(cols);

            //������������
            Grid.DataSource = null;
            Grid.DataSource = table;
            }
            
        // �������� ������� ��� �����������, ����������� ����
        public void ShowData()
        {
            //�������� ���������������� ������
            var table = Model.GetData();

            //������������
            Grid.DataSource = null;
            Grid.DataSource = table;
        }
        
        // ������������ ��� �������
        //public void ShowAll()
        //{
        //    ShowGrouped(true);
        //}
    }
}