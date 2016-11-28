using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace GenomeDatabaseFlatFileDownloader
{
	public class ExtractProkaryoteWebaddresses
	{
		/// <summary>
		/// This class examines Assembly_taxid_ftppath.txt and Prokaryote_taxid_parentid.txt and extracts the tax id and ftp path of only prokaryotes to file 
		/// Matched_Prokaryote_taxid_ftppath.txt. 
		/// This class also determines list of files that needs to be downloaded and parses those ftp path in Tobe_Downloaded_Prokaryote_taxid_ftppath.txt
		/// </summary>
		private static string[] fileEntries = null;
		private static Dictionary<int, string> assembly = null;
		private static string currentDirectory = Directory.GetCurrentDirectory();

		public static long initiate()
		{
			//Stopwatch stopwatch = new Stopwatch();
			//stopwatch.Start();
			currentDirectory = currentDirectory.Substring(0, currentDirectory.Length - 57);
			long taxidMatched = 0;
			long taxidAlreadyPreset = 0;
			string line = null;
			long parsedBacteria = 0;
			long parsedAssembly = 0;
			assembly = new Dictionary<int, string>();
			List<int> bacteria = new List<int>();
			fileEntries = Directory.GetFiles(currentDirectory+"GenomeDatabaseData/Bacteria_assembly_summary/Downloaded");

			//Console.WriteLine("Prokaryote tax id comparing update     [INITIATED]");
			//Console.WriteLine("Searching file update                  [INITIATED]");
			FileStream fileStreamRead0 = new FileStream(currentDirectory+"GenomeDatabaseData/Taxonomy/Parsedtaxonomy/Assembly_taxid_ftppath.txt", FileMode.Open, FileAccess.Read, FileShare.None);
			FileStream fileStreamRead1 = new FileStream(currentDirectory+"GenomeDatabaseData/Taxonomy/Parsedtaxonomy/Prokaryote_taxid_parentid.txt", FileMode.Open, FileAccess.Read, FileShare.None);
			FileStream fileStreamWrite = new FileStream(currentDirectory+"GenomeDatabaseData/Taxonomy/Parsedtaxonomy/Matched_Prokaryote_taxid_ftppath.txt", FileMode.Create, FileAccess.Write, FileShare.None);
			FileStream fileStreamWrite1 = new FileStream(currentDirectory+"GenomeDatabaseData/Taxonomy/Parsedtaxonomy/Tobe_Downloaded_Prokaryote_taxid_ftppath.txt", FileMode.Create, FileAccess.Write, FileShare.None);
			try
			{
				StreamReader sr0 = new StreamReader(fileStreamRead0);
				StreamReader sr1 = new StreamReader(fileStreamRead1);
				StreamWriter sw = new StreamWriter(fileStreamWrite);
				StreamWriter sw1 = new StreamWriter(fileStreamWrite1);

			//	Console.WriteLine("Searching file update                  [SUCCEEDED]");
			//	Console.WriteLine("Parsing prokaryote_taxid_parentid.txt [PROCESSING]");

				//parsing Prokaryote_taxid_parentid.txt
				while ((line = sr1.ReadLine()) != null)
				{
					int sN = 0;
					if (int.TryParse(parsed_node(line, 1), out sN))
					{
						int iD = 0;
						if (!int.TryParse(parsed_node(line, 2), out iD))
						{
							throw new Exception("Invalid Tax_id!");
						}
						else
						{
							bacteria.Add(iD);
							parsedBacteria++;
						}
					}
				}
			//	Console.WriteLine("Total parsed bacterias: {0}", parsedBacteria);

				//parsing Assembly_taxid_ftppath.txt
			//	Console.WriteLine("Parsing assembly_taxid_ftppath.txt    [PROCESSING]");
				while ((line = sr0.ReadLine()) != null)
				{
					int Sn = 0;
					if (int.TryParse(parsed_node(line, 1), out Sn))
					{
						int Id = 0;
						if (!int.TryParse(parsed_node(line, 2), out Id))
						{
							throw new Exception("Invalid Tax_id!");
						}
						else
						{
							if (!assembly.ContainsKey(Id))
							{
								assembly.Add(Id, parsed_node(line, 3));
								parsedAssembly++;
							}
						}
					}
				}
			//	Console.WriteLine("Total parsed Assembly: {0}", parsedAssembly);

				//checking number of bacterias with webaddress
			//	Console.WriteLine("Prokaryote tax id comparing update    [PROCESSING]");
				sw.WriteLine("S.N\t|\tTax_id\t|\tFtp_path");
				sw.WriteLine("======================================");
				sw1.WriteLine("S.N\t|\tTax_id\t|\tFtp_path");
				sw1.WriteLine("======================================");
				foreach (var node in assembly)
				{
					if (bacteria.Contains(node.Key))
					{
						if (check_downloaded_files(node.Value))
						{
							taxidAlreadyPreset++;
						}
						else
						{
							sw1.WriteLine("{0}\t|\t{1}\t|\t{2}", taxidMatched + 1, node.Key, node.Value);
						}
						sw.WriteLine("{0}\t|\t{1}\t|\t{2}", taxidMatched + 1, node.Key, node.Value);
						taxidMatched++;
					}
				}


				sr0.Close();
				sr1.Close();
				sw.Flush();
				sw1.Flush();
				sw1.Close();
				sw.Close();
				check_file();
			}

			catch (System.IO.IOException e)
			{
				Console.WriteLine("Error: Cannot find the file");
				Console.WriteLine(e.Message);
			}
			finally
			{
				//Console.WriteLine("Number of tax_id matched : {0}", taxidMatched);
			//	Console.WriteLine("Unmatched tax_is in prokaryote_node.txt: {0}", 410868 - taxidMatched);
			//	stopwatch.Stop();
			//	TimeSpan ts = stopwatch.Elapsed;
			//	string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
			//	Console.WriteLine("Total RunTime: " + elapsedTime);
			//	Console.WriteLine("\n");
			}
			return taxidMatched-taxidAlreadyPreset;
		}



		private static bool check_downloaded_files(string ftp)
		{
			while (true)
			{
				if (ftp[0] == 'f')
				{
					break;
				}
				ftp = ftp.Substring(1);
			}
			ftp += "_genomic.gbff";
			foreach (string fileName in fileEntries)
			{
				string s = "ftp://ftp.ncbi.nlm.nih.gov/genomes/all/" + extractFileName(fileName);
				if (s.Equals(ftp))
				{
					return true;
				}
			}
			return false;
		}

		private static String extractFileName(String path)
		{
			int count = 0;
			int slas = 0;
			foreach (char ch in path)
			{
				count++;
				if (ch == '/')
					slas++;
				if (slas == 8)
					break;
			}
			return path.Substring(count);
		}

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
				else if (col == column && (ch != '\t' || ch != '|'))
				{
					parsed += ch;
				}
			}
			return parsed;
		}

		private static void check_file()
		{
			if (new FileInfo(currentDirectory+"GenomeDatabaseData" +
			                 "/Taxonomy/Parsedtaxonomy/Matched_Prokaryote_taxid_ftppath.txt").Length == 0)
			{
				throw new Exception("Copying Prokaryote webaddress [FAILED]");
			}
		}
	}
}

