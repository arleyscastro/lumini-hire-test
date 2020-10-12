



using System.Runtime.Serialization;

namespace Elastic.Apm.Api
{
	public enum Outcome
	{
		[EnumMember(Value = "unknown")]
		Unknown = 0, //Make sure Unknown remains the default value

		[EnumMember(Value = "success")]
		Success,

		[EnumMember(Value = "failure")]
		Failure
	}
}
