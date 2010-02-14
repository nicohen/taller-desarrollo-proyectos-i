<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ZoosManagementSystem.Web.ViewData.EnvironmentViewData>" %>
<%@ Import Namespace="System.Globalization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Administraci&oacute;n Inteligente de Zool&oacute;gicos - Administraci&oacute;n/Ambientes
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   

    <div class="mainblock">
        <h2>Administraci&oacute;n de Ambientes</h2>       
        
        <% if (!string.IsNullOrEmpty((string)this.TempData["EditMessage"]))
           { %>
        <h3 class="accionerror"><%= Html.Encode(this.TempData["EditMessage"])%></h3> 
        <% }
           else
           { %>
                
        <div id="maincontent">
            <%= Html.ValidationSummary("No se pudo editar el ambiente. Por favor corregir los errores e intentar nuevamente.") %>
            <% using (Html.BeginForm())
               {%>
            <div class="editbox">
                    <fieldset>
                        <p><label for="Name">Nombre:</label><%= this.Html.TextBox("Name") %></p>
                        <div class="clear"></div>                    
                        <p><label for="Description">Descripci&oacute;n:</label><%= this.Html.TextBox("Description") %></p>
                        <div class="clear"></div>
                        <p><label for="Surface">Superficie (m²):</label><%= this.Html.TextBox("Surface") %></p>
                        <div class="clear"></div>                        
                        <p><label for="Type">Tipo:</label><%= this.Html.TextBox("Type") %></p>
                        <div class="clear"></div>                        
                        <%= this.Html.Hidden("EnvironmentId", this.Model.EnvironmentId) %>
                    </fieldset>
                    <fieldset>
                    <legend id="animals">Animales</legend>
                        <ul id="listanimals">
                        <%
                           var animalIndex = 0;
                           foreach (var animal in this.Model.Animals)
                           { %>
                               <li id="<%= animal.AnimalId %>">
                                 <label><%= string.Format(CultureInfo.CurrentCulture, "{0} ({1}: {2})", animal.Name, animal.Sex, animal.Species) %></label>
                                 <a href="JavaScript:removeAnimal('<%= animal.AnimalId %>')" class="deletelink">Remover</a>
                                 <%= Html.Hidden("Animals[" + animalIndex + "].AnimalStatus", animal.AnimalStatus) %>
                                 <%= Html.Hidden("Animals[" + animalIndex + "].AnimalId", animal.AnimalId) %>                                 
                                 <%= Html.Hidden("Animals[" + animalIndex + "].Name", animal.Name) %>                                 
                                 <%= Html.Hidden("Animals[" + animalIndex + "].Sex", animal.Sex) %>                                 
                                 <%= Html.Hidden("Animals[" + animalIndex + "].Species", animal.Species) %>                                 
                                 <div class="clear"></div>
                               </li>
                               <% animalIndex++; %>
                        <% } %>
                        </ul>                   
                        <% if((this.Model.FreeAnimals != null) && (this.Model.FreeAnimals.Count > 0))
                           { %>
                              <div>
                                  <%= Html.DropDownList(
                                           "freeanimals",
                                           this.Model.FreeAnimals.Select(
                                               a =>
                                               new SelectListItem
                                                   {
                                                       Text =
                                                           string.Format(
                                                           CultureInfo.CurrentCulture,
                                                           "{0} ({1}: {2})",
                                                           a.Name,
                                                           a.Sex,
                                                           a.Species),
                                                       Value = a.AnimalId
                                                   }),
                                           "Seleccionar animal...")%>&nbsp;          
                                  <a href="JavaScript:addToList('freeanimals', 'listanimals');" class="newlink">Agregar</a>
                              </div>
                        <% } %>
                    </fieldset>
                    <fieldset id="timeslots">
                        <legend>Intervalos de Tiempo para Sensores</legend>
                        <span><a href="JavaScript:addNewTimeSlot('timeslotlist');" class="newlink">Agregar</a></span>
                        <div id="timeslotlist">

                        <%
                           var timeSlotIndex = 1;
                           foreach (var timeSlot in this.Model.TimeSlots)
                           { %>
                            <p id="<%= timeSlot.TimeSlotId + "-HEAD" %>" class="timeslothead">
                                Itervalo <%= timeSlotIndex %><a href="JavaScript:removeTimeSlot('<%= timeSlot.TimeSlotId %>')" class="deletelink">Remover</a>
                            </p>
                            <div id="<%= timeSlot.TimeSlotId + "-BODY" %>" class="timeslotbody">
                                <p>
                                    <label>Hora inicial:</label><%= Html.TextBox("TimeSlots[" + (timeSlotIndex - 1) + "].InitialTime", timeSlot.InitialTime)%>
                                </p>
                                <p>
                                    <label>Hora final:</label><%= Html.TextBox("TimeSlots[" + (timeSlotIndex - 1) + "].FinalTime", timeSlot.FinalTime)%>
                                </p>
                                <p>
                                    <label>Temperatura M&iacute;nima (&#176;C):</label><%= Html.TextBox("TimeSlots[" + (timeSlotIndex - 1) + "].TemperatureMin", timeSlot.TemperatureMin)%>
                                </p>
                                <p>
                                    <label>Temperatura M&aacute;xima (&#176;C):</label><%= Html.TextBox("TimeSlots[" + (timeSlotIndex - 1) + "].TemperatureMax", timeSlot.TemperatureMax)%>
                                </p>
                                <p>
                                    <label>Humedad M&iacute;nima (%):</label><%= Html.TextBox("TimeSlots[" + (timeSlotIndex - 1) + "].HumidityMin", timeSlot.HumidityMin)%>
                                </p>
                                <p>
                                    <label>Humedad M&aacute;xima (%):</label><%= Html.TextBox("TimeSlots[" + (timeSlotIndex - 1) + "].HumidityMax", timeSlot.HumidityMax)%>
                                </p>
                                <p>
                                    <label>Luminosidad M&iacute;nima (lx):</label><%= Html.TextBox("TimeSlots[" + (timeSlotIndex - 1) + "].LuminosityMin", timeSlot.LuminosityMin)%>
                                </p>
                                <p>
                                    <label>Luminosidad M&aacute;xima (lx):</label><%= Html.TextBox("TimeSlots[" + (timeSlotIndex - 1) + "].LuminosityMax", timeSlot.LuminosityMax)%>
                                </p>
                                <%= Html.Hidden("TimeSlots[" + (timeSlotIndex - 1) + "].TimeSlotStatus", timeSlot.TimeSlotStatus)%>
                                <%= Html.Hidden("TimeSlots[" + (timeSlotIndex - 1) + "].TimeSlotId", timeSlot.TimeSlotId)%>                                
                                <div class="clear"></div>
                            </div>
                               <% timeSlotIndex++; %>
                        <% } %>
   
                        </div>
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
