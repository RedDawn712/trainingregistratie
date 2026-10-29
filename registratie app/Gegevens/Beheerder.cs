namespace registratie_app.Gegevens;

public class Beheerder : Gebruiker
{
    public Beheerder(string gebruikersId, string naam)
        : base(gebruikersId, naam)
    {
    }

    public override void VoerActieUit()
    {
        Console.WriteLine("Beheerder kan gebruikers beheren.");
    }

    public void MaakGebruikerAan()
    {
        Console.WriteLine("Gebruiker aanmaken");
    }

    public void WijzigGebruiker()
    {
        Console.WriteLine("Gebruiker wijzigen");
    }

    public void VerwijderGebruiker()
    {
        Console.WriteLine("Gebruiker verwijderen");
    }

    public void BeheerGebruikers()
    {
        Console.WriteLine("Gebruikers beheren");
    }
}