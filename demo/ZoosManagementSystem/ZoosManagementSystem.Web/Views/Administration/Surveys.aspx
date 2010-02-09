<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Administraci&oacute;n Inteligente de Zool&oacute;gicos - Administraci&oacute;n/Encuestas
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mainblock">
        <h2>
            Encuestas</h2>
        <div id="surveyselection">
            <p>
                Seleccionar el tipo de encuesta a llenar:</p>
            <%=
            Html.DropDownList(
                "availablesurveys",
                new List<SelectListItem>
                    {
                        new SelectListItem
                            { Text = "Encuesta de Animales", Value = "Survey1" },
                        new SelectListItem
                            { Text = "Encuesta de Instalaciones y Comodidades", Value = "Survey2" },
                        new SelectListItem
                            { Text = "Encuesta de Actividades y Espectáculos", Value = "Survey3" },
                    },
                "Seleccionar Encuenta...",
                new { onchange = "changeSurvey();" })%>
        </div>
        <a id="newsurvey" href="#">Nueva Encuesta</a>
        
        <div class="clear"></div>
        
        <div id="maincontent">
            <div id="Survey1">
                <h3>Encuesta de Animales</h3>
                
                <p><strong>TODO</strong></p>
                
                <input type="submit" value="Enviar Encuesta" />
            </div>
            <div id="Survey2">
                <h3>Encuesta de Instalaciones y Comodidades</h3>
                
                <p><strong>TODO</strong></p>
                
                <input type="submit" value="Enviar Encuesta" />
            </div>
            <div id="Survey3">
                <h3>Encuesta de Actividades y Espectáculos</h3>
                
                <p><strong>TODO</strong></p>
                
                <input type="submit" value="Enviar Encuesta" />          
            </div>
        </div>
    </div>
</asp:Content>
