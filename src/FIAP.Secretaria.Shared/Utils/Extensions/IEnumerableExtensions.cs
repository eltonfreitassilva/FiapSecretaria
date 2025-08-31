using FIAP.Secretaria.Shared.Utils.Pagination;

namespace FIAP.Secretaria.Shared.Utils.Extensions;

/// <summary>
/// Extension para listas.
/// </summary>
public static class IEnumerableExtensions
{
    /// <summary>
    /// Realiza a paginação sobre a coleção informada e retorna um <c>PagedList</c> com os dados paginados e informações como total de páginas, total de itens e etc.
    /// </summary>
    /// <param name="list">A colecao que vai ser paginada</param>
    /// <param name="order">Expressão para ordenação. Exemplo: (it => it.Id)</param>
    /// <param name="page">Numero da pagina.</param>
    /// <param name="itensPerPage">Quantidade de registros por pagina.</param>
    /// <param name="orderAscending">Ordenação ascendente (true) ou descendente (false).</param>
    public static PagedList<T> ToPagedList<T, TKey>(this List<T> list, Func<T, TKey> order, int page, int itensPerPage, bool orderAscending = true)
    {
        if (list == null)
            return null;

        if (page < 0)
            page = 0;

        if (itensPerPage <= 0)
            itensPerPage = 1;

        IOrderedEnumerable<T> query = orderAscending ? list.OrderBy(order) : list.OrderByDescending(order);

        var itens = query.Skip(itensPerPage * page).Take(itensPerPage).ToList();
        int totalItems = list.Count();

        return new PagedList<T>(itens, page, totalItems, itensPerPage);
    }

    /// <summary>
    /// Verifica se a lista é nula ou não possui item.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> list) => list == null || !list.Any();

    /// <summary>
    /// Concatena as strings da coleção, separando pelo delimitador informado.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="delimiter"></param>
    /// <param name="ignoreNullValues">Indica se valores null entram (false) ou não (true) para a composição da string.</param>
    /// <returns></returns>
    public static string ToString<T>(this IEnumerable<T> list, string delimiter, bool ignoreNullValues = true)
    {
        if (list == null || !list.Any())
            return null;

        if (ignoreNullValues && !list.Any(it => it != null))
            return null;

        IEnumerable<T> listToJoin;

        if (ignoreNullValues)
            listToJoin = list.Where(it => it != null);
        else
            listToJoin = list;

        return string.Join(delimiter, listToJoin.Select(i => i.ToString()).ToArray());
    }

    /// <summary>
    /// Concatena as strings da coleção, colocando o caractere desejado antes e depois de cada valor, dividindo as string pelo delimitador informado. Ex: new List { "val1", "val2", "val3" }, 
    /// com wrapp "'" (aspas simples) e delimitador "," (virgula), o resultado será: "'val1','val2','val3'".
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="wrappValue">Valor que será adicionado no começo e no final da string.</param>
    /// <param name="delimiter"></param>
    /// <param name="ignoreNullValues">Indica se valores null entram (false) ou não (true) para a composição da string.</param>
    /// <returns></returns>
    public static string ToStringWrapped<T>(this IEnumerable<T> list, string wrappValue, string delimiter, bool ignoreNullValues = true) => list.ToStringWrapped(wrappValue, wrappValue, delimiter, ignoreNullValues);

    /// <summary>
    /// Concatena as strings da coleção, colocando o caractere desejado antes e depois de cada valor, dividindo as string pelo delimitador informado. Ex: new List { "val1", "val2", "val3" }, 
    /// com wrapp "'" (aspas simples) e delimitador "," (virgula), o resultado será: "'val1','val2','val3'".
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="wrappValueBegin">Valor que será adicionado no começoda string.</param>
    /// <param name="wrappValueEnd">Valor que será adicionado no no final da string.</param>
    /// <param name="delimiter"></param>
    /// <param name="ignoreNullValues">Indica se valores null entram (false) ou não (true) para a composição da string.</param>
    /// <returns></returns>
    public static string ToStringWrapped<T>(this IEnumerable<T> list, string wrappValueBegin, string wrappValueEnd, string delimiter, bool ignoreNullValues = true)
    {
        if (list == null || !list.Any())
            return null;

        if (ignoreNullValues && !list.Any(it => it != null))
            return null;

        IEnumerable<T> listToJoin;

        if (ignoreNullValues)
            listToJoin = list.Where(it => it != null);
        else
            listToJoin = list;

        return string.Join(delimiter, listToJoin.Select(i => $"{wrappValueBegin}{i.ToString()}{wrappValueEnd}").ToArray());
    }

    public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
    {
        foreach (var item in items)
        {
            action(item);
        }
    }
}