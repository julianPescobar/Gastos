using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlServerCe;
using System.Data;
using System.IO;

namespace MaterialSkinExample
{
    class Conexion
    {
        //reset key value : ALTER TABLE t1 ALTER COLUMN id IDENTITY (1,1)

        static string co = "\\db.sdf;Password=mataias;";
        
        static SqlCeConnection conn = new SqlCeConnection("Data Source=" + Directory.GetCurrentDirectory() + co);
       //para ahorrarse crear informe de ventas, informe de gastos etc. le paso un string
                                   //por ejemplo si abro el form informes y data = ventas, mostrar inf ventas, si data = gastos, mostrar inf gastos etc.


        internal static void abrir()
        {
            if (abierto() != true)
            {

                conn.Open();

            }
        } //abre conexion con la base local
        internal static void cerrar()
        {
            if (abierto() == true) Conexion.conn.Close();
        } //cierra conex. con la base local
        internal static bool abierto()
        {
            if (Conexion.conn.State.ToString() == "Open") return true; else return false;
        } //devuelve True si esta abierto, de lo contrario devuelve False
        internal static void Insertar(string tabla, string items, string valores, SqlCeCommand cmd)
        {
            string sqlquery = ("INSERT INTO " + tabla + " (" + items + ")" + "Values(" + valores + ")");

            cmd.CommandText = sqlquery;
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            //cmd.Parameters.AddWithValue("@client", clientName); EJEMPLO DE ADD PARAMETER
        }
        internal static void Actualizar(string tabla, string items, string where, string opciones, SqlCeCommand cmd)
        {
            string sqlquery = ("UPDATE " + tabla + " SET " + items + " " + where + " " + opciones);

            cmd.CommandText = sqlquery;
            cmd.Connection = conn;
            //cmd.Parameters.AddWithValue("@client", clientName); EJEMPLO DE ADD PARAMETER
            cmd.ExecuteNonQuery();

        }
        internal static void Eliminar(string tabla, string where, SqlCeCommand cmd)
        {
            string sqlquery = ("DELETE FROM " + tabla + " WHERE " + where);

            cmd.CommandText = sqlquery;
            cmd.Connection = conn;
            //cmd.Parameters.AddWithValue("@client", clientName); EJEMPLO DE ADD PARAMETER
            cmd.ExecuteNonQuery();

        }
        //la funcion Consultar hace un select en la base de datos y los devuelve en forma de Datatable.
        internal static DataTable Consultar(string items, string tabla, string where, string opts, SqlCeCommand cmd)
        {
            string sqlquery = ("SELECT " + items + " FROM " + tabla + " " + where + " " + opts);
            cmd.CommandText = sqlquery;
            cmd.Connection = conn;
            //cmd.Parameters.AddWithValue("@client", clientName); EJEMPLO DE ADD PARAMETER
            var dataReader = cmd.ExecuteReader();
            var dataTable = new DataTable();
            dataTable.Load(dataReader);
            return dataTable;
        }

        
    }
}
