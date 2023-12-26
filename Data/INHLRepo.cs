using AzureTestAPI.Models;
using AzureTestAPI.Dtos;

namespace AzureTestAPI.Data;
public interface INHLRepo
{
    #region NHL

    Task<IEnumerable<NHLRoster>> GetAllNHLRoster();
    Task<IEnumerable<NHLRosterDto>> GetNHLRoster();

    #endregion
}