using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MusicManagementSystem.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {
        private const string EmailDomain = "gmail.com";
        public string MailTo { get; set; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";

            var content = await output.GetChildContentAsync();
            var target = content.GetContent();
            var adress = MailTo + "@" + EmailDomain;
            output.Attributes.SetAttribute("href", "mailto:" + adress);
            output.Content.SetContent(target);
        }
    }
}
