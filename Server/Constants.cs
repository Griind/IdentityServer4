namespace Server
{
    public static class Constants
    {
        public const string Audience = "https://localhost:44312/";
        public const string Issuer = Audience;
        public const string Secret = "not_too_short_secret_otherwise_it_will_error";
    }
}
