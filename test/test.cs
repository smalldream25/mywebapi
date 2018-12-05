using bcg_dot_com.Utils;
using DD4T.ContentModel;
using DD4T.ContentModel.Factories;
using DD4T.Factories;
using DD4T.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using bcg_dot_com.Models.ContentModels;

namespace bcg_dot_com.Helpers {
	public class LinkHelper {
		private static LinkHelper _instance;
		private ILinkFactory lf;
		private LinkHelper()
		{
			lf = DependencyUtils.GetDependency<ILinkFactory>();
		}

		private static LinkHelper Instance {
			get {
				if (_instance == null) {
					try {
						_instance = new LinkHelper();
					}
					catch (Exception e) {
						Logger.Error(string.Format("Error initializing LinkFactory:\r\n{0}", e.Message));
					}
				}
				return _instance;
			}
		}

		public static string ResolveArticleHeaderLink(IComponent comp)
		{
			if (comp.Fields.ContainsKey("external_url")) {
				return comp.Fields["external_url"].Value;
			}
			else if (!string.IsNullOrEmpty(ResolveLink(comp.Id))) {
				return ResolveLink(comp.Id);
			}

	
			if (comp.Fields.ContainsKey("link")) {
				CTALinkList ctaList = new CTALinkList(comp.Fields);
				var embedCta_1 = ctaList.LastLink;
				var embedLink_1 = "";				
		
				if (embedCta_1.internalLink != null) {
					embedLink_1 = embedCta_1.internalLink;
				}
				if ((embedCta_1.internalLink == null && embedCta_1.externalLink != null)) {
					embedLink_1 = embedCta_1.externalLink;
				}

				if (embedLink_1 != null && !Equals(embedLink_1.ToString(), "#"))
					return embedLink_1.ToString();
			}
			return string.Empty;
		}
		public static string ResolveArticleHeaderLinkTarget(IComponent comp)
		{
			if (comp.Fields.ContainsKey("external_url")) {
				return "_blank";
			}

			if (comp.Fields.ContainsKey("link")) {
	
				return "_blank";
			}

			return string.Empty;
		}
		public static string ResolveArticleWrapperLinkTarget(IComponent comp)
		{
			if (comp.Fields.ContainsKey("cta")) {
				var embedCta = comp.Fields["cta"].EmbeddedValues.First();
				return embedCta.GetLinkTarget();
			}

			if (comp.Fields.ContainsKey("link")) {
				CTALinkList ctaList = new CTALinkList(comp.Fields);
				var embedCta_1 = ctaList.LastLink;
				var target = embedCta_1.target;
				return target;
			}

			return string.Empty;
		}

		public static string ResolveArticleWrapperLink(IComponent comp)
		{
			HtmlString embedLink = null;
			var linkedArticleLink = string.Empty;
			var artHeaderLink = string.Empty;

			//First Check: If the CTA internal selector field is complete, go to that location
			//Second Check: If above is empty and CTA external link field is complete, go to that location in a new window, based on defined link behavior.
			if (comp.Fields.ContainsKey("cta")) {
				var embedCta = comp.Fields["cta"].EmbeddedValues.First();
				embedLink = embedCta.GetLinkUrl();
				if (embedLink != null && !Equals(embedLink.ToString(), "#")) {
					return embedLink.ToString();
				}
			}

			//Third Check: If above are empty and Linked Articles field contains a collection component, go to nearest page where the collection component is published.
			if ((embedLink == null || Equals(embedLink.ToString(), "#")) && comp.Fields.ContainsKey("linked_articles")) {
				foreach (var linkedComp in comp.Fields["linked_articles"].LinkedComponentValues) {
					if (Equals(linkedComp.Schema.RootElementName, "article_wrapper_content") || Equals(linkedComp.Schema.RootElementName, "collection_group_schema")) {
						linkedArticleLink = ResolveLink(linkedComp.Id);
						if (!string.IsNullOrEmpty(linkedArticleLink)) {
							return linkedArticleLink;
						}
					}
				}
			}

			//Fourth Check: If above are empty and Article Header field has an attached article header component, go to the nearest page where the component is published.
			if (string.IsNullOrEmpty(linkedArticleLink) && comp.Fields.ContainsKey("article_header")) {
				artHeaderLink = ResolveLink(comp.Fields["article_header"].LinkedComponentValues.First().Id);
				if (!string.IsNullOrEmpty(artHeaderLink))
					return artHeaderLink;
			}

			//Fifth Check: If all above are empty, render link for nearest use of the Article Wrapper component. If the Article Wrapper component is not published anywhere else, the item is not clickable.
			if (string.IsNullOrEmpty(artHeaderLink) && !string.IsNullOrEmpty(ResolveLink(comp.Id))) {
				var link = ResolveLink(comp.Id);
				return link;
			}

			if (comp.Fields.ContainsKey("link")) {
				CTALinkList ctaList = new CTALinkList(comp.Fields);
				var embedCta_1 = ctaList.LastLink;
				var embedLink_1 = "";
			
				if (embedCta_1.internalLink != null) {
					embedLink_1 = embedCta_1.internalLink;
				}
				if ((embedCta_1.internalLink == null && embedCta_1.externalLink != null)) {
					embedLink_1 = embedCta_1.externalLink;
				}
		

				if (embedLink_1 != null && !Equals(embedLink_1.ToString(), "#"))
					return embedLink_1.ToString();
			}

			return string.Empty;
		}
		public static string ResolveArticleWrapperLinkIcon(IComponent comp)
		{
			if (comp.Fields.ContainsKey("cta")) {
				var embedCta = comp.Fields["cta"].EmbeddedValues.First();
				return embedCta.GetLinkIcon();
			}
			return string.Empty;
		}
		public static string ResolveArticleWrapperLinkText(IComponent comp)
		{
			if (comp.Fields.ContainsKey("cta")) {
				var embedCta = comp.Fields["cta"].EmbeddedValues.First();
				return embedCta.GetLinkTitle();
			}

			return string.Empty;
		}

