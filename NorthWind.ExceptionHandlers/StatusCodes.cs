﻿namespace NorthWind.ExceptionHandlers
{
    public class StatusCodes
    {
        public const int Status400BadRequest = 400;
        public const string Status400BadRequestType = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
        public const int Status500InternalServerError = 500;
        public const string Status500InternalServerErrorType = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1";
    }
}