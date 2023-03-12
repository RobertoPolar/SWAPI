namespace WebApplication1.Data.ViewModels
{
    public class PlanetsViewModel
    {
        public string? count { get; set; }
        public string? next { get; set; }
        public string? previous { get; set; }
        public List<PlanetViewModel> results { get;set; }

    }
}
