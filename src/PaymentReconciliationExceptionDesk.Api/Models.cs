namespace PaymentReconciliationExceptionDesk.Api;

public sealed record PaymentBatch(
    string Id,
    string Name,
    string PaymentRail,
    string Status,
    string SnapshotStatus,
    string Owner,
    int TransactionCount,
    int ExceptionBacklog,
    DateTimeOffset CollectedAt
);

public sealed record ReconciliationException(
    string Id,
    string BatchId,
    string ExceptionFamily,
    string Severity,
    string Subject,
    string ExpectedState,
    string ObservedState,
    int HoursOpen,
    bool BlocksClose
);

public sealed record PaymentLane(
    string Id,
    string Lane,
    string Owner,
    string Status,
    string Focus,
    string NextAction,
    string Note
);

public sealed record SettlementPacket(
    string PacketId,
    string Lane,
    string Owner,
    string Status,
    int CompletenessScore,
    string Blocker,
    string DecisionNote,
    int ReviewWindowHours
);

public sealed record PaymentReconciliationExport(
    IReadOnlyList<PaymentBatch> Batches,
    IReadOnlyList<ReconciliationException> Exceptions
);

public sealed record PaymentReconciliationFinding(
    string Code,
    string Severity,
    string Subject,
    string Message,
    string Owner
);

public sealed record PaymentReconciliationPostureReport(
    int Batches,
    int CurrentBatches,
    int Exceptions,
    int BlockingExceptions,
    int SettlementRisks,
    int CloseRisks,
    IReadOnlyList<PaymentReconciliationFinding> Findings,
    bool Ok
);
