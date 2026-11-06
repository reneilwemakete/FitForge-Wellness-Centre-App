using Project_Fit_Forge;

var members = new List<Member>();
bool eligibilityChecked = false;

while (true)
{
    Console.WriteLine("\n=== FITFORGE WELLNESS CENTRE ===");
    Console.WriteLine("1. Capture member details");
    Console.WriteLine("2. Check rewards eligibility");
    Console.WriteLine("3. FitForge statistics");
    Console.WriteLine("4. Exit");
    int option = Input.ReadInt("Choose an option: ", 1, 4);
    switch (option)
    {
        case 1:
            int count = Input.ReadInt("How many new members would you like to capture? ", 1, 1000);
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\nMember {i + 1} of {count}");
                var name = Input.ReadRequired("Name: ");
                int age = Input.ReadInt("Age: ", 0, 120);
                int rank = Input.ReadInt("Fitness rank (nonnegative score): ", 0, int.MaxValue);
                int years = Input.ReadInt("Membership duration (completed years): ", 0, 120);
                int smoothiesBought = Input.ReadInt("Smoothies bought during current visit: ", 0, int.MaxValue);
                int distance = Input.ReadInt("Treadmill distance (km): ", 0, int.MaxValue);
                bool employed = Input.ReadYesNo("Employed (yes/no): ");
                string flavour = Input.ReadRequired("Favourite smoothie flavour: ");
                int totalSmoothies = Input.ReadInt("Total smoothies bought since joining: ", 0, int.MaxValue);
                members.Add(new Member(name, age, rank, years, smoothiesBought, distance, employed, flavour, totalSmoothies));
            }
            eligibilityChecked = false;
            Console.WriteLine($"Saved {count} member(s). Total members: {members.Count}.");
            break;
        case 2:
            if (members.Count == 0)
            {
                Console.WriteLine("No members found. Capture member details first.");
                break;
            }
            eligibilityChecked = true;
            Console.WriteLine("\nREWARDS ELIGIBILITY");
            foreach (Member member in members)
            {
                Console.WriteLine($"{member.Name}: {(Rewards.IsEligible(member) ? "Eligible" : "Not eligible")}");
            }
            break;
        case 3:
            if (members.Count == 0)
            {
                Console.WriteLine("No member statistics available. Capture members first.");
                break;
            }
            if (!eligibilityChecked)
            {
                Console.WriteLine("Eligibility has not been checked since the latest member capture. Run option 2 to include eligibility statistics.");
            }
            var stats = Statistics.Calculate(members);
            Console.WriteLine("\nFITFORGE STATISTICS");
            Console.WriteLine($"Total members: {stats.MemberCount}");
            Console.WriteLine($"Average age: {stats.AverageAge:F1}");
            Console.WriteLine($"Average years of membership: {stats.AverageYears:F1}");
            Console.WriteLine($"Total smoothies bought at current visits: {stats.TotalVisitSmoothies}");
            Console.WriteLine($"Total smoothies since joining: {stats.TotalLifetimeSmoothies}");
            Console.WriteLine($"Longest treadmill distance: {stats.MaxTreadmillDistance} km");
            if (eligibilityChecked)
                Console.WriteLine($"Rewards eligible: {stats.EligibleCount} ({stats.EligibilityRate:F1}%)");
            break;
        case 4:
            Console.WriteLine("See you soon. Stay fit!");
            return;
    }
}
