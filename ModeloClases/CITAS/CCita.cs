using System;
using System.Data;
using System.Data.SqlClient;

namespace ModeloClases.CITAS
{
    public class CCita:CDatos
    {
        public CCita(string Conexion)
        {
            Con = Conexion;
        }

        public DataTable GetDatos(int Id, DateTime FechaCita, int IdPaciente, int IdDoctor, int Opcion)
        {
           
            try
            {
                Conectar(Con);
                ObjAdapter = new SqlDataAdapter("SP_TBC_CITA_GET", ObjConnection);
                ObjAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                ObjAdapter.SelectCommand.Parameters.AddWithValue("@ID", Id);
                ObjAdapter.SelectCommand.Parameters.AddWithValue("@FECH_CITA", FechaCita);
                ObjAdapter.SelectCommand.Parameters.AddWithValue("@ID_PACIENTE", IdPaciente);
                ObjAdapter.SelectCommand.Parameters.AddWithValue("@ID_DOCTOR", IdDoctor);
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

        public CDatos Actualizacion(int Id, DateTime FechaCita, DateTime HRCita, int IdPaciente, int IdDoctor, string LoginUsuario, TipoActualizacion OpcionActualizacion)
        {
            CDatos objResultado = new CDatos();
            try
            {
                string StrCommand = "";

                switch (OpcionActualizacion)
                {
                    case TipoActualizacion.Adicionar:
                        StrCommand = "SP_TBC_CITA_INSERT";
                        break;
                    case TipoActualizacion.Actualizar:
                        StrCommand = "SP_TBC_CITA_UPDATE";
                        break;
                    case TipoActualizacion.Eliminar:
                        StrCommand = "SP_TBC_CITA_DELETE";
                        break;
                }

                Conectar(Con);

                ObjCommand = new SqlCommand(StrCommand, ObjConnection);
                ObjParam = new SqlParameter();
                ObjCommand.CommandType = CommandType.StoredProcedure;

                if (OpcionActualizacion == TipoActualizacion.Adicionar)
                {
                    ObjParam = ObjCommand.Parameters.Add("@ID", SqlDbType.Int, 0);
                    ObjParam.Direction = ParameterDirection.Output;
                }
                else
                {
                    ObjCommand.Parameters.AddWithValue("@ID", Id);
                }

                ObjCommand.Parameters.AddWithValue("@FECH_CITA", FechaCita);
                ObjCommand.Parameters.AddWithValue("@HR_CITA", HRCita);
                ObjCommand.Parameters.AddWithValue("@ID_PACIENTE", IdPaciente);
                ObjCommand.Parameters.AddWithValue("@ID_DOCTOR", IdDoctor);
                ObjCommand.Parameters.AddWithValue("@LOGIN_USUARIO", LoginUsuario);

                ObjParam = ObjCommand.Parameters.Add("@FILAS_AFECTADAS", SqlDbType.Int, 0);
                ObjParam.Direction = ParameterDirection.Output;

                ObjParam = ObjCommand.Parameters.Add("@NumeroError", SqlDbType.Decimal);
                ObjParam.Precision = 38;
                ObjParam.Scale = 0;
                ObjParam.Direction = ParameterDirection.Output;

                ObjParam = ObjCommand.Parameters.Add("@MensajeError", SqlDbType.NVarChar, 4000);
                ObjParam.Direction = ParameterDirection.Output;

                ObjConnection.Open();
                ObjCommand.ExecuteNonQuery();

                objResultado.CodigoAuxiliar = (object)ObjCommand.Parameters["@ID"].Value;
                objResultado.FilasAfectadas = (int)ObjCommand.Parameters["@FILAS_AFECTADAS"].Value;
                objResultado.CodigoError = (decimal)ObjCommand.Parameters["@NumeroError"].Value;
                objResultado.MensajeError = (string)ObjCommand.Parameters["@MensajeError"].Value;

                 CerrarConexion();
            }
            catch (Exception ex)
            {
                objResultado.CodigoError = -1;
                objResultado.MensajeError = ex.Message;
            }

            return objResultado;

        }

    }
}
