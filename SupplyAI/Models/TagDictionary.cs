using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SupplyAI.Models
{
    public class TagDictionary : IEnumerable<Tag>
    {
        private Dictionary<string, Tag> _data = new Dictionary<string, Tag>();



        //the Add function needs to be defined for anonymous initializations. Formated as => new DataSetDictionary(){ {new UInt64(), new Repository()  },...  }
        public void Add(Tag data) {
            if (_data.ContainsKey(data.Name))
                throw new Exception("Fatal Error: Tag Name collision {" + data.Name + "}");
            _data[data.Name] = data;
        }

        //Allows the DataSetDictionary to be used like a dictionary 
        public Tag this[string element]
        {
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


        public IEnumerator<Tag> GetEnumerator() {
            return _data.Values.GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator() {
            return _data.Values.GetEnumerator();
        }
    }
}