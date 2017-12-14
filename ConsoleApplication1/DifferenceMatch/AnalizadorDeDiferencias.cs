using ConsoleApplication1.Parser;
using DiffMatchPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DifferenceMatch
{
    public class AnalizadorDeDiferencias
    {
        private static diff_match_patch recolectorDeDiferencias = new diff_match_patch();

        public static List<Diferencia> sacarDiferencias(Informe informe)
        {
            List<Diferencia> diferenciasRelevantes = new List<Diferencia>();
            string textoInicial = PreprocemientoDeTexto.LimpiarTextoInicial(informe.CuerpoRealizaEnc);
            string textoFinal = PreprocemientoDeTexto.LimpiarTextoInicial(informe.CuerpoValidaEnc);
            Diff[] diferencias = recolectorDeDiferencias.diff_main(textoInicial, textoFinal,true).ToArray();
            var listaDeDiferencias = diferencias.ToList();
            recolectorDeDiferencias.diff_cleanupSemantic(listaDeDiferencias);
            diferencias = listaDeDiferencias.ToArray();
            for (int i = 0; i < diferencias.Count()-1; i++)
            {
                if (EsRelevante(diferencias, i))
                {
                    Diferencia diferenciaRelevante = new Diferencia()
                    {
                        Tecnico = informe.medicoinforma,
                        MedicoSupervisor = informe.medicorevisa,
                        CadenaInicial = diferencias[i].text,
                        CadenaFinal = diferencias[i + 1].text
                    };
                    diferenciasRelevantes.Add(diferenciaRelevante);
                }
            }
            return diferenciasRelevantes;
        }

        private static bool EsRelevante(Diff[] diferencias, int i)
        {
            bool SeBorroEnEstaOperacion = diferencias[i].operation == Operation.DELETE;
            bool SeInsertoEnLaSiguienteOperacion = diferencias[i+1].operation == Operation.INSERT;
            bool EsUnaCadenaLarga = diferencias[i].text.Length > 15 && diferencias[i + 1].text.Length > 15;
            return SeBorroEnEstaOperacion && SeInsertoEnLaSiguienteOperacion && EsUnaCadenaLarga;
        }
    }

    public class Diferencia{
        public string Tecnico { get; set; }
        public string MedicoSupervisor { get; set; }
        public string CadenaInicial { get; set; }
        public string CadenaFinal { get; set; }

        public void Print()
        {
            Console.WriteLine("=====================");
            Console.WriteLine(this.CadenaInicial);
            Console.WriteLine("-------------------");
            Console.WriteLine(this.CadenaFinal);
        }
    }
}
