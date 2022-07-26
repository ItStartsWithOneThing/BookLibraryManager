﻿using BookLibraryManagerDAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookLibraryManagerDAL
{
    public interface IDbBooksRepository
    {
        Task<Book> GetFullBookInfo(Guid id);
    }
}