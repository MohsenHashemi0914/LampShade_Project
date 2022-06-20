namespace _0_Framework.Application.ZarinPal
{
    public class PaymentResult
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public string IssueTrackingNo { get; set; }

        public PaymentResult Succeeded(string issueTrackingNo, string message = "پرداخت با موفقیت انجام شد .")
        {
            IsSuccessful = true;
            Message = message;
            IssueTrackingNo = issueTrackingNo;
            return this;
        }

        public PaymentResult Failed(string message = "عملیات پرداخت با شکست مواجه شد. در صورت کسر وجه از حساب مبلغ تا 24 ساعت به حساب شما بازگردانده خواهد شد .")
        {
            Message = message;
            IsSuccessful = false;
            return this;
        }
    }
}