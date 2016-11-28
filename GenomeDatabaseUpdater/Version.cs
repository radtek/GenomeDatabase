using System;
using Bio.IO.GenBank;
using MySql.Data.MySqlClient;

namespace GenomeDatabaseUpdater
{
	public class Version
	{
		private long id { get; }
		private long metadata_id { get; }
		private string accession { get; }
		private string compound_accession { get; }
		private string ginumber { get; }
		private string version { get; }

		public Version(GenBankVersion ver, long id)
		{
			if (id < 1 || ver.Accession.Length > 40 || ver.GiNumber.Length > 40 || ver.Version.Length > 2)
			{
				UpdaterException.check_data_exception(true);
			}
			this.id = 0;
			this.metadata_id = id;
			this.accession = ver.Accession;
			this.compound_accession = ver.CompoundAccession;
			this.ginumber = ver.GiNumber;
			this.version = ver.Version;

		}


		public void execute_query(MySqlConnection connection)
		{
			string query = @"INSERT INTO prokaryote_schema.version(id, metadata_id, accession, compound_accession, ginumber, version) VALUES(@id,@metadata_id,@accession,@compound_accession,@ginumber,@version)";
			MySqlCommand cmd = connection.CreateCommand();
			cmd.CommandText = query;
			cmd.Parameters.AddWithValue("@id", this.id);
			cmd.Parameters.AddWithValue("@metadata_id", this.metadata_id);
			cmd.Parameters.AddWithValue("@accession", this.accession);
			cmd.Parameters.AddWithValue("@compound_accession", this.compound_accession);
			cmd.Parameters.AddWithValue("@ginumber", this.ginumber);
			cmd.Parameters.AddWithValue("@version", this.version);
			cmd.ExecuteNonQuery();
		}
	}
}

