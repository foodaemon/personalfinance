﻿using System;
using System.Web.Mvc;
using System.Text;
using Newtonsoft.Json;
using System.Web;

namespace Web.Infrastructure
{
	public class JsonNetResult : ActionResult
	{
		public Encoding ContentEncoding { get; set; }
		public string ContentType { get; set; }
		public object Data { get; set; }

		public JsonSerializerSettings SerializerSettings { get; set; }
		public Formatting Formatting { get; set; }

		public JsonNetResult()
		{
			SerializerSettings = new JsonSerializerSettings();
			Formatting = Formatting.Indented;
		}

		public override void ExecuteResult(ControllerContext context)
		{
			if (context == null)
				throw new ArgumentNullException("context");

			HttpResponseBase response = context.HttpContext.Response;

			response.ContentType = !string.IsNullOrEmpty(ContentType)
				? ContentType
				: "application/json";

			if (ContentEncoding != null)
				response.ContentEncoding = ContentEncoding;

			if (Data != null)
			{
				JsonTextWriter writer = new JsonTextWriter(response.Output) { Formatting = Formatting };

				JsonSerializer serializer = JsonSerializer.Create(SerializerSettings);
				serializer.Serialize(writer, Data);

				writer.Flush();
			}
		}
	}
}