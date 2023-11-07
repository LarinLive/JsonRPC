using System.Text.Json.Serialization;

namespace Larine.JsonRPC.OpenRPC;

public enum ParamStructure { ByName, ByPosition, Either }

/// <summary>
/// Describes the interface for the given method name. The method name is used as the method field of the JSON-RPC body. It therefore MUST be unique.
/// </summary>
public record class MethodObject
{
	/// <summary>
	/// REQUIRED. The cannonical name for the method. The name MUST be unique within the methods array.
	/// </summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>
	/// A list of tags for API documentation control. Tags can be used for logical grouping of methods by resources or any other qualifier.
	/// </summary>
	[JsonPropertyName("tags")]
	public TagObject[]? tags { get; init; }

	/// <summary>
	/// A short summary of what the method does.
	/// </summary>
	[JsonPropertyName("summary")]
	public string? Summary { get; init; }

	/// <summary>
	/// A verbose explanation of the method behavior. GitHub Flavored Markdown syntax MAY be used for rich text representation.
	/// </summary>
	[JsonPropertyName("description")]
	public string? Description { get; init; }

	/// <summary>
	/// Additional external documentation for this method.
	/// </summary>
	[JsonPropertyName("externalDocs")]
	public ExternalDocumentationObject? ExternalDocs { get; init; }

	/// <summary>
	/// REQUIRED. A list of parameters that are applicable for this method. The list MUST NOT include duplicated parameters and therefore require name to be unique. 
	/// The list can use the Reference Object to link to parameters that are defined by the Content Descriptor Object. 
	/// All optional params (content descriptor objects with “required”: false) MUST be positioned after all required params in the list.
	/// </summary>
	[JsonPropertyName("params")]
	public ContentDescriptorObject[]? Params { get; init; }

	/// <summary>
	/// The description of the result returned by the method. If defined, it MUST be a Content Descriptor or Reference Object. If undefined, the method MUST only be used as a notification.
	/// </summary>
	[JsonPropertyName("result")]
	public ContentDescriptorObject? result { get; init; }

	/// <summary>
	/// Declares this method to be deprecated. Consumers SHOULD refrain from usage of the declared method. Default value is false.
	/// </summary>
	[JsonPropertyName("deprecated")]
	public bool Deprecated { get; init; }

	/// <summary>
	/// An alternative servers array to service this method. If an alternative servers array is specified at the Root level, it will be overridden by this value.
	/// </summary>
	[JsonPropertyName("servers")]
	public ServerObject[]? Servers { get; init; }

	/// <summary>
	/// A list of custom application defined errors that MAY be returned. The Errors MUST have unique error codes.
	/// </summary>
	[JsonPropertyName("errors")]
	public ErrorObject[]? Errors { get; init; }

	/// <summary>
	/// An alternative servers array to service this method. If an alternative servers array is specified at the Root level, it will be overridden by this value.
	/// </summary>
	[JsonPropertyName("links")]
	public LinkObject[]? Links { get; init; }

	/// <summary>
	/// The expected format of the parameters. As per the JSON-RPC 2.0 specification, the params of a JSON-RPC request object may be an array, object, or either (represented as by-position, by-name, and either respectively). 
	/// When a method has a paramStructure value of by-name, callers of the method MUST send a JSON-RPC request object whose params field is an object. 
	/// Further, the key names of the params object MUST be the same as the contentDescriptor.names for the given method. Defaults to "either".
	/// </summary>
	[JsonPropertyName("paramStructure")]
	public ParamStructure ParamStructure { get; init; } = ParamStructure.Either;

	/// <summary>
	/// Array of Example Pairing Objects where each example includes a valid params-to-result Content Descriptor pairing.
	/// </summary>
	[JsonPropertyName("examples")]
	public ExampleObject[]? Examples { get; init; }
}
