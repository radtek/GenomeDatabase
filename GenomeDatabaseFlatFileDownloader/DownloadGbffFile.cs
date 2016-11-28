using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace GenomeDatabaseFlatFileDownloader
{
	public class DownloadGbffFile
	{
		/// <summary>
		/// This class is responsible for downloading the bacterias .gbff.gz files using address in Tobe_Downloaded_Prokaryote_taxid_ftppath.txt
		/// </summary>
		private static string file_name = "genomic.gbff.gz";
		private static Dictionary<int, string> assembly = new Dictionary<int, string>();
		private static long count = 0;
		private static string currentDirectory = Directory.GetCurrentDirectory();

		public static Dictionary<int, string> get_bacterias()
		{
			return new Dictionary<int, string>(assembly);
		}

		public static void initiate()
		{
			//Console.WriteLine("Reading bacteria webaddress from file   \t\t\t[PROCESSING]");
			currentDirectory = currentDirectory.Substring(0, currentDirectory.Length - 57);
			String line = null;
			int bacteria = 0;
			FileStream fileStreamRead = new FileStream(currentDirectory+"GenomeDatabaseData/Taxonomy/Parsedtaxonomy/Tobe_Downloaded_Prokaryote_taxid_ftppath.txt", FileMode.Open, FileAccess.Read, FileShare.None);
			try
			{
				StreamReader sr = new StreamReader(fileStreamRead);

				//parsing Prokaryote_taxid_parentid.txt
				while ((line = sr.ReadLine()) != null)
				{
					if (parse_bacteria(line, 1) == "number")
					{
						int key = int.Parse(parse_bacteria(line, 2));
						assembly.Add(key, parse_bacteria(line, 3));
						bacteria++;
					}
				}
			}
			catch (System.IO.IOException e)
			{
				Console.WriteLine("Error: Cannot find the file");
				Console.WriteLine(e.Message);
			}
			finally
			{
			//	Console.WriteLine("Number of bacterias in assembly_report to be downloaded : \t[{0}]", bacteria);
			}
		}

		public static void download_gff_file(string Value, string user, string pwd)
		{
				string name = extract_name(Value);
			    download_file(Value, name, user, pwd);
		}


		private static string extract_name(String s)
		{
			String name = null;
			int c = 0;
			foreach (char ch in s)
			{
				if (ch == '/')
					c++;
				if (c == 5 && ch != '/')
					name += ch;
			}
			return name;
		}


		private static void download_file(string path, string name, string username, string password)
		{
			string localfullpath = currentDirectory+"GenomeDatabaseData/Bacteria_assembly_summary/Downloaded/" + name + "_" + file_name;
			using (WebClient request = new WebClient())
			{
				request.Credentials = new NetworkCredential(username, password);
				byte[] fileData = request.DownloadData(path + "/" + name + "_" + file_name);
				using (FileStream file = File.Create(localfullpath))
				{
					file.Write(fileData, 0, fileData.Length);
					file.Close();
					count++;
				}
			}
		}

		private static string parse_bacteria(String l, int column)
		{
			String raw = null;
			int id;
			int col = 1;
			foreach (char ch in l)
			{
				if (ch == '|')
				{
					col++;
					if (col > column)
					{
						break;
					}
				}
				else if (col == column && ch != '\t')
				{
					raw += ch;
				}
			}
			if (int.TryParse(raw, out id) && column == 1)
				return "number";
			else
				return raw;
		}
	}
}

