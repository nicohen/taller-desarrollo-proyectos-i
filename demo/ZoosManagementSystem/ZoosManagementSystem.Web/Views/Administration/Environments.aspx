<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<ZoosManagementSystem.Web.Models.Environment>>" %>
<%@ Import Namespace="System.Globalization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Administraci&oacute;n Inteligente de Zool&oacute;gicos - Administraci&oacute;n/Ambientes
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mainblock">
        <h2>Administraci&oacute;n de Ambientes</h2>       
        
        <% if (!string.IsNullOrEmpty((string)this.TempData["DeleteMessage"]))
           { %>
            <h3 class="<%= ((bool)this.TempData["DeleteSucess"]) ? "accionsucess" : "actionerror" %>"><%= Html.Encode(this.TempData["DeleteMessage"])%></h3> 
        <% } %>
        
        <div id="search">
                
        Buscar ambiente: <%= this.Html.TextBox("searchCriteria", Html.Encode(this.TempData["SearchCriteria"]), new { style = "width: 190px", onkeypress = "searchEnvironmentKeyPressed(this, event)" })%>
        <input type="submit" value="Buscar" onclick="redirectSearchEnvironment();" />
        
        </div>
        
        <%= Html.ActionLink("Nuevo Ambiente", "NewEnvironment", "Administration", null, new { Class = "newlinkbig" })%>
        <div class="clear"></div>
        
        <% if ((this.Model != null) && (this.Model.Count > 0))
           {
               var count = 0; %>
           
               <div id="maincontent">
               
            <% foreach (var environment in this.Model)
               {
                 count++; %>
                 
                 <div class="contentbox">
                    <h3><%= environment.Name %></h3>
                    <span>
                    <%= Html.ActionLink("Editar", "EditEnvironment", "Administration", new { environmentId = environment.Id.ToString() }, new { Class = "editlink" })%> &nbsp;|&nbsp;
                    <%= Html.ActionLink("Eliminar", "DeleteEnvironment", "Administration", new { environmentId = environment.Id.ToString() }, new { Class = "deletelink" }) %>
                    </span>
                    <fieldset class="clear">
                        <p><label for="Description">Descripci&oacute;n: </label><%= environment.Description%></p>
                        <p><label for="Surface">Superficie (m²): </label><%= environment.Surface %></p>
                        <p><label for="Description">Tipo: </label><%= environment.Type %></p>
                    </fieldset>
                    <fieldset>
                        <%
                           if ((environment.Animal != null) && (environment.Animal.Count > 0))
                           { %>
                            <legend>Animales</legend>
                            <ul>
                            <% foreach (var animal in environment.Animal)
                               { %>
                                   <li><%= Html.ActionLink(animal.Name, "EditAnimal", "Administration", new { animalId = animal.Id }, null) %> (<%= string.Format(CultureInfo.CurrentCulture, "{0}: {1}", (animal.Sex.ToLowerInvariant() == "m") ? "Macho" : "Hembra", animal.Species) %>)</li>
                            <% } %>
                            </ul>                          
                        <% }
                           else
                           { %>
                             <h4><%= Html.Encode("No se encontró ningún animal.") %></h4>
                        <% } %>
                    </fieldset>
                    <fieldset>
                        <%
                            if ((environment.TimeSlot != null) && (environment.TimeSlot.Count > 0))
                           { %>
                            <legend>Intervalos de Tiempo para Sensores</legend>
                            <ul>                             
                            <%
                               var timeSlotCount = 0;
                               foreach (var timeSlot in environment.TimeSlot.OrderBy(t => t.InitialTime))
                               {
                                   timeSlotCount++; %>
                                   <li>Itervalo <%= timeSlotCount %>: <%= string.Format(CultureInfo.CurrentUICulture, "Desde {0} hs hasta {1} hs.", timeSlot.InitialTime, timeSlot.FinalTime) %></li>
                            <% } %>
                            </ul>
                        <% }
                           else
                           { %>
                             <h4><%= Html.Encode("No se encontró ningún intervalo de tiempo.") %></h4>
                        <% } %>
                    </fieldset>
                    <% if ((count % 2) == 0) 
                       { %>
                       <div class="clear"></div>
                    <% } %>   
                 </div>
                 
                 <% if ((count % 2) == 0) 
                    { %>
                       <div class="clear"></div>
                 <% } %>   
            <% } %>
               
               </div>
               <div class="clear"></div>          
        <% }
           else
           { %>
            <h3 class="normal"><%= Html.Encode(this.TempData["NoItemsMessage"]) %></h3> 
        <% } %>
   
    </div>
</asp:Content>
