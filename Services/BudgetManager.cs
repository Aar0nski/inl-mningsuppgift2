using PersonalBudgetTracker.Models;
using System.Transactions;

namespace personalbudgettracker.Services
{
    public class BudgetManager
    {
        // Lista för att lagra transaktioner. List<Transaction> är en generisk lista som kan hålla objekt av typen Transaction. new() är en kort syntax för att skapa en ny instans av List<Transaction>.
        private readonly List<BudgetTransaction> transactions = new();

        // Metod för att lägga till en ny transaktion i listan.
        public void AddTransaction(BudgetTransaction t)
        {
            transactions.Add(t);
        }

        // Metod för att visa alla transaktioner i listan. Om listan är tom, visas ett meddelande annars visas varje transaktion med dess index.
        public void ShowAll()
        {
            if (transactions.Count == 0)
            {
                Console.WriteLine("Inga transaktioner ännu.");
                return;
            }

            for (int i = 0; i < transactions.Count; i++) // Loopar genom varje transaktion i listan. int i används som en räknare som börjar från 0 och ökar med 1 för varje iteration tills den når antalet transaktioner i listan.
            {
                Console.Write($"{i}. ");
                transactions[i].ShowInfo();
            }
        }
        // Metod för att beräkna och returnera den totala balansen genom att summera alla transaktioners belopp.
        public decimal CalculateBalance()
        {
            return transactions.Sum(t => t.Amount); 
        } 

        // Metod för att ta bort en transaktion baserat på dess index i listan. Returnerar true om borttagningen lyckades, annars false.
        public bool DeleteTransaction(int index) 
        {
            if (index < 0 || index >= transactions.Count)
                return false;

            transactions.RemoveAt(index); 
            return true;
        }
        public void ShowByCategory(string category)
        {
            // Filtrerar ut transaktioner som matchar kategorin (ignorerar stora/små bokstäver)
            var filtered = transactions
                .Where(t => t.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (filtered.Count == 0)
            {
                Console.WriteLine($"Inga transaktioner hittades för kategorin \"{category}\".");
                return;
            }

            Console.WriteLine($"\nTransaktioner i kategorin \"{category}\":");
            Console.WriteLine("----------------------------------------");

            foreach (var t in filtered)
            {
                t.ShowInfo(); // Använder redan färger för belopp
            }

            Console.WriteLine("----------------------------------------");
        }
        public void ShowStats()
        {
            if (transactions.Count == 0)
            {
                Console.WriteLine("Inga transaktioner ännu.");
                return;
            }

            decimal totalIncome = transactions
                .Where(t => t.Amount > 0)
                .Sum(t => t.Amount);

            decimal totalExpense = transactions
                .Where(t => t.Amount < 0)
                .Sum(t => t.Amount);

            decimal balance = CalculateBalance();

            Console.WriteLine("Statistik:");
            Console.WriteLine("--------------------------------");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Totala inkomster: {totalIncome} kr");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Totala utgifter: {totalExpense} kr");

            Console.ResetColor();
            Console.WriteLine("--------------------------------");
            Console.Write("Total balans: ");
            Console.ForegroundColor = balance >= 0 ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine($"{balance} kr");
            Console.ResetColor();
            Console.WriteLine("--------------------------------");

            Console.WriteLine($"Antal transaktioner: {transactions.Count}");
        }
    }
}
