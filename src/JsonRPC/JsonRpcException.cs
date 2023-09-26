using Json.Schema;
using System;

namespace Larine.JsonRPC;

public class JsonRpcException : ApplicationException
{
	public JsonRpcException(string? message)
		: base(message) { }

	public JsonRpcException(string? message, Exception? innerException)
		: base(message, innerException) { }

	public EvaluationResults[] SchemaEvaluationResult { get; init; } = Array.Empty<EvaluationResults>();
}
