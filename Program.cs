using Newtonsoft.Json;
using System.Linq;
using System.Text.Json;

namespace API_PART1_TEST
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            int randomItem = new Random().Next(1, 10000);
            List<string> characters = new List<string>();
            List<string> itemsFound = new List<string>();
            Dictionary<string, List<string>> characterDictionary = new Dictionary<string, List<string>>();
            string charName = "";
            Player player = new Player(charName);
            int intefacerSelect = 0;
            itemsFound.Clear();
            characterDictionary.Clear();
            characters.Clear();

            do
            {
                Console.Clear();
                FFItnerface();
                Console.Write("What option would you like to select? ");
                intefacerSelect = Int32.Parse(Console.ReadLine());

                switch (intefacerSelect)
                {
                    case 1:
                        Console.Clear();
                        Console.Write("Would you like to make a character? ");
                        string responseYOrN = Console.ReadLine().ToLower();

                        if (responseYOrN == "yes")
                        {
                            Console.Write("Please enter in a name to make your character: ");
                            charName = Console.ReadLine();
                            itemsFound.Clear();

                            if (characterDictionary.ContainsKey(charName))
                            {
                                Console.WriteLine("This player currently exists!");
                                while (characterDictionary.ContainsKey(charName))
                                {
                                    Console.Write("Please enter in another name to make your character: ");
                                    charName = Console.ReadLine();
                                }
                            }
                        }
                        else if (responseYOrN == "no")
                        {
                            itemsFound.Clear();
                            Console.WriteLine($"------------------------------Character List-----------------------------\n");

                            int p = 0;
                            foreach (string c in characters)
                            {
                                Console.WriteLine($"Character {p}: {c}\n");
                                p++;
                            }
                            Console.WriteLine("What character would you like to play as one of these characters?");
                            charName = Console.ReadLine();
                            foreach (var c in characterDictionary[charName])
                            {
                                Console.WriteLine("------------------------------------------CURRENT ITEMS------------------------------------");
                                Console.WriteLine(c +"\n");
                                Console.Write("Please press enter to continue ");
                                Console.ReadLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: Invalid Input!");
                        }
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Would you like to explore?");
                        Console.Write("Enter 0 for yes or 1 for no: ");
                        int yON = Int32.Parse(Console.ReadLine());

                        while (yON < 0 || yON > 1)
                        {
                            Console.WriteLine("Error: Invalid Input!");
                            Console.WriteLine("Please enter in a valid Input!");

                            Console.WriteLine("Would you like to explore?");
                            Console.Write("Enter 0 for yes or 1 for no and to see your current amount of items: ");
                            yON = Int32.Parse(Console.ReadLine());
                        }

                        if (yON == 0)
                        {
                            while (yON == 0)
                            {
                                using var httpClient = new HttpClient();
                                string apiUrl = $"https://xivapi.com/Item/{randomItem}";

                                try
                                {
                                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                                    if (response.IsSuccessStatusCode)
                                    {
                                        Console.Clear();

                                        string content = await response.Content.ReadAsStringAsync();

                                        dynamic item = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                                        FFItems ff = JsonConvert.DeserializeObject<FFItems>(content);

                                        string foundItem = $"{ff.ToString()} \n";
                                        Console.WriteLine(foundItem);
                                        itemsFound.Add(foundItem);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Error: {response.StatusCode}");
                                    }

                                    Console.WriteLine("Would you like to continue your exploration?");
                                    Console.Write("Enter 0 for yes or 1 for no: ");
                                    yON = Int32.Parse(Console.ReadLine());
                                    randomItem = new Random().Next(1, 10000);
                                }
                                catch (HttpRequestException e)
                                {
                                    Console.WriteLine($"HTTP Request Error: {e.Message}");
                                }
                            }
                        }
                        break;
                    case 3:
                        
                        if (!characterDictionary.ContainsKey(charName))
                        {
                            characterDictionary.TryAdd(charName, itemsFound);
                        }
                        characters.Add(charName);

                        if (characterDictionary.ContainsKey(charName))
                        {
                            foreach (var v in itemsFound)
                            {
                                characterDictionary[charName].Append(v);
                            }
                        }

                        Console.Clear();

                        Console.WriteLine($"-----------------------------------{charName}----------------------------------------\n\n\n");
                        Console.WriteLine("----------------------------------ITEMS FOUND---------------------------------------");

                        foreach (var f in characterDictionary[charName])
                        {
                            Console.WriteLine(f);
                        }

                        if (itemsFound.Count() < 1)
                        {
                            Console.WriteLine("\nYou Currently have found no items!\n");
                        }

                        Console.WriteLine("------------------------------------------------------------------------------------\n\n\n");
                        Console.Write("Press enter to continue ");
                        Console.ReadLine();
                        break;
                }
            }
            while (intefacerSelect != 4); 
        }

        public static void FFItnerface()
        {
            string msg = "";
            msg += "[1] Create Character\n";
            msg += "[2] Search for Items\n";
            msg += "[3] View current Items\n";
            msg += "[4] Exit Menu";

            Console.WriteLine(msg);
        }
    }
}