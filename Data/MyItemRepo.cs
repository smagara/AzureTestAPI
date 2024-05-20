using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using azure_test_api.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using Azure.Identity;

namespace azure_test_api.Data
{
    public class MyItemRepo : IMyItemRepo
    {

        private readonly IConfiguration configuration;
        
        public MyItemRepo(IConfiguration config)
        {
            configuration = config;
        }

        public async Task<IEnumerable<MyItem>> GetItems()
        {
            var sql = @"select ItemNo, ItemName from [TestData]";

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var credential = new ManagedIdentityCredential("5cae1e9c-aa36-4c89-963f-5930f751e41a");
                var token = await credential.GetTokenAsync(new Azure.Core.TokenRequestContext(new[] { "https://database.windows.net/.default" }));
                connection.AccessToken = token.Token;
                return await connection.QueryAsync<MyItem>(sql);
            }
        }

    }
}