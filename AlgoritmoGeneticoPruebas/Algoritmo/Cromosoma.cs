using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoritmoGeneticoPruebas.Algoritmo
{
    class Cromosoma
    {
        public int valor { get; set; }
        public int fittnes { get; set; }
        public string codigoGenetico { get; set; } 

        public Cromosoma(int valor)
        {
            this.valor = valor;
            hexaABinario();
        }

        public void hexaABinario()
        {
            string codigo = Convert.ToString(this.valor, 2);
            string codigoGenetico = "";
            int j = 0;
            for (int i= 8; i > 0;i--)
            {
                if (i > codigo.Length)
                {
                    codigoGenetico += '0';
                }
                else
                {
                    codigoGenetico += codigo[j];
                    j++;
                }
            }
            this.codigoGenetico = codigoGenetico;
            
        }
        
        internal static int binarioDecimal(string binario)
        {
            int dec = 0;
            for (int i = binario.Length-1;i> -1;i--)
            {
                if (binario[i] == '1')
                {
                    dec += (int) Math.Pow(2, i);
                }
            }
            return dec;
        }


    }
}
