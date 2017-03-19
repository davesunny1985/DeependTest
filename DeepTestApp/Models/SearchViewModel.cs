using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeepTestApp.Models
{
    public class SearchViewModel
    {
        public SearchViewModel()
        {
            this.Results = new PagedList<SearchResult>(null, 1, 10);
            this.PageNumber = 1;
        }
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        public string Gender { get; set; }

        public IPagedList<SearchResult> Results { get; set; }

        public int PageCount { get; set; }

        public int PageNumber { get; set; }
    }

    public class SearchResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string BirthPlace { get; set; }
    }
}