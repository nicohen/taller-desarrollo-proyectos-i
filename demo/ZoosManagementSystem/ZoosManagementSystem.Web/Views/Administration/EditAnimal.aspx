<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ZoosManagementSystem.Web.ViewData.AnimalViewData>" %>

<%@ Import Namespace="System.Globalization" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Administraci&oacute;n Inteligente de Zool&oacute;gicos - Administraci&oacute;n/Ambientes
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mainblock">
        <h2>
            Administraci&oacute;n de Animales</h2>
        <% if (!string.IsNullOrEmpty((string)this.TempData["EditMessage"]))
           { %>
        <h3 class="accionerror">
            <%= Html.Encode(this.TempData["EditMessage"])%></h3>
        <% }
           else
           { %>
        <div id="maincontent">
            <%= Html.ValidationSummary("No se pudo guardar el animal. Por favor corregir los errores e intentar nuevamente.") %>
            <% using (Html.BeginForm())
               {%>
            <div class="editbox">
                <fieldset>
                    <p>
                        <label for="Name">
                            Nombre:</label><%= this.Html.TextBox("Name") %></p>
                    <div class="clear">
                    </div>
                    <p>
                        <label for="Species">
                            Especie:</label><%= this.Html.TextBox("Species") %></p>
                    <div class="clear">
                    </div>
                    <p>
                        <label for="Sex">
                            Sexo:</label><%= this.Html.TextBox("Sex")%></p>
                    <div class="clear">
                    </div>
                    <p>
                        <label for="BirthDate">
                            Fecha de nacimiento:</label><%= this.Html.TextBox("BirthDate")%></p>
                    <div class="clear">
                    </div>
                    <p>
                        <label for="Cost">
                            Costo ($):</label><%= this.Html.TextBox("Cost")%></p>
                    <div class="clear">
                    </div>
                    <p>
                        <label for="BornInCaptivity">
                            Nacido en cautiverio:</label><%= this.Html.CheckBox("BornInCaptivity") %></p>
                    <div class="clear">
                    </div>
                    <p>
                        <label for="ResponsibleId">
                            Responsable:</label><%= Html.DropDownList("ResponsibleId",
                                                        this.Model.ResponsiblesAvailable.Select(
                                                                    r => new SelectListItem
                                                                        {
                                                                            Text = string.Format(
                                                                                CultureInfo.CurrentCulture,
                                                                                "{0} {1} ({2})",
                                                                                r.Name,
                                                                                r.LastName,
                                                                                r.Email),
                                                                            Value = r.ResponsibleId}),
                                                        "Seleccionar Responsable...") %></p>
                    <div class="clear">
                    <p>
                        <label for="EnvironmentId">
                            Ambiente:</label><%= Html.DropDownList("EnvironmentId",
                                                        this.Model.EnvironmentsAvailable.Select(
                                                                    env => new SelectListItem
                                                                        {
                                                                            Text = string.Format(
                                                                                CultureInfo.CurrentCulture,
                                                                                "{0} ({1} m²)",
                                                                                env.Name,
                                                                                env.Surface),
                                                                            Value = env.EnvironmentId
                                                                        }),
                                                        "Seleccionar Ambiente...") %></p>
                    <div class="clear">
                    </div>
                    <p>
                        <label for="NextHealthMeasure">
                            Pr&oacute;ximo examen m&eacute;dico:</label><%= this.Html.TextBox("NextHealthMeasure")%></p>
                    <div class="clear">
                    </div>
                    <%= this.Html.Hidden("AnimalId", this.Model.AnimalId)%>
                </fieldset>
                <fieldset id="withaddlink">
                    <legend>Ex&aacute;menes M&eacute;dicos</legend>
                    <span>
                         <%= Html.ActionLink("Agregar", "NewHealthMeasure", "Administration", new { animalId = this.Model.AnimalId } , new { Class = "newlink" })%>
                    </span>
                    <div class="clear"></div>
                    <ul id="dinlist">
                        <% var index = 0;
                           foreach (var healthMeasure in this.Model.HealthMeasures)
                           {
                               index++; %>
                        <li>
                            <label><%= string.Format(CultureInfo.CurrentCulture, "Examen {0} ({1})", index, healthMeasure.MeasurementDate) %></label>                            
                            <%= Html.ActionLink("Editar", "EditHealthMeasure", "Administration", new { healthMeasureId = healthMeasure.HealthMeasureId }, new { Class = "editlink" })%>
                            <div class="clear"></div>
                        </li>
                        <% } %>
                    </ul>
                </fieldset>
                <fieldset id="withaddlink" class="last">
                    <legend>Horas de Alimentaci&oacute;n</legend>
                    <span>
                        <a href="JavaScript:addFeedingTime('dinlist2');" class="newlink">Agregar</a>
                    </span>
                    <div id="dinlist2">
                        <% var feedingTimeIndex = 1;
                           foreach (var feedingTime in this.Model.FeedingTimes)
                           { %>
                        <p id="<%= feedingTime.FeedingTimeId + "-HEAD" %>" class="timeslothead">
                            Horario <%= feedingTimeIndex %>
                            <a href="JavaScript:removeCollapsableItem('<%= feedingTime.FeedingTimeId %>')" class="deletelink">Remover</a>
                        </p>
                        <div id="<%= feedingTime.FeedingTimeId + "-BODY" %>" class="timeslotbody">
                            <p class="threerow">
                                <label>
                                    Hora:&nbsp;</label><%= Html.TextBox("FeedingTimes[" + (feedingTimeIndex - 1) + "].Time", feedingTime.Time)%>
                            </p>
                            <p class="threerow">
                                <label>
                                    Cantidad (gramos):&nbsp;</label><%= Html.TextBox("FeedingTimes[" + (feedingTimeIndex - 1) + "].Amount", feedingTime.Amount)%>
                            </p>
                            <p class="threerow">
                                <label>
                                    Alimento:&nbsp;</label><%= Html.DropDownList("FeedingTimes[" + (feedingTimeIndex - 1) + "].FeedingId",
                                                         this.Model.FeedingsAvailable.Select(
                                                                    feeding => new SelectListItem
                                                                    {
                                                                        Text = feeding.Name,
                                                                        Value = feeding.FeedingId,
                                                                        Selected = feeding.FeedingId.Equals(feedingTime.FeedingId, StringComparison.InvariantCultureIgnoreCase)
                                                                    }),
                                                        "Seleccionar Alimento...")%>
                            </p>
                           
                            <%= Html.Hidden("FeedingTimes[" + (feedingTimeIndex - 1) + "].FeedingTimeStatus", feedingTime.FeedingTimeStatus) %>
                            <%= Html.Hidden("FeedingTimes[" + (feedingTimeIndex - 1) + "].FeedingTimeId", feedingTime.FeedingTimeId)%>
                            <div class="clear"></div>
                        </div>
                            <% feedingTimeIndex++; %>
                        <% } %>
                    </div>
                </fieldset>
                <div style="float: right;">
                    <input type="submit" value="Guardar" />
                    <input type="reset" value="Cancelar" />
                </div>
                <div class="clear">
                </div>
            </div>
            <% } %>
        </div>
        <% } %>
        <div class="clear"></div>
        
        <%= Html.DropDownList("feedingsTemplate",
                            this.Model.FeedingsAvailable.Select(
                                feeding => new SelectListItem
                                            {
                                                Text = feeding.Name,
                                                Value = feeding.FeedingId,
                                            }),
                                "Seleccionar Alimento...", new { style="display: none;" }) %>
    </div>
</asp:Content>
