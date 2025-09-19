using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
                                           //============================ CAMADA DE PERSISTÊNCIA (Arquivos)============================

        //Inclui todas as entradas e saídas nos arquivos registroVendas e registroPecas

        // Váriavel para receber conteúdos em arquivos
        static string arquivopeças = @"C:\Users\USER\OneDrive\Desktop\faculdade C#\Projeto Venda de Peças\CodigoProgramaDeVendas\registroPecas.txt";
        static string arquivovendas = @"C:\Users\USER\OneDrive\Desktop\faculdade C#\Projeto Venda de Peças\CodigoProgramaDeVendas\registroVendas.txt";

                                            //============================ CAMADA DE DADOS (Modelo e Estruturas)============================

        //Define as estruturas das entidades.

        // Criação da struct para as Peças que serão cadastradas e vendidas no sistema
        public struct peça
        {
            public string nome, tipo;
            public int quant, id;
            public float preço;
           

            public peça(string nome, int quantidade, string tipo, float preço, int id)
            {
                this.nome = nome;
                this.quant = quantidade;
                this.tipo = tipo;
                this.preço = preço;
                this.id = id;
            }
            public override string ToString()//Sobrescrevendo o método Tostring () para que seja exibido a impressão dos dados das peças;
            {
                return $"╔══════════════════════════════════════════════════════╗\n" +
                       $"║ Nome: {nome,-40}                                     ║\n" +
                       $"║ Quantidade: {quant,-33}                              ║\n" +
                       $"║ Preço: R$ {preço,-36}                                ║\n" +
                       $"║ Tipo:{tipo,-36}                                      ║\n" +
                       $"║ Código:{id,-36}                                      ║\n" +
                       $"╚══════════════════════════════════════════════════════╝";
            }
        }

        //Struct criada para as Operações de Venda
        public struct venda
        {
            public string nome, tipo;
            public int quant, id;
            public float preço;
            public string data;


            public venda(string nome, int quantidade, string tipo, float preço, int id, string data)
            {
                this.nome = nome;
                this.quant = quantidade;
                this.tipo = tipo;
                this.preço = preço;
                this.id = id;
                this.data = data;

            }
            public override string ToString()//Sobrescrevendo o método Tostring () para que seja exibido a impressão da caixa abaixo ao efetuar uma venda;
            {
                return "  ╔════════════════════════════════════════════════════════════════════════════════════╗\n" +
                        $"║ Nome: {nome,-20} Quantidade vendida: {quant,-8} Preço unitário: R$ {preço,-10:N2}  ║\n" +
                        $"║ Tipo: {tipo,-20}                                                                   ║\n" +
                        $"║ Id: {id,-20} Total: R$ {(quant * preço),-38:N2}                                    ║\n" +
                        " ╚═════════════════════════════════════════════════════════════════════════════════=══╝";
            }
        }

                                          //============================ CAMADA DE LÓGICA DE NEGÓCIO ============================

        //Regras de negócio, processar cálculos e operações principais

        //Cria a Função para imprimir a relação de todas as peças cadastradas no sistema;
        static void imprimir(List<peça> listaparaimpressao)
        {
            int i, j;
            Console.Clear();
            if (listaparaimpressao.Count == 0) //Caso a lista ESTEJA vazia o sistema informa ao usuário e pede para voltar ao loop principal(MENU);
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                 ATENÇÃO: ESTOQUE DE PEÇAS VAZIO!                     ║");
                Console.WriteLine("╠══════════════════════════════════════════════════════════════════════╣");
                Console.WriteLine("║  NÂO há peças cadastradas no estoque para impressão no momento.      ║");
                Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                Console.WriteLine("\nPressione ENTER para voltar ao menu...");
                Console.ReadLine();
            }
            else//Caso a lista NÃO esteja vazia exibe todas as peças cadastradas;
            {
                Console.WriteLine("=======================================================:");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                           RELAÇÃO DE PEÇAS NO ESTOQUE                        ║");
                Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                j = listaparaimpressao.Count;
                for (i = 0; i < j; i++)
                {
                    Console.WriteLine(listaparaimpressao[i]);
                }
                Console.WriteLine("Pressione ENTER para continuar...");//Faz uma pausa após imprimir a relação de PEÇAS ao usuário;
                Console.ReadLine();
                Console.WriteLine("================================================================================");
            }
        }

                //============================ CAMADA DE INTERFACE COM O USUÁRIO ============================

        //Responsável pela interação e exibição de menus, ler entradas e mostrar resultados ao usuário pelo console.

        //Cria a Função de Consulta/Exclusão de todas as peças;
        static void consultar(List<peça> consultarpeças, string encontrarNome)
        {
            int i, c, emestoque, opç;
            c = consultarpeças.Count;
            emestoque = 0;

            bool encontrado = false;

            for (i = 0; i < consultarpeças.Count; i++)
            {
                if (consultarpeças[i].nome == encontrarNome)
                {
                    encontrado = true;
                    emestoque = consultarpeças[i].quant;

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" ╔══════════════════════════════════════════════════════════════════════════════╗");
                    Console.WriteLine(" ║                        CONSULTA DE PEÇAS NO ESTOQUE                          ║");
                    Console.WriteLine(" ╠══════════════════════════════════════════════════════════════════════════════╣");
                    Console.WriteLine($"║  Produto encontrado: {encontrarNome,-60}                                     ║");
                    Console.WriteLine($"║  Quantidade em estoque: {emestoque,-52}                                      ║");
                    Console.WriteLine(" ╚══════════════════════════════════════════════════════════════════════════════╝");
                    Console.ResetColor();

                    //Oferce ao usuário poder excluir o produto cadastrado
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════╗");
                    Console.WriteLine("║  Deseja EXCLUIR essa peça do estoque?                                        ║");
                    Console.WriteLine("║    [1] Sim                                                                   ║");
                    Console.WriteLine("║    [9] Não                                                                   ║");
                    Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════════╝");
                    Console.ResetColor();

                    //opç é usado para armazenar a opção do usuário;
                    opç = int.Parse(Console.ReadLine());

                    if (opç == 1)
                    {
                        consultarpeças.RemoveAt(i);//Remove a peça que foi encontrada na lista na posição que i estiver;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(" ╔═══════════════════════════════════════════════════════════════════════════════════════════════╗");
                        Console.WriteLine($"║  Produto \"{encontrarNome}\" excluído com sucesso!{new string(' ', 52 - encontrarNome.Length)}║");
                        Console.WriteLine(" ╚═══════════════════════════════════════════════════════════════════════════════════════════════╝");
                        Console.ResetColor();

                        File.WriteAllLines(arquivopeças, consultarpeças.Select(r => $"{r.nome};{r.tipo};{r.quant};{r.preço};{r.id}"));
                        Console.WriteLine("Estoque salvo em " + arquivopeças);

                        Console.WriteLine("\nPressione ENTER para voltar ao menu...");
                        Console.ReadLine();
                        break;//Para o "Processo" após excluir a peça;
                    }
                    else if (opç == 9)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
                        Console.WriteLine("║                 PEÇA NÃO EXCLUÍDA DO ESTOQUE!                        ║");
                        Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");
                        Console.ResetColor();
                        Console.WriteLine("\nPressione ENTER para voltar ao menu...");
                        Console.ReadLine();
                        break;
                    }
                    else//Caso o usuário não insira uma opção válida 
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
                        Console.WriteLine("║                        ATENÇÃO: OPÇÃO INVÁLIDA!                      ║");
                        Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");
                        Console.ResetColor();
                        Console.WriteLine("\nPressione ENTER para voltar ao menu...");
                        Console.ReadLine();
                        break;
                    }
                }

            }
            //Caso produto não foi encontrado o sistema informa o usuário;
            if (!encontrado)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                        ATENÇÃO: PEÇA NÃO ENCONTRADA!                 ║");
                Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                Console.WriteLine("\nPressione ENTER para voltar ao menu...");
                Console.ReadLine();
            }

        }


        static void imprimirvendas(List<venda> listarparaimprimir, float totalpeças)
        {
            int i, q;
            Console.Clear();
            //Caso não tenha sido executado nenhuma venda, não há o que imprimir.
            if (listarparaimprimir.Count == 0)
            {
                Console.WriteLine("Não há vendas para imprimir");
                Console.ReadLine();
            }
            //Caso uma ou mais vendas tenham sido realizadas, peças serão impressas ao usuário.
            else
            {
                Console.WriteLine("=======================================================:");
                Console.WriteLine("Relação da venda efetivada:");

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(" ╔══════════════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine(" ║             RELATÓRIO DE TODAS VENDAS EFETIVADAS NO SISTEMA                  ║");
                Console.WriteLine(" ╚══════════════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                q = listarparaimprimir.Count;
                for (i = 0; i < q; i++)
                {
                    Console.WriteLine(listarparaimprimir[i]);
                }

                //Faz a soma de todas as peças

                Console.WriteLine("=======================================================:");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(" ╔══════════════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine(" ║                            RELATÓRIO DA VENDA ATUAL                          ║");
                Console.WriteLine(" ╠══════════════════════════════════════════════════════════════════════════════╣");
                Console.WriteLine($"║  TOTAL VENDA DE PEÇAS: R$ {totalpeças,-40:N2}                                ║");
                Console.WriteLine(" ╚══════════════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                Console.WriteLine("\nPressione ENTER para continuar...");
                Console.ReadLine();
            }
        }
        static void Main(string[] args)
        {

            string nome, nomeconsulta, tipoDePpeça;
            int i, quantidade, impressao, escolha, escolheroperaçao, codigo;
            float preço, valorvendapeça, totalvendapeça;
            char operaçao;
            DateTime agora;

            valorvendapeça = 0;
            totalvendapeça = 0;

            peça peçasAutomotivas = new peça();
            venda vendas = new venda();

            //Cria listas para armazenar a peça cadastrada e a venda

            List<peça> listadepeças = new List<peça>();
            List<venda> listadevendas = new List<venda>();

            //Verifica se o arquivo de peças existe, cria caso não exista.

            if (!File.Exists(arquivopeças))
            {
                File.Create(arquivopeças).Close();
                Console.WriteLine("Arquivo de peças criado, pois não existia.");
            }

            //Verifica se o arquivo de vendas existe, cria caso não exista
            if (!File.Exists(arquivovendas))
            {
                File.Create(arquivovendas).Close();
                Console.WriteLine("Arquivo de vendas criado, pois não existia.");

            }
            // Irá ocorrer a leitura dos arquivos

            var carregarpeças = File.ReadAllLines(arquivopeças);
            Console.WriteLine("Estoque de peças carregando:");
            var cargadevendas = File.ReadAllLines(arquivovendas);

            listadepeças = carregarpeças.Select(line =>
            {
                var parts = line.Split(';');
                return new peça(parts[0], int.Parse(parts[1]), parts[2], float.Parse(parts[3]), int.Parse(parts[4]));
            }).ToList();
            listadevendas = cargadevendas.Select(line =>
            {
                var parts = line.Split(';');
                return new venda(parts[0], int.Parse(parts[1]), parts[2], float.Parse(parts[3]), int.Parse(parts[4]));
            }).ToList();

            Console.WriteLine("Registro de vendas carregado:");
            Console.WriteLine("==================================================================");
            Console.WriteLine("  REGISTROS CARREGADOS COM SUCESSO NO SISTEMA !");
            Console.WriteLine("==================================================================");
            Console.ResetColor();
            Console.WriteLine("\nPressione ENTER para iniciar o MENU...");
            Console.ReadLine();
            Console.Clear();


            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=================================================================================");
                Console.WriteLine("        SISTEMA DE GESTÃO DE ESTOQUE E VENDAS - PEÇAS AUTOMOTIVAS ");
                Console.WriteLine("=================================================================================");
                Console.ResetColor();
                Console.WriteLine();

                // Menu do sistema

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("╔═════════════════════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                                   MENU PRINCIPAL                                    ║");
                Console.WriteLine("╠═════════════════════════════════════════════════════════════════════════════════════╣");
                Console.WriteLine("║  [1] Cadastrar Peça                                                                 ║");
                Console.WriteLine("║  [2] Realizar Venda                                                                 ║");
                Console.WriteLine("║  [3] Imprimir peças cadastradas                                                     ║");
                Console.WriteLine("║  [4] Consultar ou EXCLUIR peça no estoque                                           ║");
                Console.WriteLine("║  [5] Sair                                                                           ║");
                Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                Console.WriteLine();
                Console.Write("Escolha a operação desejada:");

                // O Usuário irá escolher a opção que deseja realizar

                escolheroperaçao = int.Parse(Console.ReadLine());
                switch (escolheroperaçao)
                {
                    case 1:
                        {
                            do
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
                                Console.WriteLine("║         CADASTRO DE PRODUTOS - ESCOLHA UMA OPÇÃO                     ║");
                                Console.WriteLine("╠══════════════════════════════════════════════════════════════════════╣");
                                Console.WriteLine("║  [1] Peças automotivas                                               ║");
                                Console.WriteLine("║                                                                      ║");
                                Console.WriteLine("║  [99] Imprimir peças cadastradas                                     ║");
                                Console.WriteLine("║                                                                      ║");
                                Console.WriteLine("║  [00] Consultar ou EXCLUIR peças no estoque                          ║");
                                Console.WriteLine("║                                                                      ║");
                                Console.WriteLine("║  [-1] Voltar                                                         ║");
                                Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");
                                Console.ResetColor();
                                Console.Write("\nDigite o número da opção desejada: ");
                                escolha = int.Parse(Console.ReadLine()); //Lê a opção a qual o usuário escrever.

                                switch (escolha)
                                {
                                    case 1:
                                        {   
                                            operaçao = 's'; // Redefine o op, para permitir que realize um novo cadastro ao voltar para o menu;
                                            while (operaçao == 's' || operaçao == 'S')
                                            {
                                                Console.WriteLine("Insira os Dados da(s) Peça(s):");
                                                Console.WriteLine("Nome:");
                                                nome = Console.ReadLine();
                                                Console.WriteLine("Quantidade:");
                                                quantidade = int.Parse(Console.ReadLine());
                                                Console.WriteLine("Tipo:");
                                                tipoDePpeça = Console.ReadLine();
                                                Console.WriteLine("Preço unitário:");
                                                preço = float.Parse(Console.ReadLine());
                                                Console.WriteLine("Código para o produto:");
                                                codigo = int.Parse(Console.ReadLine());


                                                peçasAutomotivas = new peça(nome, quantidade, tipoDePpeça, preço, codigo);//método construtor para peçasAutomotiva;
                                                listadepeças.Add(peçasAutomotivas); //Aqui a lista recebe os objetos dentro de peçasAutomotivas em sequencia;
                                                Console.WriteLine("Deseja cadastar mais uma peça?");

                                                File.WriteAllLines(arquivopeças, listadepeças.Select(b => $"{b.nome};{b.quant};{b.tipo};{b.preço};{b.id}"));
                                                Console.WriteLine("Estoque salvo em " + arquivopeças);

                                                operaçao = char.Parse(Console.ReadLine());
                                                Console.Clear();
                                            }
                                            break;
                                        }

                                    case 99:
                                        {
                                            Console.Clear();
                                            Console.Clear();
                                            Console.ForegroundColor = ConsoleColor.Magenta;
                                            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
                                            Console.WriteLine("║                           IMPRESSÃO DE ESTOQUE                       ║");
                                            Console.WriteLine("╠══════════════════════════════════════════════════════════════════════╣");
                                            Console.WriteLine("║  [1] Peças Automotivas                                               ║");
                                            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");
                                            Console.ResetColor();

                                            impressao = int.Parse(Console.ReadLine());
                                            if (impressao == 1)
                                            {
                                                imprimir(listadepeças);// Chama a função IMPRIMIR com listadepeças dentro ;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Opção inválida");
                                            }
                                            break;
                                        }
                                    case 00:
                                        {
                                            Console.Clear();
                                            Console.ForegroundColor = ConsoleColor.Blue;
                                            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
                                            Console.WriteLine("║        CONSULTA DE PRODUTOS - ESCOLHA UMA OPÇÃO                      ║");
                                            Console.WriteLine("╠══════════════════════════════════════════════════════════════════════╣");
                                            Console.WriteLine("║  [1] Peças Automotivas                                               ║");
                                            Console.WriteLine("║  [-1] Voltar                                                         ║");
                                            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");
                                            Console.ResetColor();
                                            Console.Write("\nDigite o número da opção desejada: ");

                                            impressao = int.Parse(Console.ReadLine());
                                            if (impressao == 1)
                                            {
                                                if (listadepeças.Count == 0)
                                                {

                                                    Console.Clear();
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
                                                    Console.WriteLine("║                        ATENÇÃO: NENHUMA PEÇA ENCONTRADA!             ║");
                                                    Console.WriteLine("╠══════════════════════════════════════════════════════════════════════╣");
                                                    Console.WriteLine("║  Não há PEÇAS cadastradas no estoque para essa busca no momento.     ║");
                                                    Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");
                                                    Console.ResetColor();
                                                    Console.WriteLine("\nPressione ENTER para voltar ao menu...");
                                                    Console.ReadLine();
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Informe o nome da peça:");
                                                    nomeconsulta = Console.ReadLine();
                                                    consultar(listadepeças, nomeconsulta);//Chama a função de CONSULTAR e passando dois parâmetros sendo a lista e o nome do produto
                                                }
                                            }
                                            else
                                            {
                                                Console.Clear();
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
                                                Console.WriteLine("║                   ATENÇÃO: NENHUM PRODUTO ENCONTRADO!                ║");
                                                Console.WriteLine("╠══════════════════════════════════════════════════════════════════════╣");
                                                Console.WriteLine("║  Não há produtos cadastrados no estoque para essa busca no momento.  ║");
                                                Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");
                                                Console.ResetColor();
                                                Console.WriteLine("\nPressione ENTER para voltar ao menu...");
                                                Console.ReadLine();
                                            }
                                            break;
                                        }
                                    case 1000:
                                        {
                                            imprimirvendas(listadevendas, totalvendapeça);
                                            break;
                                        }
                                    default:
                                        if (escolha != -1)
                                        {
                                            Console.Clear();
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
                                            Console.WriteLine("║                        ATENÇÃO: OPÇÃO INVÁLIDA!                      ║");
                                            Console.WriteLine("╠══════════════════════════════════════════════════════════════════════╣");
                                            Console.WriteLine("║  A opção selecionada não é válida. Por favor, escolha novamente.     ║");
                                            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");
                                            Console.ResetColor();
                                            Console.WriteLine("\nPressione ENTER para voltar ao menu...");
                                            Console.ReadLine();
                                            break;
                                        }
                                        else { break; }
                                }
                            } while (escolha != -1);

                            break;

                        }


                    case 2:
                        {
                            do
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════╗");
                                Console.WriteLine("║                        MENU DE VENDAS - ESCOLHA UMA OPÇÃO                    ║");
                                Console.WriteLine("╠══════════════════════════════════════════════════════════════════════════════╣");
                                Console.WriteLine("║  [1] Peças Automotivas                                                       ║");
                                Console.WriteLine("║  [99] Imprimir estoque                                                       ║");
                                Console.WriteLine("║  [00] Consultar peça no estoque                                              ║");
                                Console.WriteLine("║  [1000] Imprimir relatório da venda ATUAL e valor total a pagar              ║");
                                Console.WriteLine("║  [-1] Voltar                                                                 ║");
                                Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════════╝");
                                Console.ResetColor();
                                Console.Write("\nDigite o número da opção desejada: ");

                                escolha = int.Parse(Console.ReadLine());

                                switch (escolha)
                                {
                                    case 1:
                                        {

                                            if(listadepeças.Count == 0)
                                            {
                                                Console.WriteLine("Não há peças no estoque!");
                                                Console.ReadLine();
                                            }
                                            else
                                            {
                                                Console.WriteLine("Nome da peça a ser vendida:");
                                                nome = Console.ReadLine();
                                                Console.WriteLine("Digite a quatidade vendida:");
                                                quantidade = int.Parse(Console.ReadLine());

                                                bool produtoEncontrado = false;
                                                for (i = 0; i < listadepeças.Count; i++)
                                                {
                                                    if (listadepeças[i].nome == nome)
                                                    {
                                                        produtoEncontrado = true;
                                                        if (listadepeças[i].quant <= 0)
                                                        {
                                                            Console.WriteLine("Peça esgotada!!!");
                                                            Console.WriteLine("ATENÇÃO: Solicitar peça ao fornecedor");
                                                        }
                                                        else
                                                        {
                                                            if (listadepeças[i].quant < quantidade)
                                                            {
                                                                Console.WriteLine($"\nNão há peças suficientes no estoque = {listadepeças[i].quant}");
                                                            }
                                                            else
                                                            {
                                                                peçasAutomotivas = listadepeças[i];
                                                                peçasAutomotivas.quant = listadepeças[i].quant - quantidade;
                                                                listadepeças[i] = peçasAutomotivas;
                                                                vendas.quant = quantidade;

                                                                Console.ForegroundColor = ConsoleColor.Green;
                                                                Console.WriteLine(" ╔══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
                                                                Console.WriteLine(" ║                                             VENDA DE PEÇAS CONCLUÍDA                                             ║");
                                                                Console.WriteLine(" ╠══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╣");
                                                                Console.WriteLine($"║  Venda de {quantidade} unidade(s) do produto \"{nome}\" realizada com sucesso!{new string(' ', 18 - nome.Length)}║");
                                                                Console.WriteLine(" ╚══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");
                                                                Console.ResetColor();

                                                                valorvendapeça = listadepeças[i].preço * quantidade;
                                                                agora = DateTime.Now;
                                                                vendas.data agora.ToString("yyyy-MM-dd HH:mm:ss");
                                                                vendas = new venda(peçasAutomotivas.nome, vendas.id = "#0000", vendas.quant, peçasAutomotivas.preço, vendas.data);
                                                                listadepeças.Add(vendas);
                                                                totalvendapeça = totalvendapeça + valorvendapeça;

                                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                                Console.WriteLine(" ╔═════════════════════════════════════════════════════════════════════╗");
                                                                Console.WriteLine($"║  Valor da venda atual de peças: R$ {valorvendapeça,-48:N2}          ║");
                                                                Console.WriteLine($"║  Valor acumulado de vendas de peças: R$ {totalvendapeça,-39:N2}     ║");
                                                                Console.WriteLine(" ╚═════════════════════════════════════════════════════════════════════╝");
                                                                Console.ResetColor();

                                                                File.WriteAllLines(arquivopeças, listadepeças.Select(b => $"{b.nome};{b.quant};{b.preço}"));
                                                                File.WriteAllLines(arquivovendas, listadevendas.Select(v => $"{v.nome};{v.id};{v.quant};{v.data}"));
                                                                Console.WriteLine("Estoque salvo em " + arquivopeças);
                                                                Console.WriteLine("Estoque salvo em " + arquivovendas);
                                                            }
                                                        }
                                                    }
                                                }
                                                if (!produtoEncontrado)
                                                {
                                                    Console.WriteLine("Peça esgotada no estoque!");
                                                }
                                                Console.ReadLine();

                                            }
                                            break;
                                        }

                                    case 99:
                                        {
                                            Console.Clear();
                                            Console.ForegroundColor = ConsoleColor.Magenta;
                                            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
                                            Console.WriteLine("║                      IMPRESSÃO DE ESTOQUE                            ║");
                                            Console.WriteLine("╠══════════════════════════════════════════════════════════════════════╣");
                                            Console.WriteLine("║  [1] Peças Automotivas                                               ║");
                                            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");
                                            Console.ResetColor();
                                            Console.WriteLine();

                                            impressao = int.Parse(Console.ReadLine());

                                            if (impressao == 1)
                                            {
                                                imprimir(listadepeças);
                                            }
                                            break;
                                        }

                                    case 00: {

                                            Console.Clear();
                                            Console.ForegroundColor = ConsoleColor.Blue;
                                            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
                                            Console.WriteLine("║                           CONSULTA DE PEÇAS                          ║");
                                            Console.WriteLine("╠══════════════════════════════════════════════════════════════════════╣");
                                            Console.WriteLine("║  [1] Peças Automotivas                                               ║");
                                            Console.WriteLine("║  [-1] Voltar                                                         ║");
                                            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");
                                            Console.ResetColor();
                                            Console.Write("\nDigite o número da opção desejada: ");

                                            impressao = int.Parse(Console.ReadLine());

                                            if (impressao == 1)
                                            {
                                                if (listadepeças.Count == 0)
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Não há peças no estoque para essa busca!");
                                                    Console.ReadLine();
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Informe o nome da peça desejada:");
                                                    nomeconsulta = Console.ReadLine();
                                                    consultar(listadepeças, nomeconsulta);
                                                }
                                            }
                                            break;
                                        }

                                    case 1000:
                                        {
                                            totalvendapeça = totalvendapeça;
                                            imprimirvendas(listadevendas, totalvendapeça);
                                            break;
                                        }

                                    default:
                                        if (escolha != -1)
                                        {
                                            Console.Clear();
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
                                            Console.WriteLine("║                        ATENÇÃO: OPÇÃO INVÁLIDA!                      ║");
                                            Console.WriteLine("╠══════════════════════════════════════════════════════════════════════╣");
                                            Console.WriteLine("║  A opção selecionada não é válida. Por favor, escolha novamente.     ║");
                                            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");
                                            Console.ResetColor();
                                            Console.WriteLine("\nPressione ENTER para voltar ao menu...");
                                            Console.ReadLine();
                                            break;
                                        }
                                        else { break; }

                                }

                            }
                            while(escolha != -1)

                            break;
                       
                        }


                    case 3:
                        {
                            Console.Clear();
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
                            Console.WriteLine("║                  IMPRESSÃO DE ESTOQUE - ESCOLHA UMA LISTA            ║");
                            Console.WriteLine("╠══════════════════════════════════════════════════════════════════════╣");
                            Console.WriteLine("║  [1] Peças Automotivas                                               ║");
                            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");
                            Console.ResetColor();

                            impressao = int.Parse(Console.ReadLine());

                            if(impressao == 1 )
                            {
                                imprimir(listadepeças);
                            }
                            break;
                        }

                    case 4:
                        {

                            Console.Clear();
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
                            Console.WriteLine("║        CONSULTA/EXCLUSÃO DE PEÇAS - ESCOLHA UMA OPÇÃO                ║");
                            Console.WriteLine("╠══════════════════════════════════════════════════════════════════════╣");
                            Console.WriteLine("║  [1] Para consultar ou excluir peças                                 ║");
                            Console.WriteLine("║  [-1] Voltar                                                         ║");
                            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");
                            Console.ResetColor();

                            impressao = int.Parse(Console.ReadLine());

                            if(impressao == 1)
                            {
                                if (listadepeças.Count == 0)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Não há peças no estoque para realizar essa busca!");
                                    Console.ReadLine();
                                }
                                else
                                {
                                    Console.WriteLine("Informe o nome da peça:");
                                    nomeconsulta = Console.ReadLine();
                                    consultar(listadepeças, nomeconsulta);
                                }              
                            } 
                            else
                            {
                                Console.WriteLine("Não há peças no estoque para realizar essa busca!");
                            }
                            break;

                        }
                    default:
                        if(escolheroperaçao != )
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
                            Console.WriteLine("║                        ATENÇÃO: OPÇÃO INVÁLIDA!                      ║");
                            Console.WriteLine("╠══════════════════════════════════════════════════════════════════════╣");
                            Console.WriteLine("║  A opção selecionada não é válida. Por favor, escolha novamente.     ║");
                            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");
                            Console.ResetColor();
                            Console.WriteLine("\nPressione ENTER para voltar ao menu...");
                            Console.ReadLine();
                            break;
                        }
                        else { break;  }
                }

            } while (escolheroperaçao != 5);

            File.WriteAllLines(arquivopeças, listadepeças.Select(b => $"{b.nome};{b.quant};{b.preço}"));
            File.WriteAllLines(arquivovendas, listadevendas.Select(v => $"{v.nome};{v.id};{v.quant};{v.preço};{v.data}"));
            Console.WriteLine("Estoque salvo em " + arquivopeças);
            Console.WriteLine("Estoque salvo em " + arquivovendas);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                     OBRIGADO POR UTILIZAR O SISTEMA!                 ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║                    Volte sempre à Guru das Peças LTDA                ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine("\nPressione ENTER para sair...");
            Console.ReadLine();
        }
    }
}

