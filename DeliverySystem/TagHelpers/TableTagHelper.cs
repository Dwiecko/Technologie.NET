using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverySystem.Helpers
{
    public class TableTagHelper : TagHelper
    {
        public void Process(TagHelperContext context, TagHelperOutput output, IEnumerable<string> headers, IEnumerable<string> values)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }
            output.TagName = "Custom_Table";
            output.Content.SetHtmlContent("<table><thead><tr>");
            foreach(var item in headers)
            {
                output.Content.SetHtmlContent("<th>");
                Console.Write(item);
                output.Content.SetHtmlContent("</th>");
            }
            output.Content.SetHtmlContent("</tr></thead> <tbody><tr>");
            
            //TODO:
            foreach(var item in values)
            {
                output.Content.SetHtmlContent("<td>");
                Console.Write(item);
                output.Content.SetHtmlContent("</td>");
            }
            output.Content.SetHtmlContent("</tr></tbody> </table>");
        }
    }
}
