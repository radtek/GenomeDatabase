using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;

namespace GenomeDatabaseFlatFileDownloader
{
	class MainClass
	{
		private static Dictionary<int, string> bacterias = new Dictionary<int, string>();

		private static string username = "anonymous";
		private static string password = "anonymous";
		private static string ftpsite = "ftp://ftp.ncbi.nlm.nih.gov/genomes";
		private static string currentDirectory = Directory.GetCurrentDirectory();
		public static void Main(string[] args)
		{
			currentDirectory = currentDirectory.Substring(0, currentDirectory.Length - 57);
			initiate_downloader();
			Console.Write("\n[FINISHED] Press any key...");
			Console.ReadKey();
		}

		private static void initiate_downloader()
		{
			Console.WriteLine("Connecting to "+ftpsite+"... ");
			try
			{
				Uri target = new Uri(ftpsite);
				FtpWebRequest ftprequest = (FtpWebRequest)WebRequest.Create(target);
				ftprequest.Credentials = new NetworkCredential(username, password);
				Console.WriteLine("Connected to " + ftpsite);

				Console.WriteLine("\nSearching bacterias...");

				if (!download_file_from_ncbi("ftp://ftp.ncbi.nlm.nih.gov/genomes/ASSEMBLY_REPORTS/assembly_summary_genbank.txt", currentDirectory+"GenomeDatabaseData/Assembly_summary_genbank/Assembly_summary_genbank.txt"))
				{
					Console.WriteLine("Error in downloading the file");
					return;
				}

				FindWebAddresses.initiate();
				FindProkaryotes.initiate();

				long newbacterias = ExtractProkaryoteWebaddresses.initiate();
				Console.WriteLine("\nNumber of new bacterias found: {0}\n", newbacterias);
				Console.WriteLine("Number of flat files to be downloaded: {0}\n", newbacterias);

				Console.WriteLine("\t[INFO] Dowloading can be aborted by pressing key 'a'\n");
				Console.Write("Press any key to initiate download");
				Console.ReadKey();
				Console.WriteLine();
				DownloadGbffFile.initiate();
				bacterias = DownloadGbffFile.get_bacterias();

				int i = 1;
				foreach (var bacteria in bacterias)
				{
					if (Console.KeyAvailable)
					{
						ConsoleKeyInfo key = Console.ReadKey(true);
						if (key.Key == ConsoleKey.A)
						{
							Console.WriteLine("\n[WARNING] A is pressed. Download aborted!!");
							break;
						}
					}
					Console.WriteLine("Downloading bacteria with taxid: {0} progress [{1}/{2}]", bacteria.Key, i, bacterias.Count);
					DownloadGbffFile.download_gff_file(bacteria.Value, username, password);
					i++;
				}
				ftprequest.Abort();
			}
			catch (WebException ex)
			{
				Console.WriteLine(ex.Message);
			}
			Console.WriteLine("\nDecompressing newly downloaded files...");
			extract_gz_files();
		}

		private static bool download_file_from_ncbi(string ftppath, string localpath)
		{
			using (WebClient request = new WebClient())
			{
				request.Credentials = new NetworkCredential(username, password);
				byte[] fileData = request.DownloadData(localpath);
				using (FileStream file = File.Create(localpath))
				{
					file.Write(fileData, 0, fileData.Length);
					file.Close();
				}
			}
			if (new FileInfo(localpath).Length == 0)
			{
				return false;
			}
			return true;
		}


		private static void extract_gz_files()
		{
			string path = @currentDirectory+"GenomeDatabaseData/Bacteria_assembly_summary/Downloaded/";
			DirectoryInfo directory = new DirectoryInfo(path);
			//extract the gz files 
			foreach (FileInfo filetodecompress in directory.GetFiles("*.gz"))
			{
				decompress_file(filetodecompress);
				filetodecompress.Delete();
			}

		}

		private static void decompress_file(FileInfo filetodecompress)
		{
			using (FileStream originalFileStream = filetodecompress.OpenRead())
			{
				string currentFileName = filetodecompress.FullName;
				string newFileName = currentFileName.Remove(currentFileName.Length - filetodecompress.Extension.Length);

				using (FileStream decompressedFileStream = File.Create(newFileName))
				{
					using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
					{
						decompressionStream.CopyTo(decompressedFileStream);
						Console.WriteLine("Decompressed: {0}", filetodecompress.Name);
					}
				}
			}
		}


	}
}
