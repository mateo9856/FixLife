namespace FixLife.Admin.Db.Tools
{
    public static class CodeGenerator
    {
        private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public static string GenerateAuthCode()
        {
            var chars = new char[6];
            var random = new Random();

            for(int i = 0; i < chars.Length; i++)
            {
                chars[i] = Characters[random.Next(Characters.Length)];
            }

            return new string(chars);
        }
    }
}
