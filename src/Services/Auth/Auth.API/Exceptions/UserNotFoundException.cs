using BuildingBlocks.Exceptions;

namespace Auth.API.Exceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(int id) : base("User", id)
        {
        }        
        public UserNotFoundException(string email) : base("User", email)
        {
        }
    }
}
