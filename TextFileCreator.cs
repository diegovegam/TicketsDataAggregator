using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig.PdfFonts;



namespace TicketsDataAggregator
{
    public class TextFileCreator
    {
        private readonly Dictionary<string, string> _domainToCulture = new()
        {
            ["com"] = "en-US",
            ["fr"] = "fr-FR",
            ["jp"] = "ja-JP"
        };
        public void WriteTicketsToTextFile()
        {
            List<string> tickets = PDFReader.ReadPDFFiles();
            File.AppendAllText(BuildFilePath(), Format(tickets));
            File.AppendAllText(BuildFilePath(), Environment.NewLine);
        }

        public string BuildFilePath()
        {
            return "C:\\Users\\dvegamolina\\Desktop\\SoftwareDevelopmentCourses\\C#MasterClass\\Projects\\TicketsDataAggregator\\Tickets\\tickets.txt";   
        }

        public string Format(List<string> ticketsInfo)
        {
            List<string> ticketInfoSpplited;
            string format = "";
            string title = "";
            string? ticketCulture = "";
            DateOnly date = default;
            TimeOnly time = default;
            foreach (string ticketInfo in ticketsInfo)
            {
               ticketInfoSpplited = ticketInfo.Split(" ").ToList();
                if (ticketInfoSpplited[0] == "com" || ticketInfoSpplited[0] == "fr" || ticketInfoSpplited[0] == "jp")
                {
                    ticketCulture = _domainToCulture[ticketInfoSpplited[0]];
                }

                if (ticketInfoSpplited[0] == "Title:")
                {
                    ticketInfoSpplited.RemoveAt(0);
                    title = string.Join(" ", ticketInfoSpplited);
                }

                if (ticketInfoSpplited[0] == "Date:")
                {
                    ticketInfoSpplited.RemoveAt(0);
                    date = DateOnly.Parse(ticketInfoSpplited[0], new CultureInfo(ticketCulture));
                }
                if (ticketInfoSpplited[0] == "Time:")
                {
                    ticketInfoSpplited.RemoveAt(0);
                    time = TimeOnly.Parse(ticketInfoSpplited[0]);
                
                    if (title != "" && date != default && time != default)
                    {
                        format += string.Format("{0,-50} | {1} | {2}",
                        title,
                        date.ToString(CultureInfo.InvariantCulture),
                        time.ToString(CultureInfo.InvariantCulture));
                        format += Environment.NewLine;
                    }
                }
                
                
            }

            return format;

        }
    }
}
