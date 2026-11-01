
using registratie_app.Gegevens;
using Xunit;

namespace RegistratieAppTest;

public class CsvAanmeldingenImportTests
{
    private const string Kop =
        "TrainingId;TrainingTitel;Datum;DeelnemerId;" +
        "DeelnemerNaam;MinisterieId;MinisterieNaam";

    [Fact]
    public void GeldigeCsv_LevertDeelnameOp()
    {
        // Arrange
        string pad = Path.GetTempFileName();

        try
        {
            File.WriteAllText(
                pad,
                Kop + "\n" +
                "1;C# basis;2026-11-15;20;Menma;5;Testministerie\n");

            CsvAanmeldingenImport import =
                new CsvAanmeldingenImport();

            // Act
            List<Deelname> resultaat = import.LeesBestand(pad);

            // Assert
            Assert.Single(resultaat);
            Assert.Equal("Menma", resultaat[0].Deelnemer.Naam);
            Assert.Equal("C# basis", resultaat[0].Training.Titel);
            Assert.Null(resultaat[0].Status);
        }
        finally
        {
            File.Delete(pad);
        }
    }

    [Fact]
    public void VerkeerdeKolommen_GeeftFout()
    {
        // Arrange
        string pad = Path.GetTempFileName();

        try
        {
            File.WriteAllText(
                pad,
                "Naam;Training\nMenma;C# basis");

            CsvAanmeldingenImport import =
                new CsvAanmeldingenImport();

            // Act en Assert
            Assert.Throws<FormatException>(
                () => import.LeesBestand(pad));
        }
        finally
        {
            File.Delete(pad);
        }
    }

    [Fact]
    public void DubbeleAanmelding_GeeftFout()
    {
        // Arrange
        string pad = Path.GetTempFileName();

        try
        {
            string regel =
                "1;C# basis;2026-11-15;20;Menma;5;Testministerie";

            File.WriteAllText(
                pad,
                Kop + "\n" + regel + "\n" + regel);

            CsvAanmeldingenImport import =
                new CsvAanmeldingenImport();

            // Act en Assert
            Assert.Throws<FormatException>(
                () => import.LeesBestand(pad));
        }
        finally
        {
            File.Delete(pad);
        }
    }
}
