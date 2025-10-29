# DailyJournalSystem (.NET 8)

This fixed build reverts password policy to default Identity settings and adds a layout fix
to render optional Scripts sections.

* Target: .NET 8.0 (LTS)
* Default Identity password policy (standard ASP.NET Core Identity)
* Layout includes @RenderSection("Scripts", required: false)
