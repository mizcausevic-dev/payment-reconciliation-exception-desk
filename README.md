# payment-reconciliation-exception-desk

C# / ASP.NET operator surface for payment reconciliation exceptions, settlement blockers, close-safe packet readiness, and finance-safe review posture.

## Why this matters

Finance teams do not need another dashboard that says payments are healthy while ledger mismatches, returned payouts, and settlement evidence are still unresolved. They need a board that keeps batch pressure, exception routing, and close readiness visible together before month-end signoff becomes guesswork.

This repo is the public proof surface for that pattern:

- `Hosted preview planned` for a browser-based reconciliation exception desk
- `Embedded by engagement` for teams that need the routing model inside payments, treasury, or revenue operations workflows

## What it includes

- ASP.NET Core minimal API in C#
- synthetic payment batches, reconciliation exceptions, and settlement packets
- operator surfaces for:
  - `/payment-lane`
  - `/exception-queue`
  - `/settlement-posture`
  - `/verification`
  - `/docs`
- structured JSON endpoints under `/api/*`
- static Pages export with `robots.txt`, `sitemap.xml`, and `CNAME`

## Screenshots

![Overview](./screenshots/01-overview.svg)
![Payment lane](./screenshots/02-payment-lane.svg)
![Settlement posture](./screenshots/03-settlement-posture.svg)

## Verification

- synthetic payment reconciliation and settlement evidence only
- no bank account, processor, customer, or production ledger data
- no claim of PCI DSS, SOC 1, SOC 2, ISO 27001, or compliance certification
- this is a control-plane proof surface for FinTech workflow depth, not a compliance certification claim

## Local run

```powershell
dotnet test
dotnet run --project src/PaymentReconciliationExceptionDesk.Api -- --demo
dotnet run --project src/PaymentReconciliationExceptionDesk.Api
```

Then open:

- `http://127.0.0.1:5088/`
- `http://127.0.0.1:5088/payment-lane`
- `http://127.0.0.1:5088/exception-queue`
- `http://127.0.0.1:5088/settlement-posture`

## Render static site

```powershell
dotnet run --project src/PaymentReconciliationExceptionDesk.Api -- --prerender
```

## Related docs

- [Embedded framing](./docs/KINETIC_GAIN_EMBEDDED.md)
- [Origin story](./docs/ORIGIN.md)
