﻿static class Downloader
{
    static HttpClient httpClient = new()
    {
        Timeout = TimeSpan.FromMinutes(30)
    };

    public static async Task DownloadFile(string allCountriesPath, string requestUri)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Head, requestUri);

        DateTime remoteLastModified;
        using (var headResponse = await httpClient.SendAsync(requestMessage))
        {
            remoteLastModified = headResponse.Content.Headers.LastModified!.Value.UtcDateTime;
        }

        if (File.Exists(allCountriesPath))
        {
            if (remoteLastModified <= File.GetLastWriteTimeUtc(allCountriesPath))
            {
                return;
            }

            File.Delete(allCountriesPath);
        }


        using var response = await httpClient.GetAsync(requestUri);
        await using var httpStream = await response.Content.ReadAsStreamAsync();
        await using (var fileStream = new FileStream(allCountriesPath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            await httpStream.CopyToAsync(fileStream);
        }

        File.SetLastWriteTimeUtc(allCountriesPath, remoteLastModified);
    }
}