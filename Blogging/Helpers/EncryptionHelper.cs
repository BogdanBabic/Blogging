using System.Text;

namespace Blogging.Helpers
{
    public static class EncryptionHelper
    {
        const string key = "E546C8DG278CZ5931069B522E695D4G3";

        public static string Encrypt(this string inputText)
        {
            if (string.IsNullOrEmpty(inputText))
            {
                return "";
            }

            inputText += key;
            var inputTextBytes = Encoding.UTF8.GetBytes(inputText);
            return Convert.ToBase64String(inputTextBytes);
        }

        public static string Decrypt(this string inputText)
        {
            if (string.IsNullOrEmpty (inputText))
            {
                return "";
            }

            var byteArray = Convert.FromBase64String(inputText);
            var text = Encoding.UTF8.GetString(byteArray);
            var result = text.Replace(key, string.Empty);

            return result;
        }
    }
}
