var userId = document.querySelector("#userId").value;
var adminList

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/messageHub")
    .build();

connection.start().then(() => {
    connection.invoke("Register", userId, "Client");
    console.log("SignalR Conectado");
    console.log("Client registrado en el Hub");
}).catch((error) => console.log(error));


connection.on("OnlineAdminList", function (connectedAdmins) {
    adminList = connectedAdmins
    console.log(`Admin:${adminList}`)
});



connection.on("ReceiveSupportMessage", function (userSenderId,message) {
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

