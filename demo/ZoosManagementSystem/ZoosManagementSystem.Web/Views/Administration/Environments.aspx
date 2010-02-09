<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<ZoosManagementSystem.Web.Models.Environment>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Administraci&oacute;n Inteligente de Zool&oacute;gicos - Administraci&oacute;n/Ambientes
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mainblock">
        <h2>Administraci&oacute;n de Ambientes</h2>
        
        <% this.Html.BeginForm("SearchEnvironments", "Administration", FormMethod.Get, new { style = "margin-top: 8px; padding: 2px" }); %>
        
        Buscar Ambiente: <%= this.Html.TextBox("searchCriteria", Html.Encode(this.TempData["SearchCriteria"]), new { style = "width: 190px" })%>
        <input type="submit" value="Buscar" />
        
        <% this.Html.EndForm(); %>        
        
        <% if ((this.Model != null) && (this.Model.Count > 0))
           { %>
           
               <div id="maincontent">
               
            <% foreach (var environment in this.Model)
               { %>
                 
                 <div id="<%= environment.Id %>">
                    <h3><%= environment.Name %></h3>
                    <p><strong>TODO</strong></p>
                 </div>
                 
            <% } %>
               
               </div>
               <div class="clear"></div>          
        <% }
           else
           { %>
            <h3><%= Html.Encode(this.TempData["NoItemsMessage"]) %></h3> 
        <% } %>
   
    </div>
</asp:Content>