		public static string ResolveLocalArticleLink(IComponent comp)
		{
			if (comp.Fields.ContainsKey("cta")) {
				var embedCta = comp.Fields["cta"].EmbeddedValues.First();
				var embedLink = embedCta.GetLinkUrl();
				if (embedLink != null && !Equals(embedLink.ToString(), "#"))
					return embedLink.ToString();
			}

			if (comp.Fields.ContainsKey("link")) {
				CTALinkList ctaList = new CTALinkList(comp.Fields);
				var embedCta_1 = ctaList.LastLink;
				var embedLink_1 = "";

				if (embedCta_1.internalLink != null) {
					embedLink_1 = embedCta_1.internalLink;
				}
				if ((embedCta_1.internalLink == null && embedCta_1.externalLink != null)) {
					embedLink_1 = embedCta_1.externalLink;
				}

				if (embedLink_1 != null && !Equals(embedLink_1.ToString(), "#"))
					return embedLink_1.ToString();
			}
			return string.Empty;
		}
		public static string ResolveLocalArticleLinkTarget(IComponent comp)
		{
			if (comp.Fields.ContainsKey("cta")) {
				var embedCta = comp.Fields["cta"].EmbeddedValues.First();
				var target = embedCta.GetLinkTarget();
				return target;
			}

			if (comp.Fields.ContainsKey("link")) {
				CTALinkList ctaList = new CTALinkList(comp.Fields);
				var embedCta_1 = ctaList.LastLink;
				var target = embedCta_1.target;
				return target;
			}
			return string.Empty;
		}
		public static string ResolveLocalArticleLinkIcon(IComponent comp)
		{
			if (comp.Fields.ContainsKey("cta")) {
				var embedCta = comp.Fields["cta"].EmbeddedValues.First();
				var icon = embedCta.GetLinkIcon();
				return icon;
			}

			if (comp.Fields.ContainsKey("link")) {
				CTALinkList ctaList = new CTALinkList(comp.Fields);
				var embedCta_1 = ctaList.LastLink;
				var icon = embedCta_1.icon;
				return icon;
			}

			return string.Empty;
		}
		public static string NormalizeComponentName(string name)
		{
			name = name.Replace(" ", "-");
			name = Regex.Replace(name, @"[^\w\-]", "");
			return name;
		}

		public static string ResolveLink(string id)
		{
			try {
				return Instance.lf.ResolveLink(id);
			}
			catch (Exception e) {
				Logger.Error(string.Format("Error resolving link for id: {0}\r\n{1}", id, e.Message));
			}
			return string.Empty;
		}

		public static string ResolveLink(string pageId, string componentId, string excludeComponentTemplateId)
		{
			try {
				return Instance.lf.ResolveLink(pageId, componentId, excludeComponentTemplateId);
			}
			catch (Exception e) {
				Logger.Error(string.Format("Error resolving link for id: {0}\r\n{1}", componentId, e.Message));
			}
			return string.Empty;
		}
	}
}
