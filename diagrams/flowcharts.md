```mermaid
flowchart TD
    A([Start]) --> B[Visa meny]
    B -->|1: Lägg till| C[Läs in beskrivning, belopp, kategori, datum]
    C --> D["AddTransaction()"]
    D --> B

    B -->|2: Visa alla| E["ShowAll()"]
    E --> B

    B -->|3: Balans| F["CalculateBalance()"]
    F --> B

    B -->|4: Ta bort| G["DeleteTransaction()"]
    G --> H{Index giltigt?}
    H -->|Ja| I["Ta bort post"]
    H -->|Nej| J["Visa felmeddelande"]
    I --> B
    J --> B

    B -->|5: Avsluta| K([Slut])

    %% Bonus
    B -->|6: Filtrera| L["GetByCategory()"]
    L --> B
    B -->|7: Statistik| M["GetStatistics()"]
    M --> B
