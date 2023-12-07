using System.ComponentModel.DataAnnotations;

namespace GIHUN_MVC_Project.Models.Hotels
{
    public class Paid
    {
        public string? Paid_Guid { get; set; } = string.Empty;

        [Display(Name = "총 액수")]
        public int? Paid_Total { get; set; } = 1;

        [Display(Name = "통화")]
        public string? Paid_Currency { get; set; } = "USD";

        [Display(Name = "결제한 날")]
        public DateTime Paid_Date { get; set; }
    }
}
