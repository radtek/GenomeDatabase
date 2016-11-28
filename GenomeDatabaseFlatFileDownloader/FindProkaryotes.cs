using System;
using System.Diagnostics;
using System.IO;

namespace GenomeDatabaseFlatFileDownloader
{
	public class FindProkaryotes
	{
		/// <summary>
		/// This class is responsible for extracting the organisms tax_id and its correspoding parent_id from nodes.dmp and also responsible to retaining 
		/// this information in parsed_nodes.txt file.
		/// </summary>
		public const string BACTERIA = "0";
		private static string currentDirectory = Directory.GetCurrentDirectory();

		public static void initiate()
		{
			currentDirectory = currentDirectory.Substring(0, currentDirectory.Length - 57);
			//Stopwatch stopwatch = new Stopwatch();
			//stopwatch.Start();
			String line;
			long number_of_lines = 0;
			long bacteria = 0;
			//Console.WriteLine("Prokaryote node parsing update  [INITIATED]");
			//Console.WriteLine("Searching file update           [INITIATED]");
			FileStream fileStreamRead = new FileStream(currentDirectory+"GenomeDatabaseData/Taxonomy/nodes.dmp", FileMode.Open, FileAccess.Read, FileShare.None);
			FileStream fileStreamWrite = new FileStream(currentDirectory+"GenomeDatabaseData/Taxonomy/Parsedtaxonomy/Prokaryote_taxid_parentid.txt", FileMode.Create, FileAccess.Write, FileShare.None);

			try
			{
				StreamWriter sw = new StreamWriter(fileStreamWrite);
				StreamReader sr = new StreamReader(fileStreamRead);
				//Console.WriteLine("Searching file update           [SUCCEEDED]");
				//Console.WriteLine("Prokaryote node parsing update  [PROCESSING]");

				sw.WriteLine("S.N\t|\tTax_id\t|\tParent_id");
				sw.WriteLine("=======================================");
				//scans the whole file
				while ((line = sr.ReadLine()) != null)
				{
					if (parsed_node(line, 5) == BACTERIA)
					{
						String taxid = parsed_node(line, 1);
						String parentid = parsed_node(line, 2);
						//Console.WriteLine("Found bacteria in line {0}: Taxid = {1} Parentid = {2}", number_of_lines, taxid, parentid);
						sw.WriteLine("{0}\t|\t{1}\t|\t{2}", bacteria + 1, taxid, parentid);
						bacteria++;
					}
					number_of_lines++;
				}
				sw.Flush();
				sw.Close();
				sr.Close();

				check_file();
			}
			catch (System.IO.IOException e)
			{
				Console.WriteLine("Error: Cannot find the file");
				Console.WriteLine(e.Message);
			}
			finally
			{
				//Console.WriteLine("Number of lines scanned: {0}", number_of_lines);
				//Console.WriteLine("Number of bacteria nodes: {0}", bacteria);
				//stopwatch.Stop();
				//TimeSpan ts = stopwatch.Elapsed;
				//string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
				//Console.WriteLine("Total RunTime: " + elapsedTime);
				//Console.WriteLine("\n");
			}
		}
		/// <summary>
		/// This function parse the line and extracts the taxid and its corresponding parent_id
		/// </summary>
		/// <returns>tax_id,parent_id</returns>
		/// <param name="line">Line</param>
		private static String parsed_node(String line, int column)
		{
			int col = 1;
			string parsed = null;
			foreach (char ch in line)
			{
				if (ch == '|')
				{
					col++;
					if (col > column)
					{
						break;
					}
				}
				else if (col == column && char.IsNumber(ch))
				{
					parsed += ch;
				}
			}
			return parsed;
		}
		/// <summary>
		/// Checks the file if program was successful for extracting the taxids of organism
		/// </summary>
		private static void check_file()
		{
			if (new FileInfo(currentDirectory+"GenomeDatabaseData/Taxonomy/Parsedtaxonomy/Prokaryote_taxid_parentid.txt").Length == 0)
			{
				throw new Exception("Prokaryote node parsing update [FAILED]");
			}
		}

	}
}

