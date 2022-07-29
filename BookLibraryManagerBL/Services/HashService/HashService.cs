using BookLibraryManagerBL.Options;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibraryManagerBL.Services.HashService
{
    public class HashService : IHashService
    {
        private readonly AuthOptions _authOptions;

        public HashService(IOptions<AuthOptions> options)
        {
            _authOptions = options.Value;
        }

        public string HashString(string source)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: source,
                salt: Convert.FromBase64String(_authOptions.Salt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 32));
        }
    }
}
