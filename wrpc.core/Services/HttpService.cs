/*
 * wrpc A Graphical User Interface for using the Wasabi Wallet RPC.
 * Copyright (C) 2023  Wiesław Šoltés
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as
 * published by the Free Software Foundation, either version 3 of the
 * License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 *
 * You should have received a copy of the GNU Affero General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WasabiRpc.Models.Services;

namespace WasabiRpc.Services;

public class HttpService : IHttpService
{
    private readonly HttpClient _httpClient = new();

    public async Task<string?> GetResponseDataAsync(string requestUri, string requestBodyJson, CancellationToken token)
    {
        var content = new StringContent(requestBodyJson, Encoding.UTF8, "application/json");
#if DEBUG
            Console.WriteLine($"RequestBody:{Environment.NewLine}{requestBodyJson}");
#endif
        var response = await _httpClient.PostAsync(requestUri, content, token);
        var responseBody = await response.Content.ReadAsStringAsync(token);
#if DEBUG
            Console.WriteLine($"Status code: {response.StatusCode}");
            Console.WriteLine($"Response body:{Environment.NewLine}{responseBody}");
#endif
        return response.StatusCode switch
        {
            HttpStatusCode.OK => responseBody,
            _ => null
        };
    }
}
