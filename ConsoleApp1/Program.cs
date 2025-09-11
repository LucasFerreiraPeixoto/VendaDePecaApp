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

            public override string ToString()
            {
                return"hhhhhh"
                    
        }

            public struct venda
            {
                public string nome;
                public int quantidade;
                public float preço;
                public string data;

                public venda (string nome, int quantidade, float preço, string data)
                {
                    this.nome = nome;
                    this.quantidade = quantidade;
                    this.preço = preço;
                    this.data = data;
                }
                public override string ToString()
                {
                    return " hhhh"
          
                }
            }
        static void Main(string[] args)
        {
        }
    }
}
