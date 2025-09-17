$(document).ready(function () {
    // Add to cart functionality
    $('.add-to-cart').click(function (e) {
        e.preventDefault();
        const productId = $(this).data('id');
        
        $.ajax({
            url: '/Shop/AddToCart',
            type: 'POST',
            data: { productId: productId },
            success: function (response) {
                if (response.success) {
                    // Update cart count
                    $('.notification').text(response.cartCount);
                    // Show success message
                    toastr.success('Product added to cart successfully');
                } else {
                    toastr.error(response.message);
                }
            },
            error: function () {
                toastr.error('Error adding product to cart');
            }
        });
    });

    // Quick view functionality
    $('.quick-view').click(function (e) {
        e.preventDefault();
        const productId = $(this).data('id');
        
        $.ajax({
            url: '/Shop/QuickView',
            type: 'GET',
            data: { productId: productId },
            success: function (response) {
                $('#quick_view .modal-body').html(response);
            }
        });
    });
});