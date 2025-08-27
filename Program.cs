using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Project_Fit_Forge
{
    internal class Program
    {
        enum Menu { DetailsCapture = 1, EligibilityCheck, ShowStats, Exit }

        static int totalMember; //total number of memebers
        static string[] names; //the names of the memebers
        static int[] age; //memeber ages
        static int[] ranks; //member ranks 
        static int[] yearsPresent; // years since joined 
        static int[] smoothiesBought; // totals number pf smoothies initially bought.
        static int[] distances;  // treadmill PB
        static string[] status; // the status of the employeee
        static string[] flavours; // favorite smoothie
        static int[] smoothiesConsumed; // this is the total number of smoothies that are consumed since joining 
        static bool[] qualifies; // eligibility flag ]

        // see wheter eligibility check has been carried out 
        static bool checkedEligibility = false;

        static void Main(string[] args)
        {
          

            // allocate each parallel array to the specified size 

            names = new string[totalMember];
            age = new int[totalMember];
            ranks = new int[totalMember];
            ranks = new int[totalMember];
            yearsPresent = new int[totalMember];
            smoothiesBought = new int[totalMember];
            distances = new int[totalMember];
            status = new string[totalMember];
            flavours = new string[totalMember];
            smoothiesConsumed = new int[totalMember];
            qualifies = new bool[totalMember];

            bool exit = false; // this is what controls the main menu loop

            while (!exit)
            {
                //This is where we display the menu options to the user
                Console.WriteLine("1. Capture member details: ");
                Console.WriteLine("2. Check rewards: ");
                Console.WriteLine("3. FitForge Stats: ");
                Console.WriteLine("4. Exit: ");
                Console.WriteLine("Please select an Option: ");

                int choice;
                //Check whether the input is an interger.
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input . Please we know that it's tempting , but stay between 1-4!");
                    continue; // return back to the menu 
                }
                //First part , Capturing details
                switch ((Menu)choice)
                {
                    case Menu.DetailsCapture:
                        DetailsCapture();
                        checkedEligibility = false;
                        break; // reset when re entering data

                    case Menu.EligibilityCheck: //Second part checking eligibility 
                        EligibilityCheck();
                        checkedEligibility = true; break; // indicate that the check has been done

                    case Menu.ShowStats: // Third Part showing all the statistics and totals.
                        ShowStats();
                        break;

                    case Menu.Exit: // Exit the app
                        exit = true;
                        Console.WriteLine("See you soon soon , stay Fit!");
                        break;

                }
            }
        }

        // First Part:
        // Part one is responsible for storing the details into the parralel arrays.
        static void DetailsCapture()
        {
            for (int i = 0; i < totalMember; i++)
            {
                Console.WriteLine("Member # " + (i + 1));
                Console.WriteLine("Name : ");
                names[i] = Console.ReadLine();

                Console.WriteLine("Age: ");
                age[i] = int.Parse(Console.ReadLine());

                Console.WriteLine("Rank 1-10: ");
                ranks[i] = int.Parse(Console.ReadLine());

                Console.WriteLine("Membership duration: ");
                yearsPresent[i] = int.Parse(Console.ReadLine());

                Console.WriteLine("Smoothies bought: ");
                smoothiesBought[i] = int.Parse(Console.ReadLine());

                Console.WriteLine("Distance covered on treadmill (km) : ");
                distances[i] = int.Parse(Console.ReadLine());

                Console.WriteLine("Employee status: ");
                status[i] = Console.ReadLine();

                Console.WriteLine("Favorite smoothie flavour: ");
                flavours[i] = Console.ReadLine();

                Console.WriteLine("Total amount of smoothies bought since joining: ");
                smoothiesConsumed[i] = int.Parse(Console.ReadLine());

            }
        }
        
        
            //PArt 2 goes here 

            static void EligibilityCheck()
            {
                // Check if any members have been captured
                if (totalMember == 0)
                {
                    Console.WriteLine("No members found. Please capture member details first.");
                    return;
                }

                // Loop through all captured members
                for (int i = 0; i < totalMember; i++)
                {
                    // Check employment status or if under 18
                    bool isEmployed = status[i].ToLower() == "yes";
                    bool under18 = age[i] < 18;
                    bool employmentCheck = isEmployed || under18;

                    // Check membership duration (must be 2 years or more)
                    bool membershipDurationCheck = yearsPresent[i] >= 2;

                    // Check if rank is above 2000
                    bool rankCheck = ranks[i] > 2000;

                    // Check if treadmill distance is more than 20 km
                    bool treadmillCheck = distances[i] > 20;

                    // Combined average of rank and treadmill must also exceed 2000
                    double average = (ranks[i] + distances[i]) / 2.0;
                    bool averageCheck = average > 2000;

                    // At least one of the three fitness conditions must be met
                    bool fitnessCheck = rankCheck || treadmillCheck || averageCheck;

                    // Calculate average smoothies per month (avoid divide by zero)
                    double averageSmoothiesPerMonth = smoothiesConsumed[i] / (yearsPresent[i] * 12.0);

                    // Minimum required: 4/month
                    bool minimumSmoothies = averageSmoothiesPerMonth >= 4;

                    // Maximum allowed: 20/month
                    bool maximumSmoothies = averageSmoothiesPerMonth <= 20;

                    // Favourite flavour must not be "ChocoChurned Chaos"
                    bool flavourCheck = flavours[i].ToLower() != "chocochurned chaos";

                    // FINAL check: all conditions must be met to qualify
                    qualifies[i] = employmentCheck &&
                                   membershipDurationCheck &&
                                   fitnessCheck &&
                                   minimumSmoothies &&
                                   maximumSmoothies &&
                                   flavourCheck;
                }

                Console.WriteLine("Eligibility check complete for all members.");
            }

        
        static void ShowStats()
        {
            //Part 3 goes here 
        }


    }
}
