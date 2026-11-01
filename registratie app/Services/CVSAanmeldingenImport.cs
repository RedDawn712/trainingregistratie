
using System.Globalization;
using System.Text;

namespace registratie_app.Gegevens;

public class CsvAanmeldingenImport
{
    public List<Deelname> LeesBestand(string pad)
    {
        if (!File.Exists(pad))
        {
            throw new FileNotFoundException(
                "CSV-bestand niet gevonden.", pad);
        }

        string[] regels = File.ReadAllLines(pad, Encoding.UTF8);

        if (regels.Length == 0)
        {
            throw new FormatException("Het CSV-bestand is leeg.");
        }

        string verwachteKop =
            "TrainingId;TrainingTitel;Datum;DeelnemerId;" +
            "DeelnemerNaam;MinisterieId;MinisterieNaam";

        if (regels[0].Trim().TrimStart('\uFEFF') != verwachteKop)
        {
            throw new FormatException(
                "Het CSV-bestand heeft niet de verwachte kolommen.");
        }

        List<Deelname> deelnames = new();

        Dictionary<int, Training> trainingen = new();
        Dictionary<int, Deelnemer> deelnemers = new();
        Dictionary<int, Ministerie> ministeries = new();

        HashSet<(int, int)> combinaties = new();

        for (int i = 1; i < regels.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(regels[i]))
            {
                continue;
            }

            string[] velden = regels[i].Split(';');

            if (velden.Length != 7 ||
                velden.Any(string.IsNullOrWhiteSpace))
            {
                throw new FormatException(
                    $"Regel {i + 1}: verwacht zeven ingevulde velden.");
            }

            if (!int.TryParse(velden[0], out int trainingId) ||
                !int.TryParse(velden[3], out int deelnemerId) ||
                !int.TryParse(velden[5], out int ministerieId))
            {
                throw new FormatException(
                    $"Regel {i + 1}: ongeldig ID.");
            }

            if (!DateTime.TryParseExact(
                    velden[2],
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime datum))
            {
                throw new FormatException(
                    $"Regel {i + 1}: ongeldige datum.");
            }

            if (!combinaties.Add((trainingId, deelnemerId)))
            {
                throw new FormatException(
                    $"Regel {i + 1}: dubbele aanmelding.");
            }

            string ministerieNaam = velden[6].Trim();

            if (!ministeries.TryGetValue(
                    ministerieId, out Ministerie? ministerie))
            {
                ministerie = new Ministerie
                {
                    Id = ministerieId,
                    Naam = ministerieNaam
                };

                ministeries.Add(ministerieId, ministerie);
            }
            else if (ministerie.Naam != ministerieNaam)
            {
                throw new FormatException(
                    $"Regel {i + 1}: tegenstrijdig ministerie.");
            }

            string deelnemerNaam = velden[4].Trim();

            if (!deelnemers.TryGetValue(
                    deelnemerId, out Deelnemer? deelnemer))
            {
                deelnemer = new Deelnemer
                {
                    Id = deelnemerId,
                    Naam = deelnemerNaam,
                    Ministerie = ministerie
                };

                deelnemers.Add(deelnemerId, deelnemer);
            }
            else if (deelnemer.Naam != deelnemerNaam ||
                     deelnemer.Ministerie.Id != ministerieId)
            {
                throw new FormatException(
                    $"Regel {i + 1}: tegenstrijdige deelnemer.");
            }

            string trainingTitel = velden[1].Trim();

            if (!trainingen.TryGetValue(
                    trainingId, out Training? training))
            {
                training = new Training
                {
                    Id = trainingId,
                    Titel = trainingTitel,
                    Datum = datum
                };

                trainingen.Add(trainingId, training);
            }
            else if (training.Titel != trainingTitel ||
                     training.Datum != datum)
            {
                throw new FormatException(
                    $"Regel {i + 1}: tegenstrijdige training.");
            }

            Deelname deelname = new Deelname
            {
                Id = deelnames.Count + 1,
                Deelnemer = deelnemer,
                Training = training
            };

            deelnames.Add(deelname);
        }

        return deelnames;
    }
}
