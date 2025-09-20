$(document).ready(function () {
    $(".BasketBtn").click(function (ev) { 
        ev.preventDefault();
        let url = $(this).attr("href");
        fetch(url)
            .then(response => response.text())
            .then(data => {
                $(".minicart-item-wrapper").html(data)
          })
})
})
 