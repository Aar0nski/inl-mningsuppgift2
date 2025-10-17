using personalbudgettracker.Models;
using personalbudgettracker.Services;
using System.Globalization;

namespace personalbudgettracker
{
    internal class Program
    {
        static void Main()
        {
            // Skapa en instans av BudgetManager och starta menyn.
            var manager = new BudgetManager();
            RunMenu(manager);
        }

        // En loop som visar menyn och hanterar användarens val.
        static void RunMenu(BudgetManager manager) // runmenu tar en parameter av typen BudgetManager.
        {
            bool running = true; // en bool som styr loopen. den är true så länge programmet körs.

            while (running) // så länge running är true körs loopen.
            {
                PrintMenu(); // anropar metoden Print Menu för att visa menyn.
                Console.Write("Välj ett alternativ (1–5): ");
                string? choice = Console.ReadLine();

                switch (choice) // switch sats som hanterar användarens val.
                {
                    case "1":
                        AddTransactionFlow(manager);
                        break;
                    case "2":
                        ShowAllFlow(manager);
                        break;
                    case "3":
                        ShowBalanceFlow(manager);
                        break;
                    case "4":
                        DeleteTransactionFlow(manager);
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val. Försök igen.");
                        Pause();
                        break;
                }
            }
        }

        // En metod för att lägga till en transaktion.

        static void AddTransactionFlow(BudgetManager manager)
        {
            Console.WriteLine("\n➕ Lägg till transaktion");

            Console.Write("Beskrivning: ");
            string desc = Console.ReadLine();

            Console.Write("Belopp (positivt = inkomst, negativt = utgift): ");
            // En if-sats som försöker konvertera användarens inmatning till en decimal. Annars skrivs ett felmeddelande ut.
            if (!decimal.TryParse(Console.ReadLine(), NumberStyles.Number, CultureInfo.CurrentCulture, out decimal amount))
            {
                Console.WriteLine("Fel: belopp måste vara numeriskt.");
                Pause();
                return;
            }

            Console.Write("Kategori: ");
            string cat = Console.ReadLine();

            Console.Write("Datum (YYYY-MM-DD): ");
            string date = Console.ReadLine();

            var t = new Transaction(desc, amount, cat, date); // skapar en ny transaktion med användarens inmatning. Var t är en instans av Transaction-klassen.
            manager.AddTransaction(t);

            Console.WriteLine("Transaktion tillagd!");
            Pause();
        }
        // En metod för atta visa alla transaktioner.
        static void ShowAllFlow(BudgetManager manager)
        {
            Console.WriteLine("\n📋 Alla transaktioner:");
            manager.ShowAll();
            Pause(); // pausar programmet tills användaren trycker på en tangent.
        }

        static void ShowBalanceFlow(BudgetManager manager)
        {
            Console.WriteLine("\n💰 Total balans:");
            decimal balance = manager.CalculateBalance();
            Console.WriteLine($"{balance} kr");
            Pause();
        }
        // en metod för att ta bort en transaktion.
        static void DeleteTransactionFlow(BudgetManager manager)
        {
            Console.WriteLine("\n🗑️ Ta bort transaktion");
            manager.ShowAll();

            Console.Write("Ange index att ta bort: ");
            if (int.TryParse(Console.ReadLine(), out int idx))
            {
                bool ok = manager.DeleteTransaction(idx);
                Console.WriteLine(ok ? "Transaktion togs bort." : "Ogiltigt index.");
            }
            else
            {
                Console.WriteLine("Ange ett heltal.");
            }
            Pause();
        }

        // Em metod för att skriva ut menyn.

        static void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Personal Budget Tracker ===");
            Console.WriteLine("1) Lägg till transaktion");
            Console.WriteLine("2) Visa alla transaktioner");
            Console.WriteLine("3) Visa total balans");
            Console.WriteLine("4) Ta bort transaktion");
            Console.WriteLine("5) Avsluta");
            Console.WriteLine();
        }
        // En metod för att pausa programmet tills användaren trycker på en tangent.
        static void Pause()
        {
            Console.WriteLine("\nTryck valfri tangent för att fortsätta...");
            Console.ReadKey(true);
        }
    }
}