/// <reference path="jquery-1.9.1.js" />
/// <reference path="jquery.signalR-1.0.1.js" />
/// <reference path="jquery-ui-1.10.2.js" />
$(function () {
    var chat = $.connection.chat;

    chat.client.addMessage = function (message) {
        $("#messages").append('<li>' + message + '</li>');
    };

    $.connection.hub.start().done(function () {
        $("#broadcast").click(function () {
            chat.server.send($('#msg').val());
        });
    });
});