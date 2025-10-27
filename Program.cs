using PersonalBudgetTracker.Models;
﻿using personalbudgettracker.Services;
using personalBudgetTracker.Models;
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
                       manager.ShowStats();
                        Pause();
                        break;
                    case "6":
                        ShowByCategoryFlow(manager);
                        break;
                    case "7":
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
            Console.WriteLine("Lägg till transaktion");

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

            var t = new BudgetTransaction(desc, amount, cat, date); // skapar en ny transaktion med användarens inmatning. Var t är en instans av Transaction-klassen.
            manager.AddTransaction(t);

            Console.WriteLine("Transaktion tillagd!");
            Pause();
        }
        // En metod för atta visa alla transaktioner.
        static void ShowAllFlow(BudgetManager manager)
        {
            Console.WriteLine("Alla transaktioner:");
            manager.ShowAll();
            Pause(); // pausar programmet tills användaren trycker på en tangent.
        }

        static void ShowBalanceFlow(BudgetManager manager)
        {
            Console.WriteLine("Total balans:");
            decimal balance = manager.CalculateBalance();
            Console.WriteLine($"{balance} kr");
            Pause();
        }
        // en metod för att ta bort en transaktion.
        static void DeleteTransactionFlow(BudgetManager manager)
        {
            Console.WriteLine("Ta bort transaktion");
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
        static void ShowByCategoryFlow(BudgetManager manager)
        {
            Console.WriteLine("Visa transaktioner efter kategori");
            Console.Write("Ange kategori: ");
            string category = Console.ReadLine();
            manager.ShowByCategory(category);
            Pause();
        }

        // En metod för att skriva ut menyn.

        static void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Personal Budget Tracker ===");
            Console.WriteLine("1) Lägg till transaktion");
            Console.WriteLine("2) Visa alla transaktioner");
            Console.WriteLine("3) Visa total balans");
            Console.WriteLine("4) Ta bort transaktion");
            Console.WriteLine("5) Visa statistik");
            Console.WriteLine("6) Visa transaktioner efter kategori");
            Console.WriteLine("7) Avsluta");
            Console.WriteLine();
        }
        // En metod för att pausa programmet tills användaren trycker på en tangent.
        static void Pause()
        {
            Console.WriteLine("Tryck valfri tangent för att fortsätta...");
            Console.ReadKey(true);
        }
    }
}