$(document).ready(function () {
    // Basket add functionality
    $(document).on("click", ".BasketBtn", function (e) {
        e.preventDefault();
        
        let productId = $(this).data('id');
        if (!productId) {
            console.error('Product ID not found');
            return;
        }

        $.ajax({
            url: '/Basket/AddToBasket',
            type: 'POST',
            data: { id: productId },
            success: function (response) {
                if (response.success) {
                    // Update cart count
                    $(".notification").text(response.basketCount);
                    
                    // Update mini cart content
                    $(".minicart-inner-content").html(response.basketHtml);
                    
                    // Show success message
                    Swal.fire({
                        position: 'top-end',
                        icon: 'success',
                        title: 'Added to cart!',
                        showConfirmButton: false,
                        timer: 1500
                    });
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: response.message || 'Something went wrong!'
                    });
                }
            },
            error: function () {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Could not add to cart!'
                });
            }
        });
    });
});