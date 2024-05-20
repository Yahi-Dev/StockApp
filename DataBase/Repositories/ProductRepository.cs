﻿using Microsoft.EntityFrameworkCore;
using StockApp.Core.Application.Interfaces.Repositories;
using StockApp.Core.Domain.Entities;
using StockApp.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositorio
{
    public class ProductRepository : GenericsRepository<Product>, IProductRepository
    {
        private readonly ApplicationContext _dbcontext;

        public ProductRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            _dbcontext = applicationContext;
        }
    }
}
