using System.ComponentModel;

namespace Auth.API.Enums
{
    public enum UserLoginProvider
    {
        [Description("System Account")]
        SystemAccount = 1,
        [Description("Phone Number")]
        PhoneNumber = 2,
        [Description("Google Account")]
        Google = 3,
    }
}
