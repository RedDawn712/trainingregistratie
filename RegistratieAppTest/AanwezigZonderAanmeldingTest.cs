using registratie_app.Gegevens;
using Xunit;

namespace RegistratieApp.Tests;

public class AanwezigZonderAanmeldingTest
{
    [Fact]
    public void BeschrijfStatus_GeefJuisteTekstTerug()
    {
        AanwezigZonderAanmelding status = new AanwezigZonderAanmelding();
        
        string resultaat = status.BeschrijfStatus();
        
        Assert.Equal("Aanwezig zonder aanmelding", resultaat);
    }
}
