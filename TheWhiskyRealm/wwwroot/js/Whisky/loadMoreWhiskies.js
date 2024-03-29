$(document).ready(function () {
    var sortOrder = ""; 
    $(document).on("scroll", function () {
        if ($(window).scrollTop() + $(window).height() == $(document).height()) {
            loadMoreWhiskies();
        }
    });
    function loadMoreWhiskies() {
        $.ajax({
            url: "/Whisky/LoadMoreWhiskies",
            type: "GET",
            data: {
                skip: $(".card").length,
                take: 9,
                sortOrder: sortOrder
            },
            success: function (data) {
                $("#whiskyRow").append(data);
            },
            error: function () {
                alert("Error loading more whiskies.");
            }
        });
    }
    $("#sortOrder").change(function () {
        sortOrder = $(this).val(); 
        $("#whiskyRow").empty();
        loadMoreWhiskies(); 
    });
    loadMoreWhiskies();
});
