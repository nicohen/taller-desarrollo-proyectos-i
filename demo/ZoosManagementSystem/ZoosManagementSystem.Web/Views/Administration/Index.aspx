<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Administraci&oacute;n Inteligente de Zool&oacute;gicos - Administraci&oacute;n
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mainblock">
        <h2>Administraci&oacute;n</h2>
        <ul>
            <li><%= Html.ActionLink("Ambientes", "Environments", "Administration")%></li>
            <li><%= Html.ActionLink("Animales", "Animals", "Administration")%></li>
        </ul>
    </div>
</asp:Content>
