using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MI3.Models
{
    public class RepositoryDictionary : IEnumerable<Repository>
    {
        private Dictionary<ulong, Repository> _data = new Dictionary<ulong, Repository>();



        //the Add function needs to be defined for anonymous initializations. Formated as => new DataSetDictionary(){ {new UInt64(), new Repository()  },...  }
        public void Add(Repository data) {
            if (_data.ContainsKey(data.ID))
                throw new Exception("Fatal Error: ID collision {" + data.ID + "}");
            _data[data.ID] = data;
        }

        //Allows the DataSetDictionary to be used like a dictionary 
        public Repository this[ulong element] {
            get {
                if (_data.ContainsKey(element))
                    return _data[element];
                else
                    return null;
            }
            set {
                _data[element] = value;
            }
        }
       

        public static explicit operator RepositoryDictionary(Dictionary<ulong,Repository> data) {
            RepositoryDictionary dsd = new RepositoryDictionary();
            dsd._data = data;
            return dsd;
        }

        public IEnumerator<Repository> GetEnumerator() {
            return _data.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return _data.Values.GetEnumerator();
        }
    }
}