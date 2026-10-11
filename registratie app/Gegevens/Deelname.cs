namespace registratie_app.Gegevens;

public class Deelname
{
    public int Id { get; set; }

    public Deelnemer Deelnemer { get; set; } = null!;
    public Training Training { get; set; } = null!;

    public Aanwezigheidsstatus Status { get; set; }
        = Aanwezigheidsstatus.Onbekend; 
}