using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter the number of cities in the model: ");
        int numCities = int.Parse(Console.ReadLine());

        string[] cityNames = new string[numCities];
        int[] cityContacts = new int[numCities];
        int[] covidLevels = new int[numCities];

        for (int i = 0; i < numCities; i++)
        {
            Console.WriteLine($"\nCity {i}:");
            Console.Write("Enter the city name: ");
            cityNames[i] = Console.ReadLine();

            Console.Write("Enter the number of cities in contact: ");
            int numContacts = int.Parse(Console.ReadLine());

            while (true)
            {
                Console.Write("Enter the city ID in contact: ");
                int cityID = int.Parse(Console.ReadLine());

                if (cityID >= 0 && cityID < numCities && cityID != i && cityContacts[cityID] == 0)
                {
                    cityContacts[i] = cityID;
                    cityContacts[cityID] = i;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid ID. Please enter again.");
                }
            }
        }

        while (true)
        {
            Console.WriteLine("\nCity Details:");
            for (int i = 0; i < numCities; i++)
            {
                Console.WriteLine($"City ID: {i} | City Name: {cityNames[i]} | COVID-19 Level: {covidLevels[i]}");
            }

            Console.WriteLine("\nEnter an event (Outbreak, Vaccinate, Lockdown, Spread, Exit): ");
            string eventName = Console.ReadLine();

            if (eventName == "Outbreak" || eventName == "Vaccinate" || eventName == "Lockdown")
            {
                Console.Write("Enter the city ID where the event occurred: ");
                int cityID = int.Parse(Console.ReadLine());

                if (cityID >= 0 && cityID < numCities)
                {
                    if (eventName == "Outbreak")
                    {
                        covidLevels[cityID] = Math.Min(covidLevels[cityID] + 2, 3);
                        covidLevels[cityContacts[cityID]] = Math.Min(covidLevels[cityContacts[cityID]] + 1, 3);
                    }
                    else if (eventName == "Vaccinate")
                    {
                        covidLevels[cityID] = 0;
                    }
                    else if (eventName == "Lockdown")
                    {
                        covidLevels[cityID] = Math.Max(covidLevels[cityID] - 1, 0);
                        covidLevels[cityContacts[cityID]] = Math.Max(covidLevels[cityContacts[cityID]] - 1, 0);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid city ID.");
                }
            }
            else if (eventName == "Spread")
            {
                for (int i = 0; i < numCities; i++)
                {
                    if (covidLevels[cityContacts[i]] > covidLevels[i])
                    {
                        covidLevels[i]++;
                    }
                }
            }
            else if (eventName == "Exit")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid event.");
            }
        }
    }
}