using System.Text.Json;

namespace PaymentReconciliationExceptionDesk.Api;

public static class RenderService
{
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    public static string Overview() => Layout(
        "Payment Reconciliation Exception Desk",
        "/",
        $$"""
        <section class="section">
          <div class="sh"><h2>Operator Snapshot</h2><div class="note">fintech / payment ops / reconciliation</div></div>
          <div class="kpis">
            {{Metric("2", "batches", "Synthetic card and ACH reconciliation batches across active settlement lanes.", "cyan")}}
            {{Metric("1", "current batches", "Only one batch snapshot is current enough to trust without replay.", "green")}}
            {{Metric("6", "exceptions", "Breaks across gateway fees, returns, ledger drift, ownership, cutoff timing, and signoff.", "plum")}}
            {{Metric("4", "blocking exceptions", "Close-blocking issues still need closure before certification.", "red")}}
            {{Metric("3", "settlement risks", "Processor, ACH, and ledger risks still need repair.", "amber")}}
            {{Metric("3", "close risks", "Owner evidence, cutoff replay, and final packet posture remain incomplete.", "red")}}
          </div>
        </section>
        <section class="section">
          <div class="sh"><h2>Why this lane matters</h2><div class="note">c# / dotnet / fintech</div></div>
          <div class="stack">
            <div class="src"><div class="src-name">close confidence</div><div class="src-tit">Stop bad settlements before they become finance noise or merchant distrust</div><p>Payment teams need one board where gateway deltas, returned payouts, ledger breaks, owner evidence, and close signoff stay visible together.</p></div>
            <div class="src"><div class="src-name">operator depth</div><div class="src-tit">Payment operations and treasury mechanics, not generic dashboard filler</div><p>This follows the Kinetic Gain pattern: routing, evidence, approvals, and operator-safe remediation posture for real reconciliation work.</p></div>
            <div class="src"><div class="src-name">monetization path</div><div class="src-tit">Hosted preview planned · Embedded by engagement</div><p>The free surface shows the operator model; the commercial path is an embedded reconciliation module for finance and settlement workflows.</p></div>
          </div>
        </section>
        {{ProductDepthSection()}}
        <section class="section">
          <div class="sh"><h2>Board questions this answers</h2><div class="note">exposure · leakage · control investment</div></div>
          <div class="stack">
            <div class="src"><div class="src-name">exposure</div><div class="src-tit">Which payment batches are unsafe to certify?</div><p>Gateway fee deltas, ACH returns, ledger breaks, cutoff timing, and final signoff posture stay visible before finance closes on an incomplete packet.</p></div>
            <div class="src"><div class="src-name">savings</div><div class="src-tit">Where is margin leaking through preventable reconciliation work?</div><p>The desk makes fee drift, return queues, duplicate research, and stale owner evidence visible so payment teams can stop rebuilding the same settlement explanation.</p></div>
            <div class="src"><div class="src-name">investment</div><div class="src-tit">Which settlement control should be automated first?</div><p>Blocking exceptions show whether processor exports, ACH return triage, ledger matching, cutoff replay, or close signoff needs the next systems pass.</p></div>
          </div>
        </section>
        <section class="section">
          <div class="sh"><h2>Evidence model</h2><div class="note">signal · proof · decision</div></div>
          <table class="ttbl">
            <thead><tr><th>Signal</th><th>Owner</th><th>Required proof</th><th>Decision supported</th></tr></thead>
            <tbody>
              <tr><td><b>Gateway fee delta</b></td><td>Payment Operations</td><td>Processor export, fee schedule, variance reason, owner memo</td><td>Approve settlement, dispute fee, or hold close</td></tr>
              <tr><td><b>ACH return pressure</b></td><td>Treasury Operations</td><td>Return code, payout exposure, retry posture, merchant notice</td><td>Retry, recover, reserve, or escalate account risk</td></tr>
              <tr><td><b>Ledger close packet</b></td><td>Finance Controls</td><td>Ledger match, cutoff replay, exception owner, final signoff</td><td>Certify close or require reconciliation repair</td></tr>
            </tbody>
          </table>
        </section>
        """
    );

