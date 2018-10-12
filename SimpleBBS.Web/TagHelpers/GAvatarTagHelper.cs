using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBBS.Web.TagHelpers
{
    [HtmlTargetElement("GAvatar")]
    public class GAvatarTagHelper : TagHelper
    {
        public string Email { get; set; }

        public string DefaultImage { get; set; } = "mm";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            output.Attributes.SetAttribute("src", AvatarHelper.GetSrc(Email, DefaultImage));
            output.Attributes.SetAttribute("alt", Email);

        }

        public static class AvatarHelper
        {
            const string defaultHash = "00000000000000000000000000000000";
            const string gravatarUrl = "https://www.gravatar.com/avatar/{0}?d={1}";


            public static string GetSrc(string email, string defaultImage = "mm")
            {
                if (string.IsNullOrWhiteSpace(email))
                    return string.Format(gravatarUrl, defaultHash, defaultImage);

                var hash = GetMd5String(email.Trim().ToLowerInvariant());

                return string.Format(gravatarUrl, hash, defaultImage);
            }


            static string GetMd5String(string source)
            {
                var md5 = MD5.Create();

                var data = md5.ComputeHash(Encoding.UTF8.GetBytes(source));

                var result = string.Concat(data.Select(t => t.ToString("x2").ToLowerInvariant()));

                return result;
            }
        }
    }
}
