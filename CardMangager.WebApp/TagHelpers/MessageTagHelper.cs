using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardMangager.WebApp.TagHelpers
{
    [HtmlTargetElement("message")]
    public class MessageTagHelper : TagHelper
    {
        [HtmlAttributeName("type")]
        public string Type { get; set; } = "info";
        [HtmlAttributeName("content")]
        public string Content { get; set; }

        [HtmlAttributeName("statusCode")]
        public string Code { get; set; }

        [HtmlAttributeName("IdCard")]
        public string Id { get; set; }

        [HtmlAttributeName("CardNumber")]
        public string CardNumber { get; set; }

        [HtmlAttributeName("Money")]
        public string Money { get; set; }

        [HtmlAttributeName("CreatedDate")]
        public string CreatedDate { get; set; }

        [HtmlAttributeName("ExpiredDate")]
        public string ExpiredDate { get; set; }

        [HtmlAttributeName("PatientId")]
        public string PatientId { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            string type = this.Type.ToString().ToLower();
            string content = this.Content;
            string code = this.Code;
            string id = this.Id;
            string cardnumber = this.CardNumber;
            string money = this.Money;
            string createdDate = this.CreatedDate;
            string expiredDate = this.ExpiredDate;
            string patientId = this.PatientId;

            if (string.IsNullOrEmpty(content))
            {
                var elemContent = await output.GetChildContentAsync();
                content = elemContent.GetContent();
                this.Content = content;

            }
           
            string template = $@"<div class='alert alert-{type}'>
                                    <button type='button' class='close'
                                    data-dismiss='alert'>&times;</button>
                                    <span>{content}</span>
                                      <span>{id}</span>
                                      <span>{cardnumber}</span>
                                      <span>{money}</span>
                                      <span>{createdDate}</span>
                                      <span>{expiredDate}</span>
                                      <span>{patientId}</span>
                                      <span>{code}</span>
                                    </div>";
            output.TagName = string.Empty;
            output.Content.SetHtmlContent(template);
        }
    }

}
