using Microsoft.AspNetCore.Mvc.Testing;
using PaymentReconciliationExceptionDesk.Api;

namespace PaymentReconciliationExceptionDesk.Tests;

public sealed class PaymentReconciliationExceptionDeskTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public PaymentReconciliationExceptionDeskTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Overview_route_renders_reconciliation_shell()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/");
        var html = await response.Content.ReadAsStringAsync();

        Assert.True(response.IsSuccessStatusCode);
        Assert.Contains("Payment Reconciliation Exception Desk", html);
        Assert.Contains("returned payouts", html);
    }

    [Fact]
    public async Task Api_summary_returns_expected_counts()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/dashboard/summary");
        var json = await response.Content.ReadAsStringAsync();

        Assert.True(response.IsSuccessStatusCode);
        Assert.Contains("\"batches\":2", json);
        Assert.Contains("\"blockingExceptions\":4", json);
    }

    [Fact]
    public void Analysis_flags_high_risk_reconciliation_exceptions()
    {
        var report = AnalysisService.Analyze(SampleData.Payload);

        Assert.Equal(2, report.Batches);
        Assert.Equal(6, report.Exceptions);
        Assert.Contains(report.Findings, finding => finding.Code == "merchant-ledger-break");
        Assert.Contains(report.Findings, finding => finding.Code == "close-signoff-gap");
    }
}
