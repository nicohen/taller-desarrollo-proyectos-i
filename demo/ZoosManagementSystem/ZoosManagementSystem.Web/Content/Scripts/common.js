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


function addToList(comboId, listId) {
    var index = $("#" + comboId)[0].selectedIndex;
    var currentSelected = $("#" + comboId).children()[index].value;

//<li id="AAAAAAAAAAAAAAAAAAAAAA">
//BBBBBBBBBBBBBBBBBBBBB&nbsp;-&nbsp;
//<a class="deletelink" href="JavaScript:removeAnimal('AAAAAAAAAAAAAAAAAAAAAAa')">Remover</a>
//<input type="hidden" value="AAAAAAAAAAAAAAAAAAAAAAAAA" name="Animals[AAAAAAAAAAAAAAA].AnimalId" id="Animals[AAAAAAAAAAAAAAAAAAAA]_AnimalId">
//<input type="hidden" value="New" name="Animals[AAAAAAAAAAAAAAAAAAAAAA].AnimalStatus" id="Animals[AAAAAAAAAAAAAAAAA]_AnimalStatus">
//</li>

    $("#" + listId)[0].innerHTML += "<li>Nuevo</li>";    
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