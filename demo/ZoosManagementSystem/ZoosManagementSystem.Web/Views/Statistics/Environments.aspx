<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<ZoosManagementSystem.Web.Models.Environment>>" %>
<%@ Import Namespace="System.Globalization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Administraci&oacute;n Inteligente de Zool&oacute;gicos - Estad&iacute;sticas/Ambientes
	
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="mainblock">
        <h2>Estad&iacute;sticas de Ambientes</h2>
       <% if((this.Model != null) && (this.Model.Count > 0))
           { %>
                <div style="margin-top:10px;">
                  <%= Html.DropDownList(
                           "environments",
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
                           "Seleccionar ambiente...")%>&nbsp;          
                  <a href="JavaScript:redirectViewEnvironmentStats('environments')" class="editlink">Ver Estad&iacute;sticas</a>
                </div>
        <% } 
           else
           { %>
                <h3 class="normal"><%= Html.Encode(this.TempData["NoItemsMessage"]) %></h3> 
        <% } %>
        
        <% if (!string.IsNullOrEmpty((string)this.TempData["EnvironmentStatMessage"]))
          { %>
            <h3 class="accionerror"><%= Html.Encode(this.TempData["EnvironmentStatMessage"])%></h3> 
        <% }
           else
           { %>
            <div id="maincontent">
            <div style="border-bottom:solid 1px #CCCCCC;"></div>
                <h2 style="text-align:center"><%= (string)this.ViewData["GraphTitle"] %></h2>
            <div style="border-bottom:solid 1px #CCCCCC;margin-bottom:10px;"></div>
            <div class="clear"></div>
            <div class="contentbox" style="text-align:center">
                <h3>Historial de Temperatura</h3>
                <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase=http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0" width="300" height="220" id="Column3D" >
                 <param name="movie" value="<%=Url.Content("~/Content/FusionCharts/FCF_Column3D.swf")%>" />
                 <param name="FlashVars" value="&dataXML=<%= (string)this.ViewData["TempGraph"] %>&chartWidth=300&chartHeight=220">
                 <param name="quality" value="high" />
                 <embed src="<%=Url.Content("~/Content/FusionCharts/FCF_Column3D.swf")%>" flashVars="&dataXML=<%= (string)this.ViewData["TempGraph"] %>&chartWidth=300&chartHeight=220" quality="high" width="300" height="220" name="Column3D" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />
                </object>
            </div>
            <div class="contentbox" style="text-align:center">
                <h3>Historial de Humedad</h3>
                <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase=http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0" width="300" height="220" id="Object1" >
                 <param name="movie" value="<%=Url.Content("~/Content/FusionCharts/FCF_Area2D.swf")%>" />
                 <param name="FlashVars" value="&dataXML=<%= (string)this.ViewData["HumiGraph"] %>&chartWidth=300&chartHeight=220">
                 <param name="quality" value="high" />
                 <embed src="<%=Url.Content("~/Content/FusionCharts/FCF_Area2D.swf")%>" flashVars="&dataXML=<%= (string)this.ViewData["HumiGraph"] %>&chartWidth=300&chartHeight=220" quality="high" width="300" height="220" name="Column3D" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />
                </object>
            </div>
            <div class="clear"></div>
            <div class="contentbox" style="text-align:center">
                <h3>Historial de Luminosidad</h3>
                <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase=http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0" width="300" height="220" id="Object2" >
                 <param name="movie" value="<%=Url.Content("~/Content/FusionCharts/FCF_Line.swf")%>" />
                 <param name="FlashVars" value="&dataXML=<%= (string)this.ViewData["LumiGraph"] %>&chartWidth=300&chartHeight=220">
                 <param name="quality" value="high" />
                 <embed src="<%=Url.Content("~/Content/FusionCharts/FCF_Line.swf")%>" flashVars="&dataXML=<%= (string)this.ViewData["LumiGraph"] %>&chartWidth=300&chartHeight=220" quality="high" width="300" height="220" name="Column3D" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />
                </object>
            </div>
            <div class="clear"></div>
        </div>
        <% } %>
    </div>
   
</asp:Content>