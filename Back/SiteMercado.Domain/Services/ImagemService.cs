using System;
using System.IO;

namespace SiteMercado.Domain
{

    public interface IImagemService
    {
        string GerarUrl(string base64);
        bool IsUrlValidaIfExists(string url);
    }

    public class ImagemService : IImagemService
    {
        public string GerarUrl(string base64)
        {
            var name = $"{Guid.NewGuid()}.jpg";
            var file = $"StaticFiles/{name}";
            File.WriteAllBytes(file, Convert.FromBase64String(base64.Replace("data:image/jpeg;base64,", string.Empty)));

            return file;
        }

        public bool IsUrlValidaIfExists(string url)
        {
            var result = true;

            if (!string.IsNullOrEmpty(url))
            {
                result = Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            }

            return result;
        }
    }
}
