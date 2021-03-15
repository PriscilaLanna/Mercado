using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using SiteMercado.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SiteMercado.Api.Filters
{
    public class ValidatePhotosFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var form = actionContext.HttpContext.Request.Form;
            if (form.ContainsKey("data"))
            {
                if (form.Files.Count > 0)
                {
                    form.TryGetValue("data", out var data);
                    var produto = JsonConvert.DeserializeObject<Produto>(data);
                    produto.Imagem = "dddddd";
      
                    if (actionContext.ActionArguments.ContainsKey("data"))
                    {
                        actionContext.ActionArguments.Remove("data");
                        actionContext.ActionArguments.Add("data", produto);
                    }
                    base.OnActionExecuting(actionContext);
                }
            }
            else
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));
            }
           
        }
    }
}
