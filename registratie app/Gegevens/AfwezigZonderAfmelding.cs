namespace registratie_app.Gegevens;

public class AfwezigZonderAfmelding : AanwezigheidsstatusBasis
{
    public override string BeschrijfStatus()
    {
        return "Afwezig zonder afmelding";
    }
}