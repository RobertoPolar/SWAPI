namespace WebApplication1.Data.ViewModels
{
    public class PeopleViewModel
    {
        public string? count { get; set; }
        public string? next { get; set; }
        public string? previous { get; set; }
        public List<PersonViewModel>? results { get; set; }
    }
}
