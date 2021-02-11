using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CrocusoftProje.SharedKernel.Infrastructure
{
    public static class PasswordHasher
    {
        public static string HashPassword(string userName, string password)
        {
            return BitConverter.ToString(
                MD5.Create().ComputeHash(
                    Encoding.UTF8.GetBytes(userName + password)
                )
            );
        }
    }
}
