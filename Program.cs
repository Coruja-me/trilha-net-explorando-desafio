using System.Text;
using DesafioProjetoHospedagem.Models;
using Newtonsoft.Json;

Console.OutputEncoding = Encoding.UTF8;

List<Pessoa> hospedes = new();
Suite suite = new();

int opt = 0;
string msg = "";

Console.WriteLine("Serviço de Hospedagem!\n");

do{
    Console.WriteLine(msg +
    "O que deseja fazer?\n"
    +"1 - Cadastrar hóspede\n"
    +"2 - Cadastrar suíte\n"
    +"3 - Fazer reserva\n"
    +"0 - Sair"
    );
    
    opt = Convert.ToInt32(Console.ReadLine());
    switch(opt){
        case 1:
            // Cria um hóspede e cadastra na lista de hóspedes
            Console.Clear();

            Console.WriteLine("Digite o Primeiro Nome do hóspede:");
            string nm = Console.ReadLine();
            Console.WriteLine("Digite o Sobrenome do hóspede:");
            string sbn = Console.ReadLine();

            Pessoa pessoa = new(nome: nm, sobrenome: sbn);
            hospedes.Add(pessoa);

            //Adicionando o hóspede em um arquivo JSON

            string serialh = JsonConvert.SerializeObject(hospedes, Formatting.Indented);
            File.WriteAllText("Arquivos/Pessoas.json", serialh);
            msg = "Hospede cadastrado com sucesso!\n";
        break;

        case 2:
            //Criando a suíte
            Console.Clear();

            Console.WriteLine("Digite o tipo da Suite:");
            string tipo = Console.ReadLine();
            Console.WriteLine("Digite a Capacidade da Suite:");
            int limite = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Digite o valor da Suite:");
            decimal valor = Convert.ToDecimal(Console.ReadLine());

            suite = new(tipoSuite: tipo, capacidade: limite, valorDiaria: valor);

            string serials = JsonConvert.SerializeObject(suite, Formatting.Indented);
            File.WriteAllText("Arquivos/Suites.json", serials);
            msg = "Suite cadastrada com sucesso!\n";
        break;

        case 3:
            //Cria uma nova reserva, passando a suíte e os hóspedes
            Console.Clear();

            Console.WriteLine("Informe a quantidade de dias da hospedagem:");
            Reserva reserva = new(diasReservados: Convert.ToInt32(Console.ReadLine()));
            reserva.CadastrarSuite(suite);
            reserva.CadastrarHospedes(hospedes);
            reserva.CalcularValorDiaria();

            // Exibe a quantidade de hóspedes e o valor da diária
            string serialr = JsonConvert.SerializeObject(reserva, Formatting.Indented);
            File.WriteAllText("Arquivos/Reserva.json", serialr);
            msg = "Reserva realizada com sucesso!\n"+ 
            $"Hóspedes: {reserva.ObterQuantidadeHospedes()}\n"+ 
            $"Valor diária: {reserva.CalcularValorDiaria()}\n";
        break;

        case 0:
            Console.WriteLine("Saindo...");
        return;

        default:
            Console.Clear();
            Console.WriteLine("Comando Inválido! Digite novamente\n");
        break;
    }
    Console.Clear();
} while(opt!=0);

