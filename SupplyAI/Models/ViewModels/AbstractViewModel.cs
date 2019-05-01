using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Foolproof;

namespace MI3.Models
{
    public class AbstractViewModel
    {
        [Required(ErrorMessage = "This field is required.")]
        [MinLength(10, ErrorMessage = "Dataset title must contain at least 10 characters.")]
        public string DatasetTitle { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public bool SameModalityAndManuf { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Modality")]
        public List<string> Modality { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Manufacturer")]
        public List<string> Manufacturer { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "URL")]
        [Url(ErrorMessage = "Please enter a valid URL.")]
        public string Url { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public bool Rehost { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public bool PublicAccess { get; set; }

        public string Content { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public bool HasLabels { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Number of studies must be greater than zero.")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid number.")]
        public int NumberOfStudies { get; set; }

        // [Required(ErrorMessage = "Please select one of the above.")]
        public string LabelType { get; set; }

        // [Required(ErrorMessage = "This field is required.")]
        public string LabelFormat { get; set; }

        // [Required(ErrorMessage = "Please select one of the above.")]
        public string LabelsReviewed { get; set; }

        public string LabelLevel { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string ClinicalIssues { get; set; }

        // [Range(typeof(bool), "false", "false", ErrorMessage = "You must select this field.")]
        public bool IsAnonymized { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string HowAnonymized { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string Country { get; set; }

        public string USRegion { get; set; }

        public string State { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string PixelDataShifted { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string HowPixelDataShifted { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string PixelDataSynthesized { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string HowPixelDataSynthesized { get; set; }

        public string MultipleModalitiesAndManuf { get; set; }

        public string ModelsAndVersions { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [MinLength(5, ErrorMessage = "List of authors must be at least 5 characters long.")]
        public string Authors { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [MinLength(2, ErrorMessage = "Institution name must be at least 2 characters long.")]
        public string Source { get; set; }
        public string Summary { get; set; }
    }
}