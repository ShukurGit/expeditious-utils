
using System;
using System.Security.Cryptography;
using System.Text;


namespace Expeditious.DataSecurity
{
    // AAD (Additional Authenticated Data)
    public static class CryptoCipherAesWithAAD
    {
        private const byte Version = 1;

        private const int SaltSize = 16;
        private const int NonceSize = 12;
        private const int TagSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 500_000;


        // AAD (Additional Authenticated Data)
        public static string Encrypt(string plainText, string password, string? aadText = null)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] nonce = RandomNumberGenerator.GetBytes(NonceSize);

            byte[] key = DeriveKey(password, salt);

            byte[] plaintextBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] ciphertext = new byte[plaintextBytes.Length];
            byte[] tag = new byte[TagSize];

            byte[]? aad = aadText != null
                ? Encoding.UTF8.GetBytes(aadText)
                : null;

            using (var aes = new AesGcm(key, TagSize))
            {
                aes.Encrypt(nonce, plaintextBytes, ciphertext, tag, aad);
            }

            // [version | salt | nonce | tag | ciphertext]
            byte[] result = new byte[1 + SaltSize + NonceSize + TagSize + ciphertext.Length];

            int offset = 0;

            result[offset++] = Version;

            Buffer.BlockCopy(salt, 0, result, offset, SaltSize); offset += SaltSize;
            Buffer.BlockCopy(nonce, 0, result, offset, NonceSize); offset += NonceSize;
            Buffer.BlockCopy(tag, 0, result, offset, TagSize); offset += TagSize;
            Buffer.BlockCopy(ciphertext, 0, result, offset, ciphertext.Length);

            return Convert.ToBase64String(result);
        }



        public static string Decrypt(string cipherText, string password, string? aadText)
        {
            byte[] data = Convert.FromBase64String(cipherText);

            if (data.Length < 1 + SaltSize + NonceSize + TagSize)
                throw new ArgumentException("Wrong data");

            int offset = 0;

            byte version = data[offset++];

            byte[]? aad = aadText != null
                ? Encoding.UTF8.GetBytes(aadText)
                : null;

            return version switch
            {
                1 => DecryptV1(data, offset, password, aad),
                _ => throw new NotSupportedException($"Unsupported version: {version}")
            };
        }



        private static string DecryptV1(byte[] data, int offset, string password, byte[]? aad = null)
        {
            byte[] salt = new byte[SaltSize];
            Buffer.BlockCopy(data, offset, salt, 0, SaltSize); offset += SaltSize;

            byte[] nonce = new byte[NonceSize];
            Buffer.BlockCopy(data, offset, nonce, 0, NonceSize); offset += NonceSize;

            byte[] tag = new byte[TagSize];
            Buffer.BlockCopy(data, offset, tag, 0, TagSize); offset += TagSize;

            int cipherLength = data.Length - offset;
            byte[] ciphertext = new byte[cipherLength];
            Buffer.BlockCopy(data, offset, ciphertext, 0, cipherLength);

            byte[] key = DeriveKey(password, salt);

            byte[] plaintext = new byte[cipherLength];

            using (var aes = new AesGcm(key, TagSize))
            {
                aes.Decrypt(nonce, ciphertext, tag, plaintext, aad);
            }

            return Encoding.UTF8.GetString(plaintext);
        }





        // PBKDF2 
        private static byte[] DeriveKey(string password, byte[] salt)
        {
            byte[] key = new byte[KeySize];

            Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                key,
                Iterations,
                HashAlgorithmName.SHA256);

            return key;
        }
    }
}