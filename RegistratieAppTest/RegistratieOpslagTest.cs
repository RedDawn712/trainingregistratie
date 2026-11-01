
using registratie_app.Gegevens;
using Xunit;

namespace RegistratieAppTest;

public class RegistratieOpslagTests
{
    [Fact]
    public void BewarenEnLaden_BehoudtStatus()
    {
        // Arrange
        string pad = Path.GetTempFileName();

        try
        {
            Deelname deelname = new Deelname
            {
                Training = new Training
                {
                    Id = 1,
                    Titel = "C# basis",
                    Datum = new DateTime(2026, 11, 15)
                },

                Deelnemer = new Deelnemer
                {
                    Id = 20,
                    Naam = "Menma",
                    Ministerie = new Ministerie
                    {
                        Id = 5,
                        Naam = "Testministerie"
                    }
                }
            };

            deelname.RegistreerStatus(
                new AfwezigMetAfmelding());

            RegistratieOpslag opslag =
                new RegistratieOpslag();

            // Act
            opslag.Bewaar(pad, new[] { deelname });

            List<Deelname> geladen = opslag.Laad(pad);

            // Assert
            Assert.Single(geladen);

            Assert.IsType<AfwezigMetAfmelding>(
                geladen[0].Status);

            Assert.Equal(
                "Menma",
                geladen[0].Deelnemer.Naam);
        }
        finally
        {
            File.Delete(pad);
        }
    }

    [Fact]
    public void OnbekendeStatusInJson_GeeftFout()
    {
        // Arrange
        string pad = Path.GetTempFileName();

        try
        {
            var regel =
                new RegistratieOpslag.RegistratieRegel(
                    1,
                    "C# basis",
                    new DateTime(2026, 11, 15),
                    20,
                    "Menma",
                    5,
                    "Testministerie",
                    "NietBestaandeStatus"
                );

            string json =
                System.Text.Json.JsonSerializer.Serialize(
                    new[] { regel });

            File.WriteAllText(pad, json);

            RegistratieOpslag opslag =
                new RegistratieOpslag();

            // Act en Assert
            Assert.Throws<FormatException>(
                () => opslag.Laad(pad));
        }
        finally
        {
            File.Delete(pad);
        }
    }
}
