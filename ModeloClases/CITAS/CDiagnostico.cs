using System;
using System.Data;
using System.Data.SqlClient;

namespace ModeloClases.CITAS
{
    public class CDiagnostico:CDatos
    {
        public CDiagnostico(string Conexion)
        {
            Con = Conexion;
        }

        public DataTable GetDatos(int Id, string Observacion, int IdCita, int Opcion)
        {
           
            try
            {
                Conectar(Con);
                ObjAdapter = new SqlDataAdapter("SP_TB_DIAGNOSTICO_GET", ObjConnection);
                ObjAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                ObjAdapter.SelectCommand.Parameters.AddWithValue("@ID", Id);
                ObjAdapter.SelectCommand.Parameters.AddWithValue("@DS_OBSERVACION", Observacion);
                ObjAdapter.SelectCommand.Parameters.AddWithValue("@ID_CITA", IdCita);
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

        public CDatos Actualizacion(int Id, string Observacion, int IdCita, string LoginUsuario, TipoActualizacion OpcionActualizacion)
        {
            CDatos objResultado = new CDatos();
            try
            {
                string StrCommand = "";

                switch (OpcionActualizacion)
                {
                    case TipoActualizacion.Adicionar:
                        StrCommand = "SP_TB_DIAGNOSTICO_INSERT";
                        break;
                    case TipoActualizacion.Actualizar:
                        StrCommand = "SP_TB_DIAGNOSTICO_UPDATE";
                        break;
                    case TipoActualizacion.Eliminar:
                        StrCommand = "SP_TB_DIAGNOSTICO_DELETE";
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

                ObjCommand.Parameters.AddWithValue("@DS_OBSERVACION", Observacion);
                ObjCommand.Parameters.AddWithValue("@ID_CITA", IdCita);
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
