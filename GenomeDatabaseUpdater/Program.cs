using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bio;
using Bio.IO;
using Bio.IO.GenBank;
using MySql.Data.MySqlClient;

namespace GenomeDatabaseUpdater
{
	/// <summary>
	/// This class is responsible for updating the database "prokaryote_schema"
	/// Updating happens in following steps:
	/// 1. Extracting newbacterias
	/// 	a. extracting the bacterias paths already in database from Bacteria_in_db_path.txt
	/// 	b. extracting all the files paths that exist in folder "GenomeDabataseData/bacteria_assembly_summary/downloaded"
	///     c. comparing these list to extract bacteris path which does not exist in database
	/// 2. Parsing newbacterias
	/// 	a. parsing the scafolds of the specific organism
	/// 	b. updating the Bacteria_in_db_path.txt with parsed organism
	/// </summary>
	class MainClass
	{
		private static List<string> newbacteriaspath = null;
		private static MySqlConnection conn = null;
		private static long prokaryote_id = 0;
		private static string currentDirectory = Directory.GetCurrentDirectory();

		public static void Main(string[] args)
		{
			currentDirectory = currentDirectory.Substring(0, currentDirectory.Length - 46);
			initiate_Updater();
			Console.Write("\nPress any key to continue...");
			Console.ReadKey();
		}

