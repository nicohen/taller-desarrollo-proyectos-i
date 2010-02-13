<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ZoosManagementSystem.Web.ViewData.EnvironmentViewData>" %>
<%@ Import Namespace="System.Globalization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Administraci&oacute;n Inteligente de Zool&oacute;gicos - Administraci&oacute;n/Ambientes
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function removeAnimal(id) {
            $("#" + id + " input")[1].value = "Remove";
            $("#" + id)[0].style.display = "none";
        }
    </script>

    <div class="mainblock">
        <h2>Administraci&oacute;n de Ambientes</h2>       
        
        <% if (!string.IsNullOrEmpty((string)this.TempData["EditMessage"]))
           { %>
            <h3 class="accionerror"><%= Html.Encode(this.TempData["EditMessage"])%></h3> 
        <% }
           else
           { %>
                
        <div id="maincontent">
            <div class="editbox">
                    <h3><label for="Name">Nombre:</label><%= this.Html.TextBox("Name") %></h3>
                    
                    <%= Html.ValidationSummary("No se pudo editar el ambiente. Por favor corregir los errores e intentar nuevamente.") %>
                    <% using (Html.BeginForm())
                       {%>
                        <fieldset>
                        <p><label for="Description">Descripci&oacute;n:</label><%= this.Html.TextBox("Description") %></p>
                        <p><label for="Surface">Superficie:</label><%= this.Html.TextBox("Surface") %></p>
                        <p><label for="Type">Tipo:</label><%= this.Html.TextBox("Type") %></p>
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
                                 <%= string.Format(CultureInfo.CurrentCulture, "{0} ({1}: {2})", animal.Name, animal.Sex, animal.Species) %>&nbsp;-&nbsp;
                                 <a href="JavaScript:removeAnimal('<%= animal.AnimalId %>')" class="deletelink">Remover</a>
                                 <%= Html.Hidden("Animals[" + animalIndex + "].AnimalId", animal.AnimalId) %>
                                 <%= Html.Hidden("Animals[" + animalIndex + "].AnimalStatus", animal.AnimalStatus) %>
                               </li>
                               <% animalIndex++; %>
                        <% } %>
                        </ul>                   
                        <%= (this.Model.FreeAnimals != null) && (this.Model.FreeAnimals.Count > 0)
                           ? Html.DropDownList(
                                "freeanimals",
                                this.Model.FreeAnimals.Select(
                                    a =>
                                    new SelectListItem
                                        {
                                            Text = string.Format(CultureInfo.CurrentCulture, "{0} ({1}: {2})", a.Name, a.Sex, a.Species),
                                            Value = a.AnimalId
                                        }),
                                "Seleccionar animal...",
                                new { onchange = "addToList('freeanimals', 'listanimals');" })
                           : string.Empty %>
                    </fieldset>
                    <fieldset>
                            <legend>Intervalos de Tiempo para Sensores</legend>
                            <ul>                             
                            <%
                               var timeSlotCount = 0;
                               foreach (var timeSlot in this.Model.TimeSlots.OrderBy(t => t.InitialTime))
                               {
                                   timeSlotCount++; %>
                                   <li>Itervalo <%= timeSlotCount %>: <%= string.Format(CultureInfo.CurrentUICulture, "Desde {0} hs hasta {1} hs.", timeSlot.InitialTime, timeSlot.FinalTime) %></li>
                            <% } %>
                            </ul>
                    </fieldset>
                    <% } %>        
                 </div>
                 
            <% } %>
               
        </div>
        <div class="clear"></div>          
   
    </div>
</asp:Content>
