using registratie_app.Gegevens;
using Xunit;

namespace RegistratieAppTest;

public class DeelnameTest
{
    [Fact]
    public void RegistreerStatus_BewaartAanwezig()
    {
        // Arrange
        Deelname deelname = new Deelname();

        // Act
        deelname.RegistreerStatus(new Aanwezig());

        // Assert
        Assert.IsType<Aanwezig>(deelname.Status);
        Assert.Equal(
            "Aanwezig",
            deelname.Status!.BeschrijfStatus());
    }
}