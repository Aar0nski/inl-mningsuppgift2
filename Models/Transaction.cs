namespace personalBudgetTracker.Models
{
    public class Transaction
    {
        // Egenskaper som har public getters och setters, detta gör att vi kan skapa och ändra transaktioner.
        // Get setter används för att hämta och sätta värden på egenskaperna.
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public string Date { get; set; }

        // Konstruktor för att skapa en ny transaktion med alla nödvändiga detaljer. Konstruktorn tar emot parametrar för att sedan spara dom i klassers egna egenskaper.
        public Transaction(string description, decimal amount, string category, string date)
        {
            Description = description;
            Amount = amount;
            Category = category;
            Date = date;
        }
        // Metod för att visa information om transaktionen i ett läsbart format.
        public void ShowInfo()
        {
            Console.WriteLine($"{Date} | {Description} | {Category} | {Amount} kr");
        }
    }
}