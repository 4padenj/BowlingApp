using BowlingApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingApp.Infrastructure
{
    //HtmlTargetELement and TagHelper both come from AspNetCore.Razor.Taghelpers
    //HtmlTargetElement allows us to create this tag for a specific HTML element
    [HtmlTargetElement("div", Attributes = "page-info")]
    public class PagingTagHelper : TagHelper
    {
        private IUrlHelperFactory urlInfo;
        // Constructor initializes the urlInfo
        public PagingTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            urlInfo = urlHelperFactory;
        }
        // Creates an instance of type paging info (ViewModel)
        // Recieves the data from the 'page-info' tag in the div on the view
        public PagingInfo PageInfo {get; set;}

        // Build the view Context for the IUrlHelper Below
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        //Create Dictionary
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> KeyValuePairs { get; set; } = new Dictionary<string, object>();
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelp = urlInfo.GetUrlHelper(ViewContext);

            //Builds the Outer Div
            TagBuilder finishedTag = new TagBuilder("div");
            // ul and li tags are for the front end styling of pagination
            TagBuilder ulTag = new TagBuilder("ul");
            ulTag.Attributes["class"] = "pagination";

            for(int i = 1; i <= PageInfo.NumPages; i++)
            {
                // Builds the li tags
                TagBuilder liTag = new TagBuilder("li");
                liTag.Attributes["class"] = "page-item";
                //Builds the a tags that make up the outer tag
                TagBuilder individualTag = new TagBuilder("a");

                KeyValuePairs["pageNum"] = i;
                individualTag.Attributes["href"] = urlHelp.Action("Index", KeyValuePairs);
                individualTag.Attributes["class"] = "page-link";
                individualTag.InnerHtml.AppendHtml((i).ToString());

                //After building the a tag, we append it to the li tag
                liTag.InnerHtml.AppendHtml(individualTag);
                //Now add the li Tag to the Ul tag
                ulTag.InnerHtml.AppendHtml(liTag);
                
            }
            // Now add the ul (with all of the li and a tags
            finishedTag.InnerHtml.AppendHtml(ulTag);
            // After building the div full of the a tags, we now insert that in the div with the page-info taghelper
            output.Content.AppendHtml(finishedTag.InnerHtml);
        }
    }
}
