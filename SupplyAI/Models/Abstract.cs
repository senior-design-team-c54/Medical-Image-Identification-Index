using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Microsoft.AspNet.Identity;

namespace MI3.Models
{
    public class Abstract : IMongoDocument
    {
        [BsonIgnore]
        public static readonly List<string> Modalities = new List<string>(new string[] 
        {
            "CT",
            "X-Ray",
            "MRI",
            "Other"
        });
        [BsonIgnore]
        public static readonly List<string> Manufacturers = new List<string>(new string[]
        {
            "GE",
            "Siemens",
            "Other"
        });
        [BsonIgnore]
        public static readonly List<string> Countries = new List<string>(new string[]
        {
            "Unknown","United States","Afghanistan","Albania","Algeria","Andorra","Angola","Anguilla","Antigua & Barbuda","Argentina","Armenia","Aruba","Australia","Austria","Azerbaijan","Bahamas","Bahrain","Bangladesh","Barbados","Belarus","Belgium","Belize","Benin","Bermuda","Bhutan","Bolivia","Bosnia & Herzegovina","Botswana","Brazil","British Virgin Islands","Brunei","Bulgaria","Burkina Faso","Burundi","Cambodia","Cameroon","Cape Verde","Cayman Islands","Chad","Chile","China","Colombia","Congo","Cook Islands","Costa Rica","Cote D Ivoire","Croatia","Cruise Ship","Cuba","Cyprus","Czech Republic","Denmark","Djibouti","Dominica","Dominican Republic","Ecuador","Egypt","El Salvador","Equatorial Guinea","Estonia","Ethiopia","Falkland Islands","Faroe Islands","Fiji","Finland","France","French Polynesia","French West Indies","Gabon","Gambia","Georgia","Germany","Ghana","Gibraltar","Greece","Greenland","Grenada","Guam","Guatemala","Guernsey","Guinea","Guinea Bissau","Guyana","Haiti","Honduras","Hong Kong","Hungary","Iceland","India","Indonesia","Iran","Iraq","Ireland","Isle of Man","Israel","Italy","Jamaica","Japan","Jersey","Jordan","Kazakhstan","Kenya","Kuwait","Kyrgyz Republic","Laos","Latvia","Lebanon","Lesotho","Liberia","Libya","Liechtenstein","Lithuania","Luxembourg","Macau","Macedonia","Madagascar","Malawi","Malaysia","Maldives","Mali","Malta","Mauritania","Mauritius","Mexico","Moldova","Monaco","Mongolia","Montenegro","Montserrat","Morocco","Mozambique","Namibia","Nepal","Netherlands","Netherlands Antilles","New Caledonia","New Zealand","Nicaragua","Niger","Nigeria","Norway","Oman","Pakistan","Palestine","Panama","Papua New Guinea","Paraguay","Peru","Philippines","Poland","Portugal","Puerto Rico","Qatar","Reunion","Romania","Russia","Rwanda","Saint Pierre & Miquelon","Samoa","San Marino","Satellite","Saudi Arabia","Senegal","Serbia","Seychelles","Sierra Leone","Singapore","Slovakia","Slovenia","South Africa","South Korea","Spain","Sri Lanka","St. Kitts & Nevis","St. Lucia","St. Vincent","St. Lucia","Sudan","Suriname","Swaziland","Sweden","Switzerland","Syria","Taiwan","Tajikistan","Tanzania","Thailand","Timor L'Este","Togo","Tonga","Trinidad & Tobago","Tunisia","Turkey","Turkmenistan","Turks &amp; Caicos","Uganda","Ukraine","United Arab Emirates","United Kingdom","Uruguay","Uzbekistan","Venezuela","Vietnam","Virgin Islands (US)","Yemen","Zambia","Zimbabwe"
        });
        [BsonIgnore]
        public static readonly List<string> USRegions = new List<string>(new string[]
        {
            "Unknown",
            "Mid-Atlantic",
            "Midwest",
            "New England",
            "Pacific Northwest",
            "Southeast",
            "Southwest",
            "West",
            "West Coast"
        });
        [BsonIgnore]
        public static readonly List<string> States = new List<string>(new string[]
        {
            "Unknown","Alabama","Alaska","Arizona","Arkansas","California","Colorado","Connecticut","Delaware","District of Columbia","Florida","Georgia","Hawaii","Idaho","Illinois","Indiana","Iowa","Kansas","Kentucky","Louisiana","Maine","Maryland","Massachusetts","Michigan","Minnesota","Mississippi","Missouri","Montana","Nebraska","Nevada","New Hampshire","New Jersey","New Mexico","New York","North Carolina","North Dakota","Ohio","Oklahoma","Oregon","Pennsylvania","Rhode Island","South Carolina","South Dakota","Tennessee","Texas","Utah","Vermont","Virginia","Washington","West Virginia","Wisconsin","Wyoming"
        });

