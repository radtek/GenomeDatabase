using System;
using System.Collections.Generic;
using System.Linq;
using Bio;
using Bio.IO;
using Bio.IO.GenBank;
using MySql.Data.MySqlClient;

namespace GenomeDatabaseFlatFileDownloader
{
	public class LoadGbffFileToDatabase
	{
		private string path = null;
		private List<ISequence> sequences;
		private static MySqlConnection conn = new MySqlConnection("Server=127.0.0.1;Database=prokaryote_sequence_clone1;Uid=root;Pwd=Anitar@n@");

		public LoadGbffFileToDatabase(string p)
		{
			this.path = p;
			this.sequences = new List<ISequence>();
		}

		public void initiate()
		{
			try
			{
				extract_sequences();
				conn.Open();
				foreach (var sequence in this.sequences)
				{
					fill_sequence(sequence);
				}
				conn.Close();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		//this functiion parses the data from .gbff file
		private void extract_sequences()
		{
			ISequenceParser parser = new Bio.IO.GenBank.GenBankParser();
			//Console.WriteLine(path);
			using (parser.Open(this.path))
			{
				this.sequences = parser.Parse().ToList();
			}
		}


		private static void fill_sequence(ISequence sequence)
		{
			Sequence seq = new Sequence(sequence);
			MySqlCommand cmmd1 = new MySqlCommand(seq.get_query(), conn);
			MySqlDataReader reader1 = cmmd1.ExecuteReader();
			reader1.Close();

			long sequence_id = cmmd1.LastInsertedId;

			fill_alphabet(sequence.Alphabet, sequence_id);

			foreach (object o in sequence.Metadata.Values)
			{
				if (!o.GetType().Equals(typeof(GenBankMetadata)))
				{
					throw new Exception("Unexpected data!!");
				}
				else
				{
					fill_metadata((GenBankMetadata)o, sequence_id);
				}
			}
		}


		private static void fill_alphabet(IAlphabet alphabet, long id)
		{
			Alphabet alp = new Alphabet(alphabet, id);
			MySqlCommand cmmd2 = new MySqlCommand(alp.get_query(), conn);
			MySqlDataReader reader2 = cmmd2.ExecuteReader();
			reader2.Close();
		}


		private static void fill_metadata(GenBankMetadata metadata, long id)
		{
			//codes to add data here
			Metadatas met = new Metadatas(metadata, id);
			MySqlCommand cmmd3 = new MySqlCommand(met.get_query(), conn);
			MySqlDataReader reader3 = cmmd3.ExecuteReader();
			reader3.Close();

			long metadata_id = cmmd3.LastInsertedId;

			fill_locus(metadata.Locus, metadata_id);
			fill_version(metadata.Version, metadata_id);
			fill_source(metadata.Source, metadata_id);
			foreach (var s in metadata.Accession.Secondary)
			{
				fill_secondary_accession(s.ToString(), metadata_id);
			}
			foreach (var c in metadata.Comments)
			{
				fill_comment(c.ToString(), metadata_id);
			}
			foreach (var d in metadata.DbLinks)
			{
				fill_dblink(d, metadata_id);
			}
			foreach (var r in metadata.References)
			{
				fill_reference(r, metadata_id);
			}
			foreach (var f in metadata.Features.All)
			{
				fill_feature(f, metadata_id);
			}

		}

		private static void fill_secondary_accession(String secondary, long id)
		{
			SecondaryAccession sec = new SecondaryAccession(secondary, id);
			MySqlCommand cmmd7 = new MySqlCommand(sec.get_query(), conn);
			MySqlDataReader reader7 = cmmd7.ExecuteReader();
			reader7.Close();
		}


		private static void fill_comment(String comment, long id)
		{
			Comment com = new Comment(comment, id);
			MySqlCommand cmmd8 = new MySqlCommand(com.get_query(), conn);
			MySqlDataReader reader8 = cmmd8.ExecuteReader();
			reader8.Close();
		}


		private static void fill_dblink(CrossReferenceLink dblink, long id)
		{
			Dblink dbl = new Dblink(dblink, id);
			MySqlCommand cmmd9 = new MySqlCommand(dbl.get_query(), conn);
			MySqlDataReader reader9 = cmmd9.ExecuteReader();
			reader9.Close();
		}


		private static void fill_locus(GenBankLocusInfo locus, long id)
		{
			Locus loc = new Locus(locus, id);
			MySqlCommand cmmd10 = new MySqlCommand(loc.get_query(), conn);
			MySqlDataReader reader10 = cmmd10.ExecuteReader();
			reader10.Close();
		}


		private static void fill_version(GenBankVersion version, long id)
		{
			Version ver = new Version(version, id);
			MySqlCommand cmmd11 = new MySqlCommand(ver.get_query(), conn);
			MySqlDataReader reader11 = cmmd11.ExecuteReader();
			reader11.Close();
		}


		private static void fill_source(SequenceSource source, long id)
		{
			Source sou = new Source(source, id);
			MySqlCommand cmmd12 = new MySqlCommand(sou.get_query(), conn);
			MySqlDataReader reader12 = cmmd12.ExecuteReader();
			reader12.Close();
		}


		private static void fill_reference(CitationReference reference, long id)
		{
			Reference refe = new Reference(reference, id);
			MySqlCommand cmmd13 = new MySqlCommand(refe.get_query(), conn);
			MySqlDataReader reader13 = cmmd13.ExecuteReader();
			reader13.Close();
		}


		private static void fill_feature(FeatureItem feature, long id)
		{
			Feature feat = new Feature(feature, id);
			MySqlCommand cmmd14 = new MySqlCommand(feat.get_query(), conn);
			MySqlDataReader reader14 = cmmd14.ExecuteReader();
			reader14.Close();

			long feature_id = cmmd14.LastInsertedId;

			foreach (var q in feature.Qualifiers)
			{
				long qualifier_id = fill_qualifier(q.Key, feature_id);
				foreach (var qv in q.Value)
				{
					//Console.WriteLine("{0}", qualifier_id);
					fill_qualifier_value(qv.ToString(), qualifier_id);
				}
			}
		}


		private static long fill_qualifier(String key, long id)
		{
			Qualifier qua = new Qualifier(key, id);
			MySqlCommand cmmd15 = new MySqlCommand(qua.get_query(), conn);
			MySqlDataReader reader15 = cmmd15.ExecuteReader();
			reader15.Close();

			return cmmd15.LastInsertedId;
		}


		private static void fill_qualifier_value(String value, long id)
		{
			Qualifier_value qua_val = new Qualifier_value(value, id, conn);
			qua_val.execute_query();
		}

	}
}

