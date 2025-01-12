using System.Data;
using Server;
using Server.DataBase;

new MtcgServer().Start();

return 0;

string request = @"SELECT
    c.card_id,
    c.card_name,
    s.species_name,
    r.rarity_name,
    e.element_name,
    c.attack,
    c.defense,
    c.created_at,
    c.updated_at
FROM 
    cards c
JOIN 
    species s ON c.species_id = s.species_id
JOIN 
    rarities r ON c.rarity_id = r.rarity_id
JOIN 
    elements e ON c.element_id = e.element_id
ORDER BY
    c.card_id";

DataTable table = await DataBaseManager.ExecutePaginatedQueryAsync(request, 10, 2);

foreach (DataRow row in table.Rows)
{
    foreach (object? item in row.ItemArray)
    {
        Console.Write(item + "\t");
    }
    Console.WriteLine();
}