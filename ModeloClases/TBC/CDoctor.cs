using System;
using System.Data;
using System.Data.SqlClient;

namespace ModeloClases.CITAS
{
    public class CDoctor : CDatos
    {
        public CDoctor(string Conexion)
        {
            Con = Conexion;
        }

        public DataTable GetDatos(int Id, string NombreDoctor, string ApellidoDoctor, int Opcion)
        {

            try
            {
                Conectar(Con);
                SqlDataAdapter ObjAdapter = new SqlDataAdapter("SP_TBC_DOCTOR_GET", ObjConnection);
                ObjAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                ObjAdapter.SelectCommand.Parameters.AddWithValue("@ID", Id);
                ObjAdapter.SelectCommand.Parameters.AddWithValue("@DS_NOMBRE_DOCTOR", NombreDoctor);
                ObjAdapter.SelectCommand.Parameters.AddWithValue("@DS_APELLIDO_DOCTOR", ApellidoDoctor);
                ObjAdapter.SelectCommand.Parameters.AddWithValue("@OPCION", Opcion);

                ObjAdapter.Fill(dt);

                CerrarConexion();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dt;
        }
    }
}
