﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCatalog.Core.Interfaces.Repository
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        void Commit();
    }
}
