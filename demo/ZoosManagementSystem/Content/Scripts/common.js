$(document).ready(function() {
    var activeMenu;
    $("#nav > ul > li > a").bind("mouseover", function() {
        var ul = $(this).parent().find("ul");
        if (activeMenu && ul.get(0) != activeMenu.get(0)) {
            activeMenu.fadeOut("fast");
        }
        if (ul.length == 0) return;
        ul.show();
        var x = $(this).position().left + (this.offsetWidth / 2) - (ul.get(0).offsetWidth / 2) - 30;
        ul.css("left", x + "px");
        ul.hide().fadeIn("fast");
        activeMenu = ul;
    });
});

var lastSurveySelected = null;
function changeSurvey() {
    var index = $("#availablesurveys")[0].selectedIndex;
    var currentSurveySelected = $("#availablesurveys").children()[index].value;

    if (lastSurveySelected != currentSurveySelected) {
        if (lastSurveySelected != null) {
            $("#" + lastSurveySelected).hide();
        }

        if (currentSurveySelected != null && currentSurveySelected != "") {
            $("#" + currentSurveySelected).show();

            lastSurveySelected = currentSurveySelected;
        }
    }
}       