/// <reference path="../../bootstrap.js" />
BMS.AppConstants = {
    SessionTokenKey: "sessionToken",
    MessageType: {
        Error: "Error",
        Info: "Info",
        Warning: "Warning",
        Success: "Success"
    },
    TestType: {
        CAT: 1,
        CBT: 2,
        PBT: 3
    },
    QuestionType: {
        Used: 1,
        Unused: 2,
        Both: 3
    },
    AnswerType: {
        Single: 1,
        Multiple: 2,
    },
    QuestionConfiguration: {
        General: 1,
        EMHBase: 2
    },
    QuestionTestType: {
        Simple: 1,
        Numerical: 2,
        Diagrammatical: 3,
        NumericalDiagrammatical: 4
    },
    EmploymentType: {
        Employeed: "Employeed",
        Business: "Business"
    },
    ParentOrGuard: {
        Parent: "Parent",
        Guardian: "Guardian"
    },
    PaymentPlan: {
        FullFeature: "1",
        CustomFeature: "2"
    },

    EditorType: {
        TextEditor: "TextEditor",
        EquationEditor: "EquationEditor"
    },

    ManipulateAction: {
        Move: "Move",
        Copy: "Copy"
    },

    TextEditorId: {
        QuestionBody: "QuestionBody",
        Explanation: "Explanation",
        OptionA: "OptionA",
        OptionB: "OptionB",
        OptionC: "OptionC",
        OptionD: "OptionD",
        OptionE: "OptionE",
        Hint: "Hint",
    },
    CandidateTestContainerType: {
        TestAttempt: "testattempt",
        TestResult: "testresult",
        TestDetail: "testdetail"
    },
    ContentType: {
        Video: "1",
        Reading: "2",
    },
    AmazonBucketFolder: {
        VideoLectures: "VideoLectures",
        Assignments: "Assignments",
        Readings: "Readings",
    },
    SubjectNames: {
        Chemistry: "CHEMISTRY",
        Biology: "BIOLOGY",
        English: "ENGLISH",
        Math: "MATHEMATICS",
        Computer: "COMPUTER SCIENCE",
        Physics: "PHYSICS",
        Finals: "FINALS",
        Intelligence: "INTELLIGENCE",
        GeneralMath: "GENERAL MATHEMATICS"
    },
    RegistrationType: {
        Registration: "registration",
        Promo: "promo"
    },
    URL: {
        Action: {
            Invoice: {
                saveData: BMS.AppVar.AppHost + "/Invoice/saveData",
                PrintInvoice: BMS.AppVar.AppHost + "/Invoice/PrintInvoice",
            },
            AccessDenied: BMS.AppVar.AppHost + "/Error/AccessDenied",
            EquationEditorPartialView: BMS.AppVar.AppHost + "/Question/GetEquationEditorPartialView",
            QuestionPreviewPartialView: BMS.AppVar.AppHost + "/Question/QuestionPreviewPartialView",
            Distributor: {
                ChangeDistributorStatus: BMS.AppVar.AppHost + "/Distributor/ChangeDistributorStatus",
            },
            Item: {
                ChangeItemStatus: BMS.AppVar.AppHost + "/Item/ChangeItemStatus",
            },
            Manufacturer: {
                ChangeManufacturerStatus: BMS.AppVar.AppHost + "/Manufacturer/ChangeManufacturerStatus",
            },
            Order: {
                AddItemToOrder: BMS.AppVar.AppHost + "/Order/AddItemToOrder",
            }  ,
            Account: {
                SetPassword: BMS.AppVar.AppHost + "/Account/SetPassword",
                ChangeUserStatus: BMS.AppVar.AppHost + "/Account/ChangeUserStatus",
            },
            Common: {
                GetVideoLecturePartialView: BMS.AppVar.AppHost + "/Common/GetVideoLecturePartialView",
                GetProgramSubjectList: BMS.AppVar.AppHost + "/Common/GetProgramSubjectList",
                GetDistributorManufacturerList: BMS.AppVar.AppHost + "/Common/GetDistributorManufacturerList",

            },
            Subject: {
                PopulateTopicTreeView: BMS.AppVar.AppHost + "/Subject/GetTopicsTreeDatasource",
                CreateTopicPartialView: BMS.AppVar.AppHost + "/Subject/CreateTopicPartialView",
                EditTopicPartialView: BMS.AppVar.AppHost + "/Subject/EditTopicPartialView",
                ManageTopicTreePartialView: BMS.AppVar.AppHost + "/Subject/ManageTopicTreePartialView",
                CreateSubjectPartialView: BMS.AppVar.AppHost + "/Subject/CreateSubjectPartialView",
                EditSubjectPartialView: BMS.AppVar.AppHost + "/Subject/EditSubjectPartialView",
                SaveSubjectDetail: BMS.AppVar.AppHost + "/Subject/SaveSubject",
                DeleteTopic: BMS.AppVar.AppHost + "/Subject/DeleteTopic",
                ChangeTopicParent: BMS.AppVar.AppHost + "/Subject/ChangeTopicParent",
                ChangeSubjectStatus: BMS.AppVar.AppHost + "/Subject/ChangeSubjectStatus"
            },
            Program: {
                ChangeProgramStatus: BMS.AppVar.AppHost + "/Program/ChangeProgramStatus",
                ChangeProgramSubjectStatus: BMS.AppVar.AppHost + "/Program/ChangeProgramSubjectStatus",
                ChangeProgramSubjectChatStatus: BMS.AppVar.AppHost + "/Program/ChangeProgramSubjectChatStatus",
                AddEditProgramSubjectPartialView: BMS.AppVar.AppHost + "/Program/GetAddEditProgramSubjectPartialView",
                SaveProgramSubjectDetail: BMS.AppVar.AppHost + "/Program/SaveProgramSubjectDetail"
            },
            Content: {
                ChangeContentStatus: BMS.AppVar.AppHost + "/Content/ChangeContentStatus",
                AddEditContent: BMS.AppVar.AppHost + "/Content/CreateContent",
                SaveContent: BMS.AppVar.AppHost + "/Content/SaveContent",
                UploadContentFile: BMS.AppVar.AppHost + "/File/UploadToAmazonS3",
                AddEditTranscriptContent: BMS.AppVar.AppHost + "/Content/CreateTranscriptContent",
                SaveTranscriptContent: BMS.AppVar.AppHost + "/Content/SaveTranscriptContent",
                GetContentVideoPartialView: BMS.AppVar.AppHost + "/Content/GetContentVideoPartialView",
                PopulateTopicContentList: BMS.AppVar.AppHost + "/Content/PopulateTopicContentList",
                GetContentVideo: BMS.AppVar.AppHost + "/Content/GetContentVideo",
                GetContentReadingPartialView: BMS.AppVar.AppHost + "/Content/GetContentReadingPartialView",
                UpdateContentStatusMarkRead: BMS.AppVar.AppHost + "/Content/UpdateContentStatusMarkRead"
            },
            Calendar: {

                ChangeCalendarStatus: BMS.AppVar.AppHost + "/Calendar/ChangeCalendarStatus"
            },
           
            Tenant: {
                ChangeTenantStatus: BMS.AppVar.AppHost + "/Tenant/ChangeTenantStatus"
            },
            Candidate: {
                Koogle: BMS.AppVar.AppHost + "/Candidate/Koogle",
                GetEnrolledProgramPartialView: BMS.AppVar.AppHost + "/Candidate/GetEnrolledProgramPartialView",
                GetProgramRegistrationPartialView: BMS.AppVar.AppHost + "/Candidate/GetProgramRegistrationPartialView",
                GetProgramBuyPartialView: BMS.AppVar.AppHost + "/Candidate/GetProgramBuyPartialView",
                DeleteNotifications: BMS.AppVar.AppHost + "/Candidate/DeleteNotifications",
                ShowNotificationDetail: BMS.AppVar.AppHost + "/Candidate/ShowNotificationDetail",
                GetQuestionCount: BMS.AppVar.AppHost + "/Candidate/GetQuestionCount"
            },
            Download: {
                DownloadFromAmazonS3: BMS.AppVar.AppHost + "/File/DownloadFromAmazonS3"
            }
        },
        API: {
            CandidateTestService: BMS.AppVar.AppHost + "/api/CandidateTestService"
        },
        Script: {
            //MathJax: "http://cdn.mathjax.org/mathjax/latest/MathJax.js?config=TeX-AMS-MML_HTMLorMML",
            JQuery: BMS.AppVar.AppHost + "/Scripts/jquery-1.10.2.min.js",
            Bootstrap: BMS.AppVar.AppHost + "/Scripts/bootstrap.js",
            JqueryHotKeys: BMS.AppVar.AppHost + "/Scripts/jquery.hotkeys.min.js",
            Mousetrap: BMS.AppVar.AppHost + "/Scripts/mousetrap.min.js",
            MathJax: BMS.AppVar.AppHost + "/Scripts/MathJax/MathJax.js?config=TeX-AMS-MML_HTMLorMML",
            WirisEditor: "http://www.wiris.net/demo/editor/editor",
            Application: {
                Question: {
                    kendoEditorIframe: BMS.AppVar.AppHost + "/Scripts/Application/Question/QuestionEditorIframeManager.js"
                }
            }
        },
        Style: {
            kendoEditorIframeStyle: BMS.AppVar.AppHost + "/Content/kendoEditorIframeStyle.css",
        },
    },
    HttpStatusCode: {
        Ok: 'OK',
        Forbidden: 'Forbidden',
        InternalServerError: 'Internal Server Error'
    },
    HttpStatusCodeMessage: {
        Forbidden: 'Sorry! Access is denied. You have no permission to perform this action.'
    },
    ImagePath: {
        AjaxLoader: BMS.AppVar.AppHost + "/Content/images/ajax-loader.gif",
        NoImage: BMS.AppVar.AppHost + "/Content/images/noimage.png"
    },
    TimeIntervals: {

    },
    MouseCurrentPosition: {
        X: undefined,
        Y: undefined
    },
    ActiveStatus: {
        Active: "Active",
        InActive: "InActive"
    },
    EntityStatus: {
        All: -1,
        InActive: 0,
        Active: 1,
        Delete: 2
    },
    ActionType: {
        Add: "Add",
        Edit: "Edit",
        Delete: "Delete"
    },
    Commands: {
        Add: "Add",
        Edit: "Edit",
        Delete: "Delete",
        Question: "Question"
    },
    UserRole: {
        SuperAdmin: "superadmin",
        Admin: "admin",
        Student: "student",
        DataEntry: "dataentry",
        Teacher: "teacher",
    },
}