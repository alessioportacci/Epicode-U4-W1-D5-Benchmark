using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Epicode_U4_W1_D5_Benchmark
{
    internal class Contribuente
    {
        #region Paramethers

        private string Nome { get; set; }
        private string Cognome { get; set; }
        private DateTime DataDiNascita { get; set; }
        private string CodiceFiscale { get; set; }
        private char Sesso { get; set; }
        private string ComuneResidenza { get; set; }
        private double RedditoAnnuale { get; set; }
        private double Aliquota { get; set; }
        
        private static List<Contribuente> ContribuentiList { get; set; } = new List<Contribuente>();
        public static bool Acceso { get; set; } = true;
        
        #endregion

        #region Constructors
        public Contribuente(string nome, string cognome, string sesso, string codiceFiscale, double reddito)
        {
            Nome = nome;
            Cognome = cognome;
            try { Sesso = sesso.ToUpper()[0]; }
            catch { Sesso = '-'; }
            CodiceFiscale = codiceFiscale;
            RedditoAnnuale = reddito;
            setAliquota(reddito);
        }
        public Contribuente(string nome, string cognome, string sesso, string codiceFiscale, double reddito, string dataNascita)
            : this(nome, cognome, sesso, codiceFiscale, reddito)
        {
            try { DataDiNascita = DateTime.Parse(dataNascita); }
            catch { DataDiNascita = new DateTime(); }
        }
        public Contribuente(string nome, string cognome, string sesso, string codiceFiscale, double reddito, string dataNascita, string comune)
            : this(nome, cognome, sesso, codiceFiscale, reddito, dataNascita)
        {
            ComuneResidenza = comune;
        }
        #endregion

        #region Methods 
        private void setAliquota(double reddito)
        {
            if (reddito > 75000)
                Aliquota = 25420 + ((reddito - 75000) * 0.43);
            else if (reddito > 55000)
                Aliquota = 17220 + ((reddito - 55000) * 0.41);
            else if (reddito > 28000)
                Aliquota = 6960 + ((reddito - 28000) * 0.38);
            else if (reddito > 15000)
                Aliquota = 3.450 + ((reddito - 15000) * 0.27);
            else Aliquota = reddito * 0.23;
        }

        #region Statics

        public static void getImposta(Contribuente contribuente)
        {
            Console.WriteLine($"Contribuente: {contribuente.Nome} {contribuente.Cognome}");
            Console.WriteLine($"Nato il {contribuente.DataDiNascita.ToShortDateString()} ({contribuente.Sesso})");
            Console.WriteLine($"Residente in {contribuente.ComuneResidenza}");
            Console.WriteLine($"Codice fiscale: {contribuente.CodiceFiscale}");

            Console.WriteLine($"\nReddito dichiarato: {contribuente.RedditoAnnuale}");
            Console.WriteLine($"Imposta da versare: {contribuente.Aliquota}");

            Console.WriteLine("\n\nPremere un tasto per continuare...");
            Console.ReadLine();
        }
        public static void menu()
        {
            Console.WriteLine("   ___            _        _ _                      _   _     _ _   \r\n" +
                              "  / __\\___  _ __ | |_ _ __(_) |__  _   _  ___ _ __ | |_(_)   (_) |_ \r\n" +
                              " / /  / _ \\| '_ \\| __| '__| | '_ \\| | | |/ _ \\ '_ \\| __| |   | | __|\r\n" +
                              "/ /__| (_) | | | | |_| |  | | |_) | |_| |  __/ | | | |_| |  _| | |_ \r\n" +
                              "\\____/\\___/|_| |_|\\__|_|  |_|_.__/ \\__,_|\\___|_| |_|\\__|_| (_)_|\\__|\r\n");
            Console.WriteLine("1 - Aggiungere contribuente");
            Console.WriteLine("2 - Lista contribuenti");
            Console.WriteLine("3 - Chiudi");

            Console.Write("Scelta: ");
            int scelta;
            try { scelta = Int32.Parse(Console.ReadLine()); }
            catch { scelta = 0; }
            Console.Clear();

            switch (scelta)
            {
                case 1:
                    newContribuente();
                    break; 
                case 2:
                    getContribuenti();
                    Console.Clear();
                    break;
                case 3:
                    chiudi();
                    break;
                default: 
                    Console.WriteLine("Inserire un valore valido"); 
                    break;
            }
        }
        private static void newContribuente()
        {
            //Nome
            Console.Write("Inserire nome: ");
            string nome = Console.ReadLine();
            //Cognome
            Console.Write("Inserire cognome: ");
            string cognome = Console.ReadLine();
            //Data di nascita
            Console.Write("Inserire data di nascita: ");
            string data = Console.ReadLine();
            //Sesso
            Console.Write("Inserire il proprio sesso: ");
            string sesso = Console.ReadLine();
            //Citta
            Console.Write("Inserire comune di appartenenza: ");
            string comune = Console.ReadLine();
            //CF
            Console.Write("Inserire il proprio codice fiscale: ");
            string cf = Console.ReadLine();
            //reddito
            Console.Write("Inserire il proprio reddito: ");
            double reddito;
            try { reddito = Convert.ToDouble(Console.ReadLine()); }
            catch { reddito = 0; }

            Contribuente con = new Contribuente(nome, cognome, sesso, cf, reddito, data, comune);
            ContribuentiList.Add(con);

            Console.Clear();
            getImposta(con);
            Console.Clear();
        }
        private static void getContribuenti()
        {
            for(int i = 0; i < ContribuentiList.Count; i++) 
            {
                getImposta(ContribuentiList[i]);
                Console.WriteLine();
            }
        }
        private static void chiudi()
        {
            Console.WriteLine("Chiusura");
            Acceso = false;
        }

        #endregion

        #endregion

    }
}
