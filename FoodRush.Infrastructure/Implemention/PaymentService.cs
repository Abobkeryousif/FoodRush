
using Stripe;

namespace FoodRush.Infrastructure.Implemention
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitofwork _unitofwork;
        private readonly ApplicationDbContaxt _contaxt;
        public PaymentService(IUnitofwork unitofwork, ApplicationDbContaxt contaxt)
        {
            _unitofwork = unitofwork;
            _contaxt = contaxt;
        }

        public async Task<Basket> CreateOrUpdatePaymentAsync(string basketId,int? deliveryId)
        {
            var basket = await _unitofwork.BasketRepository.GetBasketAsync(basketId);

            var stripeSecretKey = Environment.GetEnvironmentVariable("STRIPE_SECRET_KEY");
            StripeConfiguration.ApiKey = stripeSecretKey;

            decimal shippingPrice = 0m;

            if (deliveryId.HasValue)
            {
                var delivery = await _contaxt.Deliveries.AsNoTracking().FirstOrDefaultAsync(d=> d.Id == deliveryId.Value);
                if (delivery == null) 
                    throw new ArgumentNullException(nameof(delivery));

                shippingPrice = delivery.Price;
            }

            foreach (var item in basket.basketItems)
            {
                var meal = await _unitofwork.MealRepository.GetByIdAsync(item.Id);
                item.Price = meal.Price;
            }

            PaymentIntentService paymentIntentService = new PaymentIntentService();
            PaymentIntent _intent;

            if(string.IsNullOrEmpty(basket.paymentIntentId))
            {
                var option = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.basketItems.Sum(p=> p.Quantity * (p.Price*100) + (shippingPrice*100)),
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                _intent = await paymentIntentService.CreateAsync(option);
                basket.paymentIntentId = _intent.Id;
                basket.clientSecret = _intent.ClientSecret;
            } 

            else
            {
                var option = new PaymentIntentUpdateOptions
                {
                    Amount = (long)basket.basketItems.Sum(p => p.Quantity * (p.Price * 100) + (shippingPrice * 100))
                };

                await paymentIntentService.UpdateAsync(basket.paymentIntentId,option);
            }
            await _unitofwork.BasketRepository.UpdateBasketAsync(basket);
            return basket;
        }
    }
}
