<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<ZoosManagementSystem.Web.Models.Animal>>" %>
<%@ Import Namespace="System.Globalization"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Administraci&oacute;n Inteligente de Zool&oacute;gicos - Administraci&oacute;n/Animales
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mainblock">
        <h2>Administraci&oacute;n de Ambientes</h2>       
        
        <% if (!string.IsNullOrEmpty((string)this.TempData["AnimalMessage"]))
           { %>
            <h3 class="<%= ((bool)this.TempData["ActionSucess"]) ? "accionsucess" : "actionerror" %>"><%= Html.Encode(this.TempData["AnimalMessage"])%></h3> 
        <% } %>
        
        <div id="search">
                
        Buscar animal: <%= this.Html.TextBox("searchCriteria", Html.Encode(this.TempData["SearchCriteria"]), new { style = "width: 190px", onkeypress = "searchAnimalKeyPressed(this, event)" })%>
        <input type="submit" value="Buscar" onclick="redirectSearchAnimal();" />
        
        </div>
        
        <%= Html.ActionLink("Nuevo Animal", "NewAnimal", "Administration", null, new { Class = "newlinkbig" })%>
        <div class="clear"></div>
        
        <% if ((this.Model != null) && (this.Model.Count > 0))
           {
               var count = 0; %>
           
               <div id="maincontent">
               
            <% foreach (var animal in this.Model)
               {
                 count++; %>
                 
                 <div class="contentbox">
                    <h3><%= animal.Name%></h3>
                    <span>
                    <%= Html.ActionLink("Editar", "EditAnimal", "Administration", new { animalId = animal.Id.ToString() }, new { Class = "editlink" })%> &nbsp;|&nbsp;
                    <%= Html.ActionLink("Eliminar", "DeleteAnimal", "Administration", new { animalId = animal.Id.ToString() }, new { Class = "deletelink" })%>
                    </span>
                    <fieldset class="clear">
                        <p><label for="Species">Especie: </label><%= animal.Species %></p>
                        <p><label for="Sex">Sexo: </label><%= (animal.Sex.ToLowerInvariant() == "m") ? "Macho" : "Hembra" %></p>
                        <p><label for="BirthDate">Fecha de nacimiento: </label><%= animal.BirthDate.ToString("yyyy/MM/dd") %></p>                        
                        <p><label for="BornInCaptivity">Nacido en cautiverio: </label><%= animal.BornInCaptivity ? "Si" : "No" %></p>
                        <% if (!animal.BornInCaptivity)
                           { %>
                        <p><label for="Cost">Costo ($): </label><%= animal.Cost %></p>
                        <% } %>
                        <p><label for="NextHealthMeasure">Pr&oacute;ximo examen m&eacute;dico: </label><%= animal.NextHealthMeasure.ToString("yyyy/MM/dd")%></p>
                        <p><label for="Responsible">Responsable: </label><%= animal.Responsible.Name %> (<a href="mailto:<%= animal.Responsible.Email %>"><%= animal.Responsible.Email %></a>)</p>                                                
                        <p><label for="Environment">Ambiente: </label><%= (animal.Environment != null) ? string.Format(CultureInfo.CurrentCulture, "{0} ({1} m²)", animal.Environment.Name, animal.Environment.Surface) : "¡El animal no est&aacute; asignado a ning&uacute;n ambiente!"%></p>                                                
                    </fieldset>
                    <fieldset>
                        <%
                           if ((animal.FeedingTime != null) && (animal.FeedingTime.Count > 0))
                           { %>
                            <legend>Horas de Alimentaci&oacute;n</legend>
                            <ul>
                            <% foreach (var feedingTime in animal.FeedingTime)
                               { %>
                                   <li><%= string.Format(CultureInfo.CurrentCulture, "{0}:{1} hs: {2} ({3} gr.)", feedingTime.Time.Hours.ToString("D2"), feedingTime.Time.Minutes.ToString("D2"), feedingTime.Feeding.Name, feedingTime.Amount)%></li>
                            <% } %>
                            </ul>                          
                        <% }
                           else
                           { %>
                             <h4><%= Html.Encode("No se encontró ninguna hora de alimetación.") %></h4>
                        <% } %>
                    </fieldset>
                    <fieldset class="last">
                        <legend style="float: left;">Ex&aacute;menes M&eacute;dicos</legend>
                        <%= Html.ActionLink("Nuevo Examen Médico", "NewHealthMeasure", "Administration", null, new { Class = "newlink", style = "float: right;" })%>
                        
                        <div class="clear"></div>
                        <ul>                             
                        <%
                           var measures = 0;
                           foreach (var healthMeasure in animal.HealthMeasure.OrderBy(hm => hm.MeasurementDate))
                           {
                               measures++; %>
                               <li>Examen <%= measures %>: <%= Html.ActionLink(healthMeasure.MeasurementDate.ToString("yyyy/MM/dd"), "EditHealthMeasure", "Administration", new { healthMeasureId = healthMeasure.Id }, null)%></li>
                        <% } %>
                        </ul>
                        
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
