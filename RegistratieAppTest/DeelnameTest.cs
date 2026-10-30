using registratie_app.Gegevens;
using Xunit; 

namespace RegistratieApp.Tests;

public class DeelnameTest
{
    [Fact]
    public void NieuweStatus_KanAanwezigBewaren()
    {
        Deelname deelname = new Deelname();

        deelname.NieuweStatus = new Aanwezig();

        Assert.Equal(
            "Aanwezig",
            deelname.NieuweStatus!.BeschrijfStatus()
            );
    }
}