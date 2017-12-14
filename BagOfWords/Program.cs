using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.MachineLearning;
using Accord.Math.Distances;

namespace LanguageProcesing
{
    class Program
    {
        static void Main(string[] args)
        {
            DiferenciasEntities db = new DiferenciasEntities();
            var query = db.Edicion.Where(x => x.Tecnico.Equals("CSM05"));
            string[] edits = query.Select(x => x.CadenaInicial).ToArray();
            string[][] words = edits.Tokenize();
            var codebook = new BagOfWords()
            {
                MaximumOccurance = 1
            };
            codebook.Learn(words);

            double[] bow1 = codebook.Transform(words[0]);
            double[] bow2 = codebook.Transform(words[1]);
            Cosine cosine = new Cosine();
            
            Console.WriteLine(cosine.Similarity(bow1, bow2));
        }
    }
}
