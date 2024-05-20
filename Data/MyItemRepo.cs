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
        private readonly string azureClientId = "";
        private readonly string azureSQLAuthURL = "";

        public MyItemRepo(IConfiguration config)
        {
            configuration = config;

            azureClientId = configuration.GetValue<string?>("AzureSettings:ClientID") ?? "";
            azureSQLAuthURL = configuration.GetValue<string?>("AzureSettings:AzureSQLAuthURL") ?? "";

            if (azureClientId == "" || azureSQLAuthURL == "")
            {
                throw new ArgumentNullException("AzureSettings:ClientID or AzureSettings:AzureSQLAuthURL is not set in appsettings.json");
            }
        }

        public async Task<IEnumerable<MyItem>> GetItems()
        {
            var connectionString = "";
            var sql = @"select ItemNo, ItemName from [TestData]";

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                // connect from desktop
                connectionString = configuration.GetConnectionString("DevConnection");

                using (var connection = new SqlConnection(connectionString))
                {
                    return await connection.QueryAsync<MyItem>(sql);
                }
            }
            else
            {
                // connection from within Azure
                connectionString = configuration.GetConnectionString("DefaultConnection");

                using (var connection = new SqlConnection(connectionString))
                {
                    var credential = new ManagedIdentityCredential(azureClientId);
                    var token = await credential.GetTokenAsync(new Azure.Core.TokenRequestContext(new[] { azureSQLAuthURL }));
                    connection.AccessToken = token.Token;
                    return await connection.QueryAsync<MyItem>(sql);
                }
            }

        }

    }
}