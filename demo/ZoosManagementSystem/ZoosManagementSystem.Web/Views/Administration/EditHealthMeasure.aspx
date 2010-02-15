<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ZoosManagementSystem.Web.ViewData.HealthMeasureViewData>" %>
<%@ Import Namespace="System.Globalization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Administraci&oacute;n Inteligente de Zool&oacute;gicos - Administraci&oacute;n/Ambientes
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="mainblock">
        <h2>Administraci&oacute;n de Animales</h2>       
        
        <% if (!string.IsNullOrEmpty((string)this.TempData["EditMessage"]))
           { %>
        <h3 class="accionerror"><%= Html.Encode(this.TempData["EditMessage"])%></h3> 
        <% }
           else
           { %>
                
        <div id="maincontent">
            <%= Html.ValidationSummary("No se pudo guardar el ambiente. Por favor corregir los errores e intentar nuevamente.") %>
            <% using (Html.BeginForm())
               {%>
            <div class="editbox">
                    <fieldset>
                        <legend id="Legend1">Examen M&eacute;dico</legend>
                        <p><label for="MeasurementDate">Fecha:</label><%= this.Html.TextBox("MeasurementDate") %></p>
                        <div class="clear"></div>                    
                        <p><label for="AnimalId">Animal:</label>
                                    <%= Html.DropDownList("AnimalId",
                                                        this.Model.AnimalsAvailable.Select(
                                                                    a => new SelectListItem
                                                                        {
                                                                            Text = string.Format(
                                                                                CultureInfo.CurrentCulture,
                                                                                "{0} ({1}: {2})",
                                                                                a.Name,
                                                                                a.Sex,
                                                                                a.Species),
                                                                            Value = a.AnimalId}),
                                                        "Seleccionar Animal...") %>
                        </p>
                        <div class="clear"></div>
                        <p><label for="Weight">Peso (kg):</label><%= this.Html.TextBox("Weight")%></p>
                        <div class="clear"></div>                        
                        <p><label for="Height">Altura (cm):</label><%= this.Html.TextBox("Height")%></p>
                        <div class="clear"></div>                        
                        <p><label for="Temperature">Temperatura (&#176;C):</label><%= this.Html.TextBox("Temperature")%></p>
                        <div class="clear"></div>                        
                        <p><label for="Vaccine">Vacuna:</label><%= this.Html.TextBox("Vaccine")%></p>
                         <div class="clear"></div>                        
                        <p><label for="Notes">Notas:</label><%= this.Html.TextArea("Notes")%></p>
                        <div class="clear"></div>                        
                        <%= this.Html.Hidden("HealthMeasureId", this.Model.HealthMeasureId)%>
                    </fieldset>
                    <div style="float:right;">
                        <input type="submit" value="Guardar" />
                        <input type="reset" value="Cancelar" />
                    </div>
                    <div class="clear"></div>
                 </div>
            <% } %>
        </div>
        <% } %>  
        <div class="clear"></div>
    </div>
</asp:Content>
