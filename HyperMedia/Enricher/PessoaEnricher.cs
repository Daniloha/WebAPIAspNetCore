using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebApiCadastro.Data.VO;
using WebApiCadastro.HyperMedia.Constants;

namespace WebApiCadastro.HyperMedia.Enricher
{
    public class PessoaEnricher : ContentResponseEnricher<PessoaVO>
    {
        private readonly object _lock = new object();
        protected override Task EnrichModel(PessoaVO content, IUrlHelper urlHelper)
        {
            var patch = "api/pessoa";
            string link = getLink(content.ID, urlHelper, patch);

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.GET,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultGet
            });
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.POST,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPost
            });
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.PUT,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPut
            });
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.DELETE,
                Href = link,
                Rel = RelationType.self,
                Type = "int"
            });
            return Task.CompletedTask;
        }

        private string getLink(long iD, IUrlHelper urlHelper, string patch)
        {
            lock (_lock)
            {
                var url = new { controller = patch, id = iD };
                return new StringBuilder(urlHelper.Link("DefaultApi", url)).Replace("%2F", "/").ToString();
            }
        }
    }
}
