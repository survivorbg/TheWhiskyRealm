$(document).ready(function () {
    $(window).on("scroll",function () {
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
                take: 9 
            },
            success: function (data) {
                $("#whiskyRow").append(data);
            },
            error: function () {
                alert("Error loading more whiskies.");
            }
        });
    }
});
