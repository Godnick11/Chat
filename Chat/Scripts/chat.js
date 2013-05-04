/// <reference path="jquery-1.9.1.js" />
/// <reference path="jquery.signalR-1.0.1.js" />
/// <reference path="jquery-ui-1.10.2.js" />
$(function () {
    var addMessage = function (message) {
        $("#messages").prepend('<li><b>' + message.WhoPosted + '</b>: ' + message.Text + '</li>');
    };

    $.getJSON("api/ChatHistory/GetLastMessages", { messageCount: 10 }, function (data) {
        $.each(data, function (key, val) {
            addMessage(val);
        });
    });

    var chat = $.connection.chat;

    chat.client.addMessage = function (message) {
        addMessage(message);
    };

    $.connection.hub.start().done(function () {
        $("#broadcast").click(function () {
            chat.server.send($('#msg').val());
        });
    });
});