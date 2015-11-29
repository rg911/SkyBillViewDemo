$(document).ready(function () {
    ToggleTable();
});

function ToggleTable() {
    var trs = $("#internalActivities tr");
    var btnMore = $("#seeMoreRecords");
    var btnLess = $("#seeLessRecords");
    var trsLength = trs.length;
    var currentIndex = 6;
    trs.hide();
    trs.slice(0, 6).show();
    checkButton();

    btnMore.click(function (e) {
        e.preventDefault();
        $("#internalActivities tr").slice(currentIndex, trs.length - 1).show();
        currentIndex = trs.length - 1;
        checkButton();
    });

    btnLess.click(function (e) {
        e.preventDefault();
        $("#internalActivities tr").slice(6, trs.length - 1).hide();
        currentIndex = 6;
        checkButton();
    });

    function checkButton() {
        var currentLength = $("#internalActivities tr:visible").length;

        if (currentLength == trsLength-1) {
            btnMore.hide();
            btnLess.show();
        } else {
            btnMore.show();
            btnLess.hide();
        }
    }
}