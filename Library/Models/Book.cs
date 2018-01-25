using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Validation;
using Library.Models;

namespace Library.Models
{
    public class Book
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        [IsbnValidator]
        public string ISBN { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public List<Genre> Genres { get; set; }
        public string StringsAsString
        {
            get
            {
                if (Genres == null) return "";
                return string.Join(",", Genres.Select(genre => ((int)genre).ToString()).ToArray());
            }
            set
            {
                Genres = value.Split(',')
                    .Select(genre => (Genre)(int.Parse(genre)))
                    .ToList();
            }
        }

        [DisplayName("Cover")]
        [StringLength(200)]
        public string PictureName { get; set; }
        private int pages;
        [Required]
        public int Pages
        {
            get { return pages; }
            set
            {
                if (value < 0) return;
                pages = value;
            }
        }

        public Book()
        {
            AuthorId = -1;
        }
    }
}
