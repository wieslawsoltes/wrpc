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
using System.Text.Json.Serialization;
using WasabiRpc.Models.Rpc;

namespace WasabiRpc.Models.App;

public class Job
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("rpcMethod")]
    public RpcMethod RpcMethod { get; set; }

    [JsonPropertyName("rpcServerUri")]
    public string RpcServerUri { get; set; }

    [JsonConstructor]
    public Job(string name, RpcMethod rpcMethod, string rpcServerUri)
    {
        Name = name;
        RpcMethod = rpcMethod;
        RpcServerUri = rpcServerUri;
    }
}
