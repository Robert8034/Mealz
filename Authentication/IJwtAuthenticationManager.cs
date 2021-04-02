namespace Authentication
{
    public interface IJwtAuthenticationManager
    {
        string WriteToken(string email);

    }
}
