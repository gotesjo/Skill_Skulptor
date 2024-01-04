// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Scripts
// 

window.addEventListener('DOMContentLoaded', event => {

    // Activate Bootstrap scrollspy on the main nav element
    const sideNav = document.body.querySelector('#sideNav');
    if (sideNav) {
        new bootstrap.ScrollSpy(document.body, {
            target: '#sideNav',
            rootMargin: '0px 0px -40%',
        });
    };

    // Collapse responsive navbar when toggler is visible
    const navbarToggler = document.body.querySelector('.navbar-toggler');
    const responsiveNavItems = [].slice.call(
        document.querySelectorAll('#navbarResponsive .nav-link')
    );
    responsiveNavItems.map(function (responsiveNavItem) {
        responsiveNavItem.addEventListener('click', () => {
            if (window.getComputedStyle(navbarToggler).display !== 'none') {
                navbarToggler.click();
            }
        });
    });

});


//För konversationssida

$(document).ready(function () {

    // Uppdatera meddelandeantal vid sidan av
    updateUnreadMessagesCount();

    // Timer för uppdareringen
    setInterval(updateUnreadMessagesCount, 1000);

    $(document).on('click', '.user-div', function () {
        var otherUserID = $(this).data('user-id');

        // Anropa din metod med användarens id och uppdatera konversationen
        UpdateConversation(otherUserID);
        console.log('User clicked. User ID:', otherUserID);
    });
    scrollToBottom();
});

function UpdateConversation(otherUserID) {
    // Använd AJAX för att skicka en asynkron förfrågan till servern
    $.ajax({
        type: 'GET',
        url: '/Message/GetConversation',
        data: { otherUserID: otherUserID }, // Här skickar du med userId som parameter
        success: function (result) {
            // Uppdatera konversationssektionen med den nya delvis vyn
            $('#conversation').html(result);
            scrollToBottom();
        },
        error: function (error) {
            console.log(error);
        }
    });
}

//Metod för att skicka meddelanden och sedan uppdatera vyn.
function sendMessage() {
    console.log("knappen är klickad");
    var newMessage = document.getElementById("Newmessage").value;
    var _otherUserId = $(".receiver_user-div").data("user-id");

    var data = {
        Newmessage: newMessage,
        receiverId: _otherUserId
    };

    $.ajax({
        type: "POST",
        url: "/Message/Send", 
        data: data,
        success: function () {
            UpdateConversation(_otherUserId);
        },
        error: function (error) {
            console.error(error);
        }
    });
}

function readMessage(Message_id) {
    console.log("Försöker läsa meddelandet");
    var messageId = Message_id;
    var _otherUserId = $(".receiver_user-div").data("user-id");
    console.log(messageId);

    var data = {
        _messageID: messageId
    };

    $.ajax({
        type: "POST",
        url: "/Message/MarkRead",
        data: data,
        success: function () {
            /*UpdateConversation(_otherUserId);*/
        },
        error: function (error) {
            console.error(error);
        }
    });

}

function scrollToBottom() {
    var messageContainer = document.getElementById('messageContainer');
    messageContainer.scrollTop = messageContainer.scrollHeight;
}

// Hantering av olästa meddelanden


    // Funktion för att uppdatera meddelandeantal
function updateUnreadMessagesCount() {
        $.ajax({
            url: '/Message/UnreadMessages', 
            type: 'GET',
            success: function (result) {
                var unreadMessagesCountElement = $('#unreadMessagesCount');

                // Uppdatera antalet olästa meddelanden
                unreadMessagesCountElement.text(result);

                if (result === 0) {
                    unreadMessagesCountElement.hide();
                } else {
                    unreadMessagesCountElement.show();
                }
            },
            error: function (error) {
                console.error(error);
            }
        });
}
