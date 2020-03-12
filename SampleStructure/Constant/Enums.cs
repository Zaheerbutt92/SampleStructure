using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constant
{
    public class LanguageCulture
    {
        public const string UAEArabicCulture = "ar-AE";
        public const string USEnglishCulture = "en-US";
    }


    public class LanguageSelected
    {
        public const string Arabic = "ar";
        public const string English = "en";
    }


    public static class LocalizedColumns
    {
        public static Dictionary<string, string> Columns = new Dictionary<string, string>()
        {
            { "Name_En","Name_Ar"}
        };
    }

    public class LegislationSearchColumns
    {
        public const string ID = "ID";
        public const string SearchText = "searchText";
    }

    public static class UnupdatedColumns
    {
        public static List<string> UnchangedColumns
        {
            get
            { return new List<string> { "Created_By", "Created_Date" }; }
        }

    }

    public enum HelpingWordTypes
    {
        Greeting = 1,
        Highness = 2,
    }

    public enum RegisterUserTypes
    {
        Public = 1,
        Internal = 2,
        Other = 3,
        SuperAdmin = 4,
        SystemAdmin = 5
    }

    public enum LegalConsultationUserViewType
    {
        External = 0,
        Internal = 1,
        Researcher = 2,
        SectionHead = 3
    }

    public enum ConsultationStatus
    {
        Draft = 1,
        Rejected = 2,
        Closed = 3,
        Inprogress = 4,
        //New = 5
    }

    public enum SourceSite
    {
        LC_System = 1,
        Platform_System = 2,
        Complains_System = 3,
    }

    public enum ConsultationNature
    {
        Confidential = 1,
        Public = 2
    }

    public enum ConsultationCase
    {
        Urgent = 1,
        Normal = 2
    }

    public enum Actions
    {
        Issue_Legal_Opinion = 1,
        Submit_Legal_Opinion = 2,
        Send_For_Feedback = 3,
        Send_To_Consultant = 4,
        Send_To_Head = 5,
        Convert_To_Final_Statement = 6,
        Reject_Legal_Opinion = 7,
        Approve_Legal_Opinion = 8,
        Edit_Statement = 9,
        Submit_Legal_Statement = 10,
        Add_Request_Missing_Info = 11,
        Request_Missing_Info = 12,
        Create_Consultation = 13,
        Submit_consultation = 14,
        Assign_Consultation = 15,
        Reply_Feedback = 16,
    }

    public enum TaskStatus
    {
        Overdue = 1,
        Closed = 2,
        InProgress = 3,
    }

    public enum RequestorType
    {
        Internal = 1,
        External = 2,
    }

    public enum TaskType
    {
        Feedback = 1,
        LegalOpinion = 2,
    }

    public enum Screen
    {
        ConsultationList = 85,
        ConsultationRequest = 86,
        Dashboard = 87,
        TaskDetails = 88,
        ConsultationDetails = 89,
    }

    public enum LCRoles
    {
        //Dev
        LegalResearcher = 20112,
        LegalConsultant = 20113,
        LCSectionHead = 20115,
        Requester = 20116,

        //QA
        //LegalResearcher = 20117,
        //LegalConsultant = 20118,
        //LCSectionHead = 20119,
        //Requester = 20142,

        //Stagging
        //LegalResearcher = 20123,
        //LegalConsultant = 20124,
        //LCSectionHead = 20125,
        //Requester = 20142,
    }

    public enum ConsultationListScreenActions
    {
        //Dev
        CreateConsultation = 10427,
        RejectConsultation = 10428,
        EditConsultationRequest = 10453,
        DeleteConsultationRequest = 10454,
        ViewConsultationList = 10456,

        //QA
        //CreateConsultation = 10422,
        //RejectConsultation = 10423,
        //EditConsultationRequest = 10441,
        //DeleteConsultationRequest = 10442,
        //ViewConsultationList = 10444,

        //Stagging
        //CreateConsultation = 10436,
        //RejectConsultation = 10438,
        //EditConsultationRequest = 10466,
        //DeleteConsultationRequest = 10467,
        //ViewConsultationList = 10469,
    }

    
    
    
}
