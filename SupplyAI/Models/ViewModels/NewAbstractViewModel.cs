using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MI3.Models
{
    public class NewAbstractViewModel
    {
        public string Title { get; set; }
        public string UserName { get; set; }
        public string Url { get; set; }
        public string MongoCollectionName { get { return "Abstracts"; } }
        public string AttachmentId { get; set; }
        public string NotificationId { get; set; }
        public bool Rehost { get; set; }
        public bool PublicAccess { get; set; }
        public string Content { get; set; }

        public bool Approved { get; set; }
        public string Rationale { get; set; }
        public bool Reviewed { get; set; }
        public string ReviewedBy { get; set; }  // Mongo GUID for admin who approved/denied request
        public DateTime DateReviewed { get; set; }
        public string DatasetTitle { get; set; }  
        
        public string Authors { get; set; }
        public string Source { get; set; }
        public int NumberOfStudies { get; set; }               
        public string DatasetId { get; set; }       
        public bool SameModalityAndManuf { get; set; }       
        public string Manufacturer { get; set; }        
        public string Modality { get; set; }        
        public bool HasLabels { get; set; }        
        public string LabelType { get; set; }        
        public string LabelFormat { get; set; }        
        public string LabelsReviewed { get; set; }       
        public string LabelLevel { get; set; }       
        public string ClinicalIssues { get; set; }       
        public string Country { get; set; } 
        public string USRegion { get; set; }
        public string State { get; set; }
        public string PixelDataShifted { get; set; }       
        public string HowPixelDataShifted { get; set; }        
        public string PixelDataSynthesized { get; set; }        
        public string HowPixelDataSynthesized { get; set; }       
        public string MultipleModalitiesAndManuf { get; set; }        
        public string ModelsAndVersions { get; set; }
        public bool IsAnonymized { get; set; }
        public string HowAnonymized { get; set; }
        public DateTime DateGenerated { get; set; }
    }
}