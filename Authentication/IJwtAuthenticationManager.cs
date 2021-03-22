namespace Authentication
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(string username, string password);

        void AddUser(string email, string password);
    }
}