    private static string ProductDepthSection() =>
        """
        <section class="section">
          <div class="sh"><h2>Product depth</h2><div class="note">what this actually does</div></div>
          <div class="depth-grid">
            <div class="depth-card">
              <div class="src-name">executive intelligence surface</div>
              <h3>Payment Reconciliation Exception Desk turns settlement noise into a close-safe decision packet.</h3>
              <p>It gives finance, payment operations, treasury, revenue operations, and executive stakeholders one shared view of reconciliation evidence before gateway deltas, returned payouts, ledger breaks, or stale signoff packets create margin leakage and month-end risk.</p>
              <ul>
                <li>Non-technical leaders see which payment batches are unsafe to certify and why.</li>
                <li>Technical and operations teams see the data contract: batches, exceptions, settlement packets, owner evidence, cutoff replay, and close posture.</li>
                <li>go-to-market teams can explain a clear product story: faster close, less exception rework, cleaner settlement confidence, and fewer finance surprises.</li>
              </ul>
            </div>
            <div class="depth-card">
              <div class="src-name">operating workflow</div>
              <h3>From processor exports to one reconciliation control lane.</h3>
              <div class="workflow">
                <div class="step"><b>1. Model the payment estate.</b><br>Represent batches, exceptions, owners, returns, gateway deltas, and settlement packets with synthetic data.</div>
                <div class="step"><b>2. Score the close posture.</b><br>Separate current batches, blocking exceptions, settlement risks, and close-risk evidence.</div>
                <div class="step"><b>3. Route the decision.</b><br>Publish an operator surface that shows what to certify, dispute, retry, reserve, repair, or escalate.</div>
              </div>
            </div>
          </div>
        </section>
        <section class="section">
          <div class="sh"><h2>What these repos have in common</h2><div class="note">control plane pattern</div></div>
          <div class="stack">
            <div class="src"><div class="src-name">same product spine</div><div class="src-tit">Risk, owner, proof, and next action.</div><p>Each Kinetic Gain surface turns a messy operating lane into a simple decision model leaders can inspect without opening the source system.</p></div>
            <div class="src"><div class="src-name">safe evidence packaging</div><div class="src-tit">Representative data, no live secrets.</div><p>The repo proves the workflow with synthetic sample data and static outputs, so the public surface is useful without exposing bank, processor, customer, merchant, credential, or production ledger data.</p></div>
            <div class="src"><div class="src-name">buyer-readable GTM</div><div class="src-tit">Business value and technical proof stay together.</div><p>The page frames operational pain, the evidence model, the decision workflow, and the commercial story in one scannable product artifact.</p></div>
          </div>
        </section>
        """;

    public static string PaymentLane() => Layout(
        "Payment Reconciliation Exception Desk — Payment Lane",
        "/payment-lane",
        $$"""
        <section class="section">
          <div class="sh"><h2>Payment Lane</h2><div class="note">owner · focus · next action</div></div>
          <table class="ttbl">
            <thead><tr><th>Lane</th><th>Owner</th><th>Status</th><th>Focus</th><th>Next action</th></tr></thead>
            <tbody>
              {{string.Join("", SampleData.PaymentLanes.Select(lane => $$"""
                <tr>
                  <td><b>{{lane.Lane}}</b><br />{{lane.Note}}</td>
                  <td>{{lane.Owner}}</td>
                  <td><span class="st {{SeverityClass(lane.Status)}}">{{lane.Status}}</span></td>
                  <td>{{lane.Focus}}</td>
                  <td>{{lane.NextAction}}</td>
                </tr>
              """))}}
            </tbody>
          </table>
        </section>
        """
    );

    public static string ExceptionQueue() => Layout(
        "Payment Reconciliation Exception Desk — Exception Queue",
        "/exception-queue",
        $$"""
        <section class="section">
          <div class="sh"><h2>Exception Queue</h2><div class="note">severity · owner · subject</div></div>
          <table class="ttbl">
            <thead><tr><th>Risk</th><th>Owner</th><th>Subject</th><th>Observed state</th></tr></thead>
            <tbody>
              {{string.Join("", SampleData.Payload.Exceptions.Select(exception => $$"""
                <tr>
                  <td><span class="st {{SeverityClass(exception.Severity)}}">{{exception.Severity}}</span><br /><b>{{exception.ExceptionFamily}}</b></td>
                  <td>{{OwnerForException(exception.ExceptionFamily)}}</td>
                  <td>{{exception.Subject}}</td>
                  <td>{{exception.ObservedState}}</td>
                </tr>
              """))}}
            </tbody>
          </table>
        </section>
        """
    );

