using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EducUp.Utils
{
    public static class HashManager
    {
        /// <summary>
        /// Ritorna la stringa passata per argomento convertita con algoritmo di Hash SHA256
        /// </summary>
        /// <param name="str"> stringa in chiaro </param>
        /// <returns></returns>
        public static async Task<string> GetHashStringAsync(string str)
        {
            string hashString = string.Empty;
            if (string.IsNullOrEmpty(str))
                return hashString;

            await Task.Run(() =>
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    byte[] hashBytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(str));

                    StringBuilder stringBuilder = new StringBuilder();

                    foreach (int b in hashBytes)
                    {
                        stringBuilder.Append(b.ToString("x2"));
                    }

                    hashString = stringBuilder.ToString();
                }
            }).ConfigureAwait(false);

            return hashString;
        }

        /// <summary>
        /// Verifica che l'input corrisponda all'hash passato per argomento
        /// </summary>
        /// <param name="input"> stringa in chiaro </param>
        /// <param name="hash"> corrispondente stringa convertita con algoritmo di Hash SHA256 </param>
        /// <returns></returns>
        public static async Task<bool> VerifyHashAsync(string input, string hash)
        {
            if (string.IsNullOrEmpty(hash) || string.IsNullOrEmpty(input))
                return false;

            var hashOfInput = await GetHashStringAsync(input).ConfigureAwait(false);

            bool result = !string.IsNullOrEmpty(hashOfInput) && hash.Equals(hashOfInput);

            return result;
        }
    }
}
