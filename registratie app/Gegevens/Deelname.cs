namespace registratie_app.Gegevens;

public class Deelname
{
    public int Id { get; set; }

    public Deelnemer Deelnemer { get; set; } = null!;

    public Training Training { get; set; } = null!;

    public Aanwezigheidsstatus? Status { get; private set; }

    public void RegistreerStatus(Aanwezigheidsstatus status)
    {
        if (status == null)
        {
            throw new ArgumentNullException(nameof(status));
        }

        Status = status;
    }
}