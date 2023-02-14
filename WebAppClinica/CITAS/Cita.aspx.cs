using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModeloClases;
using ModeloClases.CITAS;


namespace WebAppClinica.CITAS
{
    public partial class Cita : CGlobalVars, InterfaceMetodosForms
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            LblTituloPantalla.Text = "Programación de Citas";

            if (!IsPostBack)
            {
                TabContainer.ActiveTab = TabLista;
                LlenarDatos();
            }
        }

        public void LlenarDatos()
        {
            FillCboDoctor();
            FillCboPaciente();
            FillGVLista();
            FillGVDiagnostico();
        }

        public void Limpiar()
        {
            TxtIDCita.Text = "0";
            TxtFechaCita.Text = "";
            TxtHrCita.Text = "0";
            TxtObservacion.Text = "";
            FillCboDoctor();
            FillCboPaciente();
            FillGVDiagnostico();
            TabContainer.ActiveTab = TabDatos;
        }

        public void FillCboDoctor()
        {
            CDoctor obDoctor = new CDoctor(StrConexion);
            dt= obDoctor.GetDatos(0, "","", 0);

            CboDoctor.DataSource = dt;
            CboDoctor.DataBind();
        }

        public void FillCboPaciente()
        {
            CPaciente obPaciente = new CPaciente(StrConexion);
            dt = obPaciente.GetDatos(0, "", "", DateTime.Now, 0);

            CboPaciente.DataSource = dt;
            CboPaciente.DataBind();
        }

        public void FillGVLista()
        {
            CCita obCita = new CCita(StrConexion);
            dt = obCita.GetDatos(0, DateTime.Now, 0, 0,  0);

            GVLista.DataSource = dt;
            GVLista.DataBind();
        }

        public void FillGVDiagnostico()
        {
            CDiagnostico obDiagnostico = new CDiagnostico(StrConexion);
            dt = obDiagnostico.GetDatos(0, "", Convert.ToInt32(TxtIDCita.Text), 1);

            GVDiagnostico.DataSource = dt;
            GVDiagnostico.DataBind();
        }

        protected void BtnLimpiar_Click(object sender, ImageClickEventArgs e)
        {
            Limpiar();
        }

        protected void BtnAgregar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                CCita obCita = new CCita(StrConexion);
                objResultado = obCita.Actualizacion(0,Convert.ToDateTime(TxtFechaCita.Text), Convert.ToDateTime(TxtHrCita.Text), 
                    Convert.ToInt32(CboPaciente.SelectedValue)
                   , Convert.ToInt32(CboDoctor.SelectedValue), "ADMIN", TipoActualizacion.Adicionar);

                if (objResultado.CodigoError == 0)
                {
                    FillGVLista();
                    TxtIDCita.Text = Convert.ToString(objResultado.CodigoAuxiliar);
                    RetornarMsj(this,"Registro Creado Correctamente");
                }
                else
                {
                    RetornarMsj(this,objResultado.MensajeError);
                }
            }
            catch (Exception ex)
            {
                RetornarMsj(this,Convert.ToString(ex));
            }
        }

        protected void BtnActualizar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                CCita obCita = new CCita(StrConexion);
                objResultado = obCita.Actualizacion(Convert.ToInt32(TxtIDCita.Text), Convert.ToDateTime(TxtFechaCita.Text), Convert.ToDateTime(TxtHrCita.Text),
                    Convert.ToInt32(CboPaciente.SelectedValue)
                   , Convert.ToInt32(CboDoctor.SelectedValue), "ADMIN", TipoActualizacion.Actualizar);

                if (objResultado.CodigoError == 0)
                {
                    FillGVLista();
                    RetornarMsj(this, "Registro Actualizado Correctamente");
                }
                else
                {
                    RetornarMsj(this, objResultado.MensajeError);
                }
            }
            catch (Exception ex)
            {
                RetornarMsj(this, Convert.ToString(ex));
            }
        }

        protected void BtnEliminar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                CCita obCita = new CCita(StrConexion);
                objResultado = obCita.Actualizacion(Convert.ToInt32(TxtIDCita.Text), DateTime.Now, DateTime.Now,
                    Convert.ToInt32(CboPaciente.SelectedValue)
                   , Convert.ToInt32(CboDoctor.SelectedValue), "ADMIN", TipoActualizacion.Eliminar);

                if (objResultado.CodigoError == 0)
                {
                    FillGVLista();
                    RetornarMsj(this, "Registro Eliminado Correctamente");
                    Limpiar();
                    TabContainer.ActiveTab = TabLista;
                }
                else
                {
                    RetornarMsj(this, objResultado.MensajeError);
                }
            }
            catch (Exception ex)
            {
                RetornarMsj(this, Convert.ToString(ex));
            }
        }

        protected void GVLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Id = GVLista.SelectedIndex;

            TxtIDCita.Text = GVLista.DataKeys[Id].Values["ID"].ToString();
            CboDoctor.SelectedValue =  GVLista.DataKeys[Id].Values["ID_DOCTOR"].ToString();
            CboPaciente.SelectedValue = GVLista.DataKeys[Id].Values["ID_PACIENTE"].ToString();
            TxtFechaCita.Text = string.Format("{0:dd/MM/yyyy}", GVLista.DataKeys[Id].Values["FECH_CITA"].ToString());
            TxtHrCita.Text = GVLista.DataKeys[Id].Values["HR_CITA"].ToString();
            FillGVDiagnostico();
            TabContainer.ActiveTab = TabDatos;
        }

        protected void GVLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            FillGVLista();
            GVLista.PageIndex = e.NewPageIndex;
            GVLista.DataBind();
        }

        protected void GVDiagnostico_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                TextBox Iddeta = GVDiagnostico.Rows[e.RowIndex].FindControl("TxtIdDiagnostico") as TextBox;
                CDiagnostico objDiagnostico = new CDiagnostico(StrConexion);
                objResultado = objDiagnostico.Actualizacion(Convert.ToInt32(Iddeta.Text), "",0,
                        "", 
                        TipoActualizacion.Eliminar);
                if (objResultado.CodigoError == 0)
                {
                    FillGVDiagnostico();
                    RetornarMsj(this, "Registro Eliminado Correctamente");
                }
                else
                {
                    RetornarMsj(this, objResultado.MensajeError);
                }
            }
            catch (Exception ex)
            {
                RetornarMsj(this, Convert.ToString(ex));
            }
        }

        protected void TxtObservacion_TextChanged(object sender, EventArgs e)
        {
            var txt = (TextBox)sender;
            var Id = txt.Attributes["CommandName"];
            string[] Ids = Id.Split(',');
            try
            {
                int ID = Convert.ToInt32(Ids[0]);
                CDiagnostico objDiagnostico = new CDiagnostico(StrConexion);
                objResultado = objDiagnostico.Actualizacion(ID, txt.Text, Convert.ToInt32(TxtIDCita.Text),
                        "",
                        TipoActualizacion.Actualizar);
                if (objResultado.CodigoError == 0)
                {
                    FillGVDiagnostico();
                    RetornarMsj(this, "Registro Actualizado Correctamente");
                }
                else
                {
                    RetornarMsj(this, objResultado.MensajeError);
                }
            }
            catch (Exception ex)
            {
                RetornarMsj(this, Convert.ToString(ex));
            }

        }

        protected void btnAgregarObservacion_Click(object sender, EventArgs e)
        {
            try
            {
                CDiagnostico objDiagnostico = new CDiagnostico(StrConexion);
                objResultado = objDiagnostico.Actualizacion(0, TxtObservacion.Text, Convert.ToInt32(TxtIDCita.Text),
                        "",
                        TipoActualizacion.Adicionar);
                if (objResultado.CodigoError == 0)
                {
                    FillGVDiagnostico();
                    RetornarMsj(this, "Registro Agregado Correctamente");
                }
                else
                {
                    RetornarMsj(this, objResultado.MensajeError);
                }
            }
            catch (Exception ex)
            {
                RetornarMsj(this, Convert.ToString(ex));
            }
        }
    }
}