#pragma checksum "/Users/alistair/Projects/COMP2003-2020-W/Admin Interface/AdminInterface/AdminInterface/Views/Ratings/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ae109fb2605d9f1e3aff2e3262b23e1ad2ae32e4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Ratings_Index), @"mvc.1.0.view", @"/Views/Ratings/Index.cshtml")]
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
#line 1 "/Users/alistair/Projects/COMP2003-2020-W/Admin Interface/AdminInterface/AdminInterface/Views/_ViewImports.cshtml"
using AdminInterface;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/alistair/Projects/COMP2003-2020-W/Admin Interface/AdminInterface/AdminInterface/Views/_ViewImports.cshtml"
using AdminInterface.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ae109fb2605d9f1e3aff2e3262b23e1ad2ae32e4", @"/Views/Ratings/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c23829e1176a46407966d16cbaed3a1dfa03c459", @"/Views/_ViewImports.cshtml")]
    public class Views_Ratings_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<AdminInterface.Models.Rating>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("\n");
#nullable restore
#line 3 "/Users/alistair/Projects/COMP2003-2020-W/Admin Interface/AdminInterface/AdminInterface/Views/Ratings/Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<h1>Index</h1>\n\n<p>\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ae109fb2605d9f1e3aff2e3262b23e1ad2ae32e43741", async() => {
                WriteLiteral("Create New");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n</p>\n<table class=\"table\">\n    <thead>\n        <tr>\n            <th>\n                ");
#nullable restore
#line 16 "/Users/alistair/Projects/COMP2003-2020-W/Admin Interface/AdminInterface/AdminInterface/Views/Ratings/Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Rating1));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th>\n                ");
#nullable restore
#line 19 "/Users/alistair/Projects/COMP2003-2020-W/Admin Interface/AdminInterface/AdminInterface/Views/Ratings/Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th>\n                ");
#nullable restore
#line 22 "/Users/alistair/Projects/COMP2003-2020-W/Admin Interface/AdminInterface/AdminInterface/Views/Ratings/Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Customer));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th>\n                ");
#nullable restore
#line 25 "/Users/alistair/Projects/COMP2003-2020-W/Admin Interface/AdminInterface/AdminInterface/Views/Ratings/Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Product));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th></th>\n        </tr>\n    </thead>\n    <tbody>\n");
#nullable restore
#line 31 "/Users/alistair/Projects/COMP2003-2020-W/Admin Interface/AdminInterface/AdminInterface/Views/Ratings/Index.cshtml"
 foreach (var item in Model) {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\n            <td>\n                ");
#nullable restore
#line 34 "/Users/alistair/Projects/COMP2003-2020-W/Admin Interface/AdminInterface/AdminInterface/Views/Ratings/Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Rating1));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </td>\n            <td>\n                ");
#nullable restore
#line 37 "/Users/alistair/Projects/COMP2003-2020-W/Admin Interface/AdminInterface/AdminInterface/Views/Ratings/Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </td>\n            <td>\n                ");
#nullable restore
#line 40 "/Users/alistair/Projects/COMP2003-2020-W/Admin Interface/AdminInterface/AdminInterface/Views/Ratings/Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Customer.Address));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </td>\n            <td>\n                ");
#nullable restore
#line 43 "/Users/alistair/Projects/COMP2003-2020-W/Admin Interface/AdminInterface/AdminInterface/Views/Ratings/Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Product.ProductName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </td>\n            <td>\n                ");
#nullable restore
#line 46 "/Users/alistair/Projects/COMP2003-2020-W/Admin Interface/AdminInterface/AdminInterface/Views/Ratings/Index.cshtml"
           Write(Html.ActionLink("Edit", "Edit", new { /* id=item.PrimaryKey */ }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\n                ");
#nullable restore
#line 47 "/Users/alistair/Projects/COMP2003-2020-W/Admin Interface/AdminInterface/AdminInterface/Views/Ratings/Index.cshtml"
           Write(Html.ActionLink("Details", "Details", new { /* id=item.PrimaryKey */ }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\n                ");
#nullable restore
#line 48 "/Users/alistair/Projects/COMP2003-2020-W/Admin Interface/AdminInterface/AdminInterface/Views/Ratings/Index.cshtml"
           Write(Html.ActionLink("Delete", "Delete", new { /* id=item.PrimaryKey */ }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </td>\n        </tr>\n");
#nullable restore
#line 51 "/Users/alistair/Projects/COMP2003-2020-W/Admin Interface/AdminInterface/AdminInterface/Views/Ratings/Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\n</table>\n\n<script>let activePageIndex = 5;</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<AdminInterface.Models.Rating>> Html { get; private set; }
    }
}
#pragma warning restore 1591
