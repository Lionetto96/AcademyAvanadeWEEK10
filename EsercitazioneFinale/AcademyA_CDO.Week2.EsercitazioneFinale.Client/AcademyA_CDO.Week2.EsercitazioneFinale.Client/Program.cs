using AcademyA_CDO.Week2.EsercitazioneFinale.Lib;
using AcademyA_CDO.Week2.EsercitazioneFinale.Lib.Helpers;
using System;
using System.Collections.Generic;


namespace AcademyA_CDO.Week2.EsercitazioneFinale.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Esercitazione Finale Week 2 ===");

            #region Warehouse creation

            //•	Realizzare una Console app che
            //    •	Crei un nuovo Magazzino
            Warehouse wh = new Warehouse("Viale del Tramonto, 988 - Milano (MI) ITALY");

            wh.FileLoadingSteps = 2;
            wh.FileReaderStarted += Wh_FileReaderStarted;
            wh.FileReaderProgress += Wh_FileReaderProgress;
            wh.FileReaderCompleted += Wh_FileReaderCompleted;

            #endregion

            #region Main loop

            bool quit = false;
            bool result = false;

            do
            {
                //•	Realizzare una Console app che
                //    •	Permetta di ricevere diverse tipologie di Merci (gestire l’input dall'utente)
                string command = ConsoleHelpers.BuildMenu("Main Menu",
                    new List<string> {
                        "[ 1 ] - Receive Good",
                        "[ 2 ] - Ship Good",
                        "[ 3 ] - Print Stock Data",
                        "[ 4 ] - Load Goods list from file",
                        "[ q ] - QUIT"
                    });

                switch (command)
                {
                    case "1":
                        // add good
                        result = ConsoleHelpers.ReceiveNewGood(wh);
                        if (!result)
                        {
                            Console.WriteLine("ERROR: cannot recive the good properly!");
                        }
                        Console.WriteLine("Press any key to continue ...");
                        Console.ReadKey();
                        break;
                    case "2":
                        // remove good
                        result = ConsoleHelpers.ShipGood(wh);
                        if (!result)
                        {
                            Console.WriteLine("ERROR: cannot ship the good properly!");
                        }
                        Console.WriteLine("Press any key to continue ...");
                        Console.ReadKey();
                        break;
                    case "3":
                        //•	Realizzare una Console app che
                        //    •	Stampi i dati del Magazzino e le Merci in giacenza
                        Console.Clear();
                        Console.WriteLine(wh.StockList());
                        Console.WriteLine("Press any key to continue ...");
                        Console.ReadKey();
                        break;
                    case "4":
                        // load goods from file
                        result = ConsoleHelpers.ReadDataFromFile(wh);
                        if (!result)
                        {
                            Console.WriteLine("ERROR: cannot parse file properly!");
                        }
                        Console.WriteLine("Press any key to continue ...");
                        Console.ReadKey();
                        break;
                    case "q":
                        quit = true;
                        break;
                    default:
                        Console.WriteLine("Comando sconosciuto.");
                        break;
                }

            } while (!quit);

            #endregion
        }

        #region File loader events

        private static void Wh_FileReaderCompleted(object sender, FileLoadingEventArgs e)
        {

            Console.WriteLine($"Good file uploading process completed. {e.NumberOfRows} row(s) loaded. ===");
        }

        private static void Wh_FileReaderProgress(object sender, FileLoadingEventArgs e)
        {
            Console.WriteLine($"\t> Good file uploading process on going. {e.NumberOfRows} row(s) loaded.");
        }

        private static void Wh_FileReaderStarted(object sender, EventArgs e)
        {
            Console.Clear();
            Console.WriteLine("=== Good file uploading process begins ...");
        }

        #endregion
    }
}
