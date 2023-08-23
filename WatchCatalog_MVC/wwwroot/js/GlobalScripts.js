const callModalBody = async (params) => {
    $("#btnSubmitModal").click(() => {
        if ($("#partialModal .modal-body form").valid()) {
            $("#partialModal .modal-body form").submit()
        }
        else {
            return false
        }
    })
  

    await $.ajax({
        type: 'POST',
        url: params.url,
        data: JSON.stringify(params.id),
        contentType: 'application/json',
        success: (res) => {
            $('#partialModal .modal-body form').off()
            $("#partialModal .modal-body").html(res)
            $('#partialModal .modal-body form').data('validator', null);
            $.validator.unobtrusive.parse('#partialModal .modal-body form');
            $('#partialModal .modal-body form').on("submit", params.submitFn)
            $('#partialModal .modal-body form #imageFile').change((e) => {
                var uploadimg = new FileReader();
                uploadimg.onload = function (displayimg) {
                    $("#partialModal .modal-body form #imageViewer").attr('src', displayimg.target.result);
                }
                uploadimg.readAsDataURL(e.target.files[0]);
            })
            $('#partialModal').modal('show');
        },
        error: (err) => {
            console.log(err)
        }
    })
}