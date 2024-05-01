using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductApi.Models.Requests
{
    public class AddBankRequest
    {
        [Required]
        public string Name { get; set; }
        [Url]
        public string Location { get; set; }
        public string BranchManager { get; set; }


    }




}
