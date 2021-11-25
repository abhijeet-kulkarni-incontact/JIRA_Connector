using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using Regression.JIRA_Rest;
using Regression.Shared;
using RestSharp;
using TestContext = NUnit.Framework.TestContext;

namespace Regression.Tests
{
    [TestClass]
    public class UnitTest1
    {
        JIRAConnector jira_connect = new JIRAConnector();


        [SetUp]
        public void setup()
        {
            Constants.currenttest = TestContext.CurrentContext.Test.MethodName;
            Console.WriteLine("Test name in setup", Constants.currenttest);
        }

        [Test]
        public void test1()
        {
            Console.WriteLine("hiii");
        }

        //    [Test]
        //   [TestCase("MS-4008")]
        public void TestMethod1(string testcase_id)
        {
            Console.WriteLine("Hii");

            //////Creating Test Plan in JIRA
            //JIRAConnector jira_connect = new JIRAConnector();
            //var res = jira_connect.CreateIssue();
            ////Console.WriteLine(res);

            //////Saving test plan ID
            //string issueID = res.Split(':')[2].Split(',')[0].Replace("\"", "");
            //Console.WriteLine(issueID);

            //////Add test cases to the test plan
            //res = jira_connect.addTestsToTheIssue(issueID);

            ////Checking for all the tests added to the test plan
            //string st = jira_connect.GetAllTestsFromTestExecution(issueID);

            //JArray json = (JArray)JsonConvert.DeserializeObject(st);
            //////Iterating over the test cases to change status.

            //foreach (var item in json)
            //{
            //    string sr = jira_connect.UpdateTestRunStatus(item["id"], "PASS", item["key"], item["rank"]);

            //}
        }


        //[Test]
        //[JiraInfoHolder(XRayTestcase.ID,"MS")]
        //public void checkIfPOCRedirectorIsAvailable()
        //{
        //    ////Creating Test Plan in JIRA
        //    JIRAConnector jira_connect = new JIRAConnector();
        //    //  bool b = jira_connect.CheckIfIssuePresent("MS-4455");
        //    //  Console.WriteLine("Status " + b);
        //    // Console.WriteLine(TestContext.CurrentContext.Test.Properties["TestCaseId"]);
        //}
        
        //[Test]
        //[JiraInfoHolder(XRayTestcase.ID,"None")]
        //public void checkIfPOCRedirectorIsreturning200ok_code()
        //{

        //}

        //[Test]
        //[JiraInfoHolder(XRayTestcase.ID,"MS-4008")]
        //public void JiraTest2()
        //{

        //}

        [TearDown]
        public void teardown()
        {
            //Checking for all the tests added to the test plan
            string st = jira_connect.GetAllTestsFromTestExecution("MS-4507");

            JArray json = (JArray)JsonConvert.DeserializeObject(st);
            ////Iterating over the test cases to change status.

            foreach (var item in json)
            {
                string sr = jira_connect.UpdateTestRunStatus(item["id"],"PASS", item["key"], item["rank"]);

            }
        }
    }
}
