using System.Runtime.Serialization;

namespace FIAP.Secretaria.Shared.Utils.Pagination;

/// <summary>
/// Classe que realiza cálculo de paginação para criação de componentes de paginação.
/// </summary>
[DataContract]
public class PaggingCalculation
{
    /// <summary>
    /// Construtor padrão.
    /// Realiza cálculos da paginação.
    /// </summary>
    /// <param name="page">Página atual/selecionada.</param>
    /// <param name="totalItems">Quantidade total de itens.</param>
    /// <param name="itemsPerPage">Quantidade de itens por página.</param>
    public PaggingCalculation(int page, int totalItems, int itemsPerPage)
    {
        Page = page;
        ItensPerPage = itemsPerPage;
        TotalItens = totalItems;

        int paginas;

        // Se a quantidade de itens por página for maior que a quantidade total de itens encontrados, a qtd de página é igual a 1
        if (itemsPerPage >= totalItems)
            paginas = 1;
        else
        {
            // Dividir qtd de itens encontrados por quantidade de itens por paginas
            paginas = totalItems / itemsPerPage;

            // Se houver resto da divisão, somar 1 à quantidade de páginas
            if (totalItems % itemsPerPage != 0)
                paginas++;
        }

        TotalPages = paginas;
    }

    /// <summary>Página atual/selecionada.</summary>
    [DataMember]
    public int Page { get; private set; }

    /// <summary>Quantidade de páginas.</summary>
    [DataMember]
    public int TotalPages { get; private set; }

    /// <summary>Quantidade total de items existentes.</summary>
    [DataMember]
    public int TotalItens { get; private set; }

    /// <summary>Quantidade de items que serão exibidos em uma página.</summary>
    [DataMember]
    public int ItensPerPage { get; private set; }
}
