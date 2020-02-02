using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorAspnetHostedOidcJS.Shared
{
    public interface IUserManagerInterop
    {
        RenderFragment Initialize();
        Task Initialized { get; }
    }
}
