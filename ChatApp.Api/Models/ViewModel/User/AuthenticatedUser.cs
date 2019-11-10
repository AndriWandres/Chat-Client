using ChatApp.Api.Models.Domain;

namespace ChatApp.Api.Models.ViewModel.User
{
    public class AuthenticatedUser
    {
        public string Token { get; set; }
        public AppUser User { get; set; }
    }
}
