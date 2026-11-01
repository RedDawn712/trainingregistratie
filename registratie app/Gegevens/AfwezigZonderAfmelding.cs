namespace registratie_app.Gegevens;

public class AfwezigZonderAfmelding : Aanwezigheidsstatus
{
    public override string BeschrijfStatus()
    {
        return "Afwezig zonder afmelding";
    }
}