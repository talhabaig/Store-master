using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaigMedicalStore.Common
{
    public class Enumeration
    {

        #region Enums
        public enum Module
        {
            SiteUserModule,
            QuestionModule,
            TestModule,
            CandidateTestModule,
            TenantModule,
            SubjectModule,
            ProgramModule,
            ProgramSubjectModule,
            TopicModule,
            RoleManagerModule,
            SOSModule,
            CandidateModule,
            CandidateSOSModule,
            SettingModule,
            ContentModule,
            CalendarModule,
            NotificationModule
        }

        public enum Action
        {
            Active = 1,
            View = 2,
            Create = 3,
            Update = 4,
            Delete = 5,
            TestAttempt = 6,
            ProgressChart = 7,
            Register = 8,
            ResetPassword = 9,
            ChangeSOS = 10,
            ChangeStatus = 11,
            Save = 12,
            ChatStatus = 13,
            Transcript = 14,
            TestPreview = 15,
            ViewActivity = 16,
            CreateActivity = 17,
            UpdateActivity = 18,
            CopySOS = 19,
            Manipulation = 20,
            Koogle = 21,
            Notifications = 22,
            EnrollProgramsView = 23,
            CandidateProgramsView = 24,
            SOS = 25,
            TestManagement = 26,
            Progress = 27,
            TeacherSport = 28,
            ProgramDetail = 29,
            EnrollProgram = 30,
            Dashboard = 31,
        }

        public enum KnowAboutKips
        {
            [Display(Name = "Newspaper")]
            Newspaper,
            [Display(Name = "TV")]
            TV,
            [Display(Name = "Kips Website")]
            KipsWebsite,
            [Display(Name = "Facebook")]
            Facebook
        }

        public enum LevelOfExam
        {
            [Display(Name = "9th")]
            Class9,
            [Display(Name = "10th")]
            Class10,
            [Display(Name = "1st Year")]
            InterPartOne,
            [Display(Name = "2nd Year")]
            InterPartTwo,
            [Display(Name = "O Levels")]
            ASLevel,
            [Display(Name = "AS Levels")]
            ALevel,
            [Display(Name = "A Levels")]
            OLevel,
            [Display(Name = "DAE")]
            DAE

        }


        public enum EmployementType
        {
            [Display(Name = "Private")]
            Private,
            [Display(Name = "Government")]
            Government
        }


        public enum GeneralSubjects
        {
            MATH = 4,
            BIOLOGY = 5,
            CHEMISTRY = 6,
            PHYSICS = 7,
            COMPUTER = 8,
            ENGLISH = 9,
            INTELLIGENCE = 10,
            GENERALMATH = 11
        }

        public enum TestCategory
        {
            ECAT = 1,
            MDCAT = 2,
            LMDCAT_UHS = 3,
            NUMS = 4,
            LMDCAT_NUMS = 5,
            NUST = 6,
            FUNG = 7,
            AKNUMDCAT = 8,
            ENGINEERING_MOCK = 9
        }

        public enum EntityStatus
        {
            InActive = 0,
            Active,
            Delete
        }


        public enum TestType
        {
            CAT = 1,
            CBT,
            PBT
        }

        public enum ProgramType
        {
            All = 1,
            Active = 2,
            Archive = 3
        }

        public enum AnswerType
        {
            Single = 1,
            Multiple = 2,
        }

        public enum TestQuestionType
        {
            [Display(Name = "Simple")]
            Simple = 1,
            [Display(Name = "Numerical")]
            Numerical = 2,
            [Display(Name = "Diagrammatical")]
            Diagrammatical = 3,
            [Display(Name = "Numerical + Diagrammatical")]
            NumericalDiagrammatical = 4
        }

        public enum QuestionFor
        {
            All = 1,
            Student,
            Admin
        }

        public enum LevelOfDifficulty
        {
            Easy = 1,
            Medium,
            Hard
        }

        public enum QuestionType
        {
            Used = 1,
            UnUsed,
            Both
        }

        public enum QuestionConfiguration
        {
            General = 1,
            EMH
        }

        public enum QuestionSet
        {
            Same = 1,
            Different
        }

        public enum PaymentPlan
        {
            [Display(Name = "Full Feature")]
            FullFeature = 1,
            [Display(Name = "Custom Feature")]
            CustomFeature = 2
        }
        public enum ContentTypes
        {
            Video = 1,
            Reading = 2
        }

        public enum CandidateTestStatus
        {
            Attempted = 0,
            UnAttempted
        }

        public enum NotificationTypes
        {
            INBOX = 0,
            TRASH,
            DELETE
        }

        #region APPLICATION MESSAGES


        #endregion

        #endregion

        #region Struct



        //public struct Session
        //{
        //    public const string RolePermissions = "RolePermissions";
        //}

        public struct UserRole
        {
            public const string SuperAdmin = "superadmin";
            public const string Admin = "admin";
            public const string Student = "student";
            public const string Candidate = "candidate";
            public const string DataEntry = "dataentry";
            public const string Teacher = "teacher";
        }

        public struct ProfileStatus
        {
            public const string PersonalInformation = "personal Information not completed";
            public const string GuardianInformation = "parent/guardian Information not completed";
            public const string AcademicInformation = "academic Information not completed";
            public const string ProfileCompleted = "profile is complete";
            public const string Complete = "COMPLETED";
            public const string Pending = "PENDING";
        }

        public struct UploadFolder
        {
            public const string Question = "question";
            public const string Student = "student";
            public const string Candidate = "candidate";
            public const string Assignment = "assignment";
        }

        public struct FolderName
        {
            public const string Download = "download";
            public const string Upload = "upload";
            public const string Assignment = "assignment";
            public const string AWSFiles = "AWSFiles";
        }

        //public struct TestType
        //{
        //    public const string Subject = "Subject";
        //    public const string AllSubject = "All Subject";
        //}

        public struct ParentOrGuard
        {
            public const string Parent = "Parent";
            public const string Guardian = "Guardian";
        }

        public struct ManipulateAction
        {
            public const string Move = "Move";
            public const string Copy = "Copy";
        }

        public struct EditorType
        {
            public const string TextEditor = "TextEditor";
            public const string EquationEditor = "EquationEditor";
        }

        public struct ImagePath
        {
            public const string NoImage = "/Content/images/no-picture.png";
        }

        public struct SubjectNames
        {
            public const string Chemistry = "Chemistry";
            public const string Biology = "Biology";
            public const string English = "English";
            public const string Math = "Mathematics";
            public const string Computer = "Computer Science";
            public const string Physics = "Physics";
            public const string Finals = "Finals";
            public const string Intelligence = "Intelligence";
            public const string GeneralMath = "General Mathematics";
        }

        public struct ResultType
        {
            public const string Result = "result";
            public const string Detail = "detail";
        }

        public struct ModuleAction
        {
            public const string IsView = "IsView";
            public const string IsAdd = "IsAdd";
            public const string IsUpdate = "IsUpdate";
            public const string IsDelete = "IsDelete";
        }


        public struct MessageType
        {
            public const string Error = "Error";
            public const string Info = "Info";
            public const string Warning = "Warning";
            public const string Success = "Success";
        }

        public struct EmailCode
        {
            public const string StudentSignup = "student.signup";
            public const string ForgetPassword = "forgetpassword";
            public const string ConfirmationEmail = "confirmationemail";
            public const string ContactUs = "contactus";
            public const string Welcome = "welcome.student";
            public const string PhoneConfirmation = "phoneconfirmation";
        }

        public struct Major
        {
            public const string Medical = "Medical";
            public const string Engineering = "Engineering";
            public const string ComputerScience = "Computer Science";
        }

        public struct TestCategoryName
        {
            public const string ECAT = "ecat";
            public const string MDCAT = "mcat";
            public const string LMDCATUHS = "lmcatuhs";
            public const string NUMS = "nums";
            public const string LMCATNUMS = "lmcatnums";
            public const string NUST = "nust";
            public const string FUNG = "fung";
        }

        public struct ControllerName
        {
            public const string Subject = "subject";
            public const string Program = "program";
            public const string Account = "account";
            public const string Home = "home";
            public const string Question = "question";
            public const string Test = "test";
            public const string Manage = "Manage";
            public const string Student = "student";
            public const string SchemeOfStudy = "schemeofstudy";
            public const string RoleManager = "rolemanager";
            public const string Setting = "setting";
            public const string Content = "content";
            public const string Calendar = "calendar";
            public const string Candidate = "candidate";
            public const string Notification = "notification";
            public const string Tenant = "Tenant";
        }

        public struct ActionName
        {
            public const string Index = "index";
            public const string Create = "create";
            public const string Edit = "edit";
            public const string SessionCopy = "sessioncopy";
            public const string Profile = "profile";
            public const string Registration = "registration";
            public const string About = "about";
            public const string Contact = "contact";
            public const string SchemeOfStudy = "schemeofstudy";
            public const string SaveRole = "saverole";
            public const string ProgressChart = "progresschart";
            public const string TeacherSupport = "teachersupport";
            public const string Register = "register";
            public const string Save = "save";
            public const string Manipulate = "Manipulate";
            public const string Notification = "Notification";
            public const string IndexContent = "IndexContent";
            public const string ProgramList = "ProgramList";
            public const string MyPrograms = "MyPrograms";
            public const string Koogle = "Koogle";
            public const string ChangePassword = "ChangePassword";
            public const string VerifyCandidateRegistration = "VerifyCandidateRegistration";
        }

        public struct Captcha
        {
            public const string ErrorMessage = "Answer is not correct";
        }

        public struct AWSFilesName
        {
            public const string CannedPolicy = "CannedPolicy.txt";
            public const string CustomPolicy = "CustomPolicy.txt";
            public const string PrivateKey = "PrivateKey.xml";
        }

        public struct OutReachSmsResponse
        {
            public const string InvalidCredentials = "200";
            public const string CustomerAccountExpired = "203";
            public const string InvalidMasking = "204";
            public const string InvalidDestinationNumber = "206";
            public const string RequiredParameterMissing = "208";
            public const string AccountBlocked = "209";
            public const string InvalidLanguage = "210";
            public const string InsufficientCredits = "211";
            public const string Successful = "100";
            public const string UnSuccessful = "101";
            public const string MessageSentSuccess = "300";
        }


        public struct ERPAPIResource
        {
            public const string Company = "8hqd3w";
            public const string Institute = "tw3rbc";
            public const string Region = "ge6xqw";
            public const string Zone = "ryox0k";
            public const string Campus = "fdql0y";
            public const string CampusClass = "m9qp46";
            public const string Session = "2tcr5v";
            public const string Student = "p91h2b";
        }


        public struct ERPAPIMethod
        {
            public const string Company = "companies/1bb";
            public const string Institute = "institutes/1bb";
            public const string Region = "regions/1bb";
            public const string Zone = "zones/1bb";
            public const string Campus = "campuses/1bb";
            public const string CampusClass = "campus-classes/1bb";
            public const string Session = "sessions/1bb";
            public const string Student = "students/1bb";
        }


        public struct AmazonBucketFolder
        {
            public const string VideoLectures = "VideoLectures";
            public const string Assignments = "Assignments";
            public const string Readings = "Readings";
        }

        public struct TempDataKey
        {
            public const string ProgramBuyModel = "ProgramBuyModel";
        }

        #endregion

    }
}