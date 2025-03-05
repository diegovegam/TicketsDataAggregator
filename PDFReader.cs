using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Graphics;

namespace TicketsDataAggregator
{
    public static class PDFReader
    {
        static string[] ListPDFFiles ()
        {
            const string path = "C:\\Users\\dvegamolina\\Desktop\\SoftwareDevelopmentCourses\\C#MasterClass\\Projects\\TicketsDataAggregator\\Tickets";

            string[] listOfFiles = Directory.GetFiles(path, "*.PDF");
            
            return listOfFiles;
        }

        public static List<string> ReadPDFFiles()
        {
            string[] files = ListPDFFiles();
            List<string> TicketsToInsert = new List<string>();
            for (int i = 0; i < files.Length; i++)
            {
                using (PdfDocument document = PdfDocument.Open(files[i]))
                {
                    string CompleteTitle = "";
                    string CompleteDate = "";
                    string CompleteTime = "";
                    string [] domainArray = default;
                    string domain = "";
                    int TicketsCounter = 0;
                    
                    foreach (Page page in document.GetPages())
                    {
                        for (int j = 0; j < page.GetWords().Count(); j++)
                        {
                            var words = page.GetWords().ToList();
                            
                            if (words[j].Text == "Title:")
                            {
                                
                                List<string> title = [];
                                while (words[j].Text != "Date:")
                                {
                                    title.Add(words[j].Text);
                                    j++;
                                }
                                CompleteTitle = string.Join(" ", title);
                                
                            }

                            if (words[j].Text == "Date:")
                            {
                                TicketsCounter++;
                                List<string> dates = [];

                                while (words[j].Text != "Time:")
                                {
                                    dates.Add(words[j].Text);
                                    j++;
                                }
                                CompleteDate = string.Join(" ", dates);
                            }

                            if (words[j].Text == "Time:")
                            {
                                List<string> times = [];

                                while (words[j].Text != "Visit" && words[j].Text != "Title:")
                                {
                                    times.Add(words[j].Text);
                                    j++;
                                }
                                CompleteTime = string.Join(" ", times);
                            }

                            domainArray = words.Last().Text.Split('.');
                        }

                        domain = domainArray.Last();

                    }
                    for (int x = 0; x < TicketsCounter; x++)
                    {
                        TicketsToInsert.Add(CompleteTitle);
                        TicketsToInsert.Add(domain);
                        TicketsToInsert.Add(CompleteDate);
                        TicketsToInsert.Add(CompleteTime);
                        
                    }
                    
                }
                   
            }
            return TicketsToInsert;
        }
            
    }
}
