



namespace Elastic.Apm.Api
{
	/// <summary>
	/// Represents an error which was captured by the agent.
	/// </summary>
	public interface IError
	{
		/// <summary>
		/// The culprit that caused this error.
		/// </summary>
		string Culprit { get; set; }

		/// <summary>
		/// In case the error instance was caused by an exception, this property contains all the
		/// details about the exception.
		/// </summary>
		CapturedException Exception { get; }

		/// <summary>
		/// The Id of the error. This unequally identifies the given error.
		/// </summary>
		string Id { get; }

		/// <summary>
		/// The Parent id of the error
		/// </summary>
		string ParentId { get; }

		/// <summary>
		/// The id of the trace where this error happened
		/// </summary>
		string TraceId { get; }

		/// <summary>
		/// The id of the transaction where this error happened
		/// </summary>
		string TransactionId { get; }
	}
}
