using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace GenomeDatabaseDatabaseLayer
{
	public static class GDDL
	{
		private static string connection_string = "Server=127.0.0.1;Database=prokaryote_schema;Uid=root;Pwd=Anitar@n@";

		#region database content querying methods

		public static Dictionary<string, string> listProkaryotes()
		{
			Dictionary<string, string> all_prokaryotes = new Dictionary<string, string>();
			MySqlConnection conn = new MySqlConnection(connection_string);
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = ("SELECT common_name, class_level FROM prokaryote");
			command.Connection = conn;
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				all_prokaryotes.Add(reader.GetString(0).ToString(), reader.GetString(1).ToString());
			}
			command.Connection.Close();
			return all_prokaryotes;
		}

		public static Dictionary<string, List<string>> listDblinks()
		{
			Dictionary<string, List<string>> db_links = new Dictionary<string, List<string>>();
			MySqlConnection conn = new MySqlConnection(connection_string);
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = ("SELECT p.common_name, d.value FROM prokaryote AS p, dblink AS d WHERE p.id = d.prokaryote_id");
			command.Connection = conn;
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				string key = reader.GetString(0).ToString();

				if (!db_links.ContainsKey(key))
				{
					db_links.Add(key, new List<string>());

				}
					db_links[key].Add(reader.GetString(1).ToString());
			}
			command.Connection.Close();
			return db_links;
		}

		public static Dictionary<string, List<string>> listJournals()
		{
			Dictionary<string, List<string>> reference_journals = new Dictionary<string, List<string>>();
			MySqlConnection conn = new MySqlConnection(connection_string);
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = ("SELECT p.common_name, r.journal FROM prokaryote AS p, reference AS r WHERE p.id = r.prokaryote_id");
			command.Connection = conn;
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				string key = reader.GetString(0).ToString();

				if (!reference_journals.ContainsKey(key))
				{
					reference_journals.Add(key, new List<string>());

				}
				reference_journals[key].Add(reader.GetString(1).ToString());
			}
			command.Connection.Close();
			return reference_journals;
		}

		public static Dictionary<string, List<string>> listTitles()
		{
			Dictionary<string, List<string>> reference_titles = new Dictionary<string, List<string>>();
			MySqlConnection conn = new MySqlConnection(connection_string);
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = ("SELECT p.common_name, r.title FROM prokaryote AS p, reference AS r WHERE p.id = r.prokaryote_id");
			command.Connection = conn;
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				string key = reader.GetString(0).ToString();

				if (!reference_titles.ContainsKey(key))
				{
					reference_titles.Add(key, new List<string>());

				}
				reference_titles[key].Add(reader.GetString(1).ToString());
			}
			command.Connection.Close();
			return reference_titles;
		}

		public static Dictionary<string, List<string>> listSequences()
		{
			Dictionary<string, List<string>> sequences = new Dictionary<string, List<string>>();
			MySqlConnection conn = new MySqlConnection(connection_string);
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = ("SELECT p.common_name, s.value FROM prokaryote AS p, sequence AS s WHERE p.id = s.prokaryote_id");
			command.Connection = conn;
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				string key = reader.GetString(0).ToString();

				if (!sequences.ContainsKey(key))
				{
					sequences.Add(key, new List<string>());

				}
				sequences[key].Add(reader.GetString(1).ToString());
			}
			command.Connection.Close();
			return sequences;
		}

		public static Dictionary<string, List<string>> listGenbankIDs()
		{
			Dictionary<string, List<string>> genbank_ids = new Dictionary<string, List<string>>();
			MySqlConnection conn = new MySqlConnection(connection_string);
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = ("SELECT p.common_name, s.genbank_ID FROM prokaryote AS p, sequence AS s WHERE p.id = s.prokaryote_id");
			command.Connection = conn;
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				string key = reader.GetString(0).ToString();

				if (!genbank_ids.ContainsKey(key))
				{
					genbank_ids.Add(key, new List<string>());

				}
				genbank_ids[key].Add(reader.GetString(1).ToString());
			}
			command.Connection.Close();
			return genbank_ids;
		}

		public static Dictionary<string, List<string>> listDefinitions()
		{
			Dictionary<string, List<string>> definitions = new Dictionary<string, List<string>>();
			MySqlConnection conn = new MySqlConnection(connection_string);
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = ("SELECT s.genbank_ID, m.definition FROM sequence AS s, metadata AS m WHERE s.id = m.sequence_id");
			command.Connection = conn;
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				string key = reader.GetString(0).ToString();

				if (!definitions.ContainsKey(key))
				{
					definitions.Add(key, new List<string>());

				}
				definitions[key].Add(reader.GetString(1).ToString());
			}
			command.Connection.Close();
			return definitions;
		}

		public static Dictionary<string, List<string>> listGiNumbers()
		{
			Dictionary<string, List<string>> gi_numbers = new Dictionary<string, List<string>>();
			MySqlConnection conn = new MySqlConnection(connection_string);
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = ("SELECT s.genbank_ID, v.ginumber FROM sequence AS s, metadata AS m, version AS v WHERE s.id = m.sequence_id AND m.id = v.metadata_id");
			command.Connection = conn;
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				string key = reader.GetString(0).ToString();

				if (!gi_numbers.ContainsKey(key))
				{
					gi_numbers.Add(key, new List<string>());

				}
				gi_numbers[key].Add(reader.GetString(1).ToString());
			}
			command.Connection.Close();
			return gi_numbers;
		}

		public static Dictionary<string, List<string>> listAccessionVersions()
		{
			Dictionary<string, List<string>> accession_versions = new Dictionary<string, List<string>>();
			MySqlConnection conn = new MySqlConnection(connection_string);
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = ("SELECT s.genbank_ID, v.compound_accession FROM sequence AS s, metadata AS m, version AS v WHERE s.id = m.sequence_id AND m.id = v.metadata_id");
			command.Connection = conn;
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				string key = reader.GetString(0).ToString();

				if (!accession_versions.ContainsKey(key))
				{
					accession_versions.Add(key, new List<string>());

				}
				accession_versions[key].Add(reader.GetString(1).ToString());
			}
			command.Connection.Close();
			return accession_versions;
		}

		public static Dictionary<string, List<string>> listDivisionCodes()
		{
			Dictionary<string, List<string>> division_codes = new Dictionary<string, List<string>>();
			MySqlConnection conn = new MySqlConnection(connection_string);
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = ("SELECT s.genbank_ID, l.divisioncode FROM sequence AS s, metadata AS m, locus AS l WHERE s.id = m.sequence_id AND m.id = l.metadata_id");
			command.Connection = conn;
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				string key = reader.GetString(0).ToString();

				if (!division_codes.ContainsKey(key))
				{
					division_codes.Add(key, new List<string>());

				}
				division_codes[key].Add(reader.GetString(1).ToString());
			}
			command.Connection.Close();
			return division_codes;
		}

		public static Dictionary<string, List<string>> listMoleculeTypes()
		{
			Dictionary<string, List<string>> molecule_types = new Dictionary<string, List<string>>();
			MySqlConnection conn = new MySqlConnection(connection_string);
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = ("SELECT s.genbank_ID, l.molecule FROM sequence AS s, metadata AS m, locus AS l WHERE s.id = m.sequence_id AND m.id = l.metadata_id");
			command.Connection = conn;
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				string key = reader.GetString(0).ToString();

				if (!molecule_types.ContainsKey(key))
				{
					molecule_types.Add(key, new List<string>());

				}
				molecule_types[key].Add(reader.GetString(1).ToString());
			}
			command.Connection.Close();
			return molecule_types;
		}

		public static Dictionary<string, List<string>> listStrandTopologies()
		{
			Dictionary<string, List<string>> strand_topologies = new Dictionary<string, List<string>>();
			MySqlConnection conn = new MySqlConnection(connection_string);
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = ("SELECT s.genbank_ID, l.strandtopology FROM sequence AS s, metadata AS m, locus AS l WHERE s.id = m.sequence_id AND m.id = l.metadata_id");
			command.Connection = conn;
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				string key = reader.GetString(0).ToString();

				if (!strand_topologies.ContainsKey(key))
				{
					strand_topologies.Add(key, new List<string>());

				}
				strand_topologies[key].Add(reader.GetString(1).ToString());
			}
			command.Connection.Close();
			return strand_topologies;
		}

		public static List<string> listDivisionCodeAttributes()
		{
			List<string> division_codes = new List<string>();
			string enum_string = null;

			MySqlConnection conn = new MySqlConnection(connection_string);
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = ("SELECT column_type FROM information_schema.columns WHERE table_name = 'locus' AND column_name = 'divisioncode';");
			command.Connection = conn;
			MySqlDataReader reader = command.ExecuteReader();
			while(reader.Read()) enum_string = reader.GetString(0).ToString();
			Regex r = new Regex(@"'(.+?)'");
			MatchCollection matches = r.Matches(enum_string);
			foreach (Match m in matches)
			{
				division_codes.Add(m.Value);
			}
			return division_codes;
		}

		public static List<string> listMoleculeCodeAttributes()
		{
			List<string> molecule_codes = new List<string>();
			string enum_string = null;

			MySqlConnection conn = new MySqlConnection(connection_string);
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = ("SELECT column_type FROM information_schema.columns WHERE table_name = 'locus' AND column_name = 'molecule';");
			command.Connection = conn;
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read()) enum_string = reader.GetString(0).ToString();
			Regex r = new Regex(@"'(.+?)'");
			MatchCollection matches = r.Matches(enum_string);
			foreach (Match m in matches)
			{
				molecule_codes.Add(m.Value);
			}
			return molecule_codes;
		}

		public static List<string> listStrandCodeAttributes()
		{
			List<string> strand_codes = new List<string>();
			string enum_string = null;

			MySqlConnection conn = new MySqlConnection(connection_string);
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = ("SELECT column_type FROM information_schema.columns WHERE table_name = 'locus' AND column_name = 'strand';");
			command.Connection = conn;
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read()) enum_string = reader.GetString(0).ToString();
			Regex r = new Regex(@"'(.+?)'");
			MatchCollection matches = r.Matches(enum_string);
			foreach (Match m in matches)
			{
				strand_codes.Add(m.Value);
			}
			return strand_codes;
		}

		public static List<string> listStrandTopologyAttributes()
		{
			List<string> strand_topplogies = new List<string>();
			string enum_string = null;

			MySqlConnection conn = new MySqlConnection(connection_string);
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = ("SELECT column_type FROM information_schema.columns WHERE table_name = 'locus' AND column_name = 'strandtopology';");
			command.Connection = conn;
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read()) enum_string = reader.GetString(0).ToString();
			Regex r = new Regex(@"'(.+?)'");
			MatchCollection matches = r.Matches(enum_string);
			foreach (Match m in matches)
			{
				strand_topplogies.Add(m.Value);
			}
			return strand_topplogies;
		}

		public static string getProkaryote(string genbankID)
		{
			MySqlConnection conn = new MySqlConnection(connection_string);
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = ("SELECT p.common_name, s.genbank_ID FROM prokaryote AS p, sequence AS s WHERE p.id = s.prokaryote_id");
			command.Connection = conn;
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				if (reader.GetString(1).ToString() == genbankID)
					return reader.GetString(0).ToString();
			}
			return null;
		}

		public static string getProkaryoteClassLevel(string genbankID)
		{
			MySqlConnection conn = new MySqlConnection(connection_string);
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = ("SELECT p.class_level, s.genbank_ID FROM prokaryote AS p, sequence AS s WHERE p.id = s.prokaryote_id");
			command.Connection = conn;
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				if (reader.GetString(1).ToString() == genbankID)
					return reader.GetString(0).ToString();
			}
			return null;
		}

		public static List<string> getProjects(string genbankID)
		{
			List<string> projects = new List<string>();
			MySqlConnection conn = new MySqlConnection(connection_string);
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = ("SELECT d.value, s.genbank_ID FROM dblink AS d, sequence AS s WHERE d.prokaryote_id = s.prokaryote_id");
			command.Connection = conn;
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				if (reader.GetString(1).ToString() == genbankID)
					projects.Add(reader.GetString(0).ToString());
			}
			return projects;
		}

		public static List<string> getJournals(string genbankID)
		{
			List<string> journals = new List<string>();
			MySqlConnection conn = new MySqlConnection(connection_string);
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = ("SELECT r.journal, s.genbank_ID FROM reference AS r, sequence AS s WHERE r.prokaryote_id = s.prokaryote_id");
			command.Connection = conn;
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				if (reader.GetString(1).ToString() == genbankID)
					journals.Add(reader.GetString(0).ToString());
			}
			return journals;
		}

		public static List<string> getTitles(string genbankID)
		{
			List<string> titles = new List<string>();
			MySqlConnection conn = new MySqlConnection(connection_string);
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = ("SELECT r.title, s.genbank_ID FROM reference AS r, sequence AS s WHERE r.prokaryote_id = s.prokaryote_id");
			command.Connection = conn;
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				if (reader.GetString(1).ToString() == genbankID)
					titles.Add(reader.GetString(0).ToString());
			}
			return titles;
		}

		public static string getSequence(string genbankID)
		{
			MySqlConnection conn = new MySqlConnection(connection_string);
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = ("SELECT value, genbank_ID FROM sequence");
			command.Connection = conn;
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				if (reader.GetString(1).ToString() == genbankID)
					return reader.GetString(0).ToString();
			}
			return null;
		}

		public static List<string> getDefinitions(string genbankID)
		{
			List<string> definition = new List<string>();
			MySqlConnection conn = new MySqlConnection(connection_string);
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = ("SELECT m.definition, s.genbank_ID FROM metadata AS m, sequence AS s WHERE m.sequence_id = s.id");
			command.Connection = conn;
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				if (reader.GetString(1).ToString() == genbankID)
					definition.Add(reader.GetString(0).ToString());
			}
			return definition;
		}

		public static List<string> getAccessionVersion(string genbankID)
		{
			List<string> accession_version = new List<string>();
			MySqlConnection conn = new MySqlConnection(connection_string);
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = ("SELECT v.compound_accession, s.genbank_ID FROM metadata AS m, sequence AS s, version AS v WHERE m.sequence_id = s.id AND m.id = v.metadata_id");
			command.Connection = conn;
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				if (reader.GetString(1).ToString() == genbankID)
					accession_version.Add(reader.GetString(0).ToString());
			}
			return accession_version;
		}

		public static List<string> getGiNumber(string genbankID)
		{
			List<string> ginumber = new List<string>();
			MySqlConnection conn = new MySqlConnection(connection_string);
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = ("SELECT v.ginumber, s.genbank_ID FROM metadata AS m, sequence AS s, version AS v WHERE m.sequence_id = s.id AND m.id = v.metadata_id");
			command.Connection = conn;
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				if (reader.GetString(1).ToString() == genbankID)
					ginumber.Add(reader.GetString(0).ToString());
			}
			return ginumber;
		}

		public static string getMoleculeType(string genbankID)
		{
			MySqlConnection conn = new MySqlConnection(connection_string);
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = ("SELECT l.molecule, s.genbank_ID FROM metadata AS m, sequence AS s, locus AS l WHERE m.sequence_id = s.id AND m.id = l.metadata_id");
			command.Connection = conn;
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				if (reader.GetString(1).ToString() == genbankID)
					return reader.GetString(0).ToString();
			}
			return null;
		}

		public static string getStrandType(string genbankID)
		{
			MySqlConnection conn = new MySqlConnection(connection_string);
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = ("SELECT l.strand, s.genbank_ID FROM metadata AS m, sequence AS s, locus AS l WHERE m.sequence_id = s.id AND m.id = l.metadata_id");
			command.Connection = conn;
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				if (reader.GetString(1).ToString() == genbankID)
					return reader.GetString(0).ToString();
			}
			return null;
		}

		public static string getStrandTopology(string genbankID)
		{
			MySqlConnection conn = new MySqlConnection(connection_string);
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = ("SELECT l.strandtopology, s.genbank_ID FROM metadata AS m, sequence AS s, locus AS l WHERE m.sequence_id = s.id AND m.id = l.metadata_id");
			command.Connection = conn;
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				if (reader.GetString(1).ToString() == genbankID)
					return reader.GetString(0).ToString();
			}
			return null;
		}
		#endregion

		#region geneAttribute methods
		public static List<string> getGenes(string accession_version)
		{
			List<string> genes = new List<string>();
			MySqlConnection conn = new MySqlConnection(connection_string);
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = ("SELECT qv.qualifier_values, v.compound_accession  FROM metadata AS m, version AS v, feature AS f, qualifier AS q, qualifier_value AS qv WHERE m.id = v.metadata_id AND m.id = f.metadata_id AND f.id = q.feature_id AND q.id=qv.qualifier_id AND f.key_type='gene' AND q.qualifier_key='locus_tag'");
			command.Connection = conn;
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				if (reader.GetString(1).ToString() == accession_version)
					genes.Add(reader.GetString(0).ToString());
			}
			return genes;
		}

		public static List<string> getProteinIDs(string accession_version)
		{
			List<string> protein_ids = new List<string>();
			MySqlConnection conn = new MySqlConnection(connection_string);
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = ("SELECT qv.qualifier_values, v.compound_accession  FROM metadata AS m, version AS v, feature AS f, qualifier AS q, qualifier_value AS qv WHERE m.id = v.metadata_id AND m.id = f.metadata_id AND f.id = q.feature_id AND q.id=qv.qualifier_id AND q.qualifier_key='protein_id'");
			command.Connection = conn;
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				if (reader.GetString(1).ToString() == accession_version)
					protein_ids.Add(reader.GetString(0).ToString());
			}
			return protein_ids;
		}

		public static List<string> ListNucleotideSequenceLocations(string accession_version)
		{
			List<string> locations = new List<string>();
			MySqlConnection conn = new MySqlConnection(connection_string);
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = ("SELECT f.location_start, f.location_end, v.compound_accession FROM metadata AS m, feature AS f, version AS v WHERE m.id = f.metadata_id AND m.id = v.metadata_id AND f.key_type='gene'");
			command.Connection = conn;
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				if (reader.GetString(2).ToString() == accession_version)
				{
					if (reader.GetString(0).Length > 0 && reader.GetString(1).Length > 0)
					{
						string converted = "Start: " + reader.GetString(0) + "  End: " + reader.GetString(1);
						locations.Add(converted);
					}
				}
			}
			return locations;
		}

		public static string getNucleotideSequence(string accession_version, string start, string end)
		{
			MySqlConnection conn = new MySqlConnection(connection_string);
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = ("SELECT qv.qualifier_values, v.compound_accession, f.location_start, f.location_end  FROM metadata AS m, version AS v, feature AS f, qualifier AS q, qualifier_value AS qv WHERE m.id = v.metadata_id AND m.id = f.metadata_id AND f.id = q.feature_id AND q.id=qv.qualifier_id AND q.qualifier_key='translation'");
			command.Connection = conn;
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				if (reader.GetString(1).ToString() == accession_version && reader.GetString(2).ToString() == start && reader.GetString(3) == end)
					return reader.GetString(0).ToString();
			}
			return null;
		}
		#endregion
	}

}

