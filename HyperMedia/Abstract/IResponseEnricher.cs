using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiCadastro.HyperMedia.Abstract
{
    public interface IResponseEnricher
    {
        bool CanEnrich(ResultExecutingContext context);

        Task Enrich(ResultExecutingContext context);
    }
}
