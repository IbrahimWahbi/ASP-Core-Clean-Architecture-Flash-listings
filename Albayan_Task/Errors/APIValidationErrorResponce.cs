﻿namespace Albayan_Task.Errors
{
    public class APIValidationErrorResponce : APIResponce
    {
        public APIValidationErrorResponce() : base(400)
        {

        }
        public IEnumerable<string> Errors { get; set; }

    }
}
