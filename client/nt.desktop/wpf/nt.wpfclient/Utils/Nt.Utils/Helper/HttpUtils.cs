﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Utils.Helper;

public static class HttpUtils
{
    public static string ServerUrl = "https://localhost:27017";

    public static string ValidateUserUrl = @"/api/User/ValidateUser";
    public static string UpdateUserUrl = @"/api/User/UpdateUser";
    public static string ChangePasswordUrl = @"/api/User/ChangePassword";

    public static string GetRecentReviewsUrl = @"/api/Review/GetRecentReviews";
}
