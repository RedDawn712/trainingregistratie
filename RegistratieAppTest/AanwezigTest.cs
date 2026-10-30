using registratie_app.Gegevens;
using Xunit;

namespace RegistratieApp.Tests;

public class AanwezigTests
{
    [Fact]
    public void BeschrijfStatus_GeeftAanwezigTerug()
    {
        // Arrange: maak een aanwezigheidsstatus
        Aanwezig status = new Aanwezig();

        // Act: vraag de beschrijving op
        string resultaat = status.BeschrijfStatus();

        // Assert: controleer het resultaat
        Assert.Equal("Aanwezig", resultaat);
    }
}