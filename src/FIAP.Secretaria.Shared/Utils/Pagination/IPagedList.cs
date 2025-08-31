namespace FIAP.Secretaria.Shared.Utils.Pagination;

public interface IPagedList<T>
{
    public IList<T> Items { get; set; }
    public int Page { get; set; }
    public int ItemsPerPage { get; set; }
    public int TotalItems { get; }
    public int TotalPages { get; set; }
}