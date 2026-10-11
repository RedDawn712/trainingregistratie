namespace registratie_app.Services;
using Gegevens;

public class dashboard
{
    private readonly List<Deelname> _deelnames;

    public dashboard(List<Deelname> deelnames)
    {
        _deelnames = deelnames; 
        
    }

    public void ToonDashboardPerMinisterie()
    {
        Console.WriteLine("\nAantal deelnemers per ministerie");

        var ministeries = _deelnames
            .Select(d => d.Deelnemer.Ministerie)
            .DistinctBy(m => m.Id);
        foreach (var ministere in ministeries)
        {
            var deelnamesVanMinisterie =_deelnames
                .Where(d=> d.Deelnemer.Ministerie.Id == ministere.Id)
                .ToList();
            int totaal = deelnamesVanMinisterie.Count;
            int aanwezig = deelnamesVanMinisterie.Count;
            (
                d => d.Status == Aanwezigheidsstatus.Aanwezig);
            
            Console.WriteLine(
                $"{ministeries.Naam}: {totaal} deelnames, " + 
                $"{aanwezig} aanwezig");
        }
    }

    public void ToonDashboardPerTraining()
    {
        Console.WriteLine("\nAantal deelnemers per training");

        var trainingen = _deelnames
            .Select(d => d.Training)
            .DistinctBy((t => t.Id));
        foreach (var training in trainingen)
        {
            var deelnamesVanMinisterie = _deelnames
                .Where(d => d.Training.Id == training.Id)
                .ToList();
            
            int totaal = deelnamesVanMinisterie.Count;
            int aanwezig = deelnamesVanMinisterie.Count(d => d.Status == Aanwezigheidsstatus.Aanwezig);
            
            Console.WriteLine(
                $"{training.Titel}: {totaal} deelnemers, " + 
                $"{aanwezig} aanwezig");
        }
    }
}