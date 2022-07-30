namespace _0_Framework.Application.Events
{
    public class UserDataEventArgs : EventArgs
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Moblie { get; set; }
        public string Password { get; set; }
        public string MessageBody { get; set; }

        public UserDataEventArgs(string name, string email,
            string moblie, string password, string title = "", string messageBody = "")
        {
            Name = name;
            Email = email;
            Moblie = moblie;
            Password = password;
            Title = string.IsNullOrWhiteSpace(title) ? "به فروشگاه خوش آمدید" : title;

            MessageBody = string.IsNullOrWhiteSpace(messageBody) 
                ? $"{name} عزیز ثبت نام شما در فروشگاه با موفقیت انجام شد"
                : messageBody;
        }
    }
}
