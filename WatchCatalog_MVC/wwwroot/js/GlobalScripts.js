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


const createShimmers = (shimmerCount) => {
    let shimmers = []
    for (let i = 0; i < shimmerCount; i++) {
        const card = $("<div>").addClass("card shadow-none").css("width", "16vw").css({ "height": "42vh", "cursor": "pointer", "border": "none" })
        const imgContainer = $("<div>").addClass("card").css("width", "100%").css({ "height": "100%", "position": "relative", "overflow": "hidden", "display": "flex", "justify-content": "center", "align-items": "center", "border": "none" })
        const image = $(`<video autoplay loop muted playsinline src="/videos/shimmer.mp4">`).css({ "width": "130%" }).css("position", "absolute").css("margin", "auto")
        card.append(imgContainer.append(image))

        shimmers.push(card)
    }

    return shimmers;
}

const createRandomRate = () => {
    let rating = $("<p>").text((Math.round(Math.random() * (5 - 4.56) + 4.56 * 100) / 100).toFixed(2)).css({ "position": "absolute", "top": ".5vh", "right": ".5vw", "font-size": "1.5vh" }).prepend('<img src="/images/star.png" style="height: 1.3vh; position: absolute; right: 1.6vw; top: .3vh"/>')

    return rating
}