
using System.Text.Json;

namespace registratie_app.Gegevens;

public class RegistratieOpslag
{
    public void Bewaar(
        string pad,
        IEnumerable<Deelname> deelnames)
    {
        var regels = deelnames.Select(d => new RegistratieRegel(
            d.Training.Id,
            d.Training.Titel,
            d.Training.Datum,
            d.Deelnemer.Id,
            d.Deelnemer.Naam,
            d.Deelnemer.Ministerie.Id,
            d.Deelnemer.Ministerie.Naam,
            d.Status?.GetType().Name
        )).ToList();

        string json = JsonSerializer.Serialize(
            regels,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        File.WriteAllText(pad, json);
    }

    public List<Deelname> Laad(string pad)
    {
        if (!File.Exists(pad))
        {
            throw new FileNotFoundException(
                "JSON-bestand niet gevonden.", pad);
        }

        string json = File.ReadAllText(pad);

        List<RegistratieRegel> regels =
            JsonSerializer.Deserialize<List<RegistratieRegel>>(json)
            ?? throw new FormatException("Ongeldig JSON-bestand.");

        List<Deelname> deelnames = new();

        Dictionary<int, Ministerie> ministeries = new();
        Dictionary<int, Deelnemer> deelnemers = new();
        Dictionary<int, Training> trainingen = new();

        foreach (RegistratieRegel regel in regels)
        {
            if (!ministeries.TryGetValue(
                    regel.MinisterieId, out Ministerie? ministerie))
            {
                ministerie = new Ministerie
                {
                    Id = regel.MinisterieId,
                    Naam = regel.MinisterieNaam
                };

                ministeries.Add(regel.MinisterieId, ministerie);
            }
            else if (ministerie.Naam != regel.MinisterieNaam)
            {
                throw new FormatException(
                    "Tegenstrijdige ministeries in JSON.");
            }

            if (!deelnemers.TryGetValue(
                    regel.DeelnemerId, out Deelnemer? deelnemer))
            {
                deelnemer = new Deelnemer
                {
                    Id = regel.DeelnemerId,
                    Naam = regel.DeelnemerNaam,
                    Ministerie = ministerie
                };

                deelnemers.Add(regel.DeelnemerId, deelnemer);
            }
            else if (deelnemer.Naam != regel.DeelnemerNaam ||
                     deelnemer.Ministerie.Id != regel.MinisterieId)
            {
                throw new FormatException(
                    "Tegenstrijdige deelnemers in JSON.");
            }

            if (!trainingen.TryGetValue(
                    regel.TrainingId, out Training? training))
            {
                training = new Training
                {
                    Id = regel.TrainingId,
                    Titel = regel.TrainingTitel,
                    Datum = regel.Datum
                };

                trainingen.Add(regel.TrainingId, training);
            }
            else if (training.Titel != regel.TrainingTitel ||
                     training.Datum != regel.Datum)
            {
                throw new FormatException(
                    "Tegenstrijdige trainingen in JSON.");
            }

            Deelname deelname = new Deelname
            {
                Id = deelnames.Count + 1,
                Training = training,
                Deelnemer = deelnemer
            };

            Aanwezigheidsstatus? status = regel.Status switch
            {
                null => null,

                nameof(Aanwezig) =>
                    new Aanwezig(),

                nameof(AfwezigZonderAfmelding) =>
                    new AfwezigZonderAfmelding(),

                nameof(AfwezigMetAfmelding) =>
                    new AfwezigMetAfmelding(),

                nameof(AanwezigZonderAanmelding) =>
                    new AanwezigZonderAanmelding(),

                _ => throw new FormatException(
                    "Onbekende status: " + regel.Status)
            };

            if (status != null)
            {
                deelname.RegistreerStatus(status);
            }

            deelnames.Add(deelname);
        }

        return deelnames;
    }

    public record RegistratieRegel(
        int TrainingId,
        string TrainingTitel,
        DateTime Datum,
        int DeelnemerId,
        string DeelnemerNaam,
        int MinisterieId,
        string MinisterieNaam,
        string? Status);
}
