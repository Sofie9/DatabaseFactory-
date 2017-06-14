using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace TestWinForm.Models
{
    // ������
    class Model
    {
        public string TableName { get; set; }

        // ����� �������
        public List<string> ColumnNames { get; set; }

        public Model(string tableName)
        {
            this.TableName = tableName;

            //�������� ����� ���� �������
            ColumnNames = GetColumnNames().ToList();
        }

        // ��������� ��������������� ������
        public DataTable GetGrouped(IEnumerable<string> groupColumns)
        {    
            var fields = string.Join(",", groupColumns);
            //����������� ������
          
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
            {
                var sumField1 = ColumnNames[ColumnNames.Count - 2];
                var sumField2 = ColumnNames[ColumnNames.Count - 1];
              

                var sql = string.Format("select {0}, sum({1}) as {1}, sum({2}) as {2} from {3} group by {4}", fields, sumField1, sumField2, TableName, fields);

                if (string.IsNullOrEmpty(fields))
                    sql = string.Format("select sum({1}) as {1}, sum({2}) as {2} from {3}", fields, sumField1, sumField2, TableName, fields);
                try
                {
                    var table = new DataTable(TableName);
                    new SqlDataAdapter(sql, connection).Fill(table);
                    return table;
                }
                catch (ArgumentOutOfRangeException)
                {
                    throw new Exception("��������� ������� �� ����������.\r\n");
                }
                catch (SqlException)
                {
                    throw new Exception("�� ������� ��������� ������� ���� ������.");
                }

            }
        }
        // ��������� ���������������� ������
        public DataTable GetData()
        {
            //����������� ������
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
            {
                var fields = string.Join(",", ColumnNames);

                var sql = string.Format("select {0} from {1}", fields, TableName);
                try { 
                
                    var table = new DataTable(TableName);
                    new SqlDataAdapter(sql, connection).Fill(table);
                    return table;
                    }
                catch (ArgumentOutOfRangeException)
                {
                    throw new Exception("��������� ������� �� ����������.\r\n");
                }
                catch (SqlException)
                {
                    throw new Exception("�� ������� ������������ � ���� ������.");
                }
               
            }          
        }

        IEnumerable<string> GetColumnNames()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
            using (var command = connection.CreateCommand())
            {
                try {     
                command.CommandText = string.Format("select c.name from sys.columns c inner join sys.tables t on t.object_id = c.object_id and t.name = '{0}' and t.type = 'U'", TableName);
                connection.Open();}
                catch (SqlException )
                {
                    throw new Exception("�� ������� ������������ � ���� ������.");
                }
                using (var reader = command.ExecuteReader())
                    while (reader.Read())
                        yield return reader.GetString(0);
            }
        }
    }
}