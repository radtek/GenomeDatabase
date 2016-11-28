using System;
using System.Collections.Generic;
using System.Linq;
using Bio;
using MySql.Data.MySqlClient;

namespace GenomeDatabaseUpdater
{
	public class Sequence
	{
		private long id { get; }
		private long prokaryote_id { get; }
		private string value { get; }
		private long count { get; }
		private string genbank_ID;


		public Sequence(ISequence seq, long id)
		{
			if (id < 1 || seq.ID.Count() > 40)
			{
				UpdaterException.check_data_exception(true);
			}
			this.id = 0;
			this.prokaryote_id = id;
			this.value = seq.ToString();
			this.count = seq.Count;
			this.genbank_ID = seq.ID;
		}


		public long execute_query(MySqlConnection connection)
		{
			string query = @"INSERT INTO prokaryote_schema.sequence(id, prokaryote_id, value, count, genbank_ID) VALUES(@id,@prokaryote_id,@value,@count,@genbank_ID)";
			MySqlCommand cmd = connection.CreateCommand();
			cmd.CommandText = query;
			cmd.Parameters.AddWithValue("@id", this.id);
			cmd.Parameters.AddWithValue("@prokaryote_id", this.prokaryote_id);
			cmd.Parameters.AddWithValue("@value", this.value);
			cmd.Parameters.AddWithValue("@count", this.count);
			cmd.Parameters.AddWithValue("@genbank_ID", this.genbank_ID);
			cmd.ExecuteNonQuery();
			return cmd.LastInsertedId;
		}
	}
}

