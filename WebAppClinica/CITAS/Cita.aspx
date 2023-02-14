﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterForms.master" AutoEventWireup="true" CodeBehind="Cita.aspx.cs" Inherits="WebAppClinica.CITAS.Cita" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <br />
            <div class="container text-center">
                <div class="row">
                    <div class="col">
                        <asp:Label ID="LblTituloPantalla" runat="server" Text="Label" Width="100%" Font-Bold="True" Font-Size="Medium" CssClass="form-label"></asp:Label>
                    </div>
                </div>
            </div>
            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Right">
                <asp:ImageButton ID="BtnLimpiar" runat="server" ImageUrl="~/imgs/icons8-create-24.png" ToolTip="Limpiar" OnClick="BtnLimpiar_Click" />
                <asp:ImageButton ID="BtnAgregar" runat="server" ImageUrl="~/imgs/icons8-add-new-24.png" ToolTip="Agregar Nuevo Registro" OnClick="BtnAgregar_Click" />
                <asp:ImageButton ID="BtnActualizar" runat="server" ImageUrl="~/imgs/icons8-save-24.png" ToolTip="Actualizar" OnClick="BtnActualizar_Click" />
                <asp:ImageButton ID="BtnEliminar" runat="server" ImageUrl="~/imgs/icons8-remove-24.png" ToolTip="Eliminar" OnClick="BtnEliminar_Click" />
            </asp:Panel>
            <ajaxToolkit:TabContainer ID="TabContainer" runat="server" ActiveTabIndex="1" Width="100%">
                <ajaxToolkit:TabPanel runat="server" HeaderText="Lista" ID="TabLista">
                    <ContentTemplate>
                        <asp:GridView runat="server" ID="GVLista" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ID,ID_DOCTOR,ID_PACIENTE,HR_CITA,NOMBREDOCTOR,NOMBREPACIENTE,FECH_CITA" EmptyDataText="No existen registros" EnablePersistedSelection="True" EnableSortingAndPagingCallbacks="True" OnPageIndexChanging="GVLista_PageIndexChanging" OnSelectedIndexChanged="GVLista_SelectedIndexChanged">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField Visible="False"></asp:TemplateField>
                                <asp:BoundField DataField="FECH_CITA" HeaderText="FECHA CITA" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="HR_CITA" HeaderText="HORA CITA" />
                                <asp:BoundField DataField="NOMBREPACIENTE" HeaderText="PACIENTE" />
                                <asp:BoundField DataField="NOMBREDOCTOR" HeaderText="DOCTOR" />
                                <asp:TemplateField HeaderText="SELECCIONAR">
                                    <ItemTemplate>
                                        <asp:Button ID="BtnVerDatos" class="form-control" runat="server" CommandName="Select"
                                            Text="Ver Datos" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="TabDatos" runat="server" HeaderText="Datos">
                    <ContentTemplate>
                        <asp:TextBox ID="TxtIDCita" runat="server" AutoCompleteType="Disabled" Visible="False">0</asp:TextBox>
                        <div class="form-group">
                            <asp:Label ID="Label2" runat="server" Text="Fecha:" CssClass="form-label col-sm-2" Font-Bold="True"></asp:Label>
                            <div class="col-md-4">
                                <asp:TextBox ID="TxtFechaCita" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" TargetControlID="TxtFechaCita" BehaviorID="_content_TextBox1_CalendarExtender" Format="dd/MM/yyyy" />
                            </div>


                            <asp:Label ID="Label1" runat="server" Text="Hora Cita" CssClass="form-label col-sm-2" Font-Bold="True"></asp:Label>
                            <div class="col-md-4">
                                <asp:TextBox ID="TxtHrCita" runat="server" class="form-control" TextMode="Time" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                            </div>


                        </div>
                        <br />
                        <br />
                        <div class="form-group">
                            <asp:Label ID="Label4" runat="server" Text="Doctor" CssClass="form-label col-sm-2" Font-Bold="True"></asp:Label>
                            <div class="col-md-4">
                                <asp:DropDownList ID="CboDoctor" runat="server" CssClass="form-control" DataTextField="NOMBREDOCTOR" DataValueField="ID"></asp:DropDownList>
                            </div>
                            <asp:Label ID="Label3" runat="server" Text="Paciente" CssClass="form-label col-sm-2" Font-Bold="True"></asp:Label>
                            <div class="col-md-4">
                                <asp:DropDownList ID="CboPaciente" runat="server" CssClass="form-control" DataTextField="NOMBREPACIENTE" DataValueField="ID"></asp:DropDownList>
                            </div>
                        </div>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

