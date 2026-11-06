namespace Project_Fit_Forge;

/// <summary>Member data is held in memory for this educational demonstration.</summary>
public sealed record Member(
    string Name,
    int Age,
    int Rank,
    int YearsPresent,
    int SmoothiesBought,
    int TreadmillDistanceKm,
    bool IsEmployed,
    string FavouriteSmoothie,
    int SmoothiesConsumed);

public static class Rewards
{
    /// <summary>
    /// Retains the original assignment implementation's eligibility rules.
    /// The source used rank above 2000 despite an incorrect input label of "Rank 1-10".
    /// </summary>
    public static bool IsEligible(Member member)
    {
        ArgumentNullException.ThrowIfNull(member);
        if (member.YearsPresent < 2) return false;

        bool employmentCheck = member.IsEmployed || member.Age < 18;
        bool fitnessCheck = member.Rank > 2000
            || member.TreadmillDistanceKm > 20
            || ((double)member.Rank + member.TreadmillDistanceKm) / 2 > 2000;
        double smoothiesPerMonth = member.SmoothiesConsumed / (member.YearsPresent * 12.0);
        bool smoothiesCheck = smoothiesPerMonth >= 4 && smoothiesPerMonth <= 20;
        bool flavourCheck = !string.Equals(
            member.FavouriteSmoothie.Trim(), "ChocoChurned Chaos",
            StringComparison.OrdinalIgnoreCase);

        return employmentCheck && fitnessCheck && smoothiesCheck && flavourCheck;
    }
}

public sealed record MemberStatistics(
    int MemberCount,
    double AverageAge,
    double AverageYears,
    long TotalVisitSmoothies,
    long TotalLifetimeSmoothies,
    int MaxTreadmillDistance,
    int EligibleCount)
{
    public double EligibilityRate => MemberCount == 0 ? 0 : EligibleCount * 100.0 / MemberCount;
}

public static class Statistics
{
    public static MemberStatistics Calculate(IReadOnlyCollection<Member> members)
    {
        ArgumentNullException.ThrowIfNull(members);
        if (members.Count == 0) return new MemberStatistics(0, 0, 0, 0, 0, 0, 0);
        return new MemberStatistics(
            members.Count,
            members.Average(m => m.Age),
            members.Average(m => m.YearsPresent),
            members.Sum(m => (long)m.SmoothiesBought),
            members.Sum(m => (long)m.SmoothiesConsumed),
            members.Max(m => m.TreadmillDistanceKm),
            members.Count(Rewards.IsEligible));
    }
}
