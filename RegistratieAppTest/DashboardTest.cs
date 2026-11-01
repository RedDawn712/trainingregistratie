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

    [Fact]
    public void Dashboard_TeltOudeStatusAanwezigNietMeerMee()
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
        Deelname deelname = new Deelname()
        {
            Deelnemer = deelnemer,
            Training = training,
            Status = Aanwezigheidsstatus.Aanwezig
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

            Assert.Contains("0 aanwezig", uitvoer.ToString()
            ); 
        }
        finally
        {
            Console.SetOut(origineleUitvoer);
        }
    }
    
    [Fact]
    public void Dashboard_TeltAanwezigenPerMinisterie()
    {
        // Arrange: maak een ministerie
        Ministerie ministerie = new Ministerie
        {
            Id = 1,
            Naam = "Testministerie"
        };

        // Maak een deelnemer
        Deelnemer deelnemer = new Deelnemer
        {
            Id = 1,
            Naam = "Menma",
            Ministerie = ministerie
        };

        // Maak een training
        Training training = new Training
        {
            Id = 1,
            Titel = "Menma 101"
        };

        // Maak een deelname met de nieuwe status
        Deelname deelname = new Deelname
        {
            Deelnemer = deelnemer,
            Training = training,
            NieuweStatus = new Aanwezig()
        };

        // Zet de deelname in een lijst
        List<Deelname> deelnames = new List<Deelname>
        {
            deelname
        };

        // Maak het dashboard
        Dashboard overzicht = new Dashboard(deelnames);

        // Bewaar de normale console-uitvoer
        StringWriter uitvoer = new StringWriter();
        TextWriter origineleUitvoer = Console.Out;

        try
        {
            Console.SetOut(uitvoer);

            // Act: toon het ministerieoverzicht
            overzicht.ToonDashboardPerMinisterie();

            // Assert: controleer het resultaat
            Assert.Contains(
                "Testministerie: 1 deelnames, 1 aanwezig",
                uitvoer.ToString()
            );
        }
        finally
        {
            // Herstel de console-uitvoer
            Console.SetOut(origineleUitvoer);
        }
    }

    [Fact]
    public void Dashboard_TeltAanwezigZonderAanmeldingMee()
    {
        // Arrange
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
            NieuweStatus = new AanwezigZonderAanmelding()
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

            // Act
            overzicht.ToonDashboardPerTraining();

            // Assert
            Assert.Contains("1 aanwezig", uitvoer.ToString());
        }
        finally
        {
            Console.SetOut(origineleUitvoer);
        }
    }

    [Fact]
    public void Dashboard_TeltAanwezigZonderAanmeldingPerMinisterieMee()
    {
        // Arrange: maak een ministerie
        Ministerie ministerie = new Ministerie
        {
            Id = 1,
            Naam = "Testministerie"
        };

        // Maak een deelnemer
        Deelnemer deelnemer = new Deelnemer
        {
            Id = 1,
            Naam = "Menma",
            Ministerie = ministerie
        };

        // Maak een training
        Training training = new Training
        {
            Id = 1,
            Titel = "Menma 101"
        };

        // Maak een deelname zonder voorafgaande aanmelding
        Deelname deelname = new Deelname
        {
            Deelnemer = deelnemer,
            Training = training,
            NieuweStatus = new AanwezigZonderAanmelding()
        };

        // Maak een lijst met de deelname
        List<Deelname> deelnames = new List<Deelname>
        {
            deelname
        };

        // Maak het dashboard aan
        Dashboard overzicht = new Dashboard(deelnames);

        // Vang de console-uitvoer op
        StringWriter uitvoer = new StringWriter();
        TextWriter origineleUitvoer = Console.Out;

        try
        {
            Console.SetOut(uitvoer);

            // Act: toon het ministerieoverzicht
            overzicht.ToonDashboardPerMinisterie();

            // Assert: controleer of de deelnemer wordt meegeteld
            Assert.Contains("1 aanwezig", uitvoer.ToString());
        }
        finally
        {
            Console.SetOut(origineleUitvoer);
        }
    }

}