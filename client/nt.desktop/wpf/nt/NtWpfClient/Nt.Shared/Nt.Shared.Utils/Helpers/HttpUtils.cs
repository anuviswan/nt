using System;
using System.Collections.Generic;
using System.Text;

namespace Nt.Shared.Utils.Helpers
{
    public static class HttpUtils
    {
        public static string ServerUrl = "https://localhost:44353";

        public static string ValidateUserUrl = @"/api/User/ValidateUser";
        public static string UpdateUserUrl = @"/api/User/UpdateUser";
        public static string ChangePasswordUrl = @"/api/User/ChangePassword";

        public static string GetRecentReviewsUrl = @"/api/Review/GetRecentReviews";
    }
}
