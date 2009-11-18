<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Administraci&oacute;n Inteligente de Zool&oacute;gicos - Administraci&oacute;n
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Administraci&oacute;n</h2>

    <p><strong>TODO</strong></p>
            
    <ul>
        <li><%= Html.ActionLink("Encuestas", "Surveys", "Statistics")%></li>
        <li><%= Html.ActionLink("Ambientes", "Environments", "Statistics")%></li>
        <li><%= Html.ActionLink("Animales", "Animals", "Statistics")%></li>
    </ul>

</asp:Content>
