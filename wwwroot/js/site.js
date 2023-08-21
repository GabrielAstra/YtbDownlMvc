// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function isValidUrl(url) {
    var pattern = /^(ftp|http|https):\/\/[^ "]+$/;
    return pattern.test(url);
}

function addUrlToList(url) {
    var tableBody = document.querySelector('.table tbody');
    var newRow = document.createElement('tr');
    newRow.innerHTML = '<th scope="row">' + (tableBody.children.length + 1) + '</th><td>' + url + '</td>';
    tableBody.appendChild(newRow);
}

document.getElementById('storeUrlButton').addEventListener('click', function () {
    var urlInput = document.getElementById('urlInput');
    var url = urlInput.value.trim(); // Remove espaços em branco no início e no final

    if (!url) {
        alert('Por favor, insira uma URL válida.');
        return;
    }

    if (!isValidUrl(url)) {
        alert('Por favor, insira uma URL válida.');
        return;
    }

    var storedUrls = JSON.parse(sessionStorage.getItem('storedUrls')) || [];
    
    storedUrls.push(url);
    sessionStorage.setItem('storedUrls', JSON.stringify(storedUrls));

    addUrlToList(url);
    urlInput.value = ''; 
});

var storedUrls = JSON.parse(sessionStorage.getItem('storedUrls')) || [];
for (var i = 0; i < storedUrls.length; i++) {
    addUrlToList(storedUrls[i]);
}