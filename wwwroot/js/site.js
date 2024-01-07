
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


//För konversationssidan
$(document).ready(function () {

    // Uppdatera meddelandeantal vid sidan av
    // Timer för uppdareringen
    setInterval(updateUnreadMessagesCount, 1000);

    //När man klickar på en person i Meddelandesidan. För att öppna konversationen
    $(document).on('click', '.user-div', function () {
        var otherUserID = $(this).data('user-id');

        // Anropa din metod med användarens id och uppdatera konversationen
        UpdateConversation(otherUserID);
    });
    scrollToBottom();
});

function RunTimer() {
    return new Promise(function (resolve, reject) {
        $.ajax({
            type: 'GET',
            url: '/Message/IsLoggedIn',
            success: function (result) {
                resolve(result);
            },
            error: function (error) {
                reject(error);
            }
        });
    });
}

//Uppdaterar konversationen på meddelandesidan
function UpdateConversation(otherUserID) {
    if (otherUserID == null) {
        var unknonwUserName = $(".receiver_user-div").data("user-id");
    }
    $.ajax({
        type: 'GET',
        url: '/Message/GetConversation',
        data: { otherUserID: otherUserID }, 
        success: function (result) {
            //Renderar in en _Partial Vy för konversationen
            $('#conversation').html(result);
            scrollToBottom();
        },
        error: function (error) {
            console.log(error);
        }
    });
}

//Metod för att skicka meddelanden och sedan uppdatera vyn.
function sendMessage(newMessage, otherUserId) {

    _newMessage = newMessage || document.getElementById("Newmessage").value;
    _otherUserId = otherUserId || $(".receiver_user-div").data("user-id");

    var data = {
        Newmessage: _newMessage,
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

//När man klickar på "Läs meddelande" knappen
function readMessage(Message_id, otherUserId) {
    console.log("Försöker läsa meddelandet");
    var messageId = Message_id;
    var _otherUserId = $(".receiver_user-div").data("user-id");
    if (_otherUserId == null) {
        _otherUserId = $(".unknownID").data("unknown-id");
        console.log("Okänd användare med id: " + _otherUserId);
    }

    console.log(messageId);

    var data = {
        _messageID: messageId
    };

    $.ajax({
        type: "POST",
        url: "/Message/MarkRead",
        data: data,
        success: function () {
            UpdateConversation(_otherUserId);
        },
        error: function (error) {
            console.error(error);
        }
    });

}
// Funktion för att uppdatera meddelandeantal
function updateUnreadMessagesCount() {
    RunTimer().then(function (isLoggedIn) {
        if (!isLoggedIn) {
            // Användaren är inte inloggad, så avbryt uppdateringen
            return;
        }

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
    }).catch(function (error) {
        
    });
}
//Metod för att skicka meddelanden ifall man inte är inloggad
function sendUnknownMessage() {
    var _sendTo = document.querySelector('.send-unknown-div').getAttribute('data-to-id');
    var _from = null;
    //ifall en användare är inloggad eller inte
    try {
        _from = document.getElementById("UnknowName").value;
    } catch (error) {
        console.error("Error getting value for UnknowName:", error);
        _from = null; 
    }

    var _message = document.getElementById("UnknowMessage").value;

    if (_message < 1) {
        alert('Ogiltigt Meddelande!');
        return;
        var messageinput = document.getElementById("UnknowMessage");
        messageinput.style.background("red");

    }

    if (_from === null) {
        sendMessage(_message, _sendTo);
        clearSendUnknownForm();
        return;
    }
    else if (!isNameValid(_from)) {
        alert('Ogiltigt namn!');
        return;
    }

    var data = {
        newmessage: _message,
        receiverId: _sendTo,
        from: _from
    };

    $.ajax({
        type: "POST",
        url: "/Message/SendUnkown",
        data: data,
        success: function () {
            clearSendUnknownForm();
        },
        error: function (error) {
            console.error(error);
        }
    });
}

function clearSendUnknownForm() {
    var inputName = document.getElementById('UnknowName');
    var inputMessage = document.getElementById('UnknowMessage');

    // Rensa värdet i input-elementet
    inputMessage.value = '';
    inputName.value = '';
}

function scrollToBottom() {
    var messageContainer = document.getElementById('messageContainer');
    messageContainer.scrollTop = messageContainer.scrollHeight;
}


//Validering
function isNameValid($name) {
    var $pattern = /^[a-zA-Z0-9]*$/;
    if ($pattern.test($name)) {
        return true;
    }
    return false;
}