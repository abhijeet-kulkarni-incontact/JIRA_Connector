using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Regression.Shared
{

    public class JIRAConnector
    {
        private static readonly HttpClient client = new HttpClient();

        public string CreateIssue(string body)
        {
            // Console.WriteLine("")
            // string body = "{\"fields\":{\"project\":{\"id\":\"11025\"},\"summary\":\"Creating test set to test REST api.\",\"description\":\"Creating a Test Set containing Tests\",\"issuetype\":{\"name\":\"Test Execution\"},\"assignee\":{\"name\":\"abhijeet.kulkarni\"}}}";
            var client = new RestClient("https://tlvjiratst02.nice.com/rest/api/2/issue");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Basic Q1hvbmVEZXZPcHNKaXJhOkdkMTJxdzM0");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(body);
            var response = client.Execute(request);

            return response.Content;
        }

        public string addTestsToTheIssue(string issueID, string testcase)
        {
            string body = "{\"add\":[\"" + testcase + "\"],\"remove\":[]}";
            var client = new RestClient("https://tlvjiratst02.nice.com/rest/raven/1.0/api/testexec/" + issueID + "/test");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Basic Q1hvbmVEZXZPcHNKaXJhOkdkMTJxdzM0");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(body);
            var response = client.Execute(request);
            return response.Content;
        }

        public string GetAllTestsFromTestExecution(string execution_id)
        {

            // string body = "{\"fields\":{\"project\":{\"id\":\"11025\"},\"summary\":\"Creating test set to test REST api.\",\"description\":\"Creating a Test Set containing Tests\",\"issuetype\":{\"name\":\"Test Execution\"},\"assignee\":{\"name\":\"abhijeet.kulkarni\"}}}";
            var client = new RestClient("https://tlvjiratst02.nice.com/rest/raven/1.0/api/testexec/" + execution_id + "/test");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Basic Q1hvbmVEZXZPcHNKaXJhOkdkMTJxdzM0");
            var response = client.Execute(request);
            return response.Content;
        }
        public string UpdateTestRunStatus(Object testID, string status, Object key, Object rank)
        {
            string body = "{\"id\":" + testID + ",\"status\":\"" + status + "\",\"key\":\"" + key + "\",\"rank\":" + rank + "}";
            Console.WriteLine("body" + body);
            var client = new RestClient("https://tlvjiratst02.nice.com/rest/raven/1.0/api/testrun/" + testID + "/");
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Authorization", "Basic Q1hvbmVEZXZPcHNKaXJhOkdkMTJxdzM0");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(body);
            var response = client.Execute(request);

            return response.Content;
        }

        public bool CheckIfIssuePresent(string issueID)
        {
            var client = new RestClient("https://tlvjiratst02.nice.com/rest/api/2/search?jql=id=" + issueID + "");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Basic Q1hvbmVEZXZPcHNKaXJhOkdkMTJxdzM0");
            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                return true;
            }
            else
            {

                return false;
            }
        }
    }
}
