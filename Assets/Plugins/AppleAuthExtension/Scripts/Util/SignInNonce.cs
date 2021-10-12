using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace AppleAuthExtension.Util
{
    /// <summary>
    /// ナンス
    /// </summary>
    public class SignInNonce
    {
        /// <summary>
        /// 平文
        /// </summary>
        public string RawValue { get; private set; }

        /// <summary>
        /// 暗号文
        /// </summary>
        public string HashValue { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="length">平文の長さ</param>
        public SignInNonce(int length)
        {
            RawValue = GenerateRandomString(length);
            HashValue = GenerateSHA256NonceFromRawNonce(RawValue);
        }

        #region https://qiita.com/mittsu/items/f2a7d86271748bacd733より失敬

        private static string GenerateRandomString(int length)
        {
            if (length <= 0)
            {
                throw new Exception("Expected nonce to have positive length");
            }

            const string charset = "0123456789ABCDEFGHIJKLMNOPQRSTUVXYZabcdefghijklmnopqrstuvwxyz-._";
            var cryptographicallySecureRandomNumberGenerator = new RNGCryptoServiceProvider();
            var result = string.Empty;
            var remainingLength = length;

            var randomNumberHolder = new byte[1];
            while (remainingLength > 0)
            {
                var randomNumbers = new List<int>(16);
                for (var randomNumberCount = 0; randomNumberCount < 16; randomNumberCount++)
                {
                    cryptographicallySecureRandomNumberGenerator.GetBytes(randomNumberHolder);
                    randomNumbers.Add(randomNumberHolder[0]);
                }

                for (var randomNumberIndex = 0; randomNumberIndex < randomNumbers.Count; randomNumberIndex++)
                {
                    if (remainingLength == 0)
                    {
                        break;
                    }

                    var randomNumber = randomNumbers[randomNumberIndex];
                    if (randomNumber < charset.Length)
                    {
                        result += charset[randomNumber];
                        remainingLength--;
                    }
                }
            }
            return result;
        }

        private static string GenerateSHA256NonceFromRawNonce(string rawNonce)
        {
            var sha = new SHA256Managed();
            var utf8RawNonce = Encoding.UTF8.GetBytes(rawNonce);
            var hash = sha.ComputeHash(utf8RawNonce);

            var result = string.Empty;
            for (var i = 0; i < hash.Length; i++)
            {
                result += hash[i].ToString("x2");
            }
            return result;
        }

        #endregion
    }
}
