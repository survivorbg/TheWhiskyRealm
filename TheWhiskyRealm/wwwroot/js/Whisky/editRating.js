$(document).ready(function () {
    $('#editRatingButton').click(function () {
        var whiskyId = $(this).data('whisky-id');
        $.get('/Rating/Edit', { id: whiskyId }, function (data) {
            $('.user-rating').replaceWith(data);
            $('#editRatingButton').text('Cancel Edit');
            $('#editRatingButton').off('click').click(function () {
                $('.rating-container').replaceWith('<div class="user-rating">...</div>');
                $('#editRatingButton').text('Edit Rating');
                $('#editRatingButton').off('click').click(editRatingFunction);
            });
        });
    });
});
