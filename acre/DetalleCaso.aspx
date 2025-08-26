<%@ Page Title="DetalleCaso" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalleCaso.aspx.vb" Inherits="pasosprd.DetalleCaso" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %><asp:Label ID="lblCaso" runat="server" Text="SINCASO"></asp:Label>   
            <asp:LinkButton runat="server" ID="lbCerrar"   OnClientClick="cerrarPestana(); return false;">❎</asp:LinkButton>
    </h2>


  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

        <hr />
       


        <table id="infomain" style="width: 100%" border="1">
            <table id="solicpasoprd" style="width: 100%">
                <tr>
                    <td top;"="" vertical-align:=""><b>Información principal </b></td>
                    <td></td>
                    <td><b>Información de desarrollo</b> </td>
                </tr>
                <tr>
                    <td style="width: 200px">Empresa</td>
                    <td><b>
                        <asp:Label ID="lblEmpresa" runat="server" Text="" />
                        </b></td>
                    <td style="width: 200px">Commit Impl.</td>
                    <td><b>
                        <asp:Label ID="lblCommitImpr" runat="server" Text="" />
                        </b></td>
                </tr>
                <tr>
                    <td>Módulo</td>
                    <td><b>
                        <asp:Label ID="lblModulo" runat="server" Text="" />
                        </b></td>
                    <td>Commit Plib</td>
                    <td><b>
                        <asp:Label ID="lblCommitPlib" runat="server" Text="" />
                        </b></td>
                </tr>
                <tr>
                    <td>Sistema</td>
                    <td><b>
                        <asp:Label ID="lblSistema" runat="server" Text="" />
                        </b></td>
                    <td>Commit Plan</td>
                    <td><b>
                        <asp:Label ID="lblCommitPlan" runat="server" Text="" />
                        </b></td>
                </tr>
                <tr>
                    <td>Tipo solicitud</td>
                    <td><b>
                        <asp:Label ID="lblTipoSolic" runat="server" Text="" />
                        </b></td>
                    <td>Comparación BD</td>
                    <td>
                        <asp:CheckBox ID="chkCompareDB" runat="server" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td>Desarrollador</td>
                    <td><b>
                        <asp:Label ID="lblDev" runat="server" Text="" />
                        </b></td>
                    <td>Generar Script BD</td>
                    <td>
                        <asp:CheckBox ID="chkGenScript" runat="server" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td>Solicitado por</td>
                    <td><b>
                        <asp:Label ID="lblSolicitadoPor" runat="server" Text="" />
                        </b></td>
                    <td>Generar Deploy (.zip)</td>
                    <td>
                        <asp:CheckBox ID="chkGenDeploy" runat="server" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td>Requiere SQL</td>
                    <td>
                        <asp:CheckBox ID="chkReqSQL" runat="server" Enabled="false" />
                    </td>
                    <td>Caso padre</td>
                    <td> <asp:Label ID="lblCasoPadre" runat="server" Text="" /> </td>
                </tr>
                <tr>
                    <td>Detalles (cambios en sistema)</td>
                    <td>
                        <asp:TextBox ID="txtObs" runat="server" Enabled="false" MaxLength="750" TextMode="MultiLine" Width="750px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            </td>
            <hr />
            <b>Información de solicitud</b>
            <br />
            <table id="datosolic" style="width: 100%">
                <tr>
                     <td style="width: 200px">Prioridad</td>
                     <td><b> <asp:Label ID="lblPrioridad" runat="server" Text="" />  </b></td>
                 </tr>
                 <tr>
                     <td style="width: 200px">Fecha solicitud</td>
                     <td><b>
                         <asp:Label ID="lblFechaSol" runat="server" Text="" />
                         </b></td>
                <tr>
                    <td>Nombre archivo</td>
                    <td><b>
                        <asp:Label ID="lblNombreArchivo" runat="server" Text="" />
                        </b></td>
                </tr>
                <tr>
                    <td>Estado</td>
                    <td><b>
                        <asp:Label ID="lblEstado" runat="server" Text="" />
                        </b></td>
                </tr>
            </table>
            <asp:Panel ID="pblEstado" runat="server">
                <table id="datoEstado" style="width: 100%">
                    <tr>
                        <td style="width: 200px">Actualizar</td>
                        <td>
                            <asp:DropDownList ID="ddlFiltroEstado" runat="server" AutoPostBack="True" Width="200px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Comentario</td>
                        <td>
                            <asp:TextBox ID="txtComentario" runat="server" AutoPostBack="True" MaxLength="500" TextMode="MultiLine" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Label ID="lblAlert" runat="server" Text="" />

            <hr />
            <b>Comentarios</b>
            <asp:Panel ID="pnlComentarios" runat="server">
                <asp:GridView ID="gvComentarios" runat="server" CellPadding="4" ForeColor="#333333" GridLines="Both" Width="80%">
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

    <div id="loadingMessage" style="display:none; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%); background-color: rgba(0, 0, 0, 0.5); padding: 20px; border-radius: 10px; color: white;">
    <p>Procesando, por favor espere... <br /><b>NO CIERRE ESTA PESTAÑA.</b></p>
</div>
       
    </ContentTemplate>
</asp:UpdatePanel>

      <br /> 
    


    </main>

    <script type="text/javascript">
    function cerrarPestana() {
        window.open('', '_self', '');
        window.close();
    }
</script>
     <script type="text/javascript">
         window.onbeforeunload = function () {
             document.getElementById('loadingMessage').style.display = 'block';
         };

         window.onload = function () {
             document.getElementById('loadingMessage').style.display = 'none';
         };
     </script>
</asp:Content>
