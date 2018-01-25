using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public enum Genre
    {
        [Display(Name = "Science Fiction")]
        ScienceFiction,
        [Display(Name = "Satire")]
        Satire,
        [Display(Name = "Drama")]
        Drama,
        [Display(Name = "Action")]
        Action,
        [Display(Name = "Adventure")]
        Adventure,
        [Display(Name = "Romance")]
        Romance,
        [Display(Name = "Mystery")]
        Mystery,
        [Display(Name = "Horror")]
        Horror,
        [Display(Name = "SelfHelp")]
        SelfHelp,
        [Display(Name = "Health")]
        Health,
        [Display(Name = "Guide")]
        Guide,
        [Display(Name = "Travel")]
        Travel,
        [Display(Name = "Children")]
        Children,
        [Display(Name = "Satire")]
        Religion,
        [Display(Name = "Science")]
        Science,
        [Display(Name = "History")]
        History,
        [Display(Name = "Math")]
        Math,
        [Display(Name = "Anthology")]
        Anthology,
        [Display(Name = "Poetry")]
        Poetry,
        [Display(Name = "Encyclopedia")]
        Encyclopedia,
        [Display(Name = "Dictionary")]
        Dictionary,
        [Display(Name = "Comic")]
        Comic,
        [Display(Name = "Art")]
        Art,
        [Display(Name = "Cookbook")]
        Cookbook,
        [Display(Name = "Diary")]
        Diary,
        [Display(Name = "Journals")]
        Journals,
        [Display(Name = "Prayer")]
        Prayer,
        [Display(Name = "Serie")]
        Serie,
        [Display(Name = "Biography")]
        Biography,
        [Display(Name = "Autobiography")]
        Autobiography,
        [Display(Name = "Fantasy")]
        Fantasy
    }
}