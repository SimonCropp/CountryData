using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

static class Downloader
{
    public  static async Task DownloadFile(string allCountriesPath)
    {
        var requestUri = "http://download.geonames.org/export/zip/allCountries.zip";
        var httpClient = new HttpClient();

        var requestMessage = new HttpRequestMessage(HttpMethod.Head, requestUri);

        DateTime remoteLastModified;
        using (var headResponse = await httpClient.SendAsync(requestMessage))
        {
            remoteLastModified = headResponse.Content.Headers.LastModified.Value.UtcDateTime;
        }

        if (File.Exists(allCountriesPath))
        {
            if (remoteLastModified <= File.GetLastWriteTimeUtc(allCountriesPath))
            {
                return;
            }

            File.Delete(allCountriesPath);
        }


        using (var response = await httpClient.GetAsync(requestUri))
        using (var httpStream = await response.Content.ReadAsStreamAsync())
        {
            using (var fileStream = new FileStream(allCountriesPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await httpStream.CopyToAsync(fileStream);
            }

            File.SetLastWriteTimeUtc(allCountriesPath, remoteLastModified);
        }
    }
}