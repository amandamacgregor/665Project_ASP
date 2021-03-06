#pragma checksum "A:\public_html\HappyEarthASP\HappyEarthConsignment\Views\Shop\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ec6aff360d6329a1dbe54203245a7dfa87928ebb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shop_Details), @"mvc.1.0.view", @"/Views/Shop/Details.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "A:\public_html\HappyEarthASP\HappyEarthConsignment\Views\_ViewImports.cshtml"
using HappyEarthConsignment;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "A:\public_html\HappyEarthASP\HappyEarthConsignment\Views\_ViewImports.cshtml"
using HappyEarthConsignment.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ec6aff360d6329a1dbe54203245a7dfa87928ebb", @"/Views/Shop/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"be61cd6c90a464f3985fcc3c0fe180a6f148e0fb", @"/Views/_ViewImports.cshtml")]
    public class Views_Shop_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<HappyEarthConsignment.Models.Product>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AddToCart", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success btn-sm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Search", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 6 "A:\public_html\HappyEarthASP\HappyEarthConsignment\Views\Shop\Details.cshtml"
  
    ViewData["Title"] = "ProductDetails";

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"text-center\">\r\n");
            WriteLiteral("\r\n    <div class=\"card border-info mb-3\" style=\"max-width: 40rem; margin:0 auto\">\r\n        <div class=\"card-header\" style=\"font-weight: bold\">");
#nullable restore
#line 13 "A:\public_html\HappyEarthASP\HappyEarthConsignment\Views\Shop\Details.cshtml"
                                                      Write(Html.DisplayFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n        <div class=\"card-body text-dark\">\r\n");
            WriteLiteral("            <img");
            BeginWriteAttribute("src", " src=\"", 520, "\"", 538, 1);
#nullable restore
#line 16 "A:\public_html\HappyEarthASP\HappyEarthConsignment\Views\Shop\Details.cshtml"
WriteAttributeValue("", 526, Model.Photo, 526, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" height=\"100\" width=\"100\"");
            BeginWriteAttribute("alt", " alt=\"", 564, "\"", 581, 1);
#nullable restore
#line 16 "A:\public_html\HappyEarthASP\HappyEarthConsignment\Views\Shop\Details.cshtml"
WriteAttributeValue("", 570, Model.Name, 570, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n            <h6 class=\"card-title\" style=\"margin-top: 5px\">Price</h6>\r\n            <p class=\"card-text\">");
#nullable restore
#line 18 "A:\public_html\HappyEarthASP\HappyEarthConsignment\Views\Shop\Details.cshtml"
                            Write(Model.Price.ToString("c"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n            <h6 class=\"card-title\">Size</h6>\r\n            <p class=\"card-text\">");
#nullable restore
#line 20 "A:\public_html\HappyEarthASP\HappyEarthConsignment\Views\Shop\Details.cshtml"
                            Write(Html.DisplayFor(model => model.Size));

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n            <h6 class=\"card-title\">Condition</h6>\r\n            <p class=\"card-text\">");
#nullable restore
#line 22 "A:\public_html\HappyEarthASP\HappyEarthConsignment\Views\Shop\Details.cshtml"
                            Write(Html.DisplayFor(model => model.Condition));

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n            <h6 class=\"card-title\">Gender Category</h6>\r\n            <p class=\"card-text\">");
#nullable restore
#line 24 "A:\public_html\HappyEarthASP\HappyEarthConsignment\Views\Shop\Details.cshtml"
                            Write(Html.DisplayFor(model => model.Gender));

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n            <h6 class=\"card-title\">Brand</h6>\r\n            <p class=\"card-text\">");
#nullable restore
#line 26 "A:\public_html\HappyEarthASP\HappyEarthConsignment\Views\Shop\Details.cshtml"
                            Write(Html.DisplayFor(model => model.Brand));

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n            <h6 class=\"card-title\">");
#nullable restore
#line 27 "A:\public_html\HappyEarthASP\HappyEarthConsignment\Views\Shop\Details.cshtml"
                              Write(Html.DisplayNameFor(model => model.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h6>\r\n            <p class=\"card-text\">");
#nullable restore
#line 28 "A:\public_html\HappyEarthASP\HappyEarthConsignment\Views\Shop\Details.cshtml"
                            Write(Html.DisplayFor(model => model.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n            <h6 class=\"card-title\">Date Listed</h6>\r\n            <p class=\"card-text\">");
#nullable restore
#line 30 "A:\public_html\HappyEarthASP\HappyEarthConsignment\Views\Shop\Details.cshtml"
                            Write(Model.Created.ToString("d"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n            <h6 class=\"card-title\">Category</h6>\r\n            <p class=\"card-text\">");
#nullable restore
#line 32 "A:\public_html\HappyEarthASP\HappyEarthConsignment\Views\Shop\Details.cshtml"
                            Write(Html.DisplayFor(model => model.Category.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n        </div>\r\n\r\n\r\n");
            WriteLiteral("        <div id=\"detail-btns\" style=\"padding: 10px\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ec6aff360d6329a1dbe54203245a7dfa87928ebb9014", async() => {
                WriteLiteral("Add to Cart");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 95 "A:\public_html\HappyEarthASP\HappyEarthConsignment\Views\Shop\Details.cshtml"
                                        WriteLiteral(Model.ProductId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ec6aff360d6329a1dbe54203245a7dfa87928ebb11269", async() => {
                WriteLiteral("Back to Search");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<HappyEarthConsignment.Models.Product> Html { get; private set; }
    }
}
#pragma warning restore 1591