    public static string SettlementPosture() => Layout(
        "Payment Reconciliation Exception Desk — Settlement Posture",
        "/settlement-posture",
        $$"""
        <section class="section">
          <div class="sh"><h2>Settlement Posture</h2><div class="note">packet readiness · blocker · timing</div></div>
          <div class="board">
            {{string.Join("", SampleData.SettlementPackets.Select(packet => $$"""
              <article class="pcard">
                <div class="ptop">
                  <div class="pnum">{{packet.CompletenessScore}}%</div>
                  <div class="ppri">{{packet.Owner}}</div>
                </div>
                <h3>{{packet.Lane}}</h3>
                <p class="pdesc">{{packet.DecisionNote}}</p>
                <ul class="check">
                  <li>{{packet.Blocker}}</li>
                  <li>{{packet.ReviewWindowHours}} hours to the next certification checkpoint</li>
                  <li>Status: <span class="st {{SeverityClass(packet.Status)}}">{{packet.Status}}</span></li>
                </ul>
                <div class="pfoot"><code>{{packet.PacketId}}</code></div>
              </article>
            """))}}
          </div>
        </section>
        """
    );

    public static string Verification() => Layout(
        "Payment Reconciliation Exception Desk — Verification",
        "/verification",
        $$"""
        <section class="section">
          <div class="sh"><h2>Verification</h2><div class="note">operator-safe claims only</div></div>
          <div class="stack">
            {{VerificationCard("This repo uses synthetic reconciliation and settlement evidence only; no bank account, processor, customer, or production ledger data is published.")}}
            {{VerificationCard("The control plane is rooted in payment operations, treasury returns, finance controls, cutoff timing, and packet signoff mechanics.")}}
            {{VerificationCard("This is a FinTech operator surface, not a compliance-overclaim page.")}}
            {{VerificationCard("Hosted preview is planned; embedded module delivery is available by engagement.")}}
          </div>
        </section>
        """
    );

    public static string Docs() => Layout(
        "Payment Reconciliation Exception Desk — Docs",
        "/docs",
        $$"""
        <section class="section">
          <div class="sh"><h2>Docs</h2><div class="note">routes · runbook · api</div></div>
          <div class="stack">
            <div class="src"><div class="src-name">routes</div><div class="src-tit">Public proof surface</div><p><code>/</code>, <code>/payment-lane</code>, <code>/exception-queue</code>, <code>/settlement-posture</code>, <code>/verification</code>, <code>/docs</code></p></div>
            <div class="src"><div class="src-name">api</div><div class="src-tit">Structured payloads</div><p><code>/api/dashboard/summary</code>, <code>/api/payment-lane</code>, <code>/api/exception-queue</code>, <code>/api/settlement-posture</code>, <code>/api/verification</code>, <code>/api/sample</code></p></div>
            <div class="src"><div class="src-name">runbook</div><div class="src-tit">Local execution</div><p><code>dotnet run --project src/PaymentReconciliationExceptionDesk.Api -- --demo</code> prints the same payment posture used by the public proof surface.</p></div>
          </div>
        </section>
        """
    );

    public static string Sample() => JsonSerializer.Serialize(new
    {
        summary = AnalysisService.Summary(),
        paymentLane = SampleData.PaymentLanes,
        exceptionQueue = SampleData.Payload.Exceptions,
        settlementPosture = SampleData.SettlementPackets,
        sample = SampleData.Payload
    }, JsonOptions);

    private static string VerificationCard(string title) =>
        $$"""<div class="src"><div class="src-name">verification</div><div class="src-tit">{{title}}</div><p>This lane keeps settlement pressure, exception evidence, and commercial framing honest.</p></div>""";

