using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thesaurus_API.ApiObjects
{
    class Record
    {
        [DeserializeAs(Name = "syn")]
        public List<string> Synonyms { get; set; }
        [DeserializeAs(Name = "ant")]
        public List<string> Antonyms { get; set; }
    }
}
