using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Automatas.Models
{
    public class AutomataN
    {
        public List<EstadoN> estados = new List<EstadoN>();
        public List<string> alfabeto = new List<string>();
        List<(int actual, Transition transicion)> tupla = new List<(int actual, Transition transicion)>();
        public string estadoInicial;
        public AutomataN(List<string> listaTexto)
        {
            {

                int cantidad = Convert.ToInt32(listaTexto[0]);
                this.estadoInicial = listaTexto[1];
                string finales = listaTexto[2].Trim();
                for (int i = 1; i <= cantidad; i++)
                {
                    this.estados.Add(new EstadoN(Convert.ToString(i), new List<Transition>(), false));
                }
                List<Transition> transitions = new List<Transition>();
                string linea;
                for (int i = 3; i < listaTexto.Count; i++)
                {
                    linea = listaTexto[i].ToString();
                    transitions.Add(new Transition(linea.Split(',')[0].Trim(), linea.Split(',')[1].Trim(), linea.Split(',')[2].Trim()));
                }
                transitions.Sort();
                this.alfabeto = lenguaje(transitions);
                ConstruirTransiciones(this.estados, transitions);
                estadosFinales(this.estados, finales);
            }
        }
        private List<string> lenguaje(List<Transition> transitions)
        {
            List<string> language = new List<string>();
            for (int i = 0; i < transitions.Count; i++)
            {
                if (!language.Contains(transitions[i].entrada))
                {
                    language.Add(transitions[i].entrada.Trim());
                }
            }
            return language;
        }
        private void ConstruirTransiciones(List<EstadoN> estado, List<Transition> transitions)
        {
            for (int i = 0; i < transitions.Count; i++)
            {
                int index =-1;
                for (int j = 0; j < estado.Count; j++)
                {
                    if (estado[j].nombre == transitions[i].inicio)
                    {
                        index = j;
                        j = estado.Count;
                    }
                }
                estado[index].transiciones.Add(transitions[i]);
            }
        }
        private void estadosFinales(List<EstadoN> estados, string final)
        {
            for (int i = 0; i < final.Split(',').Count(); i++)
            {
                int index = -1;
                for (int j = 0; j < estados.Count; j++)
                {
                    if (estados[j].nombre == Convert.ToString(final.Split(',')[i].Trim()))
                    {
                        index = j;
                        j = estados.Count;
                    }
                }
                estados[index].final = true;
            }
        }
        public List<string> Comprobar(string entrada)
        {
            List<string> caminos = new List<string>();
            int actual = 0;
            for (int i = 0; i < estados.Count; i++)
            {
                if (estados[i].nombre == estadoInicial)
                {
                    actual = i;
                    break;
                }
            }
            EstadoN Nactual = estados[actual];
            tupla.Add((0, new Transition("1", "", "1")));
            for (int i = 0; i < tupla.Count; i++)
            {
                var actualpath = tupla[i];
                for (int j = 0; j < estados.Count; j++)
                {
                    if (actualpath.transicion.final == estados[j].nombre)
                    {
                        actual = j;
                        j = estados.Count();
                    }
                }
                Nactual = estados[actual];
                string camino = "";
                List<(int actual, Transition transicion)> temporal = Todas(Nactual, entrada, actualpath.actual);
                if (actualpath.actual == entrada.Length)
                {
                    for (int j = 1; j <= i; j++)
                    {
                        camino += tupla[j].transicion.Recorrido() + "\n";
                        tupla.RemoveAt(j);
                        j--;
                        i--;
                    }
                    if (Nactual.final) camino += "Si";
                    else camino += "No";
                    caminos.Add(camino);
                }
                if (actualpath.actual != entrada.Length)
                {
                    if (temporal.Count > 0)
                    {
                        tupla.InsertRange(i + 1, temporal);
                    }
                    else
                    {
                        for (int j = 1; j <= i; j++)
                        {
                            camino += tupla[j].transicion.Recorrido() + "\n";
                            tupla.RemoveAt(j);
                            i--;
                            j--;
                        }
                        camino += "No";
                        caminos.Add(camino);
                    }
                }
            }
            return caminos;
        }
        List<(int current, Transition destiny)> Todas(EstadoN estado, string entrada, int actual)
        {
            List<(int current, Transition destiny)> caminos = new List<(int current, Transition destiny)>();
            string actualTexto = "";
            for (int i = actual; i < entrada.Length; i++)
            {
                actualTexto += entrada[i];
                for (int j = 0; j < estado.transiciones.Count; j++)
                {
                    if (estado.transiciones[j].entrada == actualTexto)
                    {
                        caminos.Add((actualTexto.Length + actual, estado.transiciones[j]));
                    }
                }
            }
            if (caminos.Count == 0 || alfabeto.Contains(""))
            {
                actualTexto = "";
                for (int j = 0; j < estado.transiciones.Count; j++)
                {
                    if (estado.transiciones[j].entrada == "")
                    {
                        caminos.Add((actual, estado.transiciones[j]));
                    }
                }
            }
            return caminos;
        }
    }
}
