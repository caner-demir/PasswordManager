using System.Text;

namespace PasswordManager.Utilities
{
    public static class PasswordManager
    {
        public static string EncodePassword(string password)
        {
                byte[] data = new byte[password.Length];
                data = Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(data);
                return encodedData;
        }

        public static string DecodePassword(string encodedData)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            Decoder utf8Decode = encoder.GetDecoder();
            byte[] data = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(data, 0, data.Length);
            char[] charArray = new char[charCount];
            utf8Decode.GetChars(data, 0, data.Length, charArray, 0);
            string result = new String(charArray);
            return result;
        }
    }
}
