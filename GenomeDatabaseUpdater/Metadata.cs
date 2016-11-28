using System;
using System.Collections.Generic;
using Bio.IO.GenBank;
using MySql.Data.MySqlClient;

namespace GenomeDatabaseUpdater
{
	public class Metadata
	{
		private long id { get; }
		private long sequence_id { get; }
		private string primary_accession { get; }
		private string secondary_accession { get; }
		private string contig { get; }
		private string definition { get; }
		private string keywords { get; }
		private string origin { get; }
		private string prima { get; }
		//private int project { get; }
		//private int segment { get; }
		private string basecount { get; }
		private string dbsource { get; }

		public Metadata(GenBankMetadata metadata, long id)
		{
			this.id = 0;

			if (id < 1)
			{
				UpdaterException.check_data_exception(true);
			}

			this.sequence_id = id;
			this.primary_accession = metadata.Accession.Primary;

			foreach (var sec in metadata.Accession.Secondary)
			{
				this.secondary_accession += " " + sec;
			}

			this.contig = metadata.Contig;
			this.definition = metadata.Definition;
			this.keywords = metadata.Keywords;
			this.prima = metadata.Primary;
			this.origin = metadata.Origin;
			this.basecount = metadata.BaseCount;
			this.dbsource = metadata.DbSource;

		}

		public long execute_query(MySqlConnection connection)
		{
			string query = @"INSERT INTO prokaryote_schema.metadata(id, sequence_id, primary_accession, secondary_accession, contig, definition, keywords, origin, prima, project, segment, basecount, dbsource) VALUES(@id,@sequence_id,@primary_accession,@secondary_accession,@contig,@definition,@keywords,@origin,@prima,@project,@segment,@basecount,@dbsource)";
			MySqlCommand cmd = connection.CreateCommand();
			cmd.CommandText = query;
			cmd.Parameters.AddWithValue("@id", this.id);
			cmd.Parameters.AddWithValue("@sequence_id", this.sequence_id);
			cmd.Parameters.AddWithValue("@primary_accession", this.primary_accession);
			cmd.Parameters.AddWithValue("@secondary_accession", this.secondary_accession);
			cmd.Parameters.AddWithValue("@contig", this.contig);
			cmd.Parameters.AddWithValue("@definition", this.definition);
			cmd.Parameters.AddWithValue("@keywords", this.keywords);
			cmd.Parameters.AddWithValue("@origin", this.origin);
			cmd.Parameters.AddWithValue("@prima", this.prima);
			cmd.Parameters.AddWithValue("@project", null);
			cmd.Parameters.AddWithValue("@segment", null);
			cmd.Parameters.AddWithValue("@basecount", this.basecount);
			cmd.Parameters.AddWithValue("@dbsource", this.dbsource);
			cmd.ExecuteNonQuery();
			return cmd.LastInsertedId;
		}
	}
}
