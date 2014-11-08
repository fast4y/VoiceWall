﻿namespace VoiceWall.Web.Infrastructure.Filters
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    class ValidateSoundFileAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            // 10MB
            const int AllowedMaxSize = 1024 * 1024 * 10;

            var fileAsHttpPostedFileBase = value as HttpPostedFileBase;

            if (fileAsHttpPostedFileBase == null)
            {
                ErrorMessage = "Please upload a file";
                return false;
            }

            if (fileAsHttpPostedFileBase.ContentLength > AllowedMaxSize)
            {
                ErrorMessage = string.Format("File size can not exceed {0}", AllowedMaxSize);
                return false;
            }

            var allowedMimeTypes = new List<string>() { "audio/mpeg" };

            if (allowedMimeTypes.Contains(fileAsHttpPostedFileBase.ContentType))
            {
                ErrorMessage = "File type not supported";
                return false;
            }

            return true;
        }
    }
}