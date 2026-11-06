using Project_Fit_Forge;

static void Check(bool condition, string description)
{
    if (!condition) throw new Exception("FAILED: " + description);
    Console.WriteLine("PASS: " + description);
}

var eligible = new Member("Demo One", 25, 2500, 2, 3, 25, true, "Vanilla", 120);
Check(Rewards.IsEligible(eligible), "Eligible member accepted");
Check(!Rewards.IsEligible(eligible with { YearsPresent = 0 }), "Zero membership duration safely rejected");
Check(!Rewards.IsEligible(eligible with { YearsPresent = 1 }), "Less than two years rejected");
Check(!Rewards.IsEligible(eligible with { FavouriteSmoothie = "chocochurned chaos" }), "Excluded flavour rejected case-insensitively");
Check(!Rewards.IsEligible(eligible with { SmoothiesConsumed = 10 }), "Below four smoothies per month rejected");
Check(!Rewards.IsEligible(eligible with { SmoothiesConsumed = 500 }), "Above twenty smoothies per month rejected");
Check(!Rewards.IsEligible(eligible with { Rank = 2000, TreadmillDistanceKm = 20 }), "Fitness threshold is strict");
Check(Rewards.IsEligible(eligible with { Rank = 0, TreadmillDistanceKm = 21 }), "Distance fitness condition accepted");
Check(!Rewards.IsEligible(eligible with { IsEmployed = false }), "Unemployed adult rejected");
Check(Rewards.IsEligible(eligible with { IsEmployed = false, Age = 17 }), "Under-18 exception retained");
var empty = Statistics.Calculate(Array.Empty<Member>());
Check(empty.MemberCount == 0 && empty.EligibilityRate == 0, "Empty statistics are safe");
var summary = Statistics.Calculate(new[] { eligible, eligible with { Name = "Demo Two", Age = 35, SmoothiesBought = 5, SmoothiesConsumed = 10 } });
Check(summary.MemberCount == 2 && summary.AverageAge == 30, "Member count and average age");
Check(summary.TotalVisitSmoothies == 8 && summary.TotalLifetimeSmoothies == 130, "Smoothie totals");
Check(summary.EligibleCount == 1 && summary.EligibilityRate == 50, "Eligibility summary");
Console.WriteLine("All FitForge regression checks passed.");
