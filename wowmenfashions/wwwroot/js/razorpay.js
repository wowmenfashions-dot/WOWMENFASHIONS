window.razorpayInterop = {
    openCheckout: function (options) {
        return new Promise((resolve, reject) => {
            options.handler = function (response) {
                resolve({
                    razorpayPaymentId: response.razorpay_payment_id,
                    razorpayOrderId: response.razorpay_order_id,
                    razorpaySignature: response.razorpay_signature
                });
            };
            
            options.modal = {
                ondismiss: function () {
                    reject("Payment cancelled by user.");
                }
            };

            var rzp = new Razorpay(options);
            rzp.on('payment.failed', function (response) {
                reject(response.error.description);
            });
            rzp.open();
        });
    }
};
