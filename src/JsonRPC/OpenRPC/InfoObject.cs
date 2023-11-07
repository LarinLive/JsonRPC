using System.Text.Json.Serialization;

namespace Larine.JsonRPC.OpenRPC;

/// <summary>
/// Provides metadata about the API. The metadata MAY be used by the clients if needed, and MAY be presented in editing or documentation generation tools for convenience.
/// </summary>
public record class InfoObject
{
	/// <summary>
	/// REQUIRED. The title of the application.
	/// </summary>
	[JsonPropertyName("title")]
	public string Title { get; init; } = string.Empty;

	/// <summary>
	/// A verbose description of the application. GitHub Flavored Markdown syntax MAY be used for rich text representation.
	/// </summary>
	[JsonPropertyName("description")]
	public string? Description { get; init; }

	/// <summary>
	/// A URL to the Terms of Service for the API. MUST be in the format of a URL.
	/// </summary>
	[JsonPropertyName("termsOfService")]
	public string? TermsOfService { get; init; }

	/// <summary>
	/// The contact information for the exposed API.
	/// </summary>
	[JsonPropertyName("contact")]
	public ContactObject? Contact { get; init; }

	/// <summary>
	/// REQUIRED. The license name used for the API.
	/// </summary>
	[JsonPropertyName("license")]
	public string? License { get; init; }

	/// <summary>
	/// REQUIRED. The version of the OpenRPC document (which is distinct from the OpenRPC Specification version or the API implementation version).
	/// </summary>
	[JsonPropertyName("version")]
	public string Version { get; init; } = string.Empty;
}
