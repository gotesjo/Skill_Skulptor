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
    $(document).on('click', '.user-div', function () {
        var userId = $(this).data('user-id');

        // Anropa din metod med användarens id och uppdatera konversationen
        UpdateConversation(userId);
        console.log('User clicked. User ID:', userId);
    });

    function UpdateConversation(userId) {
        // Använd AJAX för att skicka en asynkron förfrågan till servern
        $.ajax({
            type: 'GET',
            url: '/Message/GetConversation',
            data: { userId: userId }, // Här skickar du med userId som parameter
            success: function (result) {
                // Uppdatera konversationssektionen med den nya delvis vyn
                $('#conversation').html(result);
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
});