using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;

namespace PRYlab2VARELA
{
    internal class clsEmpleados
    {

        public void ConectarBase()
        {
            string rutaArchivo = @"../../archivos/EMPLEO.accdb";
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + rutaArchivo;
            OleDbConnection conn = new OleDbConnection(connectionString);

            try
            {
                conn.Open();
            }
            catch (OleDbException)
            {
                MessageBox.Show("Test");
            }
            finally
            {
                conn.Close();
            }
        }

        public void LlenarDatos(ComboBox cmbEmpleado)
        {
            string rutaArchivo = @"../../archivos/EMPLEO.accdb";
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + rutaArchivo;
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                string varSQL = "SELECT CODIGO FROM [DATOS LABORALES] ORDER BY 1 DESC";
                using (OleDbCommand cmdEmpleado = new OleDbCommand(varSQL, conn))
                {
                    using (OleDbDataReader reader = cmdEmpleado.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbEmpleado.Items.Add(reader["CODIGO"].ToString());
                        }
                    }
                }
            }
        }

        public void LlenarTreeViewConTablas(string codigo, TreeView treeView)
        {
            string rutaArchivo = @"../../archivos/EMPLEO.accdb";
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + rutaArchivo;

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                foreach (DataRow row in schemaTable.Rows)
                {
                    string tableName = row["TABLE_NAME"].ToString();
                    TreeNode tableNode = treeView.Nodes.Add(tableName);

                    LlenarNodosFiltrados(tableNode, codigo, tableName);
                }
            }
        }

        public void LlenarNodosFiltrados(TreeNode parentNode, string codigo, string tableName)
        {
            string rutaArchivo = @"../../archivos/EMPLEO.accdb";
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + rutaArchivo;

            string query = $"SELECT * FROM [{tableName}] WHERE CODIGO = '{codigo}'";

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string columnName = reader.GetName(i);
                                string columnValue = reader[i].ToString();
                                parentNode.Nodes.Add($"{columnName}: {columnValue}");
                            }
                        }
                    }
                }
            }

        }
    }
}
