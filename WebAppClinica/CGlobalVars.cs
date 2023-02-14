

using System.Data;
using System.Web.UI;
using ModeloClases;

namespace WebAppClinica
{
    public class CGlobalVars : System.Web.UI.Page
    {
        public string StrConexion = System.Configuration.ConfigurationManager.AppSettings.Get("Conexion");

        public DataTable dt;
        public CDatos objResultado;

        public static void RetornarMsj(System.Web.UI.Page page, string Mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alertMessage", "alert('" + Mensaje + "')", true);
        }
    }
}