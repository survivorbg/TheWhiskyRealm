$(document).ready(function () {
    $('#searchButton').click(function () {
        var searchValue = $('#searchInput').val().toLowerCase();

        $('tbody tr').each(function () {
            var name = $(this).find('td:eq(1)').text().toLowerCase();
            var region = $(this).find('td:eq(2)').text().toLowerCase();
            var country = $(this).find('td:eq(3)').text().toLowerCase();

            if (name.includes(searchValue) || region.includes(searchValue) || country.includes(searchValue)) {
                $(this).show();
            } else {
                $(this).hide();
            }
        });
    });

    $('#resetButton').click(function () {
        $('#searchInput').val('');
        $('tbody tr').show();
    });
});