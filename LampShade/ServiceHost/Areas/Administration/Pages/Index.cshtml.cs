using _0_Framework.Presentation;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages
{
    public class IndexModel : PageModel
    {
        public List<ChartJsDataSet> BarLineDataSets { get; set; }
        public ChartJsDataSet DoughnutDataSet { get; set; }

        public void OnGet()
        {
            BarLineDataSets = new List<ChartJsDataSet>
            {
                new ChartJsDataSet
                {
                    Label = "›—Ê‘ ”«„”Ê‰ê",
                    Data = new List<int>{ 100, 200, 150, 350, 500 },
                    BorderColor = "#FF33A5",
                    BackgroundColors = new[]{ "#FF33A5" }
                },
                new ChartJsDataSet
                {
                    Label = "ﬁ—Ê‘ «Å·",
                    Data = new List<int>{ 125, 300, 140, 50, 400 },
                    BorderColor = "#33B5FF",
                    BackgroundColors = new[]{ "#33B5FF" }
                }
            };

            DoughnutDataSet = new ChartJsDataSet
            {
                Data = new List<int> { 100, 200, 150, 350, 500 },
                BorderColor = "#FF33A5",
                BackgroundColors = new[]{ "#FF33A5", "#C70039", "#581845", "#FF3349", "#FF5733" }
            };
        }
    }
}
