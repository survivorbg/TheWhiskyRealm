$(document).ready(function () {
    $('#userRoleFilterForm .dropdown-item').click(function (e) {
        e.preventDefault();
        var role = $(this).data('role');
        if (role === 'All') {
            $('tbody tr').show();
        } else {
            $('tbody tr').hide().filter(function () {
                return $(this).find('td:eq(2)').text().trim() === role;
            }).show();
        }
    });

    $('#userClearFilterButton').click(function () {
        $('#userRoleFilterForm .dropdown-item[data-role="All"]').click();
    });
});

$(document).ready(function () {
    $('.dropdown-toggle').dropdown();
});