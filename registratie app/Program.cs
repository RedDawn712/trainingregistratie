
using registratie_app.Gegevens;

Console.WriteLine("RvIHH Trainingregistratie");
Console.WriteLine("Aanmeldingen en aanwezigheid registreren");

CsvAanmeldingenImport import = new CsvAanmeldingenImport();
RegistratieOpslag opslag = new RegistratieOpslag();

List<Deelname> deelnames = new List<Deelname>();

while (true)
{
    Console.WriteLine();
    Console.WriteLine("===== MENU =====");
    Console.WriteLine("1. CSV-bestand importeren");
    Console.WriteLine("2. Deelnames bekijken");
    Console.WriteLine("3. Aanwezigheid registreren");
    Console.WriteLine("4. Aanwezige zonder aanmelding toevoegen");
    Console.WriteLine("5. Registraties opslaan");
    Console.WriteLine("6. Registraties laden");
    Console.WriteLine("0. Afsluiten");
    Console.Write("Maak een keuze: ");

    string? keuze = Console.ReadLine();

    try
    {
        switch (keuze)
        {
            case "1":
            {
                Console.Write("Pad naar CSV-bestand: ");
                string pad = Console.ReadLine()?.Trim().Trim('"') ?? "";

                if (string.IsNullOrWhiteSpace(pad))
                {
                    Console.WriteLine("Geen bestand opgegeven.");
                    break;
                }

                List<Deelname> ingelezen = import.LeesBestand(pad);

                // Een nieuwe import vervangt de huidige lijst.
                deelnames = ingelezen;

                Console.WriteLine(
                    $"{deelnames.Count} aanmeldingen ingelezen.");

                break;
            }

            case "2":
            {
                ToonDeelnames(deelnames);
                break;
            }

            case "3":
            {
                ToonDeelnames(deelnames);

                if (deelnames.Count == 0)
                {
                    break;
                }

                Console.Write("Nummer van de deelname: ");

                if (!int.TryParse(Console.ReadLine(), out int nummer) ||
                    nummer < 1 ||
                    nummer > deelnames.Count)
                {
                    Console.WriteLine("Ongeldig nummer.");
                    break;
                }

                Console.WriteLine();
                Console.WriteLine("Kies een status:");
                Console.WriteLine("1. Aanwezig");
                Console.WriteLine("2. Afwezig zonder afmelding");
                Console.WriteLine("3. Afwezig met afmelding");
                Console.WriteLine("4. Aanwezig zonder aanmelding");
                Console.Write("Status: ");

                Aanwezigheidsstatus? status = Console.ReadLine() switch
                {
                    "1" => new Aanwezig(),
                    "2" => new AfwezigZonderAfmelding(),
                    "3" => new AfwezigMetAfmelding(),
                    "4" => new AanwezigZonderAanmelding(),
                    _ => null
                };

                if (status == null)
                {
                    Console.WriteLine("Ongeldige status.");
                    break;
                }

                deelnames[nummer - 1].RegistreerStatus(status);

                Console.WriteLine(
                    "Geregistreerd: " + status.BeschrijfStatus());

                break;
            }

            case "4":
            {
                Console.Write("Training-ID: ");

                if (!int.TryParse(
                        Console.ReadLine(), out int trainingId))
                {
                    Console.WriteLine("Ongeldig training-ID.");
                    break;
                }

                Training? training = deelnames
                    .Select(d => d.Training)
                    .FirstOrDefault(t => t.Id == trainingId);

                if (training == null)
                {
                    Console.WriteLine("Training niet gevonden.");
                    break;
                }

                Console.Write("Deelnemer-ID: ");

                if (!int.TryParse(
                        Console.ReadLine(), out int deelnemerId))
                {
                    Console.WriteLine("Ongeldig deelnemer-ID.");
                    break;
                }

                bool bestaatAl = deelnames.Any(d =>
                    d.Training.Id == trainingId &&
                    d.Deelnemer.Id == deelnemerId);

                if (bestaatAl)
                {
                    Console.WriteLine(
                        "Deze deelnemer staat al bij deze training.");
                    break;
                }

                Console.Write("Naam deelnemer: ");
                string naam = Console.ReadLine()?.Trim() ?? "";

                Console.Write("Ministerie-ID: ");

                if (!int.TryParse(
                        Console.ReadLine(), out int ministerieId))
                {
                    Console.WriteLine("Ongeldig ministerie-ID.");
                    break;
                }

                Deelnemer? bestaandeDeelnemer = deelnames
                    .Select(d => d.Deelnemer)
                    .FirstOrDefault(d => d.Id == deelnemerId);

                if (bestaandeDeelnemer != null &&
                    (bestaandeDeelnemer.Naam != naam ||
                     bestaandeDeelnemer.Ministerie.Id != ministerieId))
                {
                    Console.WriteLine(
                        "Dit deelnemer-ID hoort bij andere gegevens.");
                    break;
                }

                Ministerie? ministerie = deelnames
                    .Select(d => d.Deelnemer.Ministerie)
                    .FirstOrDefault(m => m.Id == ministerieId);

                if (ministerie == null)
                {
                    Console.Write("Naam ministerie: ");

                    string ministerieNaam =
                        Console.ReadLine()?.Trim() ?? "";

                    if (string.IsNullOrWhiteSpace(ministerieNaam))
                    {
                        Console.WriteLine("Ministerienaam ontbreekt.");
                        break;
                    }

                    ministerie = new Ministerie
                    {
                        Id = ministerieId,
                        Naam = ministerieNaam
                    };
                }

                if (string.IsNullOrWhiteSpace(naam))
                {
                    Console.WriteLine("Deelnemernaam ontbreekt.");
                    break;
                }

                Deelname nieuweDeelname = new Deelname
                {
                    Id = deelnames.Count + 1,
                    Training = training,
                    Deelnemer = bestaandeDeelnemer ?? new Deelnemer
                    {
                        Id = deelnemerId,
                        Naam = naam,
                        Ministerie = ministerie
                    }
                };

                nieuweDeelname.RegistreerStatus(
                    new AanwezigZonderAanmelding());

                deelnames.Add(nieuweDeelname);

                Console.WriteLine(
                    "Aanwezige zonder aanmelding toegevoegd.");

                break;
            }

            case "5":
            {
                Console.Write("Pad voor JSON-bestand: ");

                string pad =
                    Console.ReadLine()?.Trim().Trim('"') ?? "";

                if (string.IsNullOrWhiteSpace(pad))
                {
                    Console.WriteLine("Geen pad opgegeven.");
                    break;
                }

                opslag.Bewaar(pad, deelnames);

                Console.WriteLine("Registraties opgeslagen.");
                break;
            }

            case "6":
            {
                Console.Write("Pad naar JSON-bestand: ");

                string pad =
                    Console.ReadLine()?.Trim().Trim('"') ?? "";

                if (string.IsNullOrWhiteSpace(pad))
                {
                    Console.WriteLine("Geen pad opgegeven.");
                    break;
                }

                deelnames = opslag.Laad(pad);

                Console.WriteLine(
                    $"{deelnames.Count} registraties geladen.");

                break;
            }

            case "0":
                return;

            default:
                Console.WriteLine("Ongeldige menukeuze.");
                break;
        }
    }
    catch (Exception ex) when (
        ex is IOException ||
        ex is FormatException ||
        ex is UnauthorizedAccessException ||
        ex is System.Text.Json.JsonException)
    {
        Console.WriteLine(
            "Fout bij verwerken van gegevens: " + ex.Message);
    }
}

static void ToonDeelnames(List<Deelname> deelnames)
{
    if (deelnames.Count == 0)
    {
        Console.WriteLine(
            "Nog geen deelnames. Importeer eerst een CSV-bestand.");
        return;
    }

    Console.WriteLine();
    Console.WriteLine("===== DEELNAMES =====");

    for (int i = 0; i < deelnames.Count; i++)
    {
        Deelname deelname = deelnames[i];

        string status = deelname.Status?.BeschrijfStatus()
                        ?? "Nog niet geregistreerd";

        Console.WriteLine(
            $"{i + 1}. {deelname.Deelnemer.Naam} | " +
            $"{deelname.Deelnemer.Ministerie.Naam} | " +
            $"{deelname.Training.Titel} | " +
            $"{deelname.Training.Datum:yyyy-MM-dd} | " +
            status);
    }
}
