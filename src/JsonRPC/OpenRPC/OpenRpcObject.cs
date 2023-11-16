using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Larine.JsonRPC.OpenRPC;

/// <summary>
/// This is the root object of the OpenRPC document. The contents of this object represent a whole OpenRPC document.
/// </summary>
public record class OpenRpcObject
{
	/// <summary>
	/// REQUIRED. This string MUST be the semantic version number of the OpenRPC Specification version that the OpenRPC document uses. 
	/// The openrpc field SHOULD be used by tooling specifications and clients to interpret the OpenRPC document. 
	/// </summary>
	[JsonPropertyName("openrpc")]
	public string OpenRpc { get; init; } = "1.3.2";

	/// <summary>
	/// REQUIRED. Provides metadata about the API. The metadata MAY be used by tooling as required.
	/// </summary>
	[JsonPropertyName("info")]
	public InfoObject Info { get; init; } = new InfoObject();

	/// <summary>
	/// An array of Server Objects, which provide connectivity information to a target server.
	/// If the servers property is not provided, or is an empty array, the default value would be a Server Object with a url value of localhost.	
	/// </summary>
	[JsonPropertyName("servers")]
	public ServerObject[]? Servers { get; init; }

	/// <summary>
	/// REQUIRED. The available methods for the API. While it is required, the array may be empty (to handle security filtering, for example).
	/// </summary>
	[JsonPropertyName("methods")]
	public MethodObject[]? Methods { get; init; }

	/// <summary>
	/// An element to hold various schemas for the specification.
	/// </summary>
	[JsonPropertyName("components")]
	public ComponentsObject? Components { get; init; }

	/// <summary>
	/// Additional external documentation.
	/// </summary>
	[JsonPropertyName("externalDocs")]
	public ExternalDocumentationObject? ExternalDocs { get; init; }
}
