namespace registratie_app.Gegevens;

public class Deelnemer
{
    public int Id { get; set; } 
    public string Naam { get; set; } = string.Empty;
    public string Telefoonnummer { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public Ministerie Ministerie { get; set; } = null!; 
}
