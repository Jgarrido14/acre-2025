<%@ Page Title="Principal" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Principal.aspx.vb" Inherits="pasosprd.Principal" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <h1 id="aspnetTitle">Búsqueda de repuestos</h1>

            <asp:Panel ID="pnlPostLogin" runat="server" Visible="false">
              <%--  <p><a href="Casos" class="btn btn-primary btn-md">Buscar &raquo;</a>--%>
                <p><a href="Repuestos" class="btn btn-primary btn-md">Buscar &raquo;</a>
                <p><a href="Perfil" class="btn btn-primary btn-md">Mi perfil &raquo;</a>

            </asp:Panel>

  <%--      <asp:Panel ID="pnlGenerar" runat="server" Visible="false">
                <p><a href="Cargar" class="btn btn-primary btn-md">Generar solicitud &raquo;</a></p>
        </asp:Panel> --%>

            <asp:Panel ID="pnlAdmin" runat="server" Visible="false">
                  <p><a href="Usuarios" class="btn btn-primary btn-md">Gestion usuarios &raquo;</a></p>
                  <p>
                      &nbsp;</p>
              </asp:Panel> 
        </section>

     
    </main>
   
</asp:Content>
