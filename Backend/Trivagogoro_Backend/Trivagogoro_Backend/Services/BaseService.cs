﻿using System;

namespace Trivagogoro_Backend.Services
{
    public class BaseService
    {
        protected string ConnectionString { get; set; }

        public BaseService(IConfiguration config)
        {
            ConnectionString = config["MySQL:connectionString"]!;
        }
    }
}

