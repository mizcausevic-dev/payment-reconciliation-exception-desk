using System.Text.Json;
using PaymentReconciliationExceptionDesk.Api;

var app = PaymentReconciliationExceptionDeskApplication.BuildApp(args);

if (args.Contains("--prerender"))
{
    await SiteBuilder.WriteAsync();
    return;
}

if (args.Contains("--demo"))
{
    Console.WriteLine(JsonSerializer.Serialize(AnalysisService.Summary(), new JsonSerializerOptions { WriteIndented = true }));
    Console.WriteLine(JsonSerializer.Serialize(SampleData.PaymentLanes, new JsonSerializerOptions { WriteIndented = true }));
    return;
}

app.Run();

public partial class Program;

public static class PaymentReconciliationExceptionDeskApplication
{
    public static WebApplication BuildApp(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/", () => Results.Content(RenderService.Overview(), "text/html"));
        app.MapGet("/payment-lane", () => Results.Content(RenderService.PaymentLane(), "text/html"));
        app.MapGet("/exception-queue", () => Results.Content(RenderService.ExceptionQueue(), "text/html"));
        app.MapGet("/settlement-posture", () => Results.Content(RenderService.SettlementPosture(), "text/html"));
        app.MapGet("/verification", () => Results.Content(RenderService.Verification(), "text/html"));
        app.MapGet("/docs", () => Results.Content(RenderService.Docs(), "text/html"));

        app.MapGet("/api/dashboard/summary", () => Results.Json(AnalysisService.Summary()));
        app.MapGet("/api/payment-lane", () => Results.Json(SampleData.PaymentLanes));
        app.MapGet("/api/exception-queue", () => Results.Json(SampleData.Payload.Exceptions));
        app.MapGet("/api/settlement-posture", () => Results.Json(SampleData.SettlementPackets));
        app.MapGet("/api/verification", () => Results.Json(new[]
        {
            "Synthetic payment-reconciliation and settlement evidence only; no bank account, processor, customer, or production ledger data is published.",
            "Payment Operations, Treasury Operations, Finance Controls, and Settlement Governance are modeled as operator surfaces.",
            "This repo demonstrates FinTech workflow depth, not compliance-overclaim marketing."
        }));
        app.MapGet("/api/sample", () => Results.Text(RenderService.Sample(), "application/json"));

        return app;
    }
}

public static class SiteBuilder
{
    public static async Task WriteAsync()
    {
        var root = FindRepoRoot();
        var siteDir = Path.Combine(root, "site");
        Directory.CreateDirectory(siteDir);

        var pages = new Dictionary<string, string>
        {
            ["index.html"] = RenderService.Overview(),
            [Path.Combine("payment-lane", "index.html")] = RenderService.PaymentLane(),
            [Path.Combine("exception-queue", "index.html")] = RenderService.ExceptionQueue(),
            [Path.Combine("settlement-posture", "index.html")] = RenderService.SettlementPosture(),
            [Path.Combine("verification", "index.html")] = RenderService.Verification(),
            [Path.Combine("docs", "index.html")] = RenderService.Docs()
        };

        foreach (var (relative, html) in pages)
        {
            var target = Path.Combine(siteDir, relative);
            Directory.CreateDirectory(Path.GetDirectoryName(target)!);
            await File.WriteAllTextAsync(target, html);
        }

        var apiDir = Path.Combine(siteDir, "api");
        Directory.CreateDirectory(Path.Combine(apiDir, "dashboard"));
        await File.WriteAllTextAsync(Path.Combine(apiDir, "dashboard", "summary.json"), JsonSerializer.Serialize(AnalysisService.Summary(), new JsonSerializerOptions { WriteIndented = true }));
        await File.WriteAllTextAsync(Path.Combine(apiDir, "payment-lane.json"), JsonSerializer.Serialize(SampleData.PaymentLanes, new JsonSerializerOptions { WriteIndented = true }));
        await File.WriteAllTextAsync(Path.Combine(apiDir, "exception-queue.json"), JsonSerializer.Serialize(SampleData.Payload.Exceptions, new JsonSerializerOptions { WriteIndented = true }));
        await File.WriteAllTextAsync(Path.Combine(apiDir, "settlement-posture.json"), JsonSerializer.Serialize(SampleData.SettlementPackets, new JsonSerializerOptions { WriteIndented = true }));
        await File.WriteAllTextAsync(Path.Combine(apiDir, "verification.json"), JsonSerializer.Serialize(new[]
        {
            "Synthetic payment-reconciliation and settlement evidence only; no bank account, processor, customer, or production ledger data is published.",
            "Payment Operations, Treasury Operations, Finance Controls, and Settlement Governance are modeled as operator surfaces.",
            "This repo demonstrates FinTech workflow depth, not compliance-overclaim marketing."
        }, new JsonSerializerOptions { WriteIndented = true }));
        await File.WriteAllTextAsync(Path.Combine(apiDir, "sample.json"), RenderService.Sample());

        const string domain = "payments.kineticgain.com";
        await File.WriteAllTextAsync(Path.Combine(siteDir, "robots.txt"), $"User-agent: *{Environment.NewLine}Allow: /{Environment.NewLine}Sitemap: https://{domain}/sitemap.xml{Environment.NewLine}");
        await File.WriteAllTextAsync(Path.Combine(siteDir, "sitemap.xml"), """
<?xml version="1.0" encoding="UTF-8"?>
<urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">
  <url><loc>https://payments.kineticgain.com/</loc></url>
  <url><loc>https://payments.kineticgain.com/payment-lane/</loc></url>
  <url><loc>https://payments.kineticgain.com/exception-queue/</loc></url>
  <url><loc>https://payments.kineticgain.com/settlement-posture/</loc></url>
  <url><loc>https://payments.kineticgain.com/verification/</loc></url>
  <url><loc>https://payments.kineticgain.com/docs/</loc></url>
</urlset>
""");
        await File.WriteAllTextAsync(Path.Combine(siteDir, "CNAME"), domain + Environment.NewLine);
    }

    private static string FindRepoRoot()
    {
        var current = AppContext.BaseDirectory;
        for (var i = 0; i < 8; i++)
        {
            if (File.Exists(Path.Combine(current, "payment-reconciliation-exception-desk.sln")))
            {
                return current;
            }

            current = Directory.GetParent(current)?.FullName
                ?? throw new DirectoryNotFoundException("Unable to resolve repo root.");
        }

        throw new DirectoryNotFoundException("Unable to resolve repo root.");
    }
}
