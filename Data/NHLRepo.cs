using AzureTestAPI.Models;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using AzureTestAPI.Dtos;
using Dapper;

namespace AzureTestAPI.Data;
public class NHLRepo : INHLRepo
{
    private readonly IConfiguration configuration;

    public NHLRepo(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    #region NHL

    public async Task<IEnumerable<NHLRoster>> GetAllNHLRoster()
    {
        using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            return await connection.GetAllAsync<NHLRoster>();
        }

    }

        public async Task<IEnumerable<NHLRosterDto>> GetNHLRoster()
    {
        var sql = @"
        select TOP 3
            Name
            ,Team
            ,Number
            ,Position
            ,Handed
            ,Age
            ,Drafted
            ,BirthPlace
            ,BirthCountry
        from NHL.Roster
        order by 
        1, 3, 2";
        
        using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            return await connection.QueryAsync<NHLRosterDto>(sql);
        }
    }

    #endregion
}