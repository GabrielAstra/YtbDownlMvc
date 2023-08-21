function updateMediaList() {
    let mediaList = document.getElementById('mediaList');
    mediaList.innerHTML = ''; 

    let storedMedia = JSON.parse(localStorage.getItem('selectedMedia')) || [];

    for (let i = 0; i < storedMedia.length; i++) {
        let mediaItem = storedMedia[i];
        let newRow = document.createElement('tr');
        newRow.innerHTML = `<th scope="row">${i + 1}</th><td>${mediaItem.tipoMedia}</td><td>${mediaItem.mediaUrl}</td>`;
        mediaList.appendChild(newRow);
    }
}