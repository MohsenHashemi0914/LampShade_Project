namespace _0_Framework.Application.ZarinPal
{
    public static class ZarinpalApiUrls
    {
        private const string PaymentUrl = ".zarinpal.com/pg/StartPay/";
        public const string PaymentRequest = ".zarinpal.com/pg/rest/WebGate/PaymentRequest.json";
        public const string VerificationRequest = ".zarinpal.com/pg/rest/WebGate/PaymentVerification.json";

        public static string CompletePayment(string prefix, string authoriry)
        {
            return $"https://{prefix}{PaymentUrl}{authoriry}";
        }
    }
}