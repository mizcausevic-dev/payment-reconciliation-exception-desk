namespace PaymentReconciliationExceptionDesk.Api;

public static class SampleData
{
    public static readonly PaymentReconciliationExport Payload = new(
        Batches:
        [
            new(
                "batch-card-us",
                "US card settlement batch",
                "Card / processor",
                "WATCH",
                "CURRENT",
                "Payment Operations",
                18422,
                7,
                DateTimeOffset.Parse("2026-05-29T16:20:00Z")
            ),
            new(
                "batch-ach-vendor",
                "Vendor ACH payout batch",
                "ACH / bank ops",
                "CRITICAL",
                "STALE",
                "Treasury Operations",
                3154,
                5,
                DateTimeOffset.Parse("2026-05-28T13:40:00Z")
            )
        ],
        Exceptions:
        [
            new(
                "ex-gateway-fee",
                "batch-card-us",
                "GatewayFee",
                "high",
                "Gateway fee delta",
                "Processor fee ledger matches the internal settlement sheet for the same cutoff window.",
                "The latest card batch still shows a processor fee delta that exceeds the approved tolerance band.",
                18,
                true
            ),
            new(
                "ex-ach-returns",
                "batch-ach-vendor",
                "ACHReturn",
                "high",
                "Returned vendor payout set",
                "Returned ACH entries are reconciled to vendor tickets and funding reversals before close.",
                "Six vendor payouts were returned overnight and still lack ticket-linked funding reversal evidence.",
                29,
                true
            ),
            new(
                "ex-ledger-break",
                "batch-card-us",
                "LedgerBreak",
                "high",
                "Card ledger break",
                "Internal booking totals align to processor settlement before the batch is certified.",
                "One merchant segment still has a booking mismatch between the processor settlement and internal ledger.",
                21,
                true
            ),
            new(
                "ex-owner-gap",
                "batch-ach-vendor",
                "OwnerEvidence",
                "medium",
                "Unowned payout exception",
                "Each material exception retains a named owner and escalation timestamp.",
                "The oldest vendor exception still lacks a named owner in the daily exception packet.",
                14,
                false
            ),
            new(
                "ex-cutoff-drift",
                "batch-card-us",
                "CutoffTiming",
                "medium",
                "Cutoff timing drift",
                "Settlement cutoffs retain an explicit replay note whenever the processor snapshot moves.",
                "The replay note for the latest cutoff adjustment is missing from the packet evidence chain.",
                11,
                false
            ),
            new(
                "ex-close-signoff",
                "batch-ach-vendor",
                "CloseSignoff",
                "high",
                "Close packet signoff gap",
                "Settlement packets close with operations, treasury, and finance signoff before certification.",
                "The ACH close packet is still missing treasury signoff and one finance review note.",
                17,
                true
            )
        ]
    );

    public static readonly IReadOnlyList<PaymentLane> PaymentLanes =
    [
        new(
            "card-lane",
            "Card settlement lane",
            "Payment Operations",
            "red",
            "Processor batches, fee deltas, and merchant booking alignment",
            "Clear the fee delta and ledger break before certifying the next card close packet.",
            "The card lane remains noisy until processor settlement and internal booking totals agree."
        ),
        new(
            "ach-lane",
            "ACH payout lane",
            "Treasury Operations",
            "red",
            "Returned payouts, funding reversals, and payout-close evidence",
            "Finish the return-to-ticket mapping and collect treasury signoff for the ACH packet.",
            "ACH returns still block close confidence across the vendor payout batch."
        ),
        new(
            "owner-lane",
            "Exception ownership lane",
            "Finance Controls",
            "yellow",
            "Owner evidence, escalation notes, and close-safe accountability",
            "Attach the missing owner and escalation timestamp to the oldest payout exception.",
            "The ownership lane is recoverable once named accountability is restored."
        ),
        new(
            "packet-lane",
            "Close packet lane",
            "Settlement Governance",
            "red",
            "Certification completeness, treasury review, and finance signoff posture",
            "Close the missing treasury and finance notes before marking the packet ready.",
            "The close packet is not yet safe for final certification."
        )
    ];

    public static readonly IReadOnlyList<SettlementPacket> SettlementPackets =
    [
        new(
            "RC-12",
            "US card settlement packet",
            "Settlement Governance",
            "red",
            58,
            "Processor fee delta and merchant ledger break remain open.",
            "Do not certify the card packet until settlement and internal booking totals reconcile.",
            8
        ),
        new(
            "RC-18",
            "Vendor ACH payout packet",
            "Treasury Operations",
            "red",
            64,
            "Returned ACH entries and close signoff remain incomplete.",
            "Hold packet certification and route it back through treasury and finance review.",
            10
        ),
        new(
            "RC-21",
            "Exception replay packet",
            "Finance Controls",
            "yellow",
            77,
            "Owner evidence is incomplete on the oldest exception.",
            "Packet can recover once ownership and escalation notes close in the next cycle.",
            16
        ),
        new(
            "RC-27",
            "Close evidence replay",
            "Payment Operations",
            "yellow",
            74,
            "Cutoff replay note is still missing from the packet evidence chain.",
            "Close posture is recoverable if cutoff evidence is replayed before the next settlement window.",
            18
        )
    ];
}
