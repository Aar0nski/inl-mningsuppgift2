using personalBudgetTracker.Models;

namespace personalbudgettracker.Services
{
    public class BudgetManager
    {
        // Lista för att lagra transaktioner. List<Transaction> är en generisk lista som kan hålla objekt av typen Transaction. new() är en kort syntax för att skapa en ny instans av List<Transaction>.
        private readonly List<Transaction> transactions = new();

        // Metod för att lägga till en ny transaktion i listan.
        public void AddTransaction(Transaction t)
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
            return transactions.Sum(t => t.Amount); // retunerar summan av alla transaktioners belopp genom att använda LINQ-metoden Sum som tar en lambda-uttryck t => t.Amount för att specificera att vi vill summera Amount-egenskapen för varje transaktion t i listan.
        }
        // Metod för att ta bort en transaktion baserat på dess index i listan. Returnerar true om borttagningen lyckades, annars false.
        public bool DeleteTransaction(int index) // bool indikerar om borttagningen lyckades eller inte med hjälp av ett sant/falskt värde.
        {
            if (index < 0 || index >= transactions.Count) // Kontrollera om indexet är utanför listans gränser genom att jämföra det med 0 och listans längd.
                return false;

            transactions.RemoveAt(index); // Tar bort transaktionen vid det angivna indexet från listan med hjälp av RemoveAt-metoden.
            return true;
        }
    }
}
