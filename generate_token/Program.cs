﻿using System;

namespace GenerateToken
{
    class Program
    {
        public static void Main(string[] args)
        {
            var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
            var bytes = new byte[256 / 8];
            rng.GetBytes(bytes);
            Console.WriteLine(Convert.ToBase64String(bytes));
        }
    }
}
