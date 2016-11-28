using System;
using System.Diagnostics;
using System.IO;

namespace GenomeDatabaseFlatFileDownloader
{
class FindWebAddresses
	{
		/// <summary>
		/// This class is responsible for parsing Assembly_summary_genbank.txt and copying Tax id and ftp path of organisms in Assembly_taxid_ftppath.txt.
		/// </summary>
		private static string currentDirectory = Directory.GetCurrentDirectory();
		public static void initiate()
		{
			currentDirectory = currentDirectory.Substring(0, currentDirectory.Length - 57);
			//Stopwatch stopwatch = new Stopwatch();
			//stopwatch.Start();
			long lines = 0;
			string line = null;
			//Console.WriteLine("Extracting tax id and corresponding webaddress  [INITIATED]");
			//Console.WriteLine("Searching file update                           [INITIATED]");
			FileStream fileStreamRead = new FileStream(currentDirectory+"GenomeDatabaseData/Assembly_summary_genbank/Assembly_summary_genbank.txt", FileMode.Open, FileAccess.Read, FileShare.None);
			FileStream fileStreamWrite = new FileStream(currentDirectory+"GenomeDatabaseData/Taxonomy/Parsedtaxonomy/Assembly_taxid_ftppath.txt", FileMode.Create, FileAccess.Write, FileShare.None);
			try
			{
				StreamReader sr = new StreamReader(fileStreamRead);
				StreamWriter sw = new StreamWriter(fileStreamWrite);
				//Console.WriteLine("Searching file update                           [SUCCEEDED]");
			//	Console.WriteLine("Extracting tax id and corresponding webaddress [PROCESSING]");
				sw.WriteLine("S.N\t|\tTax_id\t|\tFtp_path");
				sw.WriteLine("======================================");
				while ((line = sr.ReadLine()) != null)
				{
					if (parsed_Column(line, 1) == "GCA")
					{
						string taxId = parsed_Column(line, 6);
						string ftpPath = parsed_Column(line, 20);
						sw.WriteLine("{0}\t|\t{1}\t|\t{2}", lines + 1, taxId, ftpPath);
						lines++;
					}
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
				//Console.WriteLine("Total parsed lines: {0}", lines);
				//stopwatch.Stop();
				//TimeSpan ts = stopwatch.Elapsed;
				//string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
				//Console.WriteLine("Total RunTime: " + elapsedTime);
				//Console.WriteLine("\n");
			}

		}
		private static string parsed_Column(String line, int column)
		{
			int col = 1;
			string parsed = null;
			foreach (char ch in line)
			{
				if (ch == '\t')
				{
					col++;
					if (col > column)
					{
						break;
					}
				}
				else if (col == column && ch != '\t')
				{
					parsed += ch;
				}
			}
			if (column == 1)
			{
				return parsed.Substring(0, 3);
			}
			return parsed;
		}

		private static void check_file()
		{
			if (new FileInfo(currentDirectory+"GenomeDatabaseData/Taxonomy/Parsedtaxonomy/Assembly_taxid_ftppath.txt").Length == 0)
			{
				throw new Exception("Extracting tax id and corresponding webaddress [FAILED]");
			}
		}
	}
}

