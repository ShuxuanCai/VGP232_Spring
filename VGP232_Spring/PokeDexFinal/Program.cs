using System;
using System.Collections.Generic;
using System.IO;

namespace PokeDexFinal
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string inputFile = string.Empty;

            string outputFile = string.Empty;

            bool appendToFile = false;

            bool displayCount = false;

            bool sortEnabled = false;

            string sortColumnName = string.Empty;

            PokemonCollection results = new PokemonCollection();

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-h" || args[i] == "--help")
                {
                    Console.WriteLine("-i <path> or --input <path> : loads the input file path specified (required)");
                    Console.WriteLine("-o <path> or --output <path> : saves result in the output file path specified (optional)");

                    Console.WriteLine("-c or --count : displays the number of entries in the input file (optional).");

                    Console.WriteLine("-a or --append : enables append mode when writing to an existing output file (optional)");

                    Console.WriteLine("-s or --sort <column name> : outputs the results sorted by column name");

                    break;
                }

                else if (args[i] == "-i" || args[i] == "--input")
                {
                    if (args.Length > i + 1)
                    {
                        ++i;
                        inputFile = args[i];

                        if (string.IsNullOrEmpty(inputFile))
                        {
                            Console.WriteLine("No input file specified");
                        }
                        else if (!File.Exists(inputFile))
                        {
                            Console.WriteLine("The specified file {0} does not exit.", inputFile);
                        }
                        else
                        {
                            results.Load(inputFile);
                        }
                    }
                }
                else if (args[i] == "-s" || args[i] == "--sort")
                {
                    if (args.Length > i + 1)
                    {
                        sortEnabled = true;
                        ++i;
                        sortColumnName = args[i];
                    }
                }
                else if (args[i] == "-c" || args[i] == "--count")
                {
                    displayCount = true;
                }
                else if (args[i] == "-a" || args[i] == "--append")
                {
                    appendToFile = true;
                }
                else if (args[i] == "-o" || args[i] == "--output")
                {
                    if (args.Length > i + 1)
                    {
                        ++i;
                        string filePath = args[i];
                        if (string.IsNullOrEmpty(filePath))
                        {
                            Console.WriteLine("No output file specified.");
                        }
                        else
                        {
                            outputFile = filePath;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("The argument Arg[{0}] = [{1}] is invalid", i, args[i]);
                }
            }

            if (sortEnabled)
            {
                Console.WriteLine($"Sorting by {sortColumnName}.");
                results.SortBy(sortColumnName);
            }

            if (displayCount)
            {
                Console.WriteLine("There are {0} entries", results.Count);
            }

            if (results.Count > 0)
            {
                if (!string.IsNullOrEmpty(outputFile))
                {
                    if (appendToFile)
                    {
                        results.Save(outputFile);
                    }
                }
                else
                {
                    for (int i = 0; i < results.Count; i++)
                    {
                        Console.WriteLine(results[i]);
                    }
                }
            }

            Console.WriteLine("Done!");
        }
    }
}
