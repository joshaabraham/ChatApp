ChatApp contains with two projects:
1. ChatServer (Asp.net SignalR chat hub server)
2. ChatApp (Xamarin forms project)


How to run the app:

Step-1: Rebuild the Solution

Step-2: Run "ChatServer" project (Its a chat hub server) and take the server IP and set the IP into function "GetInstanse()" under xamarin PCL project "Chat/Helpers/ChatHelper.cs"
Note: If you running "ChatServer" in localhost then you can take the ip through conveyor plugin. (https://keyoti.com/products/conveyor/index.html)

After running "ChatServer", a browser based chat client will open where you can chat with the App (android or iOS)
Note: You need to have a .Net Core installed in your pc in order to run "ChatServer"

Step-3: Run "ChatApp" Android or iOS Project

Step-4: Register your account

Step-5: Login you account by username and password that you created in Step-3.

Step-6: After login, you will move to chat user list page. You will find a user named "Danieal Gonzalez" who is auto logged in by browser chat client in step-1

Step-7: Now click any user to start chat and send/receive message.

Enjoy!