using Json.Schema;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Larine.JsonRPC.OpenRPC;

/// <summary>
/// Holds a set of reusable objects for different aspects of the OpenRPC. 
/// All objects defined within the components object will have no effect on the API unless they are explicitly referenced from properties outside the components object.
/// </summary>
public record class ComponentsObject
{
	/// <summary>
	/// An object to hold reusable Content Descriptor Objects.
	/// </summary>
	[JsonPropertyName("contentDescriptors")]
	public Dictionary<string, ContentDescriptorObject> СontentDescriptors { get; } = new();

	/// <summary>
	/// An object to hold reusable Schema Objects.
	/// </summary>
	[JsonPropertyName("schemas")]
	public Dictionary<string, JsonSchema> Schemas { get; } = new();

	/// <summary>
	/// An object to hold reusable Example Objects.
	/// </summary>
	[JsonPropertyName("examples")]
	public Dictionary<string, ExampleObject> Examples { get; } = new();

	/// <summary>
	/// An object to hold reusable Link Objects.
	/// </summary>
	[JsonPropertyName("links")]
	public Dictionary<string, LinkObject> Links { get; } = new();

	/// <summary>
	/// An object to hold reusable Error Objects.
	/// </summary>
	[JsonPropertyName("errors")]
	public Dictionary<string, ErrorObject> Errors { get; } = new();

	/// <summary>
	/// An object to hold reusable Example Pairing Objects.
	/// </summary>
	[JsonPropertyName("examplePairingObjects")]
	public Dictionary<string, ExamplePairingObject> ExamplePairingObjects { get; } = new();

	/// <summary>
	/// An object to hold reusable Tag Objects.
	/// </summary>
	[JsonPropertyName("tags")]
	public Dictionary<string, TagObject> Tags { get; } = new();
}
