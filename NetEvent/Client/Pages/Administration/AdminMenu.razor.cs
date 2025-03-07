using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using NetEvent.Client;
using NetEvent.Client.Shared;
using MudBlazor;
using MudBlazor.ThemeManager;
using NetEvent.Shared.Dto;
using System.Text.Json;

namespace NetEvent.Client.Pages.Administration
{
    public partial class AdminMenu
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        private ThemeManagerTheme _themeManager = new ThemeManagerTheme();
     
    }
}