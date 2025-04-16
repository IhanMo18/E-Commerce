import {Client} from "https://cdn.jsdelivr.net/npm/@gradio/client/+esm";

async function processImage() {
    const fileInput = document.getElementById('imageInput');
    if (!fileInput.files.length) return;

    const imageFile = fileInput.files[0];

    const client = await Client.connect("not-lain/background-removal");
    const result = await client.predict("/image", {
        image: imageFile
    });

    // Mostrar imagen procesada
    document.getElementById("outputImage").src = result.data[0].url || result.data[0];
}