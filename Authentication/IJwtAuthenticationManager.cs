namespace Authentication
{
    public interface IJwtAuthenticationManager
    {
        string WriteToken(int id);
        int ReadToken(string token);
    }
}
