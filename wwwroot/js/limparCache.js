function clearExpiredCache() {
    let storedMedia = JSON.parse(localStorage.getItem('selectedMedia')) || [];
    const currentTime = Date.now();
    const expirationTime = 30 * 60 * 1000; //tempo para armazenamento, deixar em 30 minutos!

    storedMedia = storedMedia.filter(item => (currentTime - item.timestamp) < expirationTime);

    localStorage.setItem('selectedMedia', JSON.stringify(storedMedia));
}

clearExpiredCache();

updateMediaList();