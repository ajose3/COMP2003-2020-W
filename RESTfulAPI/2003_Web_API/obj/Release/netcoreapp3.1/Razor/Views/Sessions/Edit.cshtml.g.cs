#pragma checksum "C:\Git\COMP2003-2020-W\RESTfulAPI\2003_Web_API\Views\Sessions\Edit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b34a6afc30d175d4fafddcc47a28cd3e84463f32"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Sessions_Edit), @"mvc.1.0.view", @"/Views/Sessions/Edit.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b34a6afc30d175d4fafddcc47a28cd3e84463f32", @"/Views/Sessions/Edit.cshtml")]
    public class Views_Sessions_Edit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<_2003_Web_API.Models.Session>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Git\COMP2003-2020-W\RESTfulAPI\2003_Web_API\Views\Sessions\Edit.cshtml"
  
    ViewData["Title"] = "Edit";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<h1>Edit</h1>

<h4>Session</h4>
<hr />
<div class=""row"">
    <div class=""col-md-4"">
        <form asp-action=""Edit"">
            <div asp-validation-summary=""ModelOnly"" class=""text-danger""></div>
            <input type=""hidden"" asp-for=""SessionId"" />
            <div class=""form-group"">
                <label asp-for=""CustomerId"" class=""control-label""></label>
                <select asp-for=""CustomerId"" class=""form-control"" asp-items=""ViewBag.CustomerId""></select>
                <span asp-validation-for=""CustomerId"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""Token"" class=""control-label""></label>
                <input asp-for=""Token"" class=""form-control"" />
                <span asp-validation-for=""Token"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""ExpiryTime"" class=""control-label""></label>
                <input asp-for=""ExpiryTime"" class=""");
            WriteLiteral(@"form-control"" />
                <span asp-validation-for=""ExpiryTime"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <input type=""submit"" value=""Save"" class=""btn btn-primary"" />
            </div>
        </form>
    </div>
</div>

<div>
    <a asp-action=""Index"">Back to List</a>
</div>

");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 43 "C:\Git\COMP2003-2020-W\RESTfulAPI\2003_Web_API\Views\Sessions\Edit.cshtml"
      await Html.RenderPartialAsync("_ValidationScriptsPartial");

#line default
#line hidden
#nullable disable
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<_2003_Web_API.Models.Session> Html { get; private set; }
    }
}
#pragma warning restore 1591
