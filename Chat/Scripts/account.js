/// <reference path="jquery-1.9.1.js" />
$(function () {
    var $firstName = $('#firstName');
    var $lastName = $('#lastName');
    var $saveFirstName = $('#saveFirstName');
    var $saveLastName = $('#saveLastName');

    $.getJSON("api/AccountSettings/GetFirstName", function (data) {
        $firstName.val(data);
    });

    $.getJSON("api/AccountSettings/GetLastName", function (data) {
        $lastName.val(data);
    });

    $saveFirstName.click(function () {
        $.post("api/AccountSettings/UpdateFirstName", { '' : $firstName.val() }, function (data) { });
    });

    $saveLastName.click(function () {
        $.post("api/AccountSettings/UpdateLastName", { '' : $lastName.val() }, function (data) { });
    });
});