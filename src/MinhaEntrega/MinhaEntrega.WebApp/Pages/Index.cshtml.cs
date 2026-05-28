using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using MinhaEntrega.WebApp.Client;
using MinhaEntrega.WebApp.Utils;

namespace Model.WebApp.Pages;

public class IndexModel : PageModel
{
    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPostUpdate(string code, string name)
    {
        DebuggingUtils.WriteLine("update", code);
        DebuggingUtils.WriteLine("code", code);
        DebuggingUtils.WriteLine("name", name);

        await MinhaEntregaClient.UpdateOrderAsync(code, name);

        return Redirect("/");
    }

    public async Task<IActionResult> OnPostAdd(string code, string name)
    {
        DebuggingUtils.WriteLine("add", code);
        DebuggingUtils.WriteLine("code", code);
        DebuggingUtils.WriteLine("name", name);

        await MinhaEntregaClient.AddOrderAsync(code, name);

        return Redirect("/");
    }

    public IActionResult OnPostRefresh()
    {
        return Redirect("/");
    }
}
