using registratie_app.Gegevens;
using Xunit;

namespace RegistratieApp.Tests;

public class AfwezigMetAfmeldingTests
{
    [Fact]
    public void BeschrijfStatus_GeeftJuisteTekstTerug()
    {
        // Arrange
        AfwezigMetAfmelding status =
            new AfwezigMetAfmelding();

        // Act
        string resultaat = status.BeschrijfStatus();

        // Assert
        Assert.Equal("Afwezig met afmelding", resultaat);
    }
}