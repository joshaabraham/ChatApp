"use strict";

var chatUsername = 'siteUserName_Danieal Gonzalez_male2.png_United Kingdom';

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub?chatUsername=" + chatUsername)
    .build();

connection.on("ReceiveMessage", function (connectionId, userId, name, photo, message, uniqueId, isMe) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var div = document.createElement("div");
    div.innerHTML = msg + "<hr/>";
    document.getElementById("messages").appendChild(div);
});

document.getElementById("message").addEventListener("input", function (evt) {

    var groupElement = document.getElementById("group");
    var groupValue = groupElement.options[groupElement.selectedIndex].value;

    connection.invoke("Typing", groupValue, "Danieal Gonzalez").catch(function (err) {
        return console.error(err.toString());
    });
});

document.getElementById("btnDisconnect").addEventListener("click", function (evt) {

    connection.invoke("Disconnect").catch(function (err) {
        return console.error(err.toString());
    });
});

connection.on("TypingMessage", function () {
    //document.getElementById("typing").innerHTML = '';
    //var div = document.createElement("div");
    //div.innerHTML = "Someone is typing...";
    //document.getElementById("typing").appendChild(div);
});

connection.on("UserConnected", function (connectionId, chatUserList) {
    var groupElement = document.getElementById("group");

    for (var i = 0; i < chatUserList.length; i++) {

        if (chatUserList[i].chatUsername !== chatUsername) { // exclude own name
            var option = document.createElement("option");
            option.text = chatUserList[i].connectionId;
            option.value = chatUserList[i].connectionId;
            groupElement.add(option);
        }
    }
});

connection.on("UserDisconnected", function (connectionId) {

    alert('user disconnected');

    var groupElement = document.getElementById("group");
    for (var i = 0; i < groupElement.length; i++) {
        if (groupElement.options[i].value == connectionId) {
            groupElement.remove(i);
        }
    }
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("message").value;
    var groupElement = document.getElementById("group");
    var groupValue = groupElement.options[groupElement.selectedIndex].value;
    var userId = 'c1f00994-6e75-4b14-bf1b-ca331be5c994'; // Replace with userid
    var photo = 'male2.png'; // Replace with user photo
    var name = 'Danieal Gonzalez'; // Replace with user name

    connection.invoke("SendMessage", groupValue, userId, name, photo, message).catch(function (err) {
        return console.error(err.toString());
    });

    event.preventDefault();
});
