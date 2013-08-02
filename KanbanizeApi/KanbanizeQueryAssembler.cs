
using System;

namespace KanbanNotifier.KanbanizeApi
{
    internal class QueryElements
    {
        public const string GET_BOARD_ACTIVITIES = "get_board_activities";
        public const string GET_TASK_DETAILS = "get_task_details";

        public const string BOARD_ID = "boardid";
        public const string FROM_DATE = "fromdate";
        public const string TO_DATE = "todate";
        public const string PAGE = "page";
        public const string RESULTS_PER_PAGE = "resultsperpage";
        public const string TASK_ID = "taskid";
    }

    internal class KanbanizeQueryAssembler
    {
        private const string SEPARATOR = "/";
        private string _query = SEPARATOR;
        private const string DATE_FORMAT = "yyyy-MM-dd";

        public void ClearQuery()
        {
            _query = SEPARATOR;
        }

        public string Query
        {
            get
            {
                return _query;
            }
        }

        public KanbanizeQueryAssembler GetActivities()
        {
            _query += QueryElements.GET_BOARD_ACTIVITIES + SEPARATOR;
            return this;
        }

        public KanbanizeQueryAssembler GetTask()
        {
            _query += QueryElements.GET_TASK_DETAILS + SEPARATOR;
            return this;
        }

        public KanbanizeQueryAssembler BoardId(int id)
        {
            _query += QueryElements.BOARD_ID + SEPARATOR + id + SEPARATOR;
            return this;
        }

        public KanbanizeQueryAssembler Page(int pageNb)
        {
            _query += QueryElements.PAGE + SEPARATOR + pageNb + SEPARATOR;
            return this;
        }

        public KanbanizeQueryAssembler FromDate(DateTime date)
        {
            _query += QueryElements.FROM_DATE + SEPARATOR + date.ToString(DATE_FORMAT) + SEPARATOR;
            return this;
        }

        public KanbanizeQueryAssembler FromDate(string from)
        {
            _query += QueryElements.FROM_DATE + SEPARATOR + from + SEPARATOR;
            return this;
        }

        public KanbanizeQueryAssembler ToDate(DateTime date)
        {
            _query += QueryElements.TO_DATE + SEPARATOR + date.ToString(DATE_FORMAT) + SEPARATOR;
            return this;
        }
        
        public KanbanizeQueryAssembler ToDate(string to)
        {
            _query += QueryElements.TO_DATE + SEPARATOR + to + SEPARATOR;
            return this;
        }

        public KanbanizeQueryAssembler ResultsPerPage(int results)
        {
            _query += QueryElements.RESULTS_PER_PAGE + SEPARATOR + results + SEPARATOR;
            return this;
        }

        public KanbanizeQueryAssembler TaskId(int id)
        {
            _query += QueryElements.TASK_ID + SEPARATOR + id + SEPARATOR;
            return this;
        }
    }
}