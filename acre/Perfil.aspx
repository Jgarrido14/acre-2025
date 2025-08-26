<%@ Page Title="Perfil" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.vb" Inherits="pasosprd.Perfil" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title"><asp:LinkButton ID="lnkVolver" runat="server">⬅️ </asp:LinkButton><%: Title %></h2>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

                <table id="tbltrabajador">
                    <tr><td style="width:200px" >Código</td><td> 
                        <asp:TextBox ID="txtUsuaID" runat="server" Width="220px" Enabled="false" ></asp:TextBox>
                        </td></tr>
                     <tr><td>Nombre</td><td> 
                         <asp:TextBox ID="txtNombre" runat="server" Width="220px" Enabled="false"></asp:TextBox>
                         </td></tr>
                     <tr><td>E-mail</td><td> 
                         <asp:TextBox ID="txtEmail" runat="server" Width="220px" Enabled="false"></asp:TextBox>
                         </td></tr>
                
                     <tr><td>Activo</td><td> 
                         <asp:CheckBox ID="chkTrabActiv" runat="server" Checked="true" Enabled="False" />
                         </td></tr>

                </table>
                <hr />
        
                <asp:Panel ID="pnlUsuario" runat="server" Visible="true"  >
                <table id="tblusuario">
                      <tr><td style="width:200px">Password actual</td><td> 
                          <asp:TextBox ID="txtPassw" runat="server" Width="220px"  MaxLength="12" TextMode="Password"  ></asp:TextBox>
                          </td></tr>

                      <tr><td style="width:200px">Password nueva</td><td> 
                      <asp:TextBox ID="txtPasswNew" runat="server" Width="220px" MaxLength="12" TextMode="Password" ></asp:TextBox>
                      </td></tr>

                      <tr><td style="width:200px">Password repetir nueva</td><td> 
                      <asp:TextBox ID="txtPasswNewRep" runat="server" Width="220px"   MaxLength="12" TextMode="Password"  ></asp:TextBox>
                      </td></tr>

                       <tr><td>Fecha exp.</td><td> 
                           <asp:TextBox ID="txtFechaexp" runat="server" Width="220px"  Enabled ="false"></asp:TextBox>
                           </td></tr>
                       <tr><td>Tipo de usuario</td><td> 
                           <asp:TextBox ID="txtTipoUsuario" runat="server" Width="220px"  Enabled ="false"></asp:TextBox>
                           </td></tr>
                     <tr><td>Activo</td><td> 
                         <asp:CheckBox ID="chkUsuaActiv" runat="server"  Checked="true" Enabled="False" /> 
                         </td></tr>
                     </table>
                   </asp:Panel>
           <br /><asp:Button ID="btnAceptar" runat="server" Text="Aceptar" />
         <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" />
                <br /><br />
                <asp:Label ID="lblAlert" runat="server" Text="" />
       
    </ContentTemplate>
</asp:UpdatePanel>
    </main>

</asp:Content>
