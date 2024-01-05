
function showAlreadyMemberAlert()(ProjectId) {
    if (confirm(Alert("Du är redan med i detta projektet"))) {
        location.href = '/ProjketController/JoinProject?userId=' + ProjectId;
    }
}