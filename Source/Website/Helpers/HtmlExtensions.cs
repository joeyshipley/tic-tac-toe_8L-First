﻿using System.Web;
using System.Web.Mvc;

namespace TTT.Website.Helpers
{
	public static class HtmlExtensions
	{
		public static IHtmlString LoadScript(this HtmlHelper helper, string file) 
		{
			var root = VirtualPathUtility.ToAbsolute("~/Resources/Scripts");
			return new HtmlString(string.Format("<script src=\"{0}/{1}\" type=\"text/javascript\"></script>\r\n", root, file));
		}

		public static IHtmlString LoadStylesheet(this HtmlHelper helper, string file) 
		{
			var root = VirtualPathUtility.ToAbsolute("~/Resources/Styles");
			return new HtmlString(string.Format("<link rel=\"stylesheet\" href=\"{0}/{1}\">\r\n", root, file));
		}

		public static IHtmlString DrawImage(this HtmlHelper helper, string file) 
		{
			var root = VirtualPathUtility.ToAbsolute("~/Resources/Images");
			return new HtmlString(string.Format("<img src=\"{0}/{1}\" />\r\n", root, file));
		}
	}
}