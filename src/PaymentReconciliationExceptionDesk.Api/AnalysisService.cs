namespace PaymentReconciliationExceptionDesk.Api;

public static class AnalysisService
{
    public static PaymentReconciliationPostureReport Analyze(PaymentReconciliationExport payload)
    {
        var findings = new List<PaymentReconciliationFinding>();

        foreach (var batch in payload.Batches)
        {
            if (batch.SnapshotStatus == "STALE")
            {
                findings.Add(new PaymentReconciliationFinding(
                    "stale-reconciliation-batch",
                    "medium",
                    batch.Name,
                    $"Batch \"{batch.Name}\" is stale and should be recollected before close confidence is asserted.",
                    batch.Owner
                ));
            }
        }

        foreach (var exception in payload.Exceptions)
        {
            var code = exception.ExceptionFamily switch
            {
                "GatewayFee" => "gateway-fee-delta",
                "ACHReturn" => "ach-return-gap",
                "LedgerBreak" => "merchant-ledger-break",
                "OwnerEvidence" => "exception-owner-gap",
                "CutoffTiming" => "cutoff-replay-gap",
                "CloseSignoff" => "close-signoff-gap",
                _ => "payment-reconciliation-gap"
            };

            findings.Add(new PaymentReconciliationFinding(
                code,
                exception.Severity,
                exception.Subject,
                exception.ObservedState,
                ResolveOwner(exception.ExceptionFamily)
            ));

            if (exception.HoursOpen > 24)
            {
                findings.Add(new PaymentReconciliationFinding(
                    "stale-exception-window",
                    exception.HoursOpen > 32 ? "medium" : "low",
                    exception.Subject,
                    $"Exception \"{exception.Subject}\" has remained open for {exception.HoursOpen} hours.",
                    ResolveOwner(exception.ExceptionFamily)
                ));
            }
        }

        var blocking = payload.Exceptions.Count(g => g.BlocksClose);
        var settlementRisks = payload.Exceptions.Count(g => g.ExceptionFamily is "GatewayFee" or "ACHReturn" or "LedgerBreak");
        var closeRisks = payload.Exceptions.Count(g => g.ExceptionFamily is "OwnerEvidence" or "CutoffTiming" or "CloseSignoff");

        return new PaymentReconciliationPostureReport(
            payload.Batches.Count,
            payload.Batches.Count(c => c.SnapshotStatus == "CURRENT"),
            payload.Exceptions.Count,
            blocking,
            settlementRisks,
            closeRisks,
            findings,
            !findings.Any(f => f.Severity == "high")
        );
    }

    public static object Summary()
    {
        var report = Analyze(SampleData.Payload);

        return new
        {
            batches = report.Batches,
            currentBatches = report.CurrentBatches,
            exceptions = report.Exceptions,
            blockingExceptions = report.BlockingExceptions,
            settlementRisks = report.SettlementRisks,
            closeRisks = report.CloseRisks,
            recommendation = "Repair returned payouts, fee deltas, and close-signoff gaps before certifying the reconciliation cycle."
        };
    }

    private static string ResolveOwner(string family) => family switch
    {
        "GatewayFee" => "Payment Operations",
        "ACHReturn" => "Treasury Operations",
        "LedgerBreak" => "Payment Operations",
        "OwnerEvidence" => "Finance Controls",
        "CutoffTiming" => "Settlement Governance",
        "CloseSignoff" => "Settlement Governance",
        _ => "Payment Operations"
    };
}
