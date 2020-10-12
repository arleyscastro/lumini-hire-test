



namespace Elastic.Apm.Api
{
	public struct ApiConstants
	{
		public const string TypeRequest = "request";

		public const string ActionExec = "exec";
		public const string ActionQuery = "query";

		public const string SubtypeElasticsearch = "elasticsearch";
		public const string SubtypeHttp = "http";
		public const string SubtypeMssql = "mssql";
		public const string SubtypeSqLite = "sqlite";
		public const string SubtypeMySql = "mysql";
		public const string SubtypeOracle = "oracle";
		public const string SubtypePostgreSql = "postgresql";
		public const string SubTypeGrpc = "grpc";

		public const string TypeDb = "db";
		public const string TypeExternal = "external";
	}
}