using registratie_app.Gegevens;
using Xunit;

namespace RegistratieApp.Tests;

public class AfwezigZonderAfmeldingTests
{
    [Fact]
    public void BeschrijfStatus_GeeftJuisteTekstTerug()
    {
        // Arrange
        AfwezigZonderAfmelding status =
            new AfwezigZonderAfmelding();

        // Act
        string resultaat = status.BeschrijfStatus();

        // Assert
        Assert.Equal("Afwezig zonder afmelding", resultaat);
    }
}