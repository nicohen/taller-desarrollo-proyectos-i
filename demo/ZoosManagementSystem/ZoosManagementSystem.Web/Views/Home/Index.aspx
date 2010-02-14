<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ZoosManagementSystem.Web.ViewData.HomeViewData>" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Administraci&oacute;n Inteligente de Zool&oacute;gicos - Home
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    
        <div class="mainblock">
            <h2><%= Html.Encode(ViewData["Message"]) %></h2>
            <p>&nbsp;</p>
            <p><span style="font-size:1.3em;">El sistema de Administraci&oacute;n Inteligente de Zool&oacute;gicos es un conjunto de aplicaciones que
               le permiten automatizar las tareas mas comunes de mantenimiento.<br />
               <br />
               Desde esta aplicaci&oacute;n web usted podr&aacute; configurar los ambientes y/o jaulas del zool&oacute;gico
               as&iacute; c&oacute;mo los animales presentes. Tambi&eacute;n puede consultas las estad&iacute;sticas de los mismos.</span> 
            </p>
            <p>&nbsp;</p>
            <div class="mainblock" style="width:40%;border-color:#009900;font-size:1.1em;">
                <h4>Estad&iacute;sticas</h4>
                <div>
                    <ul>
                        <li>Ambientes en el sistema: <%= this.Model.environments %></li>
                        <li>Animales en el sistema: <%= this.Model.animals %></li>
                    </ul>
                </div>
            </div>
        </div>
    
</asp:Content>
