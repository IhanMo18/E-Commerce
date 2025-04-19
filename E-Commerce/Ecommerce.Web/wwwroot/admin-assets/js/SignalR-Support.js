const adminId = document.querySelector("#userId").value;
var adminList
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/messageHub")
    .build();

connection.start().then(() => {
  connection.invoke("Register", adminId, "Admin");
  console.log("Admin registrado en el Hub");
}).catch(err => console.error(err.toString()));



connection.on("OnlineAdminList",function (list){
      adminList=list
      console.log(adminList) 
})


connection.on("ReceiveMessageFromUser", function (userSender,userReceptorId, messageText) {
  const chatDiv = document.querySelector(".message-group");;
  const messageHTML = `<div class="message">
                        <img  class="rounded-circle message-avatar" alt="User">
                        <div class="message-content">
                            <p>${messageText}</p>
                            <small class="message-time">${new Date().toLocaleTimeString()}</small>
                        </div>
                    </div>`;
  
  chatDiv.insertAdjacentHTML("beforeend", messageHTML);
});



document.addEventListener("DOMContentLoaded", function () {
  const chatModal = document.getElementById("chatModal");
  

  if (chatModal) {
    chatModal.addEventListener("shown.bs.modal", () => {
      var usersOnline
      var chat_listDiv
      
      connection.on("OnlineClientList",function (clientList) {
        usersOnline=clientList
        chat_listDiv=document.querySelector(".conversation-list")
        console.log(`Los users onlines son : ${usersOnline}`)
      })
     
     
     
      usersOnline.forEach(userId=>{
        chat_listDiv.innerHTML+=`
                     <div class="conversation-item p-3 border-bottom">
                    <div class="d-flex align-items-center">
                        <img class="rounded-circle conversation-avatar" alt="User">
                        <div class="ms-3">
                            <h6 class="mb-0">${userId}</h6>
                            <small class="text-muted"></small>
                        </div>
                    </div>
                </div>`
        
        
        
      })
      
      var chatList = document.querySelector("#chatList");
      var client 
      chatList.addEventListener("click", () => {
        chatList.className="active"
        client=document.querySelector(".username").id;
      })
      
      
      const sendButton = document.getElementById("sendButton");
      const messageInput = document.getElementById("message");

      if (sendButton && messageInput) {
        sendButton.addEventListener("click", () => {
          const message = messageInput.value.trim();
          if (message === "") return;
          console.log(adminList)
          
          
          connection.invoke("SendToUser",adminList[0] ,client,message )
              .catch(err => console.error(err.toString()));
          
          const chatDiv = document.querySelector(".message-group");
          const messageHTML = `
                    <div class="message sent">
                        <img src="" class="rounded-circle message-avatar" alt="Client">
                           <div class="message-content">
                             <p>${message}</p>
                            <small class="message-time">${new Date().toLocaleTimeString()}</small>
                           </div>
                    </div>`;


          chatDiv.insertAdjacentHTML('beforeend', messageHTML);
          messageInput.value = "";
        });
        sendButtonRegistered = true;
      }
    });
  }
});







