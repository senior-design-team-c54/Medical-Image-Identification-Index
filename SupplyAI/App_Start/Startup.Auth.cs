using MI3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;


//[assembly: OwinStartupAttribute(typeof(MI3.Startup))]
namespace MI3
{
     public partial class Startup
     {
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            // app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }

        public const string AppName = "Medical Image Identification Index";

        public static Database Database { get; private set; }
        public static MongoClient client { get { return Database.client; } }
        
       // public static readonly RepositoryDictionary Database = new RepositoryDictionary();
        public static readonly TagDictionary Tags = new TagDictionary();

        public static void Init() {
            Database = new Database();
            initTags(); //must be first
            //initMongo();
        }
        private static void initMongo() {
            var cols = Database.DefaultDB.ListCollectionNames();
       
            if (cols.ToList().Contains("Repository"))
                return;

            System.Diagnostics.Debug.WriteLine("Initializing Mongo Database. This should only occur once\n\n");

            var ds = new Repository("Heart Arrhythmia");
            ds.Summary = "This dataset contains studies of MRI scans of hearts from various adult male patients with hearth Arryhthmia.";
            ds.Authors.AddRange(new List<string> { "Joe Smith", "Frank Zappa", "Charlie Brown" });
            ds.addTags("heart", "disease");
            Database.AddRepository(ds);

            ds = new Repository("Lung Cross-Section");
            ds.Summary = "Contains anonymized patient x-rays of lungs with no abnormal conditions. Because the data was anonymized there was no information on patient age, gender or height.";
            ds.Authors.AddRange(new List<string> { "Frank Zappa", "Peter Peters" });
            ds.addTags("lungs", "normal");
            Database.AddRepository(ds);

            ds = new Repository("Motor Cortex Neural Activity");
            ds.Authors.AddRange(new List<string> { "Joe Smith", "Peter Peters" });
            ds.addTags("brain", "normal");
            Database.AddRepository(ds);

            ds = new Repository("Bone density scans in elderly patients ");
            ds.Authors.AddRange(new List<string> { "Steven Franks" });
            ds.addTags("age", "disease", "bones");
            Database.AddRepository(ds);


        }



        //private static void initDatabase(){
            

        //    var ds = new Repository( "Heart Arrhythmia");
        //    ds.Summary = "This dataset contains studies of MRI scans of hearts from various adult male patients with hearth Arryhthmia.";
        //    ds.Authors.AddRange(new List<string>{ "Joe Smith", "Frank Zappa","Charlie Brown"});
        //    ds.addTags("heart", "disease");
        //    Database.Add(ds);
        //    ds = new Repository("Lung Cross-Section" );
        //    ds.Summary = "Contains anonymized patient x-rays of lungs with no abnormal conditions. Because the data was anonymized there was no information on patient age, gender or height.";
        //    ds.Authors.AddRange(new List<string> { "Frank Zappa", "Peter Peters" });
        //    ds.addTags("lungs","normal");
        //    Database.Add(ds);
        //    ds = new Repository("Motor Cortex Neural Activity");
        //    ds.Authors.AddRange(new List<string> { "Joe Smith", "Peter Peters" });
        //    ds.addTags("brain","normal");      
        //    Database.Add(ds);

        //    ds = new Repository("Bone density scans in elderly patients ");
        //    ds.Authors.AddRange(new List<string> { "Steven Franks" });
        //    ds.addTags("age","disease","bones");
        //    Database.Add(ds);

        //    //copied and pasted
        //    ds = new Repository( "Heart Arrhythmia" );
        //    ds.Summary = "This dataset contains studies of MRI scans of hearts from various adult male patients with hearth Arryhthmia.";
        //    ds.Authors.AddRange(new List<string> { "Joe Smith", "Frank Zappa", "Charlie Brown" });
        //    ds.addTags("heart", "disease");
        //    Database.Add(ds);
        //    ds = new Repository( "Lung Cross-Section" );
        //    ds.Summary = "Contains anonymized patient x-rays of lungs with no abnormal conditions. Because the data was anonymized there was no information on patient age, gender or height.";
        //    ds.Authors.AddRange(new List<string> { "Frank Zappa", "Peter Peters" });
        //    ds.addTags("lungs", "normal");
        //    Database.Add(ds);
        //    ds = new Repository("Motor Cortex Neural Activity" );
        //    ds.Authors.AddRange(new List<string> { "Joe Smith", "Peter Peters" });
        //    ds.addTags("brain", "normal");
        //    Database.Add(ds);
        //    ds = new Repository("Bone density scans in elderly patients ");
        //    ds.Authors.AddRange(new List<string> { "Steven Franks" });
        //    ds.addTags("age", "disease", "bones");
        //    Database.Add(ds);

        //}


        private static void initTags() {
            at("organ");
            at("organ:bones");
            at("organ:heart");
            at("organ:skin");
            at("organ:lungs");
            at("organ:brain");
            at("condition");
            at("condition:normal");
            at("condition:disease");
            at("anatomical region");
            at("patient");
            at("patient:age");
            at("patient:gender");
            at("patient:weight");
            at("patient:ethnicity");
            at("size");
        }
        //a shortcut method for adding Tags (at=> add tag)
        private static void at(string name) {
            MI3.Models.Tag parent = null;
            name = name.ToLower(); //only lowercase letters for Tags
            if (name.Contains(':')) { 
                string parentName = name.Substring(0, name.IndexOf(':'));
                name = name.Substring(name.IndexOf(':')+1);
                parent = Tags[parentName];
                if (parent == null)
                    throw new Exception("Parent Tag Named {"+parentName+"} does not exist."); //replace this with a browser side warning when trying to add a tag that doesn't work
            }

            Tags.Add(new MI3.Models.Tag(name,parent));
        }
     }
}