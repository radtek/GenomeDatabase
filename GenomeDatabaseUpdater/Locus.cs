using System;
using Bio.IO.GenBank;
using MySql.Data.MySqlClient;

namespace GenomeDatabaseUpdater
{
	public class Locus
	{
		private long id { get; }
		private long metadata_id { get; }
		private string date { get; }
		private int divisoncode { get; }
		private int molecule { get; }
		private int strand { get; }
		private string strand_type { get; }
		private int strandtopology { get; }

		public Locus(GenBankLocusInfo locus, long id)
		{
			this.divisoncode = (int)locus.DivisionCode + 1;
			this.molecule = (int)locus.MoleculeType + 1;
			this.strand = (int)locus.Strand + 1;
			this.strandtopology = (int)locus.StrandTopology + 1;

			if (id < 1 || this.divisoncode > 20 || this.divisoncode < 1 || this.molecule > 10 || this.molecule < 1 || this.strand > 4 || this.strand < 1 || this.strandtopology > 3 || this.strandtopology < 1 || locus.SequenceType.Length > 40)
			{
				UpdaterException.check_data_exception(true);
			}

			this.id = 0;
			this.metadata_id = id;
			this.date = locus.Date.ToString("yyyy-MM-dd HH:mm:ss");
			this.strand_type = locus.SequenceType;
		}

		public void execute_query(MySqlConnection connection)
		{
			string query = @"INSERT INTO prokaryote_schema.locus(id, metadata_id, date, divisioncode, molecule, strand, strand_type, strandtopology) VALUES(@id,@metadata_id,@date,@divisioncode,@molecule,@strand,@strand_type,@strandtopology)";
			MySqlCommand cmd = connection.CreateCommand();
			cmd.CommandText = query;
			cmd.Parameters.AddWithValue("@id", this.id);
			cmd.Parameters.AddWithValue("@metadata_id", this.metadata_id);
			cmd.Parameters.AddWithValue("@date", this.date);
			cmd.Parameters.AddWithValue("@divisioncode", this.divisoncode);
			cmd.Parameters.AddWithValue("@molecule", this.molecule);
			cmd.Parameters.AddWithValue("@strand", this.strand);
			cmd.Parameters.AddWithValue("@strand_type", this.strand_type);
			cmd.Parameters.AddWithValue("@strandtopology", this.strandtopology);
			cmd.ExecuteNonQuery();
		}
	}
}

