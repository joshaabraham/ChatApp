"use strict";

//var connection = new signalR.HubConnectionBuilder()
//    .withUrl("/chatHub?chatUsername=Mr_browser", {
//        accessTokenFactory: () => "testing"
//    })
//    .build();

var chatUsername = 'c1f00994-6e75-4b14-bf1b-ca331be5c994_no-photo-female.jpg_female_Mahmuda Akhtar_23_5ft 3in_Muslim_Bachelors_UnMarried';

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

    connection.invoke("Typing", groupValue, "Mahmuda Akter").catch(function (err) {
        return console.error(err.toString());
    });
});

document.getElementById("btnDisconnect").addEventListener("click", function (evt) {

    connection.invoke("Disconnect").catch(function (err) {
        return console.error(err.toString());
    });
});

connection.on("TypingMessage", function () {
    //document.getElementById("typing").removeChild();
    var div = document.createElement("div");
    div.innerHTML = "Someone is typing...";
    document.getElementById("typing").appendChild(div);
});

connection.on("UserConnected", function (connectionId, chatUserList) {
    var groupElement = document.getElementById("group");

    for (var i = 0; i < chatUserList.length; i++) {

        var option = document.createElement("option");
        option.text = chatUserList[i].connectionId;
        option.value = chatUserList[i].connectionId;
        groupElement.add(option);

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
    var userId = 'c1f00994-6e75-4b14-bf1b-ca331be5c994'; // todo: make it dynamic. In app it should be dynamic already
    var photo = 'no-photo-female.jpg';

    connection.invoke("SendMessage", groupValue, userId, "Mahmuda Akhtar", photo, message).catch(function (err) {
        return console.error(err.toString());
    });

    event.preventDefault();
});
