$(document).ready(function() {
    var activeMenu;
    $("#nav > ul > li > a").bind("mouseover", function() {
        var ul = $(this).parent().find("ul");
        if (activeMenu && ul.get(0) != activeMenu.get(0)) {
            activeMenu.fadeOut("fast");
        }
        if (ul.length == 0) return;
        ul.show();
        var x = $(this).position().left + (this.offsetWidth / 2) - (ul.get(0).offsetWidth / 2) - 23;
        ul.css("left", x + "px");
        ul.hide().fadeIn("fast");
        activeMenu = ul;
    });

    //hide the all of the element with class msg_body
    $(".timeslotbody").hide();
    //toggle the componenet with class msg_body
    $(".timeslothead").click(toggleAnimation);
});

function toggleAnimation(e) {
    var offset = (this.offsetWidth / 2) + $(this).position().left;

    if (e.clientX - offset <= 542) {
        $(this).next(".timeslotbody").slideToggle(600);
    }
}

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

function removeAnimal(id) {
    if ($("#" + id + " input")[1].value == "New") {
        $("#" + id + " input")[1].value = "None";
    }
    else {
        $("#" + id + " input")[1].value = "Remove";
    }

    $("#" + id)[0].style.display = "none";
}

function removeTimeSlot(id) {
    var headId = id + "-HEAD";
    var bodyId = id + "-BODY";
    
    if ($("#" + bodyId + " input")[1].value == "New") {
        $("#" + bodyId + " input")[1].value = "None";
    }
    else {
        $("#" + bodyId + " input")[1].value = "Remove";
    }

    $("#" + headId)[0].style.display = "none";
    $("#" + bodyId)[0].style.display = "none";
}

function addToList(comboId, listId) {
    var index = $("#" + comboId)[0].selectedIndex;
    var textSelected = $("#" + comboId).children()[index].text;
    var valueSelected = $("#" + comboId).children()[index].value;

    if (valueSelected != null && valueSelected != "") {
        if ($("#" + valueSelected).length == 0) {
            var html = "<li id=\"" + valueSelected + "\">"
              + "<label>" + textSelected + "</label>"
              + "<a class=\"deletelink\" href=\"JavaScript:removeAnimal('" + valueSelected + "')\">Remover</a>"
              + "<input type=\"hidden\" value=\"" + valueSelected + "\" name=\"Animals[" + valueSelected + "].AnimalId\" id=\"Animals[" + valueSelected + "]_AnimalId\">"
              + "<input type=\"hidden\" value=\"New\" name=\"Animals[" + valueSelected + "].AnimalStatus\" id=\"Animals[" + valueSelected + "]_AnimalStatus\">"
              + "<div class=\"clear\"></div>"
              + "</li>";

            $("#" + listId)[0].innerHTML += html;
        }
        else {
            $("#" + valueSelected + " input")[1].value = "New";
            $("#" + valueSelected)[0].style.display = "";
        }
    }
}

function addNewTimeSlot(id) {
    var divElement = $("#" + id)[0];
    var newIndex = (divElement.children.length / 2) + 1;

    var html = "<p id=\"" + newIndex.toString() + "-HEAD\" class=\"timeslothead\">"
      + "Itervalo " + newIndex.toString() + "<a href=\"JavaScript:removeTimeSlot('" + newIndex + "')\" class=\"deletelink\">Remover</a>"
      + "</p>"
      + "<div id=\"" + newIndex.toString() + "-BODY\" class=\"timeslotbody\">"
      + "<p>"
      + "<label>Hora inicial:</label><input id=\"TimeSlots[" + newIndex.toString() + "]_InitialTime\" name=\"TimeSlots[" + newIndex.toString() + "].InitialTime\" type=\"text\" value=\"00:00:00\" />"
      + "</p>"
      + "<p>"
      + "<label>Hora final:</label><input id=\"TimeSlots[" + newIndex.toString() + "]_FinalTime\" name=\"TimeSlots[" + newIndex.toString() + "].FinalTime\" type=\"text\" value=\"00:00:00\" />"
      + "</p>"
      + "<p>"
      + "<label>Temperatura M&iacute;nima (&#176;C):</label><input id=\"TimeSlots[" + newIndex.toString() + "]_TemperatureMin\" name=\"TimeSlots[" + newIndex.toString() + "].TemperatureMin\" type=\"text\" value=\"0\" />"
      + "</p>"
      + "<p>"
      + "<label>Temperatura M&aacute;xima (&#176;C):</label><input id=\"TimeSlots[" + newIndex.toString() + "]_TemperatureMax\" name=\"TimeSlots[" + newIndex.toString() + "].TemperatureMax\" type=\"text\" value=\"0\" />"
      + "</p>"
      + "<p>"
      + "<label>Humedad M&iacute;nima (%):</label><input id=\"TimeSlots[" + newIndex.toString() + "]_HumidityMin\" name=\"TimeSlots[" + newIndex.toString() + "].HumidityMin\" type=\"text\" value=\"0\" />"
      + "</p>"
      + "<p>"
      + "<label>Humedad M&aacute;xima (%):</label><input id=\"TimeSlots[" + newIndex.toString() + "]_HumidityMax\" name=\"TimeSlots[" + newIndex.toString() + "].HumidityMax\" type=\"text\" value=\"0\" />"
      + "</p>"
      + "<p>"
      + "<label>Luminosidad M&iacute;nima (lx):</label><input id=\"TimeSlots[" + newIndex.toString() + "]_LuminosityMin\" name=\"TimeSlots[" + newIndex.toString() + "].LuminosityMin\" type=\"text\" value=\"0\" />"
      + "</p>"
      + "<p>"
      + "<label>Luminosidad M&aacute;xima (lx):</label><input id=\"TimeSlots[" + newIndex.toString() + "]_LuminosityMax\" name=\"TimeSlots[" + newIndex.toString() + "].LuminosityMax\" type=\"text\" value=\"0\" />"
      + "</p>"
      + "<input id=\"TimeSlots[" + newIndex.toString() + "]_TimeSlotStatus\" name=\"TimeSlots[" + newIndex.toString() + "].TimeSlotStatus\" type=\"hidden\" value=\"New\" />"
      + "<div class=\"clear\"></div>"
      + "</div>";

    divElement.innerHTML += html;
    
    //toggle the componenet with class msg_body
    $(".timeslothead").click(toggleAnimation);
}

function searchEnvironmentKeyPressed(control, e) {
    key = (document.all) ? e.keyCode : e.which;

    if (key == 13) {
        redirectSearchEnvironment();
    }
}

function redirectSearchEnvironment() {
    var searchCriteria = $("#searchCriteria")[0].value;

    window.location = "/ZoosManagementSystem/Administration/Environments/Search/" + searchCriteria;
}