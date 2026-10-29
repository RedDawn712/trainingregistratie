namespace registratie_app.Gegevens;

public abstract class Gebruiker
{
    private string gebruikersId;
    private string naam;

    public Gebruiker(string gebruikersId, string naam)
    {
        this.gebruikersId = gebruikersId;
        this.naam = naam;
    }

    public abstract void VoerActieUit();
}