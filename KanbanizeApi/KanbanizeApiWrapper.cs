using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Net;

namespace KanbanNotifier.KanbanizeApi
{
    public class KanbanizeApiWrapper
    {
        private readonly string _apiUrl;
        private readonly string _apiKey;
        private readonly int _boardId;
        private readonly int _resultsPerPage;
        private readonly KanbanizeQueryAssembler _assembler;

        public KanbanizeApiWrapper(KanbanApiConfig config)
        {
            _assembler = new KanbanizeQueryAssembler();
            _boardId = config.BoardId;
            _resultsPerPage = config.ResultsPerPage;
            _apiKey = config.ApiKey;
            _apiUrl = config.ApiURL;
        }

        public List<Activity> GetActivities()
        {
            //yesterday
            DateTime date = DateTime.Now.AddDays(-1);

            string query = _assembler.GetActivities().BoardId(_boardId).FromDate(date).ToDate("now").Page(1).ResultsPerPage(_resultsPerPage).Query;
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

        private Task GetTask(int taskId)
        {
            string query = _assembler.GetTask().BoardId(_boardId).TaskId(taskId).Query;
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
            Uri uri = new Uri(_apiUrl + parameters);

            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                client.Headers.Add("apikey", _apiKey);

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
