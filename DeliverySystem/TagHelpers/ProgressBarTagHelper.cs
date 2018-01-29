using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace DeliverySystem.TagHelpers
{
    [HtmlTargetElement("div", Attributes = ProgressValueAttributeName)]
    public class ProgressBarTagHelper : TagHelper
    {
        private const string ProgressValueAttributeName = "currentValue";
        private const string ProgressMinAttributeName = "minValue";
        private const string ProgressMaxAttributeName = "maxValue";
        private const string ProgressPercentageAttributeName = "percentageValue";

        [HtmlAttributeName(ProgressValueAttributeName)]
        public int CurrentValue { get; set; }

        [HtmlAttributeName(ProgressMinAttributeName)]
        public int ProgressMin { get; set; } = 0;

        [HtmlAttributeName(ProgressMaxAttributeName)]
        public int ProgressMax { get; set; } = 100;

        [HtmlAttributeName(ProgressPercentageAttributeName)]
        public int ProgressPercentage { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //Bootstrap Progress Bar
            string progressBarContent = $@"<div class='progress-bar' role='progressbar' aria-valuenow='{CurrentValue}' aria-valuemin='{ProgressMin}' aria-valuemax='{ProgressMax}' style='width:{ProgressPercentage}%;'> <span class='sr-only'>{ProgressPercentage}% Complete</span></div>";
            output.Content.AppendHtml(progressBarContent);

            string classValue;
            if (output.Attributes.ContainsName("class"))
            {
                classValue = string.Format("{0} {1}", output.Attributes["class"].Value, "progress");
            }
            else
            {
                classValue = "progress";
            }

            output.Attributes.SetAttribute("class", classValue);
        }
    }
}