		private static void initiate_Updater()
		{
			int i = 1;
			Console.WriteLine("Connecting to database prokaryote_schema...");
			try
			{
				conn = new MySqlConnection("Server=127.0.0.1;Database=prokaryote_schema;Uid=root;Pwd=Anitar@n@");
				conn.Open();
				Console.WriteLine("Connected to database prokaryote_schema\n");

				newbacteriaspath = new List<string>();
				extract_newbacterias();
				Console.WriteLine("\nNumber of bacterias to be parsed to database: {0}\n", newbacteriaspath.Count);
				Console.WriteLine("Press any key to initiate the parsing");
				Console.ReadKey();
				Console.WriteLine("Loading newbacterias to database...");

				foreach (var bacteria in newbacteriaspath)
				{
					Console.WriteLine("Parsing .gbff file [{0}/{1}]", i,newbacteriaspath.Count);
					List<ISequence> sequences = new List<ISequence>();
					ISequenceParser parser = new Bio.IO.GenBank.GenBankParser();
					using (parser.Open(bacteria))
					{
						sequences = parser.Parse().ToList();
					}
					int j = 1;
					foreach (var sequence in sequences)
					{
						Console.WriteLine("\tParsingSequence [{0}/{1}]", j, sequences.Count);
						parse_to_database(sequence);
						j++;
					}
					Console.WriteLine();
					append_new_path(bacteria);
					i++;
					prokaryote_id = 0;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("\nError occured: "+ex.Message);
			}
			finally
			{
				if (i-1==newbacteriaspath.Count && newbacteriaspath.Count != 0)
					Console.WriteLine("\nDatabase Updated Successfully");
				else
					Console.WriteLine("\nDatabase is not Updated");
			}

		}

		private static void extract_newbacterias()
		{
			DirectoryInfo directory = new DirectoryInfo(@currentDirectory+"GenomeDatabaseData/Bacteria_assembly_summary/Downloaded");
			List<string> bacteriasindb = new List<string>();
			FileStream fileStreamRead = new FileStream(@currentDirectory+"GenomeDatabaseData/Taxonomy/Parsedtaxonomy/Bacteria_in_db_path.txt", FileMode.Open, FileAccess.Read, FileShare.None);
			try
			{
				StreamReader sr = new StreamReader(fileStreamRead);
				string line = null;
				while ((line = sr.ReadLine()) != null)
				{
					bacteriasindb.Add(line);
				}
				sr.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			foreach (FileInfo file in directory.GetFiles("*.gbff"))
			{
				if (!bacteriasindb.Contains(file.FullName))
				{
					newbacteriaspath.Add(file.FullName);
				}
			}
		}

		private static void append_new_path(string bacteriaaddedtodb)
		{
			using (StreamWriter sw = File.AppendText(currentDirectory+"GenomeDatabaseData/Taxonomy/Parsedtaxonomy/Bacteria_in_db_path.txt"))
			{
				if (sw == null)
				{
					throw new Exception("Unexpected error occured");
				}
				sw.WriteLine(bacteriaaddedtodb);
				sw.Flush();
			}
		}

		private static void parse_to_database(ISequence sequence)
		{
			//executes only once for each organism regarless of number of sequences
			if (prokaryote_id == 0)
			{
				foreach (object o in sequence.Metadata.Values)
				{
					if (!o.GetType().Equals(typeof(GenBankMetadata)))
					{
						throw new Exception("Unexpected data!!");
					}
					else
					{
						fill_prokaryote((GenBankMetadata)o);
						break;
					}
				}
			}
			fill_sequence(sequence);
		}

		private static void fill_prokaryote(GenBankMetadata metadata)
		{
			Prokaryote prokaryote = new Prokaryote(metadata.Source);
			prokaryote_id = prokaryote.execute_query(conn);
			fill_comment(metadata.Comments);
			fill_dblink(metadata.DbLinks);
			fill_reference(metadata.References);
		}

		private static void fill_comment(IList<String> com)
		{
			foreach (var c in com)
			{
				Comment comment = new Comment(c, prokaryote_id);
				comment.execute_query(conn);
				break;
			}
		}

		private static void fill_dblink(IList<CrossReferenceLink> dbls)
		{
			foreach (var db in dbls)
			{
				Dblink dblink = new Dblink(db, prokaryote_id);
				dblink.execute_query(conn);
			}
		}

		private static void fill_reference(IList<CitationReference> refe)
		{
			foreach (var r in refe)
			{
				Reference reference = new Reference(r, prokaryote_id);
				reference.execute_query(conn);
			}
		}

		private static void fill_sequence(ISequence seq)
		{
			Sequence sequence = new Sequence(seq,prokaryote_id);
			long sequence_id = sequence.execute_query(conn);
			fill_alphabet(seq.Alphabet, sequence_id);
			foreach (object o in seq.Metadata.Values)
			{
				if (!o.GetType().Equals(typeof(GenBankMetadata)))
				{
					throw new Exception("Unexpected data!!");
				}
				else
				{
					fill_metadata((GenBankMetadata)o, sequence_id);
					break;
				}
			}
		}

		private static void fill_alphabet(IAlphabet alp, long id)
		{
			Alphabet alphabet = new Alphabet(alp, id);
			alphabet.execute_query(conn);
		}

		private static void fill_metadata(GenBankMetadata meta, long id)
		{
			Metadata metadata = new Metadata(meta, id);
			long metadata_id = metadata.execute_query(conn);
			fill_locus(meta.Locus, metadata_id);
			fill_version(meta.Version, metadata_id);
			foreach (var feat in meta.Features.All)
			{
				fill_feature(feat, metadata_id);
			}
		}

		private static void fill_locus(GenBankLocusInfo loc, long id)
		{
			Locus locus = new Locus(loc, id);
			locus.execute_query(conn);
		}

		private static void fill_version(GenBankVersion ver, long id)
		{
			Version version = new Version(ver, id);
			version.execute_query(conn);
		}

		private static void fill_feature(FeatureItem fe, long id)
		{

			//this query is inconsistent with other beacause other approach creates and error;
			Feature feat = new Feature(fe, id);
			MySqlCommand cmmd14 = new MySqlCommand(feat.get_query(), conn);
			MySqlDataReader reader14 = cmmd14.ExecuteReader();
			reader14.Close();

			long feature_id = cmmd14.LastInsertedId;

			foreach (var qua in fe.Qualifiers)
			{
				long qualifier_id = fill_qualifier(qua.Key, feature_id);
				foreach (var v in qua.Value)
				{
					fill_qualifier_value(v.ToString(), qualifier_id);
				}
			}
		}

		private static long fill_qualifier(string key, long id)
		{
			Qualifier qualifier = new Qualifier(key, id);
			long qua_id = qualifier.execute_query(conn);
			return qua_id;
		}

		private static void fill_qualifier_value(string val, long id)
		{
			Qualifier_value qualifier_value = new Qualifier_value(val, id);
			qualifier_value.execute_query(conn);
		}
	}
}
