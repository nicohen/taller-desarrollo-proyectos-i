<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ZoosManagementSystem.Web.ViewData.HomeViewData>" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Administraci&oacute;n Inteligente de Zool&oacute;gicos - Home
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">

        <div class="mainblock">
            <h2><%= Html.Encode(ViewData["Message"]) %></h2>
            <p><span style="font-size:1.2em;">El sistema de Administraci&oacute;n Inteligente de Zool&oacute;gicos es un conjunto de aplicaciones que
               le permiten automatizar las tareas mas comunes de mantenimiento.<br />
               <br />
               Desde esta aplicaci&oacute;n web usted podr&aacute; configurar los ambientes y/o jaulas del zool&oacute;gico
               as&iacute; c&oacute;mo los animales presentes. Tambi&eacute;n puede consultas las estad&iacute;sticas de los mismos.</span> 
            </p>
            <div class="clear"></div>
            <div id="maincontent">
                <div class="contentbox">
                    <h3>Resumen de Ambientes y Animales</h3>
                    <div class="clear"></div>
                    <% if ((this.Model.environments != null) && (this.Model.environments.Count > 0))
                    {
                        var count = 0; %>
                        <ul style="margin-top:0px;"> 
                        <% foreach (var environment in this.Model.environments)
                        {
                            count++; %>
                            <li><%= environment.Name %>
                                <ul> 
                                <% if ((environment.Animal != null) && (environment.Animal.Count > 0))
                                { 
                                    foreach (var animal in environment.Animal)
                                    { %>
                                        <li><%= animal.Name %></li>
                                    <%}
                                } else
                                { %>
                                    <li><i><%= Html.Encode("No se encontró ningún animal en este ambiente.") %></i></li>
                                <% } %>
                                </ul>
                            </li>
                        <%} %>
                        </ul>
                    <%  } else
                    { %>
                        <h4><%= Html.Encode("No se encontró ningún ambiente.") %></h4>
                    <% } %>
                </div>
                <div class="contentbox"><!-- style="width:50%;border-color:#009900;font-size:1.1em;" -->
                    <h3>Animales sin ambiente asignado</h3>
                    <div class="clear"></div>
                    <% if ((this.Model.animals != null) && (this.Model.animals.Count > 0))
                    { %>
                        <ul style="margin-top:0px;"> 
                        <% foreach (var animal in this.Model.animals)
                        { %>
                            <li><%= animal.Name %></li>
                        <%} %>
                        </ul>
                    <%  } else
                    { %>
                        <h4><%= Html.Encode("No se encontró ningún animal sin ambiente asignado.") %></h4>
                    <% } %>
                </div>
                <div class="clear"></div>
            </div>
        </div>

</asp:Content>
