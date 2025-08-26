<%@ Page Title="Repuestos" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Repuestos.aspx.vb" Inherits="pasosprd.Repuestos" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

     <script type="text/javascript">
         // Define la función que quieres ejecutar al cargar la página
         function myCustomFunction() {
             saveCookieToSession();
         }

         // Asocia la función al evento de carga de la ventana
         window.onload = function () {
             myCustomFunction();
         };
    </script>

    <style>
    body {
        margin: 0 !important;
        padding: 0 !important;
    }

    form {
        margin: 0 !important;
        padding: 0 !important;
    }

    #MainContent {
        display: block;
        width: 100vw;
        height: 100vh;
        padding: 0;
        margin: 0;
    }
</style>


 <asp:HiddenField ID="hdnSessionVar" runat="server" />
<script type="text/javascript">
    var sessionValue = '<%= Session("IsLogin") %>';
    function checkSession() {
        if (sessionValue === '' || sessionValue === 'System.Web.SessionState.HttpSessionState' || sessionValue === False) {
            window.location.href = 'Login.aspx';
        }
    }
    window.onload = checkSession;
</script>


     <link rel="stylesheet" href="css/stilos.css" type="text/css" />

    <main aria-labelledby="title">
        <h2 id="title"> <asp:LinkButton ID="lnkVolver" runat="server" CssClass="no-underline" >⬅️ </asp:LinkButton>  <%: Title %>
            
    </h2>
      
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

 <div class="full-width-content" style="margin: 0; padding: 0; width: 100%;">


    <%-- <asp:Button ID="btnOcultrarFiltros" runat="server" Text="Ø Ocultar filtros" visible="false" />
     <asp:Button ID="btnVerFiltros" runat="server" Text="👁 Ver filtros" visible="true"   />--%>

      
   <hr />

         <div id="filtros">
          

          <asp:Panel ID="Panel1" runat="server" Visible="true">
               <table id="ddlsregistos" border="0" width="80%">
              <%-- <tr><td>&#187;Registros a ver  </td><td> <asp:DropDownList visible="false" ID="ddlTopRegSQL" runat="server" AutoPostBack="True" Width="200px"></asp:DropDownList></td> </tr>--%>
                <tr><td>Criterio</td><td><asp:TextBox ID="txtCriterio" runat="server" AutoPostBack="true" ></asp:TextBox></td></tr>

   <tr><td><asp:Label ID="mje" runat="server" Text="Artículo seleccionado"></asp:Label></td> <td>  <asp:Label ID="lblMsje" runat="server" Text="Seleccionar SKU"></asp:Label>  
       <asp:Button ID="btnAgregar" runat="server" Text="Agregar" visible="true"   /></td></tr>

             </table>
                   <hr />
          </asp:Panel>
        </div>

                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" Width="100%"
                    OnPageIndexChanging="GridView1_PageIndexChanging" 
                     OnSelectedIndexChanged="GridView1_SelectedIndexChanged"

                    OnSorting="GridView1_Sorting" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateSelectButton="True">
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

<Columns>
        <asp:TemplateField HeaderText="Imagen">
    <ItemTemplate>
        <a href="#" onclick="showImage('<%# "fotos/" & Eval("código") & ".jpg"%>'); return false;">
            <img id="imagenProducto" runat="server" src='<%# "fotos/" & Eval("código") & ".jpg"%>' alt="Img. no encontrada" style="height: 50px;" />
        </a>
    </ItemTemplate>
</asp:TemplateField>
    </Columns>

                </asp:GridView>

        </div>
        <br />
<asp:Label ID="lblAgregados" runat="server" Text="" Visible="false"></asp:Label>


        <asp:GridView ID="GridView2" runat="server"  Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#E3EAEB" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F8FAFA" />
            <SortedAscendingHeaderStyle BackColor="#246B61" />
            <SortedDescendingCellStyle BackColor="#D4DFE1" />
            <SortedDescendingHeaderStyle BackColor="#15524A" />
        </asp:GridView>
        <br />

      
      

          <asp:Button ID="BtnLimpiar" runat="server" Text="Limpiar" visible="true" Width="120px"  />
          <asp:Button ID="BtnPDF" runat="server" Text="Generar PDF" visible="true" Width="120px" />
         </ContentTemplate>
</asp:UpdatePanel>
      
        <hr />
  <asp:Panel ID="pnlPlano" runat="server">
       <!-- Imagen pequeña a pantalla completa -->
<!-- Imagen reducida (como vista previa) -->
<img src="imgs/plano.png" style="width: 100%; cursor: pointer;" onclick="mostrarImagen()" />

<!-- Modal de imagen con scroll -->
<div id="modalImagen" style="display: none; position: fixed; z-index: 1000; left: 0; top: 0; width: 100vw; height: 100vh; background-color: rgba(0,0,0,0.85); overflow: auto;" onclick="cerrarImagen()">
    <div style="width: max-content; height: max-content; margin: 50px auto;">
        <img src="imgs/plano.png" style="display: block;" />
    </div>
</div>

    </asp:Panel>
      
    </main>


<script>
 function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
 }


    function saveCookieToSession() {
        var cookieValue = getCookie('usuarioCasos'); // Lee la cookie

        // Llama a un método de servidor usando AJAX
        $.ajax({
            type: "POST",
            url: "SaveCookie.aspx/SaveCookieToSession",
            data: JSON.stringify({ cookieValue: cookieValue }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                console.log("Cookie guardada en sesión.");
            },
            error: function (xhr, status, error) {
                console.error("Error al guardar la cookie en la sesión:", error);
            }
        });
    }


</script>

<script type="text/javascript">
    function mostrarImagen() {
        document.getElementById("modalImagen").style.display = "block";
    }

    function cerrarImagen() {
        document.getElementById("modalImagen").style.display = "none";
    }
</script>

    <style>
    /* Estilos básicos para el modal */
    .modal {
        display: none;
        position: fixed;
        z-index: 1;
        padding-top: 50px;
        left: 0;
        top: 0;
        width: 100%;
        height: 100%;
        overflow: auto;
        background-color: rgb(0,0,0);
        background-color: rgba(0,0,0,0.9);
    }
    .modal-content {
        margin: auto;
        display: block;
        width: 80%;
        max-width: 700px;
    }
    .close {
        position: absolute;
        top: 15px;
        right: 35px;
        color: #f1f1f1;
        font-size: 40px;
        font-weight: bold;
        transition: 0.3s;
    }
    .close:hover,
    .close:focus {
        color: #bbb;
        text-decoration: none;
        cursor: pointer;
    }
</style>

<div id="imageModal" class="modal">
    <span class="close" onclick="closeImage()">×</span>
    <img class="modal-content" id="img01">
</div>

<script type="text/javascript">
    // Obtén el modal
    var modal = document.getElementById('imageModal');
    var modalImg = document.getElementById("img01");

    // Función para mostrar el modal
    function showImage(imageSrc) {
        modal.style.display = "block";
        modalImg.src = imageSrc;
    }

    // Función para cerrar el modal
    function closeImage() {
        modal.style.display = "none";
    }

    // Cierra el modal cuando el usuario hace clic fuera de la imagen
    modal.onclick = function (event) {
        if (event.target == modal) {
            closeImage();
        }
    }
</script>

</asp:Content>
 