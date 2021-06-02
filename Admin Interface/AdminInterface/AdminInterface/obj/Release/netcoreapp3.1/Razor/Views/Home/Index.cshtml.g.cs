#pragma checksum "C:\Git\AdminInterface2003\Admin Interface\AdminInterface\AdminInterface\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8574576d5137133d3a2fccd126a498edcc6c6592"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
#line 1 "C:\Git\AdminInterface2003\Admin Interface\AdminInterface\AdminInterface\Views\_ViewImports.cshtml"
using AdminInterface;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Git\AdminInterface2003\Admin Interface\AdminInterface\AdminInterface\Views\_ViewImports.cshtml"
using AdminInterface.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8574576d5137133d3a2fccd126a498edcc6c6592", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6650b4aef7050eb347c81bcf1aa00d5aae662df1", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<AdminInterface.Models.Products>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Orders", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Products", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#nullable restore
#line 1 "C:\Git\AdminInterface2003\Admin Interface\AdminInterface\AdminInterface\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<!--<div class=""text-center"">
    <h1 class=""display-4"">Welcome</h1>
    <p>Learn about <a href=""https://docs.microsoft.com/aspnet/core"">building Web apps with ASP.NET Core</a>.</p>
</div>
-->
<!-- grid layout -->
<div class=""dashboardSection"">
    <div class=""dashboardBlock dashboardLeftBlock"">
        <span class=""dashboardSectionHeader"">Monthly Sales</span>

        <!--<div id=""ordersTargetWidget"">

        <div class=""ordersWidgetSection"">
            <span id=""ordersTargetSettingHeader"">Last 30 days</span>

            <div class=""chartWrapper"">
                <div class=""progressContainer"">
                    <svg class=""progressBar"" viewBox=""0 0 64 64"">
                        <circle class=""progressBarTrack"" cx=""50%"" cy=""50%"" r=""30px""></circle>
                        <circle class=""progressBarThumb"" cx=""50%"" cy=""50%"" r=""30px""></circle>
                    </svg>
                    <span class=""progressValue"">982</span>
                </div>
            </div>

      ");
            WriteLiteral(@"      <div class=""chartSettingsWrapper"">
                <div class=""widgetBtn chartSettingsBtn"">settings</div>
            </div>

        </div>

        <div class=""ordersWidgetSection"">
            <div id=""targetsWrapper"">
                <div id=""targetsHeader"">Targets</div>
                <div class=""orderTarget selectedTarget"" data-value=""1500"">30 days: 1500</div>
                <div class=""orderTarget"" data-value=""3200"">60 days: 3200</div>
                <div class=""widgetBtn"">view more</div>
            </div>
        </div>-->

        <canvas id=""lineChart""></canvas>

");
            WriteLiteral(@"
    </div>
    <div class=""dashboardBlock dashboardRightBlock"">
        <span class=""dashboardSectionHeader"">Recent Orders</span>
        <table id=""orderStatusTable"" class=""table"">
            <thead>
                <tr>
                    <th>Order No.</th>
                    <th>Total</th>
                    <th>Date</th>
                </tr>
            </thead>
            <tbody>

");
#nullable restore
#line 63 "C:\Git\AdminInterface2003\Admin Interface\AdminInterface\AdminInterface\Views\Home\Index.cshtml"
                  
                    // get orders data
                    List<Orders> orders = (List<Orders>)ViewData["Orders"];
                    List<Orders> recentOrders = orders.GetRange(0, 5);
                

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 69 "C:\Git\AdminInterface2003\Admin Interface\AdminInterface\AdminInterface\Views\Home\Index.cshtml"
                 foreach (var order in recentOrders)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8574576d5137133d3a2fccd126a498edcc6c65927202", async() => {
                WriteLiteral("\r\n                        <tr>\r\n                            <td>");
#nullable restore
#line 73 "C:\Git\AdminInterface2003\Admin Interface\AdminInterface\AdminInterface\Views\Home\Index.cshtml"
                           Write(order.OrderId);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n");
#nullable restore
#line 74 "C:\Git\AdminInterface2003\Admin Interface\AdminInterface\AdminInterface\Views\Home\Index.cshtml"
                              
                                Products orderProduct = Model.Where(p => p.ProductId == order.ProductId).ToList()[0];
                                var orderTotal = orderProduct.Price * order.Quantity;
                            

#line default
#line hidden
#nullable disable
                WriteLiteral("                            <td>£");
#nullable restore
#line 78 "C:\Git\AdminInterface2003\Admin Interface\AdminInterface\AdminInterface\Views\Home\Index.cshtml"
                            Write(orderTotal.ToString("n2"));

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 79 "C:\Git\AdminInterface2003\Admin Interface\AdminInterface\AdminInterface\Views\Home\Index.cshtml"
                           Write(order.TimeOrdered);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                        </tr>\r\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 71 "C:\Git\AdminInterface2003\Admin Interface\AdminInterface\AdminInterface\Views\Home\Index.cshtml"
                                                                      WriteLiteral(order.OrderId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 82 "C:\Git\AdminInterface2003\Admin Interface\AdminInterface\AdminInterface\Views\Home\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                <!--<a asp-controller=""Orders"" asp-action=""Details"" asp-route-id=""6"">
                    <tr>
                        <td>#6</td>
                        <td>£482.96</td>
                        <td>Processing</td>
                    </tr>
                </a>
                <a asp-controller=""Orders"" asp-action=""Details"" asp-route-id=""12"">
                    <tr>
                        <td>#12</td>
                        <td>£440.00</td>
                        <td>Dispatched</td>
                    </tr>
                </a>
                <a asp-controller=""Orders"" asp-action=""Details"" asp-route-id=""18"">
                    <tr>
                        <td>#18</td>
                        <td>£229.00</td>
                        <td>Dispatched</td>
                    </tr>
                </a>-->
            </tbody>
        </table>
    </div>
</div>


");
#nullable restore
#line 111 "C:\Git\AdminInterface2003\Admin Interface\AdminInterface\AdminInterface\Views\Home\Index.cshtml"
  
    var popularProducts = new List<Products>();
    var lowStockProducts = new List<Products>();
    foreach (var item in Model)
    {
        // get popular products
        // if less than 3 products in array
        if (popularProducts.Count < 3)
        {
            // add to array
            popularProducts.Add(item);
        }
        else
        {
            // first is lowest
            popularProducts = popularProducts.OrderBy(p => p.TotalSold).ToList();
            // if current product has more sales than lowest in array
            if (popularProducts[0].TotalSold < item.TotalSold)
            {
                // replace lowest product with current one
                popularProducts[0] = item;
            }
        }

        // get low stock products
        if (lowStockProducts.Count < 3)
        {
            // add to array
            lowStockProducts.Add(item);
        }
        else
        {
            // first is highest
            lowStockProducts = lowStockProducts.OrderByDescending(p => p.Stock).ToList();
            // if current product has less stock than highest in array
            if (lowStockProducts[0].Stock > item.Stock)
            {
                // replace highest stock product with current one
                lowStockProducts[0] = item;
            }
        }

    }

    // order array by total sold (1st highest)
    popularProducts = popularProducts.OrderByDescending(p => p.TotalSold).ToList();
    // order array by stock (1st lowest)
    lowStockProducts = lowStockProducts.OrderBy(p => p.Stock).ToList();

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"dashboardSection\">\r\n    <div class=\"dashboardBlock dashboardLeftBlock\">\r\n        <span class=\"dashboardSectionHeader\">Most Popular</span>\r\n        <div id=\"productsWrapper\">\r\n\r\n");
#nullable restore
#line 166 "C:\Git\AdminInterface2003\Admin Interface\AdminInterface\AdminInterface\Views\Home\Index.cshtml"
             foreach (var product in popularProducts)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8574576d5137133d3a2fccd126a498edcc6c659214616", async() => {
                WriteLiteral("\r\n                    <div class=\"product\">\r\n                        <div class=\"productContainer\">\r\n                            <img class=\"productImage\"");
                BeginWriteAttribute("src", " src=\"", 6168, "\"", 6191, 1);
#nullable restore
#line 171 "C:\Git\AdminInterface2003\Admin Interface\AdminInterface\AdminInterface\Views\Home\Index.cshtml"
WriteAttributeValue("", 6174, product.ImageUrl, 6174, 17, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\r\n                            <div class=\"productName\">");
#nullable restore
#line 172 "C:\Git\AdminInterface2003\Admin Interface\AdminInterface\AdminInterface\Views\Home\Index.cshtml"
                                                Write(product.ProductName);

#line default
#line hidden
#nullable disable
                WriteLiteral("</div>\r\n                            <div class=\"productSold\">Total Sold: ");
#nullable restore
#line 173 "C:\Git\AdminInterface2003\Admin Interface\AdminInterface\AdminInterface\Views\Home\Index.cshtml"
                                                            Write(product.TotalSold);

#line default
#line hidden
#nullable disable
                WriteLiteral("</div>\r\n                        </div>\r\n                    </div>\r\n                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 168 "C:\Git\AdminInterface2003\Admin Interface\AdminInterface\AdminInterface\Views\Home\Index.cshtml"
                                                                    WriteLiteral(product.ProductId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 177 "C:\Git\AdminInterface2003\Admin Interface\AdminInterface\AdminInterface\Views\Home\Index.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"dashboardBlock dashboardRightBlock\">\r\n        <span class=\"dashboardSectionHeader\">Low in Stock</span>\r\n        <div id=\"productsWrapper\">\r\n\r\n");
#nullable restore
#line 186 "C:\Git\AdminInterface2003\Admin Interface\AdminInterface\AdminInterface\Views\Home\Index.cshtml"
             foreach (var product in lowStockProducts)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8574576d5137133d3a2fccd126a498edcc6c659219132", async() => {
                WriteLiteral("\r\n                    <div class=\"product\">\r\n                        <div class=\"productContainer\">\r\n                            <img class=\"productImage\"");
                BeginWriteAttribute("src", " src=\"", 6979, "\"", 7002, 1);
#nullable restore
#line 191 "C:\Git\AdminInterface2003\Admin Interface\AdminInterface\AdminInterface\Views\Home\Index.cshtml"
WriteAttributeValue("", 6985, product.ImageUrl, 6985, 17, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\r\n                            <div class=\"productName\">");
#nullable restore
#line 192 "C:\Git\AdminInterface2003\Admin Interface\AdminInterface\AdminInterface\Views\Home\Index.cshtml"
                                                Write(product.ProductName);

#line default
#line hidden
#nullable disable
                WriteLiteral("</div>\r\n                            <div class=\"productSold\">Stock: ");
#nullable restore
#line 193 "C:\Git\AdminInterface2003\Admin Interface\AdminInterface\AdminInterface\Views\Home\Index.cshtml"
                                                       Write(product.Stock);

#line default
#line hidden
#nullable disable
                WriteLiteral("</div>\r\n                        </div>\r\n                    </div>\r\n                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 188 "C:\Git\AdminInterface2003\Admin Interface\AdminInterface\AdminInterface\Views\Home\Index.cshtml"
                                                                    WriteLiteral(product.ProductId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 197 "C:\Git\AdminInterface2003\Admin Interface\AdminInterface\AdminInterface\Views\Home\Index.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n\r\n\r\n</div>\r\n\r\n\r\n");
#nullable restore
#line 206 "C:\Git\AdminInterface2003\Admin Interface\AdminInterface\AdminInterface\Views\Home\Index.cshtml"
  
    // get graph nodes
    List<DashboardGraphNode> graphNodes = (List<DashboardGraphNode>)ViewData["GraphNodes"];

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 211 "C:\Git\AdminInterface2003\Admin Interface\AdminInterface\AdminInterface\Views\Home\Index.cshtml"
 foreach (var node in graphNodes)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <!-- add data for graph -->\r\n    <data class=\"graphData\" data-sales=\"");
#nullable restore
#line 214 "C:\Git\AdminInterface2003\Admin Interface\AdminInterface\AdminInterface\Views\Home\Index.cshtml"
                                   Write(node.totalSales);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" data-month=\"");
#nullable restore
#line 214 "C:\Git\AdminInterface2003\Admin Interface\AdminInterface\AdminInterface\Views\Home\Index.cshtml"
                                                                 Write(node.month);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"></data>\r\n");
#nullable restore
#line 215 "C:\Git\AdminInterface2003\Admin Interface\AdminInterface\AdminInterface\Views\Home\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<script>
    let activePageIndex = 0;

    const monthNames = [""January"", ""February"", ""March"", ""April"", ""May"", ""June"",
        ""July"", ""August"", ""September"", ""October"", ""November"", ""December""
    ];

    // create array for node data
    var monthsData = [];
    var salesData = [];

    // for each node in data
    $("".graphData"").each(function () {
        // add sales data to array
        salesData.push($(this).attr('data-sales'));
        // add month data to array
        let monthName = monthNames[$(this).attr('data-month') - 1]
        monthsData.push(monthName);
    });

    const chart = document.getElementById(""lineChart"");
    let lineChart = new Chart(chart, {
        // The type of chart we want to create
        type: 'line',
        data: {
            labels: monthsData,
            datasets: [{
                label: 'Sales',
                fill: false,
                borderColor: 'rgb(255, 99, 132)',
                data: salesData
            }]
       ");
            WriteLiteral(@" },
        // Configuration options go here
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });

</script>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<AdminInterface.Models.Products>> Html { get; private set; }
    }
}
#pragma warning restore 1591