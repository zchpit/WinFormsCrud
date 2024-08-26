namespace WinFormsCrud.Helper
{
    public class ApiHelper
    {
        public static readonly string urlBase = "https://localhost:7033/api/";
        public static readonly string userControllerName = "User";
        public static readonly string userLoginMethodName = "Login";

        public static readonly string caseControllerName = "Case";
        public static readonly string caseGetUserCasesMethodName = "GetUserCases";
        public static readonly string caseCreateCaseMethodName = "CreateCase";
        public static readonly string caseUpdateCaseMethodName = "UpdateCase";
        public static readonly string caseDeleteCaseMethodName = "DeleteCase";

        public static readonly string reportControllerName = "Report";
        public static readonly string reportReportMethodName = "GetReport";
    }
}
