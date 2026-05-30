from pathlib import Path
import textwrap


ROOT = Path(__file__).resolve().parents[1]
SHOT_DIR = ROOT / "screenshots"
SHOT_DIR.mkdir(exist_ok=True)


def wrap(text: str, width: int):
    return textwrap.wrap(text, width=width) or [text]


def draw_lines(lines, x, y, font_size=22, color="#e9f3ff", weight="400", family="Inter,Segoe UI,Arial"):
    parts = [f'<text x="{x}" y="{y}" fill="{color}" font-size="{font_size}" font-weight="{weight}" font-family="{family}">']
    dy = 0
    for line in lines:
        parts.append(f'<tspan x="{x}" dy="{dy}">{line}</tspan>')
        dy = int(font_size * 1.25)
    parts.append("</text>")
    return "\n".join(parts)


def stat_card(x, y, w, h, label, value, detail, accent):
    return f"""
    <rect x="{x}" y="{y}" width="{w}" height="{h}" rx="20" fill="#111827" stroke="rgba(120,255,170,.14)" />
    {draw_lines([label.upper()], x + 24, y + 34, 11, "#8fdcff", "700", "Consolas, monospace")}
    {draw_lines([value], x + 24, y + 86, 34, accent, "700", "Segoe UI, Arial")}
    {draw_lines(wrap(detail, 28), x + 24, y + 124, 15, "#b8c6db", "400")}
    """


def panel(x, y, w, h, kicker, title, body, accent="#19c7ff"):
    return f"""
    <rect x="{x}" y="{y}" width="{w}" height="{h}" rx="22" fill="#0b1220" stroke="rgba(120,255,170,.18)" />
    {draw_lines([kicker.upper()], x + 26, y + 34, 11, accent, "700", "Consolas, monospace")}
    {draw_lines(wrap(title, 30), x + 26, y + 84, 28, "#f5f7ff", "700", "Georgia, serif")}
    {draw_lines(wrap(body, 50), x + 26, y + 136, 16, "#b8c6db", "400")}
    """


def bullet_list(items, x, y, accent="#37ff8b"):
    rows = []
    offset = 0
    for item in items:
        rows.append(f'<circle cx="{x}" cy="{y + offset - 6}" r="5" fill="{accent}" />')
        rows.append(draw_lines(wrap(item, 70), x + 16, y + offset, 16, "#e9f3ff", "400"))
        offset += 54
    return "\n".join(rows)


def shell(eyebrow, title, subtitle, inner):
    return f"""<svg xmlns="http://www.w3.org/2000/svg" width="1400" height="860" viewBox="0 0 1400 860">
    <rect width="1400" height="860" fill="#070a0f"/>
    <rect x="24" y="24" width="1352" height="812" rx="30" fill="#0a1426" stroke="rgba(120,255,170,.18)"/>
    <rect x="58" y="58" width="1284" height="152" rx="26" fill="#0b1220" stroke="rgba(120,255,170,.12)"/>
    {draw_lines([eyebrow.upper()], 94, 96, 13, "#37ff8b", "700", "Consolas, monospace")}
    {draw_lines(wrap(title, 36), 94, 146, 34, "#f5f7ff", "700", "Georgia, serif")}
    {draw_lines(wrap(subtitle, 100), 94, 194, 18, "#b8c6db", "400")}
    {inner}
    </svg>"""


