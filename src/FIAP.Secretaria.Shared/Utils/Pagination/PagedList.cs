using System.Runtime.Serialization;

namespace FIAP.Secretaria.Shared.Utils.Pagination;

/// <summary>
/// Classe para representar uma paginação.
/// </summary>
[DataContract]
public class PagedList<T> : IPagedList<T>
{
    public PagedList() { }

    public PagedList(IList<T> items, int page, int totalItems, int itemsPerPage)
    {
        Items = items;
        Page = page;
        TotalItems = totalItems;
        ItemsPerPage = itemsPerPage;

        var paging = new PaggingCalculation(page, totalItems, itemsPerPage);
        TotalPages = paging.TotalPages;
    }

    [DataMember]
    public IList<T> Items { get; set; }

    [DataMember]
    public int Page { get; set; }

    [DataMember]
    public int ItemsPerPage { get; set; }

    [DataMember]
    public int TotalItems { get; private set; }

    [DataMember]
    public int TotalPages { get; set; }
}
