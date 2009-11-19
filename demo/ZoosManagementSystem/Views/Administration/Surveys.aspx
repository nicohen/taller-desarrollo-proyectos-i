<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Administraci&oacute;n Inteligente de Zool&oacute;gicos - Administraci&oacute;n/Encuestas
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mainblock">
        <h2>Encuestas</h2>
        
        <div id="surveyselection">
            <p>
                Seleccionar el tipo de encuesta a llenar:</p>
            <%=
            Html.DropDownList(
                "AvailableSurveys",
                new List<SelectListItem>
                    {
                        new SelectListItem
                            { Text = "Encuesta de Animales", Value = Guid.NewGuid().ToString(), Selected = true },
                        new SelectListItem
                            { Text = "Encuesta de Instalaciones y Comodidades", Value = Guid.NewGuid().ToString() },
                        new SelectListItem
                            { Text = "Encuesta de Actividades y Espectáculos", Value = Guid.NewGuid().ToString() },
                    }) %>
        </div>
        <a id="newsurvey" href="#">Crear Nueva Encuesta</a>
        
        <div id="surveybody">
        </div>
    </div>
</asp:Content>
