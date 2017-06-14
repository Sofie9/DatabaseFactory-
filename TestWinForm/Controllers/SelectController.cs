using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TestWinForm.Models;

namespace TestWinForm.Controllers
{
    // Контроллер
    class SelectController
    {// Модель
        Model Model { get; set; }
        // Грид
        DataGridView Grid { get; set; }
        // CheckedListBox
        CheckedListBox clbColumns { get; set; }


        public SelectController(DataGridView grid, CheckedListBox clbColumns, string table  )
        {                  
            Model = new Model(table);
            Grid = grid;
            this.clbColumns = clbColumns;

            //выборка  колонок, в которых будут чекбоксы  (кроме ID и двух последних)
            var cols = Model.ColumnNames.Where(c => c.ToLower() != "id");
            cols = cols.Take(cols.Count() - 2);
            clbColumns.Items.AddRange(cols.ToArray());
        }
        // Делается выборка, обновляется грид
        public void ShowGrouped()
        {   //выбранные колонки
            var cols = new List<string>();
                foreach (object item in clbColumns.CheckedItems)
                cols.Add(item.ToString());                    
            //сгруппированные данные
            var table = Model.GetGrouped(cols);

            //отображается
            Grid.DataSource = null;
            Grid.DataSource = table;
            }
            
        // Делается выборка без группировки, обновляется грид
        public void ShowData()
        {
            //получаем негруппированные данные
            var table = Model.GetData();

            //отображается
            Grid.DataSource = null;
            Grid.DataSource = table;
        }
        
        // Отображаются все колонки
        //public void ShowAll()
        //{
        //    ShowGrouped(true);
        //}
    }
}