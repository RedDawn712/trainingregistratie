namespace registratie_app.Gegevens;

public class Trainer : Gebruiker
{
    public Trainer(string gebruikersId, string naam)
        : base(gebruikersId, naam)
    {
    }

    public override void VoerActieUit()
    {
        Console.WriteLine("Trainer kan aanwezigheid registreren.");
    }
}