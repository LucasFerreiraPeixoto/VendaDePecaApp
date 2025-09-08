using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        //váriavel para receber conteúdos em arquivos
        static string arquivopeças = @"C:\Users\USER\OneDrive\Desktop\faculdade C#\Projeto Venda de Peças\CodigoProgramaDeVendas\registroPecas.txt";
        static string arquivovendas = @"C:\Users\USER\OneDrive\Desktop\faculdade C#\Projeto Venda de Peças\CodigoProgramaDeVendas\registroVendas.txt";

        public struct peça
        {
            public string nome;
            public int quant;
            public string tipo;
            public float preço;

            public peça(string nome, int quantidade, string tipo, float preço)
            {
                this.nome = nome;
                this.quant = quantidade;
                this .tipo = tipo;
                this.preço = preço;
            }
        }
        static void Main(string[] args)
        {
        }
    }
}
