using System.Web.Mvc;
using System.Web;

namespace FlashMessageExtensions
{
    public static class Alerts
    {
        public const string SUCCESS = "success";
        public const string ATTENTION = "attention";
        public const string ERROR = "danger";
        public const string INFORMATION = "info";
        public const string WARNING = "warning";




        public static string[] ALL
        {
            get { return new[] { SUCCESS, ATTENTION, INFORMATION, ERROR, WARNING }; }
        }
    }

    public static class MenuRoles
    {
        public const string ADD = "Add";
        public const string EDIT = "Edit";
        public const string DELETE = "Delete";
        public const string PRINT = "Print";
        public const string VIEW = "View";
        public static string[] ALL
        {
            get { return new[] { ADD, EDIT, DELETE, PRINT, VIEW }; }
        }

    }

    public class ModalPopUp
    {
        public ModalPopUp()
        {
        }

        public ModalPopUp(string Title, string Body, string Action = "")
        {
            this.Title = Title;
            this.Body = Body;
            this.Action = Action;
        }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Action { get; set; }


    }


}

