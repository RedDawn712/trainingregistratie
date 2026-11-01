namespace registratie_app.Gegevens;

public class AanwezigZonderAanmelding : Aanwezigheidsstatus
{
    public override string BeschrijfStatus()
    {
        return "Aanwezig zonder aanmelding";
    }
}