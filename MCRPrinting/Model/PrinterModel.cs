using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace MCRPrinting.Model
{
    public class PrinterModel
    {
        static string PrinterStatus = string.Empty;
        public static string CheckPrinterStatus()
        {
            
            PrintServer printServer = new PrintServer();
            PrintQueue printQueue = printServer.GetPrintQueue("HP LaserJet M806 PCL 6"); 
            PrintQueueStatus status = printQueue.QueueStatus;
            switch (status)
            {
                case PrintQueueStatus.Paused:
                    PrinterStatus = "Printer is paused.";
                    break;
                case PrintQueueStatus.Error:
                    PrinterStatus = "Printer is in an error state.";
                    break;
                case PrintQueueStatus.PaperOut:
                    PrinterStatus = "Printer is out of paper.";
                    break;
                case PrintQueueStatus.Printing:
                    PrinterStatus = "Printer is printing";
                    break;
                case PrintQueueStatus.TonerLow:
                    PrinterStatus = "Printer is low on toner.";
                    break;
                case PrintQueueStatus.Offline:
                    PrinterStatus = "Printer is offline.";
                    break;
                default:
                    PrinterStatus = "Printer is ready.";
                    break;
            }
            return PrinterStatus;
        }
    }
}
