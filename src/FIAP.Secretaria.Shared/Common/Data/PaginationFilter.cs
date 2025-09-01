using FIAP.Secretaria.Shared.Common.Enums;

namespace FIAP.Secretaria.Shared.Common.Data;

public class PaginationFilter
{
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int SortIndex { get; set; } = 0;
    public SortDirection SortDirection { get; set; } = SortDirection.Ascending;
}