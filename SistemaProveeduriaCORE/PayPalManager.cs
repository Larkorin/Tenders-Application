using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.POIFS.Properties;
using NPOI.SS.Formula.Functions;
using PayPal.Api;
using Transaction = PayPal.Api.Transaction;

namespace SistemaProveeduriaCORE
{
    

    public class PayPalHelper
    {
        private static string clientId = "AYJvnFeSBXXaRs-1cuxSp-0o-K0RBQCLE_iLikA54_tNGTtoF7YW6l5ki5E6hOtrFlHOELbtvhlQITTJ";
        private static string clientSecret = "EAfDwuQ1yglHrXdImCDNghGP0tXoJ4SgCh_V1_-5qFLMaPnftoyj2i_-2vRm6cjnHX3vn3VDNA3iXTqj";

        public static Payment CreatePayment(decimal amount, string currency, string description, string returnUrl, string cancelUrl)
        {
            var apiContext = new APIContext(new OAuthTokenCredential(clientId, clientSecret).GetAccessToken());

            var payment = new Payment()
            {
                intent = "sale",
                payer = new Payer() { payment_method = "paypal" },
                transactions = new List<Transaction>()
            {
                new Transaction()
                {
                    amount = new Amount()
                    {
                        currency = currency,
                        total = amount.ToString(),
                        details = new Details()
                        {
                            subtotal = amount.ToString(),
                            tax = "0",
                            shipping = "0",
                            handling_fee = "0",
                            shipping_discount = "0"
                        }
                    },
                    description = description
                }
            },
                redirect_urls = new RedirectUrls()
                {
                    return_url = returnUrl,
                    cancel_url = cancelUrl
                }
            };

            return payment.Create(apiContext);
        }

        public static Payment ExecutePayment(string paymentId, string token, string payerId)
        {
            var apiContext = new APIContext(new OAuthTokenCredential(clientId, clientSecret).GetAccessToken());

            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };

            var payment = new Payment() { id = paymentId };

            return payment.Execute(apiContext, paymentExecution);
        }

        public static Payment GetPayment(string paymentId)
        {
            var apiContext = new APIContext(new OAuthTokenCredential(clientId, clientSecret).GetAccessToken());

            var payment = new Payment() { id = paymentId };

            return Payment.Get(apiContext, payment.id);

        }
    }

}
