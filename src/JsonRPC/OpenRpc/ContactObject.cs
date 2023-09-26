using System.Text.Json.Serialization;

namespace Larine.JsonRPC.OpenRPC;

/// <summary>
/// Contact information for the exposed API.
/// </summary>
public record class ContactObject
{
	/// <summary>
	/// The identifying name of the contact person/organization.
	/// </summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>
	/// The URL pointing to the contact information. MUST be in the format of a URL.
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; init; }

	/// <summary>
	/// The email address of the contact person/organization. MUST be in the format of an email address.
	/// </summary>
	[JsonPropertyName("email")]
	public string? Email { get; init; }
}
