// Type: System.Web.Mvc.TagBuilder
// Assembly: System.Web.WebPages, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\System.Web.WebPages.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Web.Mvc
{
	[TypeForwardedFrom("System.Web.Mvc, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class TagBuilder
	{
		public TagBuilder(string tagName);
		public IDictionary<string, string> Attributes { get; }
		public string IdAttributeDotReplacement { get; set; }
		public string InnerHtml { get; set; }
		public string TagName { get; }
		public void AddCssClass(string value);
		public static string CreateSanitizedId(string originalId);
		public static string CreateSanitizedId(string originalId, string invalidCharReplacement);
		public void GenerateId(string name);
		public void MergeAttribute(string key, string value);
		public void MergeAttribute(string key, string value, bool replaceExisting);
		public void MergeAttributes<TKey, TValue>(IDictionary<TKey, TValue> attributes);
		public void MergeAttributes<TKey, TValue>(IDictionary<TKey, TValue> attributes, bool replaceExisting);
		public void SetInnerText(string innerText);
		public override string ToString();
		public string ToString(TagRenderMode renderMode);
	}
}
