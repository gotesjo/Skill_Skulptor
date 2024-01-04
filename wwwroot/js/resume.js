
function confirmDeleteCV(userId) {
    if (confirm("Är du säker på att du vill ta bort ditt CV?")) {
        location.href = '/Resume/DeleteCV?userId=' + userId;
    }
}

