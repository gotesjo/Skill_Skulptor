
function showAlreadyMemberAlert()(ProjectId) {
    if (confirm(Alert("Du �r redan med i detta projektet"))) {
        location.href = '/ProjketController/JoinProject?userId=' + ProjectId;
    }
}