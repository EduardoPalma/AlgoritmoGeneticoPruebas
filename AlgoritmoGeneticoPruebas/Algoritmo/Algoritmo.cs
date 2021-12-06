using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoritmoGeneticoPruebas.Algoritmo
{
    class AlgoritmoGenetico
    {
        List<Cromosoma> soluciones;
        List<Cromosoma> ganadores;
        public int cantSolucions { get; set; }
        public int iteraciones { get; set; }

        public AlgoritmoGenetico(int cantSoluciones, int iteraciones)
        {
            this.soluciones = new List<Cromosoma>();
            this.cantSolucions = cantSoluciones;
            this.iteraciones = iteraciones;
            AlgoritmoGenetic();
        }

        private void CrearPoblacion()
        {
            var seed = Environment.TickCount;
            var random = new Random(seed);
            for(int i=0;i<this.cantSolucions;i++){
                int rand = random.Next(1,50);
                Cromosoma crom = new Cromosoma(rand);
                this.soluciones.Add(crom);
            }

        }

        private void calcularFitnes()
        {
            foreach (var valor in this.soluciones)
            {
                valor.fittnes = Funcion(valor.valor);
                valor.hexaABinario();
            }
        }
        private void SeleccionTorneo(Random random)
        {
            this.ganadores = new List<Cromosoma>();
            var probabilidad = random.Next(1, this.soluciones.Count);

            int i = 0;
            while (i < probabilidad)
            {
                int luchardor1 = random.Next(0, this.soluciones.Count);
                int luchardor2 = random.Next(0, this.soluciones.Count);

                
                if (this.soluciones[luchardor1].fittnes > this.soluciones[luchardor2].fittnes)
                {
                    if (this.ganadores.IndexOf(this.soluciones[luchardor1]) == -1)
                    {
                        this.ganadores.Add(this.soluciones[luchardor1]);
                        
                        i++;
                    }
                }
                else
                {
                    if (this.ganadores.IndexOf(this.soluciones[luchardor2]) == -1)
                    {
                        this.ganadores.Add(this.soluciones[luchardor2]);
                        
                        i++;
                    }
                }
                
            }

        }

        private string hijo(string padre,int pos, char gen)
        {
            string hijo = "";
            for (int i=0;i<padre.Length;i++)
            {
                if(i == pos) hijo += gen;
                else hijo += padre[i];
            }
            return hijo;
        }
        /*
         * 
         * 
         */
        private void Cruze(Random random)
        {
            double probabilidad = random.NextDouble();
            List<Cromosoma> hijos = new List<Cromosoma>();
            if(probabilidad <= 0.8){

                int i = 0;
                while (i < this.soluciones.Count)
                {
                   
                    string padre1 = this.ganadores[random.Next(0, this.ganadores.Count)].codigoGenetico;
                    string padre2 = this.soluciones[random.Next(0, this.soluciones.Count)].codigoGenetico;
       
                    int posGen1 = random.Next(0, padre1.Length);
                    int posGen2 = random.Next(0, padre2.Length);
                    char gen1 = padre1[posGen1];
                    char gen2 = padre2[posGen2];
                    string hijo1 = hijo(padre1, posGen1, gen2);
                    string hijo2 = hijo(padre2, posGen2, gen1);
   
                    Cromosoma cromosoma1 = new Cromosoma(Cromosoma.binarioDecimal(hijo1));
                    Cromosoma cromosoma2 = new Cromosoma(Cromosoma.binarioDecimal(hijo2));
                    hijos.Add(cromosoma1);
                    hijos.Add(cromosoma2);
                    i += 2;
                }
                this.soluciones = hijos;
            }
        }
        public string Muta(string gen, int mutacion1, int mutacion2)
        {
            char fenotipo1 = gen[mutacion1];
            char fenotipo2 = gen[mutacion2];
            string mutacion = "";
            for (int i=0;i<gen.Length;i++)
            {
                if (i == mutacion1)
                {
                    mutacion += fenotipo2;
                }
                else
                {
                    if (i == mutacion2)
                    {
                        mutacion += fenotipo1;
                    }
                    else
                    {
                        mutacion += gen[i];
                    }
                }
            }
            return mutacion;
        }
        private void Mutacion(Random random)
        {
            if(random.NextDouble() < 0.2)
            {
                int mutaciones = random.Next(0, this.soluciones.Count);
                int i = 0;
                while (i <  mutaciones)
                {
                    int posCromosomaMutado = random.Next(0, this.soluciones.Count);
                    Cromosoma mutado = this.soluciones[posCromosomaMutado];
                    string gen = mutado.codigoGenetico;
                    
                    int mutacion1 = random.Next(0, gen.Length);
                    int mutacion2 = random.Next(0, gen.Length);
                    
                    mutado.valor = Cromosoma.binarioDecimal(Muta(gen,mutacion1,mutacion2));
                    i++;
                }
            }
        }
        private void AlgoritmoGenetic()
        {
            var seed = Environment.TickCount;
            var random = new Random(seed);
            CrearPoblacion();
            MostrarPosiblesSoluciones();
            int ite = 0;
            while(ite < this.iteraciones)
            { 
                calcularFitnes();
                SeleccionTorneo(random);
                Cruze(random);
                Mutacion(random);
                ite++;
                Console.WriteLine("generacion "+ite);
                MostrarPosiblesSoluciones();
            }
        }

        public void MostrarPosiblesSoluciones()
        {
            foreach (var solu in this.soluciones)
            {
                Console.Write(solu.valor+ " | ");
            }
            Console.WriteLine();
        }

        private int Funcion(int x)
        {
            return 2*x + 3 *x;
        }

    }
}
