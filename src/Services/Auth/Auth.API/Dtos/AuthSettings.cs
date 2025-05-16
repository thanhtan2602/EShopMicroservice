namespace Auth.API.Dtos
{
    public class AuthSettings
    {
        public AuthSettings()
        {
            
        }
        public JwtSettings Jwt { get; set; } = new JwtSettings();
        public GoogleSettings Google { get; set; } = new GoogleSettings();
    }
}
