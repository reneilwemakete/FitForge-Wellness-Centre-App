# FitForge Wellness Centre App

A C# .NET console application for capturing gym member details, evaluating reward eligibility, and viewing membership statistics.

## Features

* Capture multiple members with input validation and add more members during the same session.
* Evaluate rewards using the eligibility rules implemented in the original assignment.
* View member totals, averages, smoothie totals, treadmill distance and rewards eligibility.
* Run automated regression checks through GitHub Actions.

## Run locally

Install the .NET 8 SDK, then:

```bash
dotnet run --project FitForge.csproj
```

To run the regression checks:

```bash
dotnet run --project tests/FitForge.Tests.csproj
```

## Rewards rules

A member must meet **all** the following checks:

1. They are employed or under 18.
2. They have been a member for at least two completed years.
3. Their rank exceeds 2000, **or** treadmill distance exceeds 20 km, **or** the average of rank and distance exceeds 2000.
4. Their lifetime smoothie purchases average between 4 and 20 per membership month, inclusive.
5. Their favourite flavour is not "ChocoChurned Chaos" (case-insensitive).

The initial code labelled rank as "1-10", which contradicts its rank >2000 calculation. The prompt now says nonnegative fitness score to preserve existing eligibility behavior. **Please compare this threshold to the attached original assignment PDF before treating the rules as a final academic specification.**

## Notes

Member information exists only in memory and disappears when the program closes. Rewards are recalculated from current member records. The statistics display clearly indicates when eligibility has not been checked since the last capture. This is an educational project, not a production gym management system.

## Technologies

C#, .NET 8, LINQ, collections, record models, defensive input handling, GitHub Actions.
