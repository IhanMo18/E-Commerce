var userId = document.querySelector("#userId").value;


const connection = new signalR.HubConnectionBuilder()
    .withUrl("/messageHub")
    .build();

connection.start().then(() => {
    connection.invoke("Register", userId, "Client");
    console.log("SignalR Conectado");
    console.log("Client registrado en el Hub");
}).catch((error) => console.log(error));


connection.on("ReceiveSupportMessage", function (message) {
    const chatDiv = document.querySelector(".message-group");
    const messageHTML = `
        <div class="message">
            <img src="https://images.unsplash.com/photo-1633332755192-727a05c4013d?auto=format&fit=crop&w=32&h=32" class="rounded-circle message-avatar" alt="Support">
            <div class="message-content">
                <p>${message}</p>
                <small class="message-time">${new Date().toLocaleTimeString()}</small>
            </div>
        </div>`;
    chatDiv.insertAdjacentHTML('beforeend', messageHTML);
});


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
                    
                    connection.invoke("SendToSupport" ,userId, message )
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

