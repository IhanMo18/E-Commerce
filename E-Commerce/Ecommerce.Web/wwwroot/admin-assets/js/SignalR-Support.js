let adminId=""
let messageToUser = "";
let _data = {};
let client = "";
let adminList = [];
let sendButton = document.querySelector("#sendButton")
document.addEventListener("DOMContentLoaded", function () {
  
  adminId = document.querySelector("#userId").value;


  connection.invoke("GetHistory",adminList[0],userId)
      .then(()=>console.log("History get Success"))
  

  //Para saber Todos los Admins Coonectados
  connection.on("OnlineAdminList", function (list) {
    adminList = list;
    console.log("Admins online:", adminList);
  });

  
  //Para saber Todos los Usuarios Conectados
  connection.on("OnlineClientList", async function (clientList) {
    const chat_listDiv = document.querySelector(".conversation-list");
    if (!chat_listDiv) return;

    chat_listDiv.innerHTML = ""; // Limpia lista anterior

    for (const userId of clientList) {
      const response = await fetch(`/users/img/${userId}`);
      const user = await fetch(`/users/username/${userId}`);
      const data = await response.json();
      const username = await user.json();
      
      chat_listDiv.innerHTML += `
        <div class="conversation-item p-3 border-bottom">
          <div class="d-flex align-items-center">
            <img src="${data.url}" class="rounded-circle conversation-avatar" alt="User">
            <div class="ms-3">
              <h6 class="mb-0">${username.username}</h6>
              <small class=" text-success">Online</small>
            </div>
          </div>
        </div>`;
    }
  });

  //Para recibir los mensajes de Usuarios a soporte
  connection.on("ReceiveMessageFromUser", async function (userSender, userReceptorId, messageFromUser) {
     client = userSender;
     
    const response = await fetch(`/users/img/${userSender}`);
    const data = await response.json();
    _data = data;

    const chatDiv = document.querySelector(".message-group");
    const messageHTML = `
      <div class="message">
        <img src="${data.url}" class="rounded-circle message-avatar" alt="User">
        <div class="message-content">
          <p>${messageFromUser}</p>
          <small class="message-time">${new Date().toLocaleTimeString()}</small>
        </div>
      </div>`;

    chatDiv.insertAdjacentHTML("beforeend", messageHTML);
  });
});



//Para enviar los mensajes de soporte a usuario
sendButton.addEventListener("click", async () => {

  const response = await fetch(`/users/img/${adminId}`);
  const data = await response.json();
  messageToUser = document.getElementById("message").value

  await connection.invoke("SendToUser",adminId,client, messageToUser).then(()=>{
    const chatDiv = document.querySelector(".message-group");
    const messageHTML = `
            <div class="message sent">
              <img src="${data.url}" class="rounded-circle message-avatar" alt="Client">
              <div class="message-content">
                <p>${messageToUser}</p>
                <small class="message-time">${new Date().toLocaleTimeString()}</small>
              </div>
            </div>`;

    chatDiv.insertAdjacentHTML("beforeend", messageHTML)
  }).catch(err => console.error(err.toString()));
})


//Recibir el historial de mensajes entre dos usuarios

  
  


