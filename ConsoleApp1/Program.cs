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
            public int id;

            public peça(string nome, int quantidade, string tipo, float preço, int id)
            {
                this.nome = nome;
                this.quant = quantidade;
                this.tipo = tipo;
                this.preço = preço;
                this.id = id;
            }
            public override string ToString()//Sobrescrevendo o método Tostring () para que seja exibido a imressão
            {
                return $"╔══════════════════════════════════════════════════════╗\n" +
                       $"║ Nome: {nome,-40}                                     ║\n" +
                       $"║ Quantidade: {quant,-33}                              ║\n" +
                       $"║ Preço: R$ {preço,-36:N2}                             ║\n" +
                       $"║ Tipo:{tipo,-36:N@}                                   ║\n" +
                       $"║ Código:{id,-36:N@}                                   ║\n" +
                       $"╚══════════════════════════════════════════════════════╝";
            }
        }
        public struct venda
        {
            public string nome;
            public int quantidade;
            public string preço;
            public string tipo;
            public int id;
        }
            static void Main(string[] args)
            {

            }
    }
 }

