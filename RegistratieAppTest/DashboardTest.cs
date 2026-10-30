using registratie_app.Gegevens;
using Xunit; 

namespace RegistratieAppTest;

public class DashboardTest
{
    [Fact]
    public void Dashboard_TeltNieuweStatusAanwezigMee()
    {
        Ministerie ministerie = new Ministerie
        {
            Id = 1,
            Naam = "Testministerie"
        };

        Deelnemer deelnemer = new Deelnemer
        {
            Id = 1,
            Naam = "Menma",
            Ministerie = ministerie
        };

        Training training = new Training
        {
            Id = 1,
            Titel = "Menma 101"
        };

        Deelname deelname = new Deelname
        {
            Deelnemer = deelnemer,
            Training = training,
            NieuweStatus = new Aanwezig()
        };

        List<Deelname> deelnames = new List<Deelname>
        {
            deelname
        };
        Dashboard overzicht = new Dashboard(deelnames);

        StringWriter uitvoer = new StringWriter(); 
        TextWriter origineleUitvoer = Console.Out;

        try
        {
            Console.SetOut(uitvoer);

            overzicht.ToonDashboardPerTraining();
            Assert.Contains(
                "1 aanwezig",
                uitvoer.ToString()
            );
        }
        finally
        {
            Console.SetOut(origineleUitvoer); 
            
        }
    }
}