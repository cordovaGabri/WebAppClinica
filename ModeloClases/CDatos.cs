using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloClases
{
    public class CDatos
    {
        public decimal CodigoError;
        public string MensajeError;
        //public DataSet ObjData;
        public bool Resultado;
        public object CodigoAuxiliar;
        public int FilasAfectadas;

        public SqlConnection ObjConnection;
        public DataTable dt = new DataTable();
        public string Con;
        public SqlDataAdapter ObjAdapter;
        public SqlCommand ObjCommand;
        public SqlParameter ObjParam;

        public void Conectar(string conexion)
        {
            ObjConnection = new SqlConnection(conexion);
        }

        public void CerrarConexion()
        {
            ObjConnection.Close();
            if (ObjConnection.State != ConnectionState.Closed)
            {
                ObjConnection.Close();
            }
        }
    }

    public enum TipoActualizacion
    {
        Adicionar = 1,
        Actualizar = 2,
        Eliminar = 3
    }
}
