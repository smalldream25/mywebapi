using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumTests.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using static SeleniumTests.Utils.Extensions;
using SeleniumTests.Helpers;
using System.Configuration;
using System.Diagnostics;
using SeleniumTests.Abstract;
using System.Threading;
using System.Net;

namespace SeleniumTests.GeneralRegression { 
	[TestClass]
	public class EnsuringAbTesting : AbstractSeleniumTestContext{
		FormHelper fh;
		private string defaultUrl = "/en-demo/tahzoo/temp/sprint16/dttm-1588/defaultstatetest-noindextag.aspx"; //default url
		private string noIndexUrl = "/en-demo/tahzoo/temp/sprint16/dttm-1588/noindextag-testcheckedstate.aspx"; //checking noindex tag url
		private string noFollowUrl = "/en-demo/tahzoo/temp/sprint16/dttm-1588/nofollowtag-testcheckedstate.aspx"; //checking nofollow tag url
		private string noIndexNofollowUrl = "/en-demo/tahzoo/temp/sprint16/dttm-1588/checkedstate-nofollow-noindex.aspx"; //career noindex and nofollow url
	    private string att_1 = "<meta name=\"robots\" content=\"noindex\" />";
		private string att_2 = "<meta name=\"robots\" content=\"nofollow\" />";
		private string att_3 = "<meta name=\"robots\" content=\"noindex, nofollow\" />";

		[TestMethod]
		[TestCategory("Chrome")]

		public void ensuringAbTesting_default()
		{
			driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["SiteUrl"] + defaultUrl);
			string pageSource = driver.PageSource;

			Boolean result = false;

			if (pageSource.Contains(att_1) || pageSource.Contains(att_2) || pageSource.Contains(att_3)) {
				result = true;
			}
			else {
				result = false;
			}

			//If you find all the attributes then pass the test, otherwise fail the test
			Assert.IsFalse(result);
		}

		[TestMethod]
		[TestCategory("Chrome")]

	    public void ensuringAbTesting_check_nofollow()
		{
			driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["SiteUrl"] + noFollowUrl);
			string pageSource = driver.PageSource;

			Boolean result = false;

			if (pageSource.Contains(att_2))) {
				result = true;
			}
			else {
				result = false;
			}

			Assert.IsTrue(result);
		}


		[TestMethod]
		[TestCategory("Chrome")]

	    public void ensuringAbTesting_check_noindex_nofollow()
		{
			driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["SiteUrl"] + noIndexNofollowUrl);
			string pageSource = driver.PageSource;

			Boolean result = false;

			if (pageSource.Contains(att_1) && pageSource.Contains(att_2))) {
				result = true;
			}
			else {
				result = false;
			}

			Assert.IsTrue(result);
		}

		
		[TestMethod]
		[TestCategory("Chrome")]

	    public void ensuringAbTesting_check_noindex()
		{
			driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["SiteUrl"] + noIndexUrl);
			string pageSource = driver.PageSource;

			Boolean result = false;

			if (pageSource.Contains(att_1))) {
				result = true;
			}
			else {
				result = false;
			}

			Assert.IsTrue(result);
		}




		[TestInitialize()]
		public void SetupTest()
		{
			base.Setup(BrowserEnum.Chrome);
			//Console.WriteLine(pageSource);

			//look for the anti clicjacking script a
			fh = new FormHelper(this);
		}

		[TestCleanup()]
		public new void Clean()
		{
			base.Clean();
		}
	}
}