overview = shell(
    "Payment Reconciliation Exception Desk",
    "Control-plane summary for payment breaks, returned payouts, fee deltas, and settlement signoff posture.",
    "Batch pressure, unresolved exceptions, owner accountability, and month-end packet readiness stay visible together before close certification.",
    f"""
    {stat_card(58, 238, 288, 150, "open batches", "4", "Card, ACH, marketplace, and treasury-close batches modeled together.", "#19c7ff")}
    {stat_card(364, 238, 288, 150, "active exceptions", "7", "Returned payouts, fee deltas, and ledger breaks still unresolved.", "#ffcc66")}
    {stat_card(670, 238, 288, 150, "stalled packets", "2", "Finance-close evidence packets still missing signoff.", "#ff5c7a")}
    {stat_card(976, 238, 366, 150, "lead recommendation", "Close returned ACH and fee drift first", "Repair payout replay gaps before certifying the settlement cycle.", "#37ff8b")}

    {panel(58, 420, 614, 356, "Exception queue", "The riskiest reconciliation breaks stay visible first.", "Merchant ledger breaks, processor fee mismatches, and returned ACH payouts stay grouped by lane and owner so the queue is readable before finance signoff.", "#19c7ff")}
    <rect x="708" y="420" width="634" height="356" rx="22" fill="#0b1220" stroke="rgba(120,255,170,.18)" />
    {draw_lines(["SETTLEMENT POSTURE"], 734, 454, 11, "#37ff8b", "700", "Consolas, monospace")}
    {draw_lines(["What must close before", "month-end signoff"], 734, 506, 28, "#f5f7ff", "700", "Georgia, serif")}
    {bullet_list([
      "Returned vendor payouts must be matched to the original disbursement and replay decision.",
      "Processor fee drift needs owner evidence before the merchant ledger can be certified.",
      "Treasury-close packets need reviewer names, export timestamps, and final finance signoff."
    ], 744, 580)}
    """,
)

lane = shell(
    "Payment Lane",
    "Each lane keeps owner, processor, batch state, and next action visible.",
    "Cards, ACH, marketplace payouts, and treasury-close work stay separated cleanly so exception routing does not collapse into a single queue.",
    f"""
    {panel(58, 238, 620, 250, "Cards lane", "Visa and Mastercard settlement batches", "Processor fee drift, refund timing mismatches, and close evidence gaps stay tied to the settlement lane that owns them.", "#19c7ff")}
    {panel(708, 238, 634, 250, "ACH lane", "Vendor and payroll payout reconciliation", "Returned payouts, stale replay decisions, and payout ledger breaks stay visible before close-safe certification.", "#ffcc66")}
    {panel(58, 520, 620, 256, "Marketplace lane", "Merchant clearing and transfer balance posture", "Reserve releases, split-fee exceptions, and cross-ledger mismatches stay readable at operator depth.", "#b88cff")}
    {panel(708, 520, 634, 256, "Treasury lane", "Month-end packet and signoff readiness", "Finance, revops, and accounting reviewers can see whether the packet is truly signoff-safe.", "#37ff8b")}
    """,
)

posture = shell(
    "Settlement Posture",
    "Packet readiness, unresolved exceptions, and the next close window stay readable for finance operators.",
    "The board keeps month-end packet posture explicit instead of hiding it behind aggregate success rates.",
    f"""
    {panel(58, 238, 402, 244, "Batch PY-1042", "Vendor ACH replay packet", "61 percent complete. Returned payouts are still unmatched and owner signoff remains blocked.", "#ff5c7a")}
    {panel(498, 238, 402, 244, "Batch CR-882", "Card fee-drift packet", "74 percent complete. Processor fee deltas are narrowed, but export evidence is still incomplete.", "#ffcc66")}
    {panel(938, 238, 404, 244, "Batch TR-330", "Treasury close packet", "84 percent complete. Final finance signoff can clear once reviewer timestamps land.", "#37ff8b")}
    {panel(58, 514, 1284, 262, "Why this monetizes cleanly", "Hosted preview planned, paid template pack later, embedded by engagement.", "This is a strong FinTech operator wedge because it lives where teams actually feel pain: payout exceptions, fee mismatches, unresolved close packets, and settlement-safe signoff before finance certifies the cycle.", "#19c7ff")}
    """,
)

(SHOT_DIR / "01-overview.svg").write_text(overview, encoding="utf-8")
(SHOT_DIR / "02-payment-lane.svg").write_text(lane, encoding="utf-8")
(SHOT_DIR / "03-settlement-posture.svg").write_text(posture, encoding="utf-8")
