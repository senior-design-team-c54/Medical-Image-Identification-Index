using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MI3.Models;

namespace MI3.Controllers
{
    public class SearchController : Controller
    {
        //Where database refrerences would be stored.
        //Since we aren't connecting to a database, we create our own termoprary one to make things look cool
        //Where we will initialize our database stuff
        private Database Database => Startup.Database; //alias the name for convenience

        public List<Repository> SearchResults;
        public int hi = 0;

        // GET: Search/Index or Search
        public ActionResult Index(string query)
        {
            if (query == null) query = "";
            //must calculate total number of results
            SearchResults =  Database.FindRepo(r => r.Name.Contains(query));
            ViewBag.Title =  SearchResults.Count + " Search Results";
            return View(SearchResults);
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
