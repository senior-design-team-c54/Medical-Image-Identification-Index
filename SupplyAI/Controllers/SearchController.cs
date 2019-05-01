using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using MI3.Models;
using System.Linq;
using MI3.Models.Repo;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MI3.Controllers
{
    public class SearchController : Controller
    {
        //Where database refrerences would be stored.
        //Since we aren't connecting to a database, we create our own termoprary one to make things look cool
        //Where we will initialize our database stuff
        private Database Database => Database.DB; //alias the name for convenience

        public List<Repository> SearchResults = new List<Repository>();
        public int hi = 0;

        // GET: Search/Index or Search
        public ActionResult Index(string query)
        {
            if (query == null) query = "";
            //must calculate total number of results
            
            int tagStart = query.IndexOf('{')+1;
            int tagEnd = query.IndexOf('}');
            string tagString = "";
            if(tagStart != 0 && tagEnd != -1)
                tagString= query.Substring(tagStart, tagEnd - tagStart);

            Dicom.DicomTag parsedTag = null;
            //parse tag if tag found
            if (tagString != "") {
                try { //try parse by tag ID
                    parsedTag = Dicom.DicomTag.Parse(tagString);
                } catch (Exception e) {
                    parsedTag = null;
                }
                try { //try parse by tag Name
                    parsedTag = Dicom.DicomDictionary.Default[tagString];
                } catch (Exception e) {

                }
            }
            //define search function in mongo database
            Expression<Func<Repository, bool>> filter = (Repository r) =>
                r.Summary.Contains(query) // look in summary
                || r.Name.Contains(query); //look in repo name


            // r.DicomFiles.Any(d => d.Dataset.Contains(parsedTag)); //look for tag (false if tag == null)
            SearchResults = Database.FindRepo(filter);
            ViewBag.Title = SearchResults.Count + " Search Results";
            return View(SearchResults);
            //SearchResults = (from r in Database.DataCollection.AsQueryable() where
            //                r.Summary.Contains(query)
            //                || r.Name.Contains(query) select r).ToList();
            ////|| r.root.Items.Count() >0 select r).ToList();
            //var results = Database.DataCollection.AsQueryable().Select(r => r.root.Items);
            //var res2 = results.Where(n=> n)


            //IQueryable<Repository> items = from r in Database.DataCollection.AsQueryable() where r.root.Items.Where(n=> n.fileType == FileType.Dicom).Cast<RepoDicomFile>().Select(x =>x.dicomFile).Any(d => d.Dataset.Contains(parsedTag)) select r ;
            // Database.FindRepo();
            // var items = Database.DataCollection.AsQueryable().Where(r=> r.root.Items.ConvertAll(x=>(RepoDicomFile)x).Contains(null));
            // FilterDefinition<Repository> qf = "{\"$match\":{\"root.Items\":null}}";

            //// var items = Database.DataCollection.Find(qf);
            // //var items = Database.DataCollection.AsQueryable();
            // //DataCollection.Find(filter)
            // //use filter to search
            // foreach (var v in items)
            //     Console.WriteLine(v);


        }
       


        // GET: Search/Tag/{name}
        [Route("Tag/{name}")]
        public ActionResult Tag(string name)
        {
            SearchResults = Database.FindRepo(x => x.Tags.ContainsKey(name)); //find each database entry with a tag with key name
            
            return View("Index", SearchResults); //we need to specify 'Index', because otherwise it will look for the 'Tag' View
        }

        // GET: Search/Author/{name}
        [Route("Author/{name}")]
        public ActionResult Author(string name)
        {
            //2nd verse, same as the first. See above method for explanation
            SearchResults = Database.FindRepo(x => x.Authors.Contains(name));
        
           // SearchResults = (RepositoryDictionary)results.ToDictionary(a => a.ID);
            return View("Index",this);
        }

        // POST: Repository/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Repository/Upload
        [HttpGet]
        public ActionResult Upload()
        {
            return View();
        }

        
    }
}
