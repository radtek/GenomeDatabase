using System;
using Bio;
using MySql.Data.MySqlClient;

namespace GenomeDatabaseUpdater
{
	public class Alphabet
	{
		public long id { set; get; }
		private int A { set; get; }
		private int C { set; get; }
		private int G { set; get; }
		private int T { set; get; }
		private int Gap { set; get; }
		private int AC { set; get; }
		private int ACT { set; get; }
		private int AT { set; get; }
		private int GA { set; get; }
		private int GAT { set; get; }
		private int GC { set; get; }
		private int GCA { set; get; }
		private int GT { set; get; }
		private int GTC { set; get; }
		private int TC { set; get; }
		private int Any { set; get; }
		private int has_ambiguity { get; }
		private int has_gaps { get; }
		private int has_termination { get; }
		private int is_complement_supported { get; }
		private string dna_name { get; }
		private int alphabet_type_count { get; }
		private long sequence_id { get; }

		//constructor
		public Alphabet(IAlphabet alphabet, long id)
		{
			if (id < 1)
			{
				UpdaterException.check_data_exception(true);
			}
			this.id = 0;
			this.sequence_id = id;
			this.A = 0;
			this.C = 0;
			this.G = 0;
			this.T = 0;
			this.Gap = 0;
			this.ACT = 0;
			this.AT = 0;
			this.GA = 0;
			this.GAT = 0;
			this.GC = 0;
			this.GCA = 0;
			this.GT = 0;
			this.GTC = 0;
			this.TC = 0;
			this.Any = 0;
			this.has_ambiguity = (alphabet.HasAmbiguity)? 1:0;
			this.has_gaps = (alphabet.HasGaps)? 1:0;
			this.has_termination = (alphabet.HasTerminations)? 1:0;
			this.is_complement_supported = (alphabet.IsComplementSupported)? 1:0;
			this.dna_name = alphabet.Name;
			this.alphabet_type_count = alphabet.Count;
			extract_alphabet_IDs(alphabet);
		}


		#region data extractor functions 
		private void extract_alphabet_IDs(IAlphabet a)
		{
			int index = 0;
			var enumerator = a.GetEnumerator();
			while (enumerator.MoveNext())
			{
				int value = enumerator.Current;
				switch (index)
				{
					case 0:
						this.A = value;
						break;
					case 1:
						this.C = value;
						break;
					case 2:
						this.G = value;
						break;
					case 3:
						this.T = value;
						break;
					case 4:
						this.Gap = value;
						break;
					case 5:
						this.AC = value;
						break;
					case 6:
						this.GA = value;
						break;
					case 7:
						this.GC = value;
						break;
					case 8:
						this.AT = value;
						break;
					case 9:
						this.TC = value;
						break;
					case 10:
						this.GT = value;
						break;
					case 11:
						this.GCA = value;
						break;
					case 12:
						this.ACT = value;
						break;
					case 13:
						this.GAT = value;
						break;
					case 14:
						this.GTC = value;
						break;
					case 15:
						this.Any = value;
						break;
					default:
						break;
				}
				index++;
			}
		}
		#endregion

		//this function returns the query to insert the data in table 'alphabet'
		public void execute_query(MySqlConnection connection)
		{
			string query = @"INSERT INTO prokaryote_schema.alphabet(id, sequence_id, A, C, G, T, Gap, AC, ACT, AT, Any, GA, GAT, GC, GCA, GT, GTC, TC, has_ambiguity, has_gaps, has_termination, is_complement_supported, dna_name, alphabet_type_count) VALUES(@id,@sequence_id,@A,@C,@G,@T,@Gap,@AC,@ACT,@AT,@Any,@GA,@GAT,@GC,@GCA,@GT,@GTC,@TC,@has_ambiguity,@has_gaps,@has_termination,@is_complement_supported,@dna_name,@alphabet_type_count)";
			MySqlCommand cmd = connection.CreateCommand();
			cmd.CommandText = query;
			cmd.Parameters.AddWithValue("@id", this.id);
			cmd.Parameters.AddWithValue("@sequence_id", this.sequence_id);
			cmd.Parameters.AddWithValue("@A", this.A);
			cmd.Parameters.AddWithValue("@C", this.C);
			cmd.Parameters.AddWithValue("@G", this.G);
			cmd.Parameters.AddWithValue("@T", this.T);
			cmd.Parameters.AddWithValue("@Gap", this.Gap);
			cmd.Parameters.AddWithValue("@AC", this.AC);
			cmd.Parameters.AddWithValue("@ACT", this.ACT);
			cmd.Parameters.AddWithValue("@AT", this.AT);
			cmd.Parameters.AddWithValue("@Any", this.Any);
			cmd.Parameters.AddWithValue("@GA", this.GA);
			cmd.Parameters.AddWithValue("@GAT", this.GAT);
			cmd.Parameters.AddWithValue("@GC", this.GC);
			cmd.Parameters.AddWithValue("@GCA", this.GCA);
			cmd.Parameters.AddWithValue("@GT", this.GT);
			cmd.Parameters.AddWithValue("@GTC", this.GTC);
			cmd.Parameters.AddWithValue("@TC", this.TC);
			cmd.Parameters.AddWithValue("@has_ambiguity",this.has_ambiguity);
			cmd.Parameters.AddWithValue("@has_gaps",this.has_gaps);
			cmd.Parameters.AddWithValue("@has_termination",this.has_termination);
			cmd.Parameters.AddWithValue("@is_complement_supported",this.is_complement_supported);
			cmd.Parameters.AddWithValue("@dna_name",this.dna_name);
			cmd.Parameters.AddWithValue("@alphabet_type_count",this.alphabet_type_count);
			cmd.ExecuteNonQuery();
		}
	}
}

