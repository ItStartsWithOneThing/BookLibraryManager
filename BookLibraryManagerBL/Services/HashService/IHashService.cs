using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibraryManagerBL.Services.HashService
{
    public interface IHashService
    {
        string HashString(string source);
    }
}
