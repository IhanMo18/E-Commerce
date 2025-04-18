const adminId = "79e6ab1e-40f4-43b3-bc18-74736d5d6ef0";
var client = "42ccde53-fb5e-4cd7-a7c5-a51fb636ab4b";

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/messageHub")
    .build();

connection.start().then(() => {
  connection.invoke("Register", adminId, "Admin");
  console.log("Admin registrado en el Hub");
}).catch(err => console.error(err.toString()));

// 🧠 Escuchar mensajes de usuarios
connection.on("ReceiveMessageFromUser", function (clientId, message) {
  const chatDiv = document.querySelector(".message-group");;
  const messageHTML = `
    <div class="message">
      <div class="message-content">
        <strong>User:${clientId}:</strong>
        <p>${message}</p>
        <small>${new Date().toLocaleTimeString()}</small>
      </div>
    </div>`;
  chatDiv.insertAdjacentHTML("beforeend", messageHTML);
});


//  Enviar mensaje al cliente seleccionado
document.addEventListener("DOMContentLoaded", function () {
  const chatModal = document.getElementById("chatModal");

  if (chatModal) {
    chatModal.addEventListener("shown.bs.modal", () => {
      const sendButton = document.getElementById("sendButton");
      const messageInput = document.getElementById("message");

      if (sendButton && messageInput) {
        sendButton.addEventListener("click", () => {
          const message = messageInput.value.trim();
          if (message === "") return;
          
          connection.invoke("SendToUser",client,message )
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