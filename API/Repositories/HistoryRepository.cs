using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class HistoryRepository : GeneralRepository<History>, IHistoryRepository
{
    public HistoryRepository(OvertimeDbContext context) : base(context) { }
}
