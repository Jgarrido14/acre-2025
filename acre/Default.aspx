<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="pasosprd._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
    window.onbeforeunload = function () {
        // Hacer una llamada al servidor para limpiar la sesión
        var xhr = new XMLHttpRequest();
        xhr.open('GET', 'Logout.aspx', true);
        xhr.send();
    };
</script>

    <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <h1 id="aspnetTitle">ACRE - Repuestos</h1>
           <%-- <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>--%>
            
            <asp:Panel ID="pnlLgin" runat="server">
             <p><a href="Login" class="btn btn-primary btn-md">Login &raquo;</a></p>
            </asp:Panel>


         <%--   <asp:Panel ID="pnlPostLogin" runat="server" Visible="false">
                <p><a href="Cargar" class="btn btn-primary btn-md">Generar solicitud &raquo;</a></p>
                <p><a href="Casos" class="btn btn-primary btn-md">Ver solicitudes &raquo;</a>
            </asp:Panel>


            <asp:Panel ID="pnlAdmin" runat="server" Visible="false">
                  <p><a href="GestionUsuarios" class="btn btn-primary btn-md">Gestion usuarios &raquo;</a></p>
              </asp:Panel> --%>
        </section>

<%--        <div class="row">
            <section class="col-md-4" aria-labelledby="gettingStartedTitle">
                <h2 id="gettingStartedTitle">Getting started</h2>
                <p>
                    ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
                A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
                </p>
                <p>
                    <a class="btn btn-default" href="Cargar">Generar solicitud &raquo;</a>
                    <a class="btn btn-default" href="Cargar">Ver solicitudes &raquo;</a>
                    <a class="btn btn-default" href="Cargar">Enviar correo &raquo;</a>


                </p>
            </section>
            <section class="col-md-4" aria-labelledby="librariesTitle">
                <h2 id="librariesTitle">Get more libraries</h2>
                <p>
                    NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.
                </p>
                <p>
                    <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
                </p>
            </section>
            <section class="col-md-4" aria-labelledby="hostingTitle">
                <h2 id="hostingTitle">Web Hosting</h2>
                <p>
                    You can easily find a web hosting company that offers the right mix of features and price for your applications.
                </p>
                <p>
                    <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301950">Learn more &raquo;</a>
                </p>
            </section>
        </div>--%>
    </main>

</asp:Content>
