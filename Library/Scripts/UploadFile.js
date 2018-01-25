var fileInputs = $('#file-inputs');
var fileNames = $('#file-names');


$('#Upload').click(function () {
    var file = $('#file').get(0).files[0];
    if (!file) {
        return;
    }
    var formData = new FormData();
    formData.append("file", file);
    $.ajax({
        url: 'Upload/BooksController',// add controller name
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            if (!data) {
                // Oops, something went wrong
                return;
            }
            $('#PicturePath').val("~/Files/Images/" + data.DisplayName);
            $('#file').val(''); // clear file input
            // Add the display name
            fileNames.append($('<div></div>').text(data.DisplayName));
            // Add the inputs
            var index = (new Date()).getTime(); // unique indexer
            var clone = $('#template').clone(); // clone the template
            clone.html($(clone).html().replace(/#/g, index)); // update the indexer
            fileInputs.append(clone.html()); // append the inputs
            // update the input values
            var lastFile = fileInputs.find('.file-details').last();
            lastFile.find('.file-path').last().val(data.Path);
            lastFile.find('.file-name').last().val(data.DisplayName);
        }
    });
});