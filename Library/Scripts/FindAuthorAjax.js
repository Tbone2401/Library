$(document).ready(function() {
    $("#author-lookup").autocomplete({
        source: function(request, response) {
            $.ajax({
                url: "/Books/SearchAuthor",
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: "{'queryString':'" + request.term + "'}",
                success: function(data) {
                    try {
                        response($.map(data,
                            function(item) {
                                return { label: item.FirstName + " " + item.LastName, value: item.FirstName + " " + item.LastName};
                            }));
                    } catch (err) {
                        
                    }
                }
            });
        },
        messages: {
            noResults: "",
            results: ""
        }
    });
});  