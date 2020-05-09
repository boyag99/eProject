using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace eProject.App.Helpers
{
    public static class GeneralHelpers
    {
        public static async Task<string> UploadedFile(this IFormFile fromFile, string folderPath)
        {
            string uniqueFileName = null;

            if (fromFile != null)
            {
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Regex.Replace(fromFile.FileName.Trim(), @"[^a-zA-Z0-9-_.]", ""); // create file name
                string webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"); // get path wwwroot
                Directory.CreateDirectory(Path.Combine(webRootPath, folderPath)); // automatically create directory if none exists
                using (var fileStream = new FileStream(Path.Combine(webRootPath, folderPath, uniqueFileName), FileMode.Create)) // write data to file
                {
                    await fromFile.CopyToAsync(fileStream);
                    fileStream.Close();
                }
            }
            return uniqueFileName;
        }

        public static void DeleteFile(this string folderPath, string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/" + folderPath, fileName); // get file path

            if (File.Exists(filePath)) // check if exist
            {
                File.Delete(filePath); // then delete file path
            }
        }

        public static string IsSelected(this IHtmlHelper htmlHelper, string controllers, string actions, string cssClass = "active")
        {
            string currentAction = htmlHelper.ViewContext.RouteData.Values["action"] as string;
            string currentController = htmlHelper.ViewContext.RouteData.Values["controller"] as string;

            IEnumerable<string> acceptedActions = (actions ?? currentAction).Split(',');
            IEnumerable<string> acceptedControllers = (controllers ?? currentController).Split(',');

            return acceptedActions.Contains(currentAction) && acceptedControllers.Contains(currentController) ?
                cssClass : String.Empty;
        }
    }
}
