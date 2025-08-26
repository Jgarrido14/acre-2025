<%@ Page Title="Usuarios" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.vb" Inherits="pasosprd.Usuarios" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title">
            <asp:LinkButton ID="lnkVolver" runat="server">⬅️ </asp:LinkButton><%: Title %></h2>


        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <hr />
                <asp:Button ID="btnVerCrear" runat="server" Text="Agregar" /> <asp:Button ID="btnVerModificar" runat="server" Text="Editar" />
                <hr />

    <asp:Panel ID="pblCrear" runat="server" Visible="true">
        <h2>Crear Usuario</h2>
                <table id="tbltrabajador">
                    <tr>
                        <td style="width: 200px">ID Usuario</td>
                        <td>
                            <asp:TextBox ID="txtUsuaID" runat="server" Width="220px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Nombre</td>
                        <td>
                            <asp:TextBox ID="txtNombre" runat="server" Width="220px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>E-Mail</td>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server" Width="220px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><%--Tipo de trabajador--%>
                            <asp:DropDownList ID="ddlTipoTrabajador" runat="server" Width="220px" Visible="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Activo</td>
                        <td>
                            <asp:CheckBox ID="chkTrabActiv" runat="server" Checked="true" Enabled="False" />
                        </td>
                    </tr>
                    <tr>
                        <td>Crear cuenta</td>
                        <td>
                            <asp:CheckBox ID="chkCreaCuenta" runat="server" AutoPostBack="True" EnableViewState="true" />
                        </td>
                    </tr>

                </table>
             

                <asp:Panel ID="pnlUsuario" runat="server" Visible="false">
                       <hr />
                    <table id="tblusuario">
                        <tr id="contenedor_password" runat="server">
                            <td style="width: 200px">Password</td>
                            <td>
                                <asp:TextBox ID="txtPassw" runat="server" Width="220px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Fecha exp.</td>
                            <td>
                                <asp:TextBox ID="txtFechaexp" runat="server" Width="220px" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Tipo de usuario</td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlTipoUsuario" Width="220px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>Activo</td>
                            <td>
                                <asp:CheckBox ID="chkUsuaActiv" runat="server" Checked="true" Enabled="False" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <br />
                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" />
                <br />
               
                <asp:Label ID="lblAlert" runat="server" Text="" />

        </asp:Panel>   



    <%--   style="border-width: 2px;  border: gray;border-style: solid;padding: 10px;border-radius: 20px; border-color: black;"--%>
         
    <asp:Panel ID="pnlEditar" runat="server" Visible="false">

                <h2>Editar Usuario</h2>
                <table id="tbltrabajador_">
                    <tr>
                        <td style="width: 200px">ID Usuario</td>
                        <td>
                            <asp:TextBox ID="txtUsuaID_" runat="server" Width="220px" placeholder="Escriba el nombre del usuario"></asp:TextBox>
                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" />
                            <asp:Label ID="lblAlertBuscar" runat="server" Text="" />
                        </td>
                    </tr>
                    <tr>
                        <td>Nombre</td>
                        <td>
                            <asp:TextBox ID="txtNombre_" runat="server" Width="220px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>E-Mail</td>
                        <td>
                            <asp:TextBox ID="txtEmail_" runat="server" Width="220px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><%--Tipo de trabajador--%>
                            <asp:DropDownList ID="DropDownList1" runat="server" Width="220px" Visible="false">
                            </asp:DropDownList>
                        </td>
                    </tr>

                        <tr>      <td>Fecha exp.</td>
          <td>
              <asp:TextBox ID="txtFechaexp_" runat="server" Width="220px" Enabled="false"></asp:TextBox>
          </td>
              </tr>
              <tr>
                  <td>Tipo de usuario</td>
                  <td>
                      <asp:DropDownList runat="server" ID="ddlTipoUsuario_" Width="220px"></asp:DropDownList></td>
              </tr>
              <tr>
                  <td>Activo</td>
                  <td>
                      <asp:CheckBox ID="chkTrabActiv_" runat="server" Checked="false" Enabled="true" />
                  </td>
              </tr>

                </table>
                 <asp:Button ID="btnModificar" runat="server" Text="Aceptar" />
                <asp:Button ID="btnCancelar_" runat="server" Text="Cancelar" />
                     <asp:Label ID="Label2" runat="server" Text="" />

                 </asp:Panel>
                <hr />

               

                
                <asp:Panel ID="pnlGrillaUsuarios" runat="server">

                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" Width="100%"
                        OnPageIndexChanging="GridView1_PageIndexChanging"
                        OnSorting="GridView1_Sorting" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>

                </asp:Panel>
               

                
            </ContentTemplate>
        </asp:UpdatePanel>
    </main>

</asp:Content>
