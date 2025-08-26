<%@ Page Title="Cargar" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cargar.aspx.vb" Inherits="pasosprd.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <link rel="stylesheet" href="css/popup.css" type="text/css" />
      <script src="js/funciones.js"></script>


    <style>
    .no-underline {
        text-decoration: none;
    }
</style>

    <main aria-labelledby="title">
        <h2 id="title"><asp:LinkButton ID="lnkVolver" runat="server">⬅️ </asp:LinkButton> Ingreso de solicitud de paso a producción  <asp:LinkButton ID="LinkButton1" runat="server"  Text="Mostrar Pop-up" OnClientClick="showModal(); return false;">&#128712;</asp:LinkButton> </h2>

  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

        <hr />
        <b>Información principal</b>  <br /> 
        <table id="solicpasoprd" style="width: 60%">
            <tr><td style="width: 200px">Empresa</td><td> <asp:DropDownList runat="server" ID="ddlEmpresa" Width="250px"></asp:DropDownList></td></tr>
             <tr><td>Módulo</td><td> <asp:DropDownList runat="server" ID="ddlModulo" Width="250px"></asp:DropDownList></td></tr>
             <tr><td>Sistema</td><td> <asp:DropDownList runat="server" ID="ddlSistemas" Width="250px"></asp:DropDownList></td></tr>
             <tr><td>Tipo solicitud</td><td> <asp:DropDownList runat="server" ID="ddlTipo" Width="250px" AutoPostBack="true"></asp:DropDownList>
                 <asp:DropDownList ID="ddlCasoPadre" runat="server" Width="250px" AutoPostBack="true"  Visible="false">   </asp:DropDownList>
                 </td></tr>
             <tr><td>Desarrollador</td><td> <asp:DropDownList runat="server" ID="ddlDev" Width="250px"></asp:DropDownList></td></tr>
             <tr><td>Solicitado por</td><td><asp:DropDownList runat="server" ID="ddlSolicita" Width="250px"></asp:DropDownList></td></tr>
             <tr><td>Prioridad</td><td><asp:DropDownList runat="server" ID="ddlPrioridad" Width="250px"></asp:DropDownList></td></tr>
             <tr><td>Requiere SQL</td><td>   <asp:CheckBox ID="chkReqSQL" runat="server" AutoPostBack="true"  ToolTip="Marcar si se incluye Script en .Zip para ejecutar en BD."/></td></tr>
             <tr><td>Detalles (cambios en sistema)<br />URL de Sistema</td><td>  <asp:TextBox ID="txtObs" runat="server" TextMode="MultiLine" Width="750px"   MaxLength="750"></asp:TextBox>  </td></tr>
        </table>

        <hr />
         <b> Información de desarrollo</b> <br /> 
    <table id="datosextra" style="width: 60%">
       <tr><td style="width: 200px">Commit Impl.</td><td> <asp:TextBox ID="txtCommitImpl" runat="server"   Width="250px"></asp:TextBox> </td></tr>
        <tr><td>Commit Plib</td><td> <asp:TextBox ID="txtCommitPlib" runat="server"   Width="250px"></asp:TextBox> <asp:CheckBox ID="chkPlib" runat="server" Checked="true" ToolTip="Desmarcar si el proyecto no usa Plib Núcleo." /></td></tr>
        <tr><td>Commit Plan</td><td> <asp:TextBox ID="txtCommitPlan" runat="server"   Width="250px"></asp:TextBox> <asp:CheckBox ID="chkPlan" runat="server" Checked="true" ToolTip="Desmarcar si el proyecto no usa Plan Núcleo." /> </td></tr>
        <tr><td>Comparación BD</td><td>  <asp:CheckBox ID="chkCompareDB" runat="server"  Enabled="false"/> </td></tr>
        <tr><td>Generar Script BD</td><td>  <asp:CheckBox ID="chkGenScript" runat="server"  Enabled="false" /> </td></tr>
        <tr><td>Generar Deploy (.zip)</td><td>  <asp:CheckBox ID="chkGenDeploy" runat="server" /> </td></tr>
     </table>
        <hr />
  
<div id="loadingMessage" style="display:none; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%); background-color: rgba(0,0,255,0.2); padding: 20px; border-radius: 10px; color: navy;">
    <p>Procesando, por favor espere... <br /><b>NO CIERRE ESTA PESTAÑA.</b></p>
</div>
       

    </ContentTemplate>
</asp:UpdatePanel>


    Agregar archivo adjunto una vez esté el formulario listo para ingresar.<br />   
       <asp:FileUpload ID="FileUpload1" runat="server" /><br /><br />
       <asp:Button ID="Button1" runat="server" Text="Aceptar" />
      <br /> 
      <asp:Label ID="lblAlert" runat="server" Text="" />

         <%--   <asp:Button ID="btnShowModals" runat="server" Text="Mostrar Pop-up" OnClientClick="showModal(); return false;" />--%>

         <div id="myModal" class="modal">
        <!-- Contenido del Modal -->
        <div class="modal-content">
            <span class="close" onclick="closeModal()">&times;</span>
            <p>
                <b>Nomenclatura de archivos</b><br /><br />
                [módulo o identificador de desarrollo]-[tipo de app]-[nombre aplicación]-[negocio]-[versión*] <br /> <br />
                Idenficador de archivo: <br />
                ps-web-px-eccu 	→ Parse Suite Web, Produx de Eccusa. <br />

                - Se debe copiar el archivo .zip (estándar actual de Windows).<br />
                - Nombre del archivo, debe mantener el nombre de la carpeta de producción, <br />
                agregando la fecha del paso a producción y un texto de la versión que se está instalando, acompañado de un identificador del desarrollador.<br /><br />
                Por ejemplo:<br />
                ps-web-plan-arg_01-08-2024_fixReporteArticulos_jgarrido.zip
                <br />  <br />
                Parse Suite: Producción, Planificación, MRP, Etc.<br />
                ASP: Demand, DRP, Pedidos, Etc.<br />
                ANG: Web-Redi, Trz-Cerveza, Etc.<br /><br />

                Módulo o identificador de desarrollo:<br />
                ps: Parse Suite. - 
                asp: Sistemas no Parse Suit desarrollados en en aspx. - 
                ang: Desarrollados en ángular. - 
                exe: Aplicación ejecutable a demanda o como servicio. - 
                qsys: Qualisys.  <br /><br />

                Tipo de app:<br />
                web: Aplicación web (aspx, angular, html, etc.) - 
                api: APIs - 
                app: Aplicación ejecutable - 
                srv: Servicio<br /><br />

                Aplicación:<br />
                px: Programación -
                plan: Planificación -
                cp: Control producción -
                Etc.<br /><br />
                Negocio:<br />
                 CERV: Cervezas -
                 ECCU: Analcohólicos -
                 ARG: Argentina -
                 BO: Bolivia -
                 PY: Paraguay -
                 UY: Uruguay  -  CPCH: Pisquera  -  CCU: Compartidos.
            </p>
        </div>
    </div>


    </main>

     <script type="text/javascript">
     window.onbeforeunload = function () {
         document.getElementById('loadingMessage').style.display = 'block';  
     };

     window.onload = function () {
         document.getElementById('loadingMessage').style.display = 'none';  
     };
</script>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(function () {
            document.getElementById('loadingMessage').style.display = 'block';  
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            document.getElementById('loadingMessage').style.display = 'none';  
        });
    </script>




</asp:Content>
