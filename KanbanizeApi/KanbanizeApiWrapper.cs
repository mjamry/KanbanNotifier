using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using System.IO;

namespace KanbanNotifier.KanbanizeApi
{
    public class KanbanizeApiWrapper
    {
        private const string URL = "http://kanbanize.com/index.php/api/kanbanize";
        private const string API_KEY = "wyUygoUtlYBIxQNzjyBXiKGH5Tx14phjFhMFAdzt";
        private const int BOARD_ID = 2;
        private const int RESULT_PER_PAGE = 100;
        KanbanizeQueryAssembler _assembler = new KanbanizeQueryAssembler();

        public List<Activity> GetActivities()
        {
            DateTime date = new DateTime(2013, 08, 01);

            string query = _assembler.GetActivities().BoardId(BOARD_ID).FromDate(date).ToDate("now").Page(1).ResultsPerPage(RESULT_PER_PAGE).Query;
            _assembler.ClearQuery();
            string result = Request(query);

            List<Activity> activitiesList = new List<Activity>();
            try
            {
                XDocument doc = XDocument.Parse(result);
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
            string query = _assembler.GetTask().BoardId(BOARD_ID).TaskId(taskId).Query;
            _assembler.ClearQuery();
            string result = Request(query);

            Task task = Task.Empty;
            try
            {
                XDocument doc = XDocument.Parse(result);
                task = new Task(int.Parse(doc.Root.Element("taskid").Value), doc.Root.Element("title").Value, doc.Root.Element("description").Value, doc.Root.Element("assignee").Value, ColorTranslator.FromHtml(doc.Root.Element("color").Value), doc.Root.Element("priority").Value, doc.Root.Element("extlink").Value);
            }
            catch(Exception e)
            {
                Console.Write(e.Message);
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

                try
                {
                    response = client.UploadString(uri, string.Empty);
                }
                catch (WebException e)
                {
                    Console.Write(e.Message);
                }
            }

            return response;
        }
    }
}
