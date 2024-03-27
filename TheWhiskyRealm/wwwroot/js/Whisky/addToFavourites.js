document.addEventListener('DOMContentLoaded', function () {
    var favoriteButton = document.getElementById('favorite-button');

    favoriteButton.addEventListener('click', function () {
        toggleFavorite();
    });

    function toggleFavorite() {
        var isFavorite = favoriteButton.classList.contains('active');
        var addToFavoritesUrl = document.getElementById('addToFavoritesUrl').value;
        var removeFromFavoritesUrl = document.getElementById('removeFromFavoritesUrl').value;
        var url = isFavorite ? removeFromFavoritesUrl : addToFavoritesUrl;
        var whiskyId = document.getElementById('whiskyId').value;

        var csrfToken = document.querySelector('input[name="__RequestVerificationToken"]').value;

        var headers = {
            'Content-Type': 'application/json',
            'RequestVerificationToken': csrfToken
        };

        fetch(url, {
            method: 'POST',
            headers: headers,
            body: JSON.stringify({ id: whiskyId })
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Failed to update favorites.');
                }

                favoriteButton.classList.toggle('active');
            })
            .catch(error => {
                console.error('Error:', error);
            });
    }
});
