namespace registratie_app.Gegevens;
using ExcelDataReader; 
using System.Data;
using System.Text;

public class ExcelImport
{
    public void LeesExcelBestand(string bestandspad)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        if (!File.Exists(bestandspad))
        {
            Console.WriteLine("Het bestand bestaat niet.");
            return; 
        }

        try
        {
            using var stream = File.Open(bestandspad, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var reader = ExcelReaderFactory.CreateReader(stream);

            int rijnummer = 0;
            while (reader.Read())
            {
                rijnummer++;
                if (reader.IsDBNull(0))
                {
                    continue;
                }

                string regel = reader.GetValue(0)?.ToString() ?? "";
                if (string.IsNullOrWhiteSpace(regel))
                {
                    continue;
                }
                string[] gegevens = regel.Split(',');

                Console.WriteLine($"\nregel {rijnummer}: ");
                for (int i = 0; i < gegevens.Length; i++)
                {
                    Console.WriteLine(
                        $"Veld {i + 1} : {gegevens[i].Trim()}"); 
                }
            }

            Console.WriteLine("\nEcxel-bestand is ingelezen.");
        }
        catch (Exception fout)
        {
            Console.WriteLine(
                $"Er is een fout opgetreden bij het lezen: {fout.Message}");
        }
    }
}