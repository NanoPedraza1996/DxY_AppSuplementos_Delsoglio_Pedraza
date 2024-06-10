using Microsoft.EntityFrameworkCore;

namespace DxY_AppSuplementos.Data;

public class DxY_DbContext : DbContext
{
    public DxY_DbContext(DbContextOptions<DxY_DbContext> options)
    : base(options)
    {
    }
}