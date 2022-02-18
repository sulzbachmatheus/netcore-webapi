using System.ComponentModel.DataAnnotations;

namespace WebApi.Dto
{
    public class ApplicationDto
    {
        [Key]
        public int ApplicationId { get; set; }

        [Required(ErrorMessage = "This field is mandatory")]
        public string Url { get; set; }

        [Required(ErrorMessage = "This field is mandatory")]
        public string PathLocal { get; set; }

        [Required(ErrorMessage = "This field is mandatory")]
        public bool DebuggingMode { get; set; }
    }
}
