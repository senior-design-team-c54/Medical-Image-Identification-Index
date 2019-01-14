using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SupplyAI.Models
{
    public class DataSetDictionary : IEnumerable<DataSet>
    {
        private Dictionary<ulong, DataSet> _data = new Dictionary<ulong, DataSet>();



        //the Add function needs to be defined for anonymous initializations. Formated as => new DataSetDictionary(){ {new UInt64(), new DataSet()  },...  }
        public void Add(DataSet data) {
            if (_data.ContainsKey(data.ID))
                throw new Exception("Fatal Error: ID collision {" + data.ID + "}");
            _data[data.ID] = data;
        }

        //Allows the DataSetDictionary to be used like a dictionary 
        public DataSet this[ulong element] {
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
       

        public static explicit operator DataSetDictionary(Dictionary<ulong,DataSet> data) {
            DataSetDictionary dsd = new DataSetDictionary();
            dsd._data = data;
            return dsd;
        }

        public IEnumerator<DataSet> GetEnumerator() {
            return _data.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return _data.Values.GetEnumerator();
        }
    }
}