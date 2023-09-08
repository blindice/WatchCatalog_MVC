const callModalBody = async (control, params) => {
    const beforeControlText = control.text();
    const submitBtn = $("#btnSubmitModal")

    submitBtn.click(() => {
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
        beforeSend: () => {
            control.attr("disabled", "disabled")
            control.text(" Loading...")
            control.prepend('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>')
        },
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

            control.removeAttr("disabled")
            control.text(beforeControlText)
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
        const card = $("<div>").addClass("card shadow-none").css("width", "16.2vw").css({ "height": "42vh", "cursor": "pointer", "border": "none" })
        const imgContainer = $("<div>").addClass("card").css("width", "100%").css({ "height": "100%", "position": "relative", "overflow": "hidden", "display": "flex", "justify-content": "center", "align-items": "center", "border": "none" })
        const image = $(`<video autoplay loop muted playsinline src="/videos/shimmer.mp4">`).css({ "width": "130%" }).css("position", "absolute").css("margin", "auto")
        card.append(imgContainer.append(image))

        shimmers.push(card)
    }

    return shimmers;
}

const createRandomRate = () => {
    let rating = $("<p>").text((Math.round(Math.random() * (5 - 4.56) + 4.56 * 100) / 100).toFixed(2)).css({ "position": "absolute", "top": ".5vh", "right": ".5vw", "font-size": "2vh" }).prepend('<img src="/images/star.png" style="height: 1.7vh; position: absolute; right: 2vw; top: .47vh"/>')

    return rating
}

let createWatchLists = async (pageNumber = 1, pageSize = 10) => {
    let searchString = document.getElementById("searchInput").value;
    await $.ajax({
        url: `/getwatches?SearchString=${searchString}&PageNumber=${pageNumber}&PageSize=${pageSize}`,
        method: "GET",
        beforeSend: () => {
            $("#watchesContainer").empty()
            $(".pagination").empty()
            $("#watchesContainer").html(createShimmers(pageSize))
        },
        success: (res) => {
            //create watches cards
            $("#watchesContainer").empty()

            if (res.watchDTOs.length === 0) {
                $("#watchesContainer").empty()
                $(".pagination").empty()
                $("#watchesContainer").html('<video autoplay loop muted playsinline src="/videos/empty-data.mp4" style="height: 50%; align-self: center; margin: auto"></video>')
            }
            else {
                $.each(res.watchDTOs, (key, value) => {
                    let createCard = (value) => {
                        let availability = value.isActive ? "" : "not-available"
                        let imageSrc = value.image ?? '/images/imageTemplate.png';
                       
                        let card = $("<div>").addClass("card shadow-none").css("width", "16.2vw").css({ "height": "42vh", "cursor": "pointer", "border": "none" }).attr("id", value.watchId)
                        let imgContainer = $("<div>").addClass(`card-img-top shadow-sm ${availability}`).css("width", "100%").css({ "height": "85%", "position": "relative", "overflow": "hidden", "display": "flex", "justify-content": "center", "align-items": "center", "border-radius": "1vh" })
                        let newTag = $("<span>")
                        let image = $(`<img src='${imageSrc}' />`).css("height", "100%").css("position", "absolute").css("margin", "auto")
                        let cardBody = $("<div>").addClass("card-body").css({ "text-overflow": "ellipsis", "padding": "1vh .5vw", "position": "relative" })
                        let cardTitle = $("<p>").addClass("card-title text-truncate").text(value.watchName.toUpperCase()).css({ "font-weight": "700", "font-size": "2vh", "line-height": "1.6vh", "width": "75%" })
                        let cardDesc = $("<p>").addClass("card-text text-truncate").text(value.short_description.toUpperCase()).css({ "font-weight": "700", "font-size": "2vh", "line-height": "1.6vh" })
                        let cardText = $("<p>").addClass("card-text").text(`Price: ฿ ${value.price.toFixed(2) }`).css({ "font-weight": "700", "font-size": "2vh", "line-height": "1vh" })
                        let cardRate = createRandomRate();

                        card.click(() => {
                            window.location.href = `/Watch/Details/${value.watchId}`
                        })

                        //if (key < 5 && pageNumber === 1)
                        //    newTag.text("NEW").css({ "z-index": "1", "color": "white", "background": "#dc3545", "position": "absolute", "top": "0", "right": "0.5vw", "font-size": ".9vh", "font-weight": "700", "border-radius": ".2vh", "padding": "0 .5vw" })

                        imgContainer.append(image, newTag)
                        cardBody.append(cardTitle, cardDesc, cardText, cardRate)                      

                        return card.append(imgContainer, cardBody)

                    }
                    $("#watchesContainer").append(createCard(value))
                })

                // create pagination
                $(".pagination").empty()
                let firstPagination = $("<li>").addClass(`page-item ${res.hasPrevious ? '' : 'disabled'}`).wrapInner(`<button class='page-link'>First</button>`)

                if (res.hasPrevious) firstPagination.click(async () => await createWatchLists(1))

                let lastPagination = $("<li>").addClass(`page-item ${res.hasNext && res.currentPage != res.totalPage ? '' : 'disabled'}`)
                    .wrapInner(`<button class='page-link'>Last</button>`)

                if (res.hasNext && res.currentPage != res.totalPage) lastPagination.click(async () => await createWatchLists(res.totalPages))

                let paginationButtons = []

                for (let i = res.currentPage - 2; i <= res.currentPage + 2; i++) {
                    let pageButton;
                    if (i > 0 && i <= res.totalPages)
                        pageButton = $("<li>").addClass("page-item").wrapInner($("<button>").addClass("page-link").text(i).click(async () => await createWatchLists(i)))

                    if (i === res.currentPage)
                        pageButton.addClass("active").children().off()

                    paginationButtons.push(pageButton)
                }

                $(".pagination").append(firstPagination, paginationButtons, lastPagination)
            }
        },
        error: (xhr, ajaxOptions, thrownError) => {
            console.log(xhr)
            window.location = `/error?statusCode=${xhr.status}&message=${xhr.responseText}`;
        }
    })
}

let createWatchListPartialScript = async (searchString) => {
    //search
    var xhr = null;
    var timeout = null;
    $("#searchInput").keyup(() => {
        clearTimeout(timeout);
        if (xhr != null)
            xhr.abort();
        timeout = setTimeout(async function () {
            xhr = await createWatchLists();
        }, 500);
    })

    $("#searchInput").val(searchString)
    $("#searchInput").trigger("keyup");

    //clear search
    $("#btnClear").click(() => {
        $("#searchInput").val('')
        $("#searchInput").trigger("keyup");
    })
}

// Back TO top Button
//Get the button
let mybutton = document.getElementById("btn-back-to-top");

// When the user scrolls down 20px from the top of the document, show the button
window.onscroll = function () {
    scrollFunction();
};

function scrollFunction() {
    if (
        document.body.scrollTop > 20 ||
        document.documentElement.scrollTop > 20
    ) {
        mybutton.style.display = "block";
    } else {
        mybutton.style.display = "none";
    }
}
// When the user clicks on the button, scroll to the top of the document
mybutton.addEventListener("click", backToTop);

function backToTop() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}