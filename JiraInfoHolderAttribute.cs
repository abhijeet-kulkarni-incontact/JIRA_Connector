using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Regression.JIRA_Rest;
using Regression.Shared;
using System;
using System.Collections;
using System.Diagnostics;

namespace Regression.Tests
{
    public enum XRayTestcase { ID, NAME, NONE };


    [AttributeUsage(AttributeTargets.All)]
    public class JiraInfoHolderAttribute : Attribute
    {
        private string test_case_id;
        private string issueID = null;
        static bool createdTestPlan = false;
        static string testPlanName = null;

        JIRAConnector jira_connect = new JIRAConnector();
        //   private TestCaseInfo iD;

        // static ArrayList testcases = new ArrayList();


        public JiraInfoHolderAttribute(XRayTestcase info)
        {
            if (info.ToString() != "NONE")
            {
                Console.WriteLine("No test case details provided.");
            }
        }

        public JiraInfoHolderAttribute(XRayTestcase tc_info, string name)
        {
            Console.WriteLine(tc_info);

            if (Constants.currenttest != "")
            {
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                var cout = t.ToString();
                Console.WriteLine(t.ToString());

                //  this.test_case_id = name;

                if (testPlanName != null || createdTestPlan == true)
                {
                    Console.WriteLine("Test Plan already created.");
                    Constants.CURRENT_TEST_PLAN_ID = testPlanName;
                }
                else
                {
                    string body = "{\"fields\":{\"project\":{\"id\":\"11025\"},\"summary\":\"Creating test set to test REST api.\",\"description\":\"Creating a Test Set containing Tests\",\"issuetype\":{\"name\":\"Test Execution\"},\"assignee\":{\"name\":\"abhijeet.kulkarni\"}}}";
                    Console.WriteLine("First Creating Test Plan...");
                    var res = jira_connect.CreateIssue(body);
                    //////Saving test plan ID
                    issueID = res.Split(':')[2].Split(',')[0].Replace("\"", "");
                    Console.WriteLine("Test Execution is Created in Jira", issueID);
                    testPlanName = issueID;
                    createdTestPlan = true;
                    Constants.CURRENT_TEST_PLAN_ID = testPlanName;
                }

                Constants.CURRENT_TEST_ID = name;

                bool issuePresent = jira_connect.CheckIfIssuePresent(name);

                if (issuePresent)
                {
                    Console.WriteLine("Issue already present in JIRA.Adding test case in Test Execution.");
                    //////Add test cases to the test plan
                    var res = jira_connect.addTestsToTheIssue(testPlanName, name);
                    Console.WriteLine("Test case added to the Test Execution.");
                }
                else
                {
                    string body = "{\"fields\":{\"project\":{\"id\":\"11025\"},\"summary\":\"" + Constants.currenttest + "\",\"description\":\"example of generic test\",\"issuetype\":{\"name\":\"Test\"},\"assignee\":{\"name\":\"abhijeet.kulkarni\"},\"priority\":{\"name\":\"P3\"}}}";

                    Console.WriteLine("Issue not present in JIRA...Creating issue and then adding to the Execution.");
                    var res = jira_connect.CreateIssue(body);
                    string testcase_id = res.Split(':')[2].Split(',')[0].Replace("\"", "");
                    Constants.CURRENT_TEST_ID = testcase_id;
                    Console.WriteLine(testcase_id);
                    var res1 = jira_connect.addTestsToTheIssue(testPlanName, testcase_id);
                    Console.WriteLine(res1);
                }

            }

        }
    }
}