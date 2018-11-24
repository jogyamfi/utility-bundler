using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using utility_web_server.Models;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace utility_web_server.Controllers
{
    public class BundleController : Controller
    {
         private IConfiguration _configuration;
        public BundleController(IConfiguration configuration)
        {
             _configuration = configuration;
        }

        [HttpGet("/pedigree-editor-tool/css/style.min.css")]
        public IActionResult Css()
        {
            //return View();

            var cssFolderLocation = _configuration["BundleLocation:CSS"];

            string css = LoadCssFiles(cssFolderLocation);

            var stream = GenerateStreamFromString(css);

            return File(stream, "text/css");
        }

        [HttpGet("/pedigree-editor-tool/js/app.min.js")]
        public IActionResult Js()
        {
            var jsFolderLocation = _configuration["BundleLocation:JS"];

            string js = LoadJSFiles(jsFolderLocation);

            var stream = GenerateStreamFromString(js);

            return File(stream, "application/javascript");
        }

        [HttpGet("/pedigree-editor-tool/resources/{resource}")]
        public IActionResult Resources(string resource)
        {
                 var resourceFolderLocation = _configuration["BundleLocation:Resource"];
                
                 List<string> subpaths = new List<string>();

                 subpaths.Add(resourceFolderLocation);

                if (resource.IndexOf('/')>-1)
                {
                    var paths = resource.Split('/');


                   subpaths.AddRange(paths);

                    
                    
                }

                var finalPath = System.IO.Path.Combine(subpaths.ToArray());

                string returnType ="font/woff2; charset=UTF-8";

                if (subpaths.Any(u=>u.IndexOf(".png")>-1))
                {
                    returnType = "image/png";
                }
                else if (subpaths.Any(u=>u.IndexOf("woff2")>-1))
                {
                     returnType = "font/woff2; charset=UTF-8";
                }

                try
                {
                     byte[] fileData = System.IO.File.ReadAllBytes(finalPath);

                    var stream = GenerateStreamFromByte(fileData);

                     return File(stream, returnType);
                }
                catch (Exception ex){
                    
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    return NotFound();
                }

                 return NotFound();
        }

        private string LoadJSFiles(string location )
        {
            
            
            var files = System.IO.Directory.GetFiles(location,"*.js");

            //StringBuilder sb = new StringBuilder();

            return JsBundler.Bundle(location);
            
            // foreach (var file in files)
            // {
            //     if (file.Contains(".min")){
            //         continue;
            //     }
            //     string fileName = System.IO.Path.GetFileName(file);

            //     string fullFilePath = System.IO.Path.Combine(location,fileName);

            //     string fileData = System.IO.File.ReadAllText(fullFilePath);

            //     sb.AppendLine(String.Format("//{0} ", fileName));
            //     sb.AppendLine(" ; " + fileData);
            //     sb.AppendLine(String.Format("//{0} --end", fileName));
               

            // }

            // return sb.ToString();
        }

        private string LoadCssFiles(string location )
        {
            var files = System.IO.Directory.GetFiles(location,"*.css");

            StringBuilder sb = new StringBuilder();

            foreach (var file in files)
            {
                if (file.Contains(".min")){
                    continue;
                }
                string fileName = System.IO.Path.GetFileName(file);

                string fullFilePath = System.IO.Path.Combine(location,fileName);

                string fileData = System.IO.File.ReadAllText(fullFilePath);

                sb.AppendLine(String.Format("/* {0} */ ", fileName));
                sb.AppendLine(fileData);
                sb.AppendLine(String.Format("/* {0} end */", fileName));
               

            }

            return sb.ToString();
        }

        public static MemoryStream GenerateStreamFromString(string value)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
        }

         public static MemoryStream GenerateStreamFromByte(byte[] value)
        {
            return new MemoryStream(value);
        }


    }
}
