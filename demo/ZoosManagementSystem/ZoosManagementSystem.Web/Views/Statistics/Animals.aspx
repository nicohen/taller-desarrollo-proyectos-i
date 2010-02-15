<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<ZoosManagementSystem.Web.Models.Animal>>" %>
<%@ Import Namespace="System.Globalization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Administraci&oacute;n Inteligente de Zool&oacute;gicos - Estad&iacute;sticas/Animales
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mainblock">
        <h2>Estad&iacute;sticas de Animales</h2>
        <% if((this.Model != null) && (this.Model.Count > 0))
           { %>
                <div style="margin-top:10px;">
                  <%= Html.DropDownList(
                           "animals",
                           this.Model.Select(
                               a =>
                               new SelectListItem
                                   {
                                       Text =
                                           string.Format(
                                           CultureInfo.CurrentCulture,
                                           "{0}",
                                           a.Name),
                                       Value = a.Id.ToString()
                                   }),
                           "Seleccionar animal...")%>&nbsp;          
                  <a href="JavaScript:redirectViewAnimalStats('animals')" class="editlink">Ver Estad&iacute;sticas</a>
                </div>
        <% } 
           else
           { %>
                <h3 class="normal"><%= Html.Encode(this.TempData["NoItemsMessage"]) %></h3> 
        <% } %>
        
        <% if (!string.IsNullOrEmpty((string)this.TempData["AnimalStatMessage"]))
          { %>
            <h3 class="accionerror"><%= Html.Encode(this.TempData["AnimalStatMessage"])%></h3> 
        <% }
           else
           { %>
            <div id="maincontent">
            <div style="border-bottom:solid 1px #CCCCCC;"></div>
                <h2 style="text-align:center"><%= (string)this.ViewData["GraphTitle"] %></h2>
            <div style="border-bottom:solid 1px #CCCCCC;margin-bottom:10px;"></div>
            <div class="clear"></div>
            <div class="contentbox" style="text-align:center">
                <h3>Historial de Altura</h3>
                <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase=http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0" width="325" height="220" id="Column3D" >
                 <param name="movie" value="<%=Url.Content("~/Content/FusionCharts/FCF_Column3D.swf")%>" />
                 <param name="FlashVars" value="&dataXML=<%= (string)this.ViewData["HeightGraph"] %>&chartWidth=325&chartHeight=220">
                 <param name="quality" value="high" />
                 <embed src="<%=Url.Content("~/Content/FusionCharts/FCF_Column3D.swf")%>" flashVars="&dataXML=<%= (string)this.ViewData["HeightGraph"] %>&chartWidth=325&chartHeight=220" quality="high" width="325" height="220" name="Column3D" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />
                </object>
            </div>
            <div class="contentbox" style="text-align:center">
                <h3>Historial de Peso</h3>
                <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase=http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0" width="325" height="220" id="Object1" >
                 <param name="movie" value="<%=Url.Content("~/Content/FusionCharts/FCF_Area2D.swf")%>" />
                 <param name="FlashVars" value="&dataXML=<%= (string)this.ViewData["WeightGraph"] %>&chartWidth=325&chartHeight=220">
                 <param name="quality" value="high" />
                 <embed src="<%=Url.Content("~/Content/FusionCharts/FCF_Area2D.swf")%>" flashVars="&dataXML=<%= (string)this.ViewData["WeightGraph"] %>&chartWidth=325&chartHeight=220" quality="high" width="325" height="220" name="Column3D" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />
                </object>
            </div>
            <div class="clear"></div>
            <div class="contentbox" style="text-align:center">
                <h3>Historial de Temperatura</h3>
                <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase=http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0" width="325" height="220" id="Object2" >
                 <param name="movie" value="<%=Url.Content("~/Content/FusionCharts/FCF_Line.swf")%>" />
                 <param name="FlashVars" value="&dataXML=<%= (string)this.ViewData["TempGraph"] %>&chartWidth=325&chartHeight=220">
                 <param name="quality" value="high" />
                 <embed src="<%=Url.Content("~/Content/FusionCharts/FCF_Line.swf")%>" flashVars="&dataXML=<%= (string)this.ViewData["TempGraph"] %>&chartWidth=325&chartHeight=220" quality="high" width="325" height="220" name="Column3D" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />
                </object>
            </div>
            <div class="contentbox">
                <h3>Historial de Vacunaci&oacute;n</h3>
                <div class="clear"></div>
                <ul> 
                <% IList<String> vaccines = (IList<String>)this.ViewData["vaccines"];
                if ((vaccines != null) && (vaccines.Count > 0))
                {
                    foreach (var vaccine in vaccines)
                    { %>
                        <li><%= vaccine %></li>
                    <%}
                } else
                { %>
                    <li><i><%= Html.Encode("No se encontró ninguna vacuna en los examenes realizados") %></i></li>
                <% } %>
                </ul>
            </div>
            <div class="clear"></div>
        </div>
        <% } %>
    </div>
</asp:Content>
