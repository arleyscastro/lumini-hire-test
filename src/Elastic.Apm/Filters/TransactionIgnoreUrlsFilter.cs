using Elastic.Apm.Api;
using Elastic.Apm.Config;
using Elastic.Apm.Helpers;

namespace Elastic.Apm.Filters
{
	/// <summary>
	/// Contains a transaction filter which filters out transactions based on request url path.
	/// </summary>
	internal class TransactionIgnoreUrlsFilter
	{
		private readonly IConfigSnapshot _configSnapshot;
		public TransactionIgnoreUrlsFilter(IConfigSnapshot configSnapshot) => _configSnapshot = configSnapshot;
		public ITransaction Filter(ITransaction transaction) =>
			WildcardMatcher.IsAnyMatch(_configSnapshot.TransactionIgnoreUrls, transaction?.Context?.Request?.Url?.PathName) ? null : transaction;
	}
}
