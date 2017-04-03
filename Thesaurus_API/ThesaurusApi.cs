using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thesaurus_API
{
    class ThesaurusApi
    {
        const string BaseUrl = "http://words.bighugelabs.com/api/2/{key}/{word}/json";
        const string ApiKey = "03416504b5b46daec382e35a44c69ddc";

        public T GetWord<T>(string word) where T : new()
        {
            var client = new RestClient();
            var request = new RestRequest();
            client.BaseUrl = new System.Uri(BaseUrl);
            request.AddParameter("key", ApiKey, ParameterType.UrlSegment);
            request.AddParameter("word", word, ParameterType.UrlSegment);
            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var twilioException = new ApplicationException(message, response.ErrorException);
                throw twilioException;
            }
            return response.Data;
        }

    }
}
