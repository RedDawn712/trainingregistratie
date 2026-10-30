namespace registratie_app.Gegevens;

public class AanwezigZonderAanmelding : AanwezigheidsstatusBasis
{
    public override string BeschrijfStatus()
    {
        return "Aanwezig zonder aanmelding"; 
    }
}