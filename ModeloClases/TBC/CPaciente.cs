using System;
using System.Data;
using System.Data.SqlClient;

namespace ModeloClases.CITAS
{
    public class CPaciente : CDatos
    {
        public CPaciente(string Conexion)
        {
            Con = Conexion;
        }

        public DataTable GetDatos(int Id, string NombrePaciente, string ApellidoPaciente, DateTime FechaNac, int Opcion)
        {
            try
            {
                Conectar(Con);
                ObjAdapter = new SqlDataAdapter("SP_TBC_PACIENTE_GET", ObjConnection);
                ObjAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                ObjAdapter.SelectCommand.Parameters.AddWithValue("@ID", Id);
                ObjAdapter.SelectCommand.Parameters.AddWithValue("@DS_NOMBRE_PACIENTE", NombrePaciente);
                ObjAdapter.SelectCommand.Parameters.AddWithValue("@DS_APELLIDO_PACIENTE", ApellidoPaciente);
                ObjAdapter.SelectCommand.Parameters.AddWithValue("@FECH_NACIMIENTO", FechaNac);
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
