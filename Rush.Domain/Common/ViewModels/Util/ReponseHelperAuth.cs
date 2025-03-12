﻿using Rush.Domain.Common.ViewModels.Auth;

namespace Rush.Domain.Common.ViewModels.Util
{
    public class ResponseHelperAuth
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public TokenResponse Token { get; set; }
        public User User { get; set; } = new User();
    }
}
