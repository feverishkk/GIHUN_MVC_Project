using GIHUN_MVC_Project.ViewModels.Hotels;

namespace GIHUN_MVC_Project.ViewModels.Search
{
    public class SearchViewModel
    {
        public SearchViewModel()
        {
            SearchString = string.Empty;
        }
        public List<HotelsInfoViewModel>? HotelsInfo  { get; set; }

        public string? SearchString { get; set; } = string.Empty;
    }
}
