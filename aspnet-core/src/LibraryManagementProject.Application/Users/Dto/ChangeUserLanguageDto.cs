using System.ComponentModel.DataAnnotations;

namespace LibraryManagementProject.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}