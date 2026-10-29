using registratie_app.Gegevens;
using Xunit;

namespace RegistratieApp.Tests;

public class TrainingTests
{
    [Fact]
    public void TrainingTitelKanWordenIngesteld()
    {
        // Arrange: een training aanmaken
        Training training = new Training();

        // Act: de titel instellen
        training.Titel = "C# basis";

        // Assert: controleren of de titel klopt
        Assert.Equal("C# basis", training.Titel);
    }
    
    [Fact]
    public void TrainingDatumKanWordenIngesteld()
    {
        // Arrange: een nieuwe training maken
        Training training = new Training();

        // Act: een datum instellen
        DateTime datum = new DateTime(2026, 11, 15);
        training.Datum = datum;

        // Assert: controleren of de datum klopt
        Assert.Equal(datum, training.Datum);
    }

}