    private static string Metric(string value, string label, string help, string tone) =>
        $$"""<div class="kpi {{tone}}"><div class="v">{{value}}</div><div class="lbl">{{label}}</div><div class="h">{{help}}</div></div>""";

    private static string OwnerForException(string family) => family switch
    {
        "GatewayFee" => "Payment Operations",
        "ACHReturn" => "Treasury Operations",
        "LedgerBreak" => "Payment Operations",
        "OwnerEvidence" => "Finance Controls",
        "CutoffTiming" => "Settlement Governance",
        "CloseSignoff" => "Settlement Governance",
        _ => "Payment Operations"
    };

    private static string SeverityClass(string value) => value switch
    {
        "high" or "red" => "red",
        "medium" or "yellow" => "yellow",
        "green" or "low" => "green",
        _ => "info"
    };

    public static string Layout(string title, string active, string body)
    {
        var nav = new[]
        {
            Nav("/", "Overview"),
            Nav("/payment-lane", "Payment Lane"),
            Nav("/exception-queue", "Exception Queue"),
            Nav("/settlement-posture", "Settlement Posture"),
            Nav("/verification", "Verification"),
            Nav("/docs", "Docs")
        };

        var navHtml = string.Join("", nav.Select(item =>
            item.Href == active
                ? $"""<a class="navchip active" href="{item.Href}">{item.Label}</a>"""
                : $"""<a class="navchip" href="{item.Href}">{item.Label}</a>"""));

        return $$$"""
        <!DOCTYPE html>
        <html lang="en">
        <head>
          <meta charset="utf-8" />
          <meta name="viewport" content="width=device-width, initial-scale=1" />
          <title>{{{title}}}</title>
          <style>
            :root{--bg:#070a0f;--panel:#0b1220;--line:rgba(120,255,170,.18);--line2:rgba(120,255,170,.10);--text:#e9f3ff;--muted:rgba(233,243,255,.72);--muted2:rgba(233,243,255,.55);--bert:#37ff8b;--bert2:#19c7ff;--warn:#ffcc66;--bad:#ff5c7a;--shadow:0 18px 60px rgba(0,0,0,.55);--mono:ui-monospace,SFMono-Regular,Menlo,Monaco,Consolas,"Liberation Mono","Courier New",monospace;--sans:ui-sans-serif,system-ui,-apple-system,Segoe UI,Roboto,Helvetica,Arial}
            *{box-sizing:border-box} body{margin:0;font-family:var(--sans);color:var(--text);background:radial-gradient(1200px 600px at 20% -10%, rgba(55,255,139,.18), transparent 60%),radial-gradient(900px 520px at 90% 0%, rgba(25,199,255,.16), transparent 55%),linear-gradient(180deg,#05070c 0%,#070a0f 35%,#05070c 100%)}
            .wrap{max-width:1280px;margin:0 auto;padding:24px 22px 80px}.topbar{display:flex;justify-content:space-between;gap:14px;border-bottom:1px solid var(--line2);padding-bottom:14px;margin-bottom:22px;font-family:var(--mono);font-size:11px;letter-spacing:.16em;color:var(--muted);text-transform:uppercase}.topbar .left{color:var(--bert)}
            .herorow{display:grid;grid-template-columns:1.5fr .9fr;gap:18px}@media (max-width:1000px){.herorow{grid-template-columns:1fr}}
            .hero,.src,.pcard,.kpi,.bluf,.corr{background:linear-gradient(180deg, rgba(11,18,32,.95), rgba(8,14,26,.92));border:1px solid var(--line);box-shadow:var(--shadow)}
            .hero{border-radius:22px;padding:28px 28px 24px;border-top:2px solid var(--bert2)} .hero h1{font-size:60px;line-height:.95;margin:0 0 18px;font-weight:800}@media (max-width:700px){.hero h1{font-size:42px}} .hero p{color:var(--muted);font-size:15px;line-height:1.55;max-width:680px;margin:0 0 18px}
            .chiprow,.navrow{display:flex;flex-wrap:wrap;gap:8px}.navrow{margin-top:18px}.meta-chip,.navchip,.ppri,.st,code{font-family:var(--mono)} .meta-chip,.navchip{font-size:11px;color:var(--muted);padding:7px 12px;border-radius:999px;border:1px solid var(--line);background:rgba(6,10,18,.4);text-decoration:none}.navchip.active{color:#071017;background:linear-gradient(135deg,var(--bert),var(--bert2));font-weight:700}
            .side{display:flex;flex-direction:column;gap:14px}.bluf,.corr{border-radius:14px;padding:16px 18px}.bluf{border-left:4px solid var(--warn)}.corr{border-left:4px solid var(--bert)}.lbl{font-family:var(--mono);font-size:10px;letter-spacing:.18em;text-transform:uppercase}.bluf .lbl{color:var(--warn)} .corr .lbl{color:var(--bert)} .bluf p,.corr p,.src p,.pcard .pdesc,.kpi .h{color:var(--muted);line-height:1.55}
            .section{margin-top:34px}.sh{display:flex;justify-content:space-between;gap:14px;padding-bottom:10px;border-bottom:1px solid var(--line2);margin-bottom:14px}.sh h2{margin:0;font-size:24px;font-weight:600}.sh .note{font-family:var(--mono);font-size:11px;color:var(--muted2);letter-spacing:.16em;text-transform:uppercase}
            .kpis{display:grid;grid-template-columns:repeat(6,1fr);gap:12px}@media (max-width:1100px){.kpis{grid-template-columns:repeat(3,1fr)}}@media (max-width:640px){.kpis{grid-template-columns:repeat(2,1fr)}} .kpi{border-radius:14px;padding:14px 14px 12px}.kpi .v{font-size:26px;font-weight:600}.kpi .lbl{font-size:10px;letter-spacing:.18em;text-transform:uppercase;color:var(--muted);margin-top:6px}.cyan .v{color:var(--bert2)} .green .v{color:var(--bert)} .plum .v{color:#b88cff} .amber .v,.yellow{color:var(--warn)} .red .v,.red{color:var(--bad)}
            .stack{display:grid;grid-template-columns:repeat(3,1fr);gap:12px}@media (max-width:1100px){.stack{grid-template-columns:repeat(2,1fr)}}@media (max-width:640px){.stack{grid-template-columns:1fr}} .src{border-radius:16px;padding:16px}.src-name{font-family:var(--mono);font-size:11px;color:var(--bert);letter-spacing:.2em;text-transform:uppercase}.src-tit{margin:8px 0 6px;font-size:17px;font-weight:600}
            .depth-grid{display:grid;grid-template-columns:1.1fr .9fr;gap:14px}@media (max-width:900px){.depth-grid{grid-template-columns:1fr}}.depth-card{border:1px solid rgba(25,199,255,.2);border-radius:18px;padding:18px;background:linear-gradient(135deg, rgba(11,18,32,.90), rgba(12,24,42,.68));box-shadow:var(--shadow)}.depth-card h3{margin:8px 0 8px;font-size:24px}.depth-card p,.depth-card li,.step{color:var(--muted);line-height:1.6}.depth-card ul{margin:12px 0 0;padding-left:18px}.depth-card li::marker{color:var(--bert)}.workflow{display:grid;gap:10px;margin-top:12px}.step{border:1px solid var(--line2);border-radius:14px;padding:12px;background:rgba(6,10,18,.35)}.step b{color:var(--text)}
            .ttbl{width:100%;border-collapse:separate;border-spacing:0;border:1px solid var(--line);border-radius:14px;overflow:hidden}.ttbl th,.ttbl td{padding:13px 14px;text-align:left;font-size:13.5px;vertical-align:top}.ttbl thead th{font-family:var(--mono);font-size:11px;letter-spacing:.16em;text-transform:uppercase;color:var(--muted2);border-bottom:1px solid var(--line);background:rgba(11,18,32,.5)}.ttbl td,.ttbl td *{color:var(--muted)}.ttbl b{color:var(--text)}
            .board{display:grid;grid-template-columns:repeat(3,1fr);gap:14px}@media (max-width:1000px){.board{grid-template-columns:1fr}} .pcard{border-radius:16px;padding:18px 20px;display:flex;flex-direction:column}.ptop{display:flex;justify-content:space-between;align-items:center;margin-bottom:8px}.pnum{font-family:var(--mono);font-size:22px;font-weight:600;color:var(--bert)}.ppri{font-size:10px;padding:5px 10px;border-radius:999px;border:1px solid var(--line);color:var(--bert);letter-spacing:.14em;background:rgba(55,255,139,.06)}.pcard h3{margin:6px 0 8px;font-size:19px}.check{list-style:none;padding:0;margin:0 0 14px}.check li{display:grid;grid-template-columns:18px 1fr;gap:10px;padding:6px 0;font-size:13.5px;color:var(--muted)}.check li:before{content:"";width:14px;height:14px;border:1px solid var(--line);border-radius:3px;background:rgba(6,10,18,.4);margin-top:3px}
            .st{font-size:10px;padding:4px 9px;border-radius:6px;letter-spacing:.1em;text-transform:uppercase;border:1px solid currentColor;display:inline-block}.st.green{color:var(--bert)}.st.yellow{color:var(--warn)}.st.red{color:var(--bad)}.st.info{color:var(--bert2)}
            .footer{margin-top:30px;padding-top:14px;border-top:1px dashed var(--line2);display:flex;justify-content:space-between;gap:10px;flex-wrap:wrap;font-family:var(--mono);font-size:11px;color:var(--muted2);letter-spacing:.08em} code{font-size:12px;color:var(--bert2);background:rgba(25,199,255,.08);padding:1px 6px;border-radius:5px;border:1px solid rgba(25,199,255,.18)}
          </style>
        </head>
        <body>
          <div class="wrap">
            <div class="topbar">
              <div class="left">Kinetic Gain · Payment Reconciliation Exception Desk</div>
              <div>synthetic settlement snapshots · no bank, processor, customer, or production ledger secrets</div>
            </div>
            <div class="herorow">
              <section class="hero">
                <div class="chiprow">
                  <span class="meta-chip">FinTech / reconciliation exception lane</span>
                  <span class="meta-chip">C# · payment ops and treasury proof</span>
                  <span class="meta-chip">Hosted preview planned · Embedded by engagement</span>
                </div>
                <h1>Payment batches, returned payouts, and close packets that stay operator-readable.</h1>
                <p>This control plane turns synthetic settlement exports into one review surface: processor fee deltas, returned ACH payouts, merchant ledger breaks, owner accountability, cutoff replay, and final close signoff before reconciliation confidence is asserted.</p>
                <div class="navrow">{{{navHtml}}}</div>
              </section>
              <aside class="side">
                <div class="bluf"><div class="lbl">Commercial front door</div><p><strong>Reconciliation exception routing, settlement evidence posture, and close-safe packet review for payment and finance teams.</strong><br />The free surface is buyer-readable proof; the commercial path is an embedded reconciliation module for FinTech workflows.</p></div>
                <div class="corr"><div class="lbl">Proof layer</div><p><strong>.NET API + static operator shell.</strong><br />The repo models payment operations, treasury returns, finance controls, and settlement signoff without pretending to be a live processor integration.</p></div>
                <div class="corr"><div class="lbl">Why it matters</div><p>Finance teams need settlement pressure and exception evidence in one lane, not scattered bank exports, processor CSVs, and stale close packets.</p></div>
              </aside>
            </div>
            {{{body}}}
            <div class="footer">
              <div>payment-reconciliation-exception-desk · synthetic sample data only</div>
              <div><a href="/docs">Docs</a> · <a href="/verification">Verification</a> · <a href="https://github.com/mizcausevic-dev/payment-reconciliation-exception-desk">Repo</a> · <a href="https://portfolio.kineticgain.com/">Portfolio</a> · <a href="https://suite.kineticgain.com/">Suite</a> · <a href="https://www.linkedin.com/in/miz-causevic/">LinkedIn</a> · <a href="https://kineticgain.com/">Kinetic Gain</a></div>
            </div>
          </div>
        </body>
        </html>
        """;
    }

    private static (string Href, string Label) Nav(string href, string label) => (href, label);
}
