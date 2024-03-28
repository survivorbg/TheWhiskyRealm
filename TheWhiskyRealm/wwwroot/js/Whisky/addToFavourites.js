document.addEventListener('DOMContentLoaded', function () {
    var favouriteButton = document.getElementById('favourite-button');

    favouriteButton.addEventListener('click', function () {
        toggleFavourite();
    });

    function toggleFavourite() {
        var isFavourite = favouriteButton.classList.contains('active');
        var addToFavouritesUrl = document.getElementById('addToFavouritesUrl').value;
        var removeFromFavouritesUrl = document.getElementById('removeFromFavouritesUrl').value;
        var url = isFavourite ? removeFromFavouritesUrl : addToFavouritesUrl;
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
                    throw new Error('Failed to update favourites.');
                }

                favouriteButton.classList.toggle('active');
            })
            .catch(error => {
                console.error('Error:', error);
            });
    }
});
