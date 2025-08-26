<%@ Page Title="Login" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.vb" Inherits="pasosprd.Login" %>




<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   
 
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %></h2>
      
&nbsp;<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

    <div>

   <hr />

         <div id="filtros">
          <asp:HiddenField ID="HiddenFieldToken" runat="server" />

          <asp:Panel ID="Panel1" runat="server">
               <table id="formlogin" border="0">

               <tr><td>Usuario</td><td> <asp:TextBox ID="txtUser" runat="server"></asp:TextBox>  </td></tr>
               <tr><td>Password</td><td> <asp:TextBox ID="txtPass" runat="server" TextMode="Password"></asp:TextBox>   </td></tr>
               <tr><td> </td><td>  <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" /> </td></tr>
               
               </table>
              <br />
      <asp:Label runat="server" ID="lblAlert" ForeColor="Crimson"></asp:Label>
          </asp:Panel>
        </div>

              

        </div>
         </ContentTemplate>
</asp:UpdatePanel>

      
    </main>

<%--    <script language="javascript" type="">
             // Recuperar el token de localStorage
            var token = localStorage.getItem('authToken');
            console.log(token);
</script>--%>

      
</asp:Content>
 