        [BsonIgnore]
        public string MongoCollectionName { get { return "Abstracts"; } }
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }
        [BsonElement]
        public string UserName { get; set; }
        // [BsonElement]
        // public string UserId { get; set; }
        [BsonElement]
        public string DatasetTitle { get; set; }
        [BsonElement]
        public string Authors { get; set; }
        [BsonElement]
        public string Source { get; set; }
        [BsonElement]
        public int NumberOfStudies { get; set; }
        [BsonElement]
        public string Url { get; set; }
        [BsonElement]
        public bool Rehost { get; set; }
        [BsonElement]
        public bool PublicAccess { get; set; }
        [BsonElement]
        public string Content { get; set; }
        [BsonElement]
        public bool Approved { get; set; }
        [BsonElement]
        public string Rationale { get; set; }
        [BsonElement]
        public bool Reviewed { get; set; }
        [BsonElement]
        public string ReviewedBy { get; set; }  // Mongo GUID for admin who approved/denied request
        [BsonElement]
        public DateTime DateReviewed { get; set; }
        [BsonElement]
        public string DatasetId { get; set; }
        [BsonElement]
        public DateTime DateGenerated { get; set; }
        [BsonElement]
        public bool IsAnonymized { get; set; }
        [BsonElement]
        public string HowAnonymized { get; set; }
        [BsonElement]
        public bool SameModalityAndManuf { get; set; }
        [BsonElement]
        public string Manufacturer { get; set; }
        [BsonElement]
        public string Modality { get; set; }
        [BsonElement]
        public bool HasLabels { get; set; }
        [BsonElement]
        public string LabelType { get; set; }
        [BsonElement]
        public string LabelFormat { get; set; }
        [BsonElement]
        public string LabelsReviewed { get; set; }
        [BsonElement]
        public string LabelLevel { get; set; }
        [BsonElement]
        public string ClinicalIssues { get; set; }
        [BsonElement]
        public string Country { get; set; }
        [BsonElement]
        public string USRegion { get; set; }
        [BsonElement]
        public string State { get; set; }
        [BsonElement]
        public string PixelDataShifted { get; set; }
        [BsonElement]
        public string HowPixelDataShifted { get; set; }
        [BsonElement]
        public string PixelDataSynthesized { get; set; }
        [BsonElement]
        public string HowPixelDataSynthesized { get; set; }
        [BsonElement]
        public string MultipleModalitiesAndManuf { get; set; }
        [BsonElement]
        public string ModelsAndVersions { get; set; }


        public Abstract(string userName, AbstractViewModel model)
        {
            Id = ObjectId.GenerateNewId().ToString();
            UserName = userName;
            DatasetTitle = model.DatasetTitle;
            Authors = model.Authors;
            Source = model.Source;
            Url = model.Url;
            Rehost = model.Rehost;
            PublicAccess = model.PublicAccess;
            Content = model.Content;
            NumberOfStudies = model.NumberOfStudies;
            SameModalityAndManuf = model.SameModalityAndManuf;
            if (SameModalityAndManuf)
            {
                Modality = model.Modality[0] == "Other" ? model.Modality[1] : model.Modality[0];
                Manufacturer = model.Manufacturer[0] == "Other" ? model.Manufacturer[1] : model.Manufacturer[0];
                ModelsAndVersions = model.ModelsAndVersions;
                MultipleModalitiesAndManuf = null;
            }
            else
            {
                Modality = null;
                Manufacturer = null;
                ModelsAndVersions = null;
                MultipleModalitiesAndManuf = model.MultipleModalitiesAndManuf;
            }
            HasLabels = model.HasLabels;
            if (HasLabels)
            {
                LabelType = model.LabelType;
                LabelFormat = model.LabelFormat;
                LabelsReviewed = model.LabelsReviewed;
            }
            else
            {
                LabelType = null;
                LabelFormat = null;
                LabelsReviewed = null;
            }
            ClinicalIssues = model.ClinicalIssues;
            IsAnonymized = model.IsAnonymized;
            HowAnonymized = model.HowAnonymized;
            Country = model.Country;
            if (Country == "United States" || Country == "Unknown")
            {
                USRegion = model.USRegion;
                State = model.State;
            }
            else
            {
                USRegion = null;
                State = null;
            }
            PixelDataShifted = model.PixelDataShifted;
            HowPixelDataShifted = PixelDataShifted == "Yes" ? model.HowPixelDataShifted : null;
            PixelDataSynthesized = model.PixelDataSynthesized;
            HowPixelDataSynthesized = PixelDataSynthesized == "Yes" ? model.HowPixelDataSynthesized : null;
            DateGenerated = DateTime.Now;
        }

        // [BsonConstructor]
        public Abstract(string userName, string url, bool rehost, bool publicAccess, string content)
        {
            Id = ObjectId.GenerateNewId().ToString();
            UserName = userName;
            Url = url;
            Rehost = rehost;
            PublicAccess = publicAccess;
            Content = content;
            DateGenerated = DateTime.Now;
        }

        public void Review(string admin, bool approved, string rationale)
        {
            Approved = approved;
            Rationale = rationale;
            Reviewed = true;
            ReviewedBy = admin;
            DateReviewed = DateTime.Now;
            //Notification<Abstract> = new Notification<Abstract>(UserId, )
        }

        public void AddToDb()
        {
            Database db = new Database();
            db.Add<Abstract>(MongoCollectionName, this);
        }

        public void UpdateInDb()
        {
            Database db = new Database();
            db.Replace<Abstract>(MongoCollectionName, doc => doc.Id == Id, this);
        }

        public Notification GenerateNewAbstractNotification()
        {
            NewAbstractNotification notification = new NewAbstractNotification();
            return new Notification(UserName, notification, this);
        }

        public Notification GenerateReviewedAbstractNotification()
        {
            ReviewedAbstractNotification notification = new ReviewedAbstractNotification();
            return new Notification(UserName, notification, this);
        }

        /*
        public NewAbstractNotification GenerateNewAbstractNotification()
        {
            return new NewAbstractNotification(UserId, new List<Abstract> { this });
        }

        public ReviewedAbstractNotification GenerateReviewedAbstractNotification()
        {
            return new ReviewedAbstractNotification(UserId, new List<Abstract> { this });
        }*/
    }
}