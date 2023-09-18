﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace AppPortfolio.Controllers.Utilities {
    public class Encryption {
        public static string Encrypt(string input, string key) {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            using (var tripleDES = new TripleDESCryptoServiceProvider()) {
                tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
                tripleDES.Mode = CipherMode.ECB;
                tripleDES.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tripleDES.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                tripleDES.Clear();
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
        }
        public static string Decrypt(string input, string key) {
            byte[] inputArray = Convert.FromBase64String(input);
            using (var tripleDES = new TripleDESCryptoServiceProvider()) {
                tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
                tripleDES.Mode = CipherMode.ECB;
                tripleDES.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tripleDES.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                tripleDES.Clear();
                return UTF8Encoding.UTF8.GetString(resultArray);
            }
        }
    }
}