namespace registratie_app.Gegevens;

public class Trainer : Gebruiker
{
    public Trainer(string gebruikersId, string naam)
        : base(gebruikersId, naam)
    {
    }

    public override void VoerActieUit()
    {
        Console.WriteLine(
            "Trainer kan aanwezigheid registreren.");
    }

    public void RegistreerAanwezigheid(
        Deelname deelname,
        Aanwezigheidsstatus status)
    {
        if (deelname == null)
        {
            throw new ArgumentNullException(nameof(deelname));
        }

        deelname.RegistreerStatus(status);
    }
}