$(document).ready(function () {
    var editRatingFunction = function () {
        var whiskyId = $(this).data('whisky-id');
        $.get('/Rating/Edit', { id: whiskyId }, function (data) {
            $('.user-rating').replaceWith(data);
            $('#editRatingButton').text('Cancel Edit');
            $('#editRatingButton').off('click').click(function () {
                location.reload(); // презарежда страницата
            });
        });
    };

    $('#editRatingButton').click(editRatingFunction);
});