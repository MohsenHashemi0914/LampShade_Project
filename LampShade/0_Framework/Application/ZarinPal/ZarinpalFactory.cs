using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace _0_Framework.Application.ZarinPal
{
    public class ZarinPalFactory : IZarinPalFactory
    {
        public string Prefix { get; set; }
        private readonly string _merchantId;

        #region Constructor

        private readonly IConfiguration _configuration;

        public ZarinPalFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            Prefix = _configuration.GetSection("payment")["method"];
            _merchantId = _configuration.GetSection("payment")["merchant"];
        }

        #endregion

        public PaymentResponse CreatePaymentRequest(string amount, string mobile, string email, string description,
             long orderId)
        {
            amount = amount.Replace(",", "");
            var finalAmount = int.Parse(amount);
            var siteUrl = _configuration.GetSection("payment")["siteUrl"];

            var requestBody = new PaymentRequest
            {
                Mobile = mobile,
                CallbackURL = $"{siteUrl}/Checkout?handler=CallBack&oId={orderId}",
                Description = description,
                Email = email,
                Amount = finalAmount,
                MerchantID = _merchantId
            };

            var response = CallZarinpalApi(string.Concat($"https://{Prefix}", ZarinpalApiUrls.PaymentRequest), requestBody);
            return JsonConvert.DeserializeObject<PaymentResponse>(response.Content);
        }

        public VerificationResponse CreateVerificationRequest(string authority, string amount)
        {
            amount = amount.Replace(",", "");
            var finalAmount = int.Parse(amount);

            var requestBody = new VerificationRequest
            {
                Amount = finalAmount,
                MerchantID = _merchantId,
                Authority = authority
            };

            var response = CallZarinpalApi(string.Concat($"https://{Prefix}", ZarinpalApiUrls.VerificationRequest), requestBody);
            return JsonConvert.DeserializeObject<VerificationResponse>(response.Content);
        }

        #region Utilities

        private static RestResponse CallZarinpalApi(string url, object requestBody = null, Method method = Method.Post)
        {
            var client = CreateRestClient(url);
            var request = CreateRestRequest(method);

            if (method is Method.Post && requestBody is not null)
                request.AddJsonBody(requestBody);

            return client.Execute(request);
        }

        private static RestRequest CreateRestRequest(Method method)
        {
            var request = new RestRequest();
            request.Method = method;
            request.AddHeader("Content-Type", "application/json");
            return request;
        }

        private static RestClient CreateRestClient(string url)
        {
            return new RestClient(url);
        }

        #endregion
    }
}