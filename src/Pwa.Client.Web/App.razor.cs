using Avalonia.Web.Blazor;

namespace Pwa.Client.Web;

public partial class App
{
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        WebAppBuilder.Configure<Pwa.Client.App>()
            .SetupWithSingleViewLifetime();
    }
}