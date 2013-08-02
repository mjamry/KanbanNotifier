using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using System.IO;

namespace KanbanNotifier
{
    public class KanbanizeApiWrapper
    {
        private const string URL = "http://kanbanize.com/index.php/api/kanbanize/";
        private const string API_KEY = "wyUygoUtlYBIxQNzjyBXiKGH5Tx14phjFhMFAdzt";
        private const int BOARD_ID = 2;
        private const int RESULT_PER_PAGE = 100;


        public List<Activity> GetActivities()
        {
            string parameters = "get_board_activities/boardid/{0}/fromdate/{1}/todate/{2}/page/{3}/resultsperpage/{4}";
            string result = Request(string.Format(parameters, BOARD_ID, "-1 day", "now", 1, RESULT_PER_PAGE));

            XDocument doc = XDocument.Parse(result);

            List<Activity> activitiesList = new List<Activity>();
            try
            {
                activitiesList = (
                                     from activities in doc.Root.Element("activities").Elements()
                                     select new Activity(
                                         activities.Element("author").Value,
                                         activities.Element("text").Value,
                                         activities.Element("event").Value,
                                         DateTime.Parse(activities.Element("date").Value),
                                         int.Parse(activities.Element("taskid").Value)
                                         )
                                 ).ToList<Activity>();
            }catch(Exception e)
            {
                Console.Write(e.Message);
            }

            foreach (var activity in activitiesList)
            {
                activity.AssociateTask(GetTask(activity.TaskId));
            }

            return activitiesList;
        }

        public Task GetTask(int taskId)
        {
            string parameters = "get_task_details/boardid/{0}/taskid/{1}";
            string result = Request(string.Format(parameters, BOARD_ID, taskId));

            


            Task task = Task.Empty;
            try
            {
                XDocument doc = XDocument.Parse(result);
                task = new Task(int.Parse(doc.Root.Element("taskid").Value), doc.Root.Element("title").Value, doc.Root.Element("description").Value, doc.Root.Element("assignee").Value, ColorTranslator.FromHtml(doc.Root.Element("color").Value), doc.Root.Element("priority").Value, doc.Root.Element("extlink").Value);
            }
            catch(Exception e)
            {
                
            }
           
            return task;
        }

        private string Request(string parameters)
        {
            string response = string.Empty;
            Uri uri = new Uri(URL + parameters);

            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                client.Headers.Add("apikey", API_KEY);

                
                    response = client.UploadString(uri, string.Empty);
                
            }

            return response;
        }
    }
}
