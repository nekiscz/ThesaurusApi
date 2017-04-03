using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thesaurus_API.ApiObjects
{
    class Word
    {
        [DeserializeAs(Name = "noun")]
        public Record Noun { get; set; }
        [DeserializeAs(Name = "verb")]
        public Record Verb { get; set; }
    }
}
