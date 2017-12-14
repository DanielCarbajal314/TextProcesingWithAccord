using ConsoleApplication1.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiffMatchPatch;
using ConsoleApplication1.DifferenceMatch;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            BaseDeDatosInformes db = new BaseDeDatosInformes();
            var informes = db.Informe;
            int contador = 1;
            List<Diferencia> bufferDeDiferencias = new List<Diferencia>();
            foreach (Informe informe in informes)
            {
                var diferencias = AnalizadorDeDiferencias.sacarDiferencias(informe);
                if (diferencias.Count > 0)
                {
                    bufferDeDiferencias.AddRange(diferencias);
                    //diferencias.ForEach(x => x.Print());
                }
                if (contador % 500 == 0)
                {
                    using (BaseDeDatosDeDiferencias diferenciasDB = new BaseDeDatosDeDiferencias())
                    {
                        var diferenciasAREgistrar = bufferDeDiferencias.Select(x => new Edicion()
                        {
                            Medico = x.MedicoSupervisor,
                            Tecnico = x.Tecnico,
                            CadenaInicial = x.CadenaInicial,
                            CadenaFinal = x.CadenaFinal
                        });
                        diferenciasDB.Edicion.AddRange(diferenciasAREgistrar);
                        diferenciasDB.SaveChanges();
                    }
                }
                Console.WriteLine(contador++);
            }
        }
    }
}
