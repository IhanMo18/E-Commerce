var userId = document.querySelector("#userId").value;
var adminList

connection.start().then(() => {
    connection.invoke("Register", userId, "Client");
    console.log("SignalR Conectado");
    console.log("Client registrado en el Hub");
}).catch((error) => console.log(error));



//Para saber Todos los Admins Coonectados
connection.on("OnlineAdminList",async function (connectedAdmins) {
    adminList = connectedAdmins
    console.log(`Admin:${adminList}`)
    console.log(`Admin0:${adminList[0]}`)
    
    const response = await fetch(`/users/img/${adminList[0]}`);
    const data = await response.json();
    console.log(data.url)
    
    const divPhotos = document.querySelectorAll(".photo");
    divPhotos.forEach((i)=>i.src=`${data.url}`)
    const messageTime = document.querySelector(".message-time")
    messageTime.innerHTML=new Date().toLocaleTimeString()
});



//Para recibir los mensajes de Soporte
connection.on("ReceiveSupportMessage", async function (userSender,message) {
   
    const response = await fetch(`/users/img/${adminList[0]}`);
    const data = await response.json();
    
    const chatDiv = document.querySelector(".message-group");
    const messageHTML = `
        <div class="message">
            <img src="${data.url}" class="rounded-circle message-avatar" alt="Support">
            <div class="message-content">
                <p>${message}</p>
                <small class="message-time">${new Date().toLocaleTimeString()}</small>
            </div>
        </div>`;
    chatDiv.insertAdjacentHTML('beforeend', messageHTML);
});


