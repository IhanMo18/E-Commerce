let adminId = "";
let messageToUser = "";
let currentClient = "";
let adminList = [];
let messagesList = [];
const sendButton = document.querySelector("#sendButton");

// Utilidad para obtener imagen y nombre de usuario
async function fetchUserInfo(userId) {
  const [imgRes, userRes] = await Promise.all([
    fetch(`/users/img/${userId}`),
    fetch(`/users/username/${userId}`)
  ]);
  const img = await imgRes.json();
  const user = await userRes.json();
  return { img, username: user.username };
}

// Añadir mensaje al grupo (HTML)
function addMessageToChat({ imgUrl, messageText, dateTime, isSender }) {
  const chatDiv = document.querySelector(".message-group");
  const messageClass = isSender ? "sent" : "";
  const messageHTML = `
    <div class="message ${messageClass}">
      <img src="${imgUrl}" class="rounded-circle message-avatar" alt="User">
      <div class="message-content">
        <p>${messageText}</p>
        <small class="message-time">${dateTime}</small>
      </div>
    </div>
  `;
  chatDiv.insertAdjacentHTML("beforeend", messageHTML);
}

// Cargar historial
async function updateChatGroup() {
  for (const message of messagesList) {
    const { img } = await fetchUserInfo(message.senderId);
    const isSender = message.senderId === adminId;
    addMessageToChat({
      imgUrl: img.url,
      messageText: message.messageText,
      dateTime: message.dateTime,
      isSender
    });
  }
}

// Evento DOM cargado
document.addEventListener("DOMContentLoaded", () => {
  adminId = document.querySelector("#userId").value;

  // Admins online
  connection.on("OnlineAdminList", (list) => {
    adminList = list;
    console.log("Admins online:", adminList);
  });

  // Lista de clientes online y carga historial
  connection.on("OnlineClientList", async (clientList) => {
    const chatListDiv = document.querySelector(".conversation-list");
    if (!chatListDiv) return;

    chatListDiv.innerHTML = "";

    for (const userId of clientList) {
      const { img, username } = await fetchUserInfo(userId);

      // Cargar historial entre admin y cliente
      await connection.invoke("GetHistory", adminId, userId)
          .then(() => {
            connection.on("ReceivedHistory", async (messages) => {
              messagesList = messages;
              await updateChatGroup();
            });
          })
          .catch((err) => console.error("Error al obtener historial", err));

      chatListDiv.innerHTML += `
        <div class="conversation-item p-3 border-bottom">
          <div class="d-flex align-items-center">
            <img src="${img.url}" class="rounded-circle conversation-avatar" alt="User">
            <div class="ms-3">
              <h6 class="mb-0">${username}</h6>
              <small class="text-success">Online</small>
            </div>
          </div>
        </div>
      `;
    }
  });

  // Mensaje recibido desde usuario
  connection.on("ReceiveMessageFromUser", async (senderId, _, messageText) => {
    currentClient = senderId;
    const { img } = await fetchUserInfo(senderId);
    addMessageToChat({
      imgUrl: img.url,
      messageText,
      dateTime: new Date().toLocaleTimeString(),
      isSender: false
    });
  });
});

// Enviar mensaje a usuario
sendButton.addEventListener("click", async () => {
  const messageInput = document.getElementById("message");
  const message = messageInput.value.trim();
  if (!message || !currentClient) return;

  const { img } = await fetchUserInfo(adminId);

  try {
    await connection.invoke("SendToUser", adminId, currentClient, message);
    addMessageToChat({
      imgUrl: img.url,
      messageText: message,
      dateTime: new Date().toLocaleTimeString(),
      isSender: true
    });
    messageInput.value = ""; // Limpiar campo
  } catch (err) {
    console.error("Error al enviar mensaje:", err.toString());
  }
});

