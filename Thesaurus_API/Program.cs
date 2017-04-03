using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thesaurus_API.ApiObjects;

namespace Thesaurus_API
{
    class Program
    {
        static Random rnd;
        static ThesaurusApi api;
        static string[] skipWords;

        static void Main(string[] args)
        {
            api = new ThesaurusApi();
            rnd = new Random();
            skipWords = GetSkipwords();

            do
            {
                Console.Write("Write name to change: ");

                var input = Console.ReadLine();
                var name = GenerateName(input);

                Console.WriteLine("New name: " + name);
            } while (Continue());

            Console.WriteLine("Pres Enter...");
            Console.ReadLine();
        }

        private static string GenerateName(string input)
        {
            var oldName = SeparateWords(input);

            var newName = new List<string>();

            foreach (var word in oldName)
            {
                if (!skipWords.Contains(word))
                {
                    var response = api.GetWord<Word>(word);

                    if (response == null)
                        newName.Add(word);
                    else
                        newName.Add(GetSynonym(response));
                }
                else
                    newName.Add(word);
            }

            var name = string.Join(" ", newName);

            return name;
        }

        private static bool Continue()
        {
            Console.Write("Another one? (Y/N) ");
            var input = Console.ReadLine();

            if (input.ToLower() == "y")
            {
                Console.WriteLine();
                return true;
            }
            else
                return false;
        }

        private static string[] SeparateWords(string text)
        {
            return text.Split(' ');
        }

        private static string GetSynonym(Word word)
        {
            var synonyms = new List<string>();

            if (word.Noun != null)
            {
                if (word.Noun.Synonyms != null)
                    synonyms.AddRange(word.Noun.Synonyms);
            }

            if (word.Verb != null)
            {
                if (word.Verb.Synonyms != null)
                    synonyms.AddRange(word.Verb.Synonyms);
            }

            var some = synonyms.OrderBy(r => r.Length).Reverse().Take(11);
            return some.OrderBy(r => rnd.Next()).FirstOrDefault();
        }

        private static string[] GetSkipwords()
        {
            return ConfigurationManager.AppSettings["skip"].Split(';');
        }
    }
}
