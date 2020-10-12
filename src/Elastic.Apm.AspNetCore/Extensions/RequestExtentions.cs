



using System;
using System.IO;
using System.Text;
using Elastic.Apm.Config;
using Elastic.Apm.Helpers;
using Elastic.Apm.Logging;
using Microsoft.AspNetCore.Http;

namespace Elastic.Apm.AspNetCore.Extensions
{
	internal static class HttpRequestExtensions
	{
		/// <summary>
		/// Extracts the request body using measure to prevent the 'read once' problem (cannot read after the body ha been already
		/// read).
		/// </summary>
		/// <param name="request"></param>
		/// <param name="logger"></param>
		/// <returns></returns>
		public static string ExtractRequestBody(this HttpRequest request, IApmLogger logger, IConfigSnapshot configSnapshot)
		{
			string body = null;

			try
			{
				if (request.HasFormContentType)
				{
					var form = request.Form;

					var itemProcessed = 0;
					if (form != null && form.Count > 0)
					{
						var sb = new StringBuilder();

						foreach (var item in form)
						{
							sb.Append(item.Key);
							sb.Append("=");

							if(WildcardMatcher.IsAnyMatch(configSnapshot.SanitizeFieldNames, item.Key))
								sb.Append(Elastic.Apm.Consts.Redacted);
							else
								sb.Append(item.Value);

							itemProcessed++;
							if (itemProcessed != form.Count)
								sb.Append("&");
						}

						body = sb.ToString();
					}
				}
				else
				{
					request.EnableBuffering();
					request.Body.Position = 0;

					using (var reader = new StreamReader(request.Body,
						Encoding.UTF8,
						false,
						1024 * 2,
						true))
						body = reader.ReadToEnd();

					// Truncate the body to the first 2kb if it's longer
					if (body.Length > Consts.RequestBodyMaxLength) body = body.Substring(0, Consts.RequestBodyMaxLength);
					request.Body.Position = 0;
				}
			}
			catch (IOException ioException)
			{
				logger.Error()?.LogException(ioException, "IO Error reading request body");
			}
			catch (Exception e)
			{
				logger.Error()?.LogException(e, "Error reading request body");
			}
			return body;
		}
	}
}
