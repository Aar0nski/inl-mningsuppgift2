```mermaid
classDiagram
    class Transaction {
        +string Description
        +decimal Amount
        +string Category
        +string Date
        +void ShowInfo()
    }

    class BudgetManager {
        -List~Transaction~ transactions
        +void AddTransaction(Transaction t)
        +void ShowAll()
        +decimal CalculateBalance()
        +bool DeleteTransaction(int index)
        +void ShowByCategory(string category)
        +void ShowStats()
    }

    BudgetManager --> "1..*" Transaction : hanterar
