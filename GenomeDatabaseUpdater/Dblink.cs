using System;
using Bio.IO.GenBank;
using MySql.Data.MySqlClient;

namespace GenomeDatabaseUpdater
{
	public class Dblink
	{
		private long id { get; }
		private long prokaryote_id { get; }
		private string value { get; }
		private int type { get;}

		public Dblink(CrossReferenceLink dblink, long id)
		{
			this.type = (int)dblink.Type + 1;

			if (id < 1 || this.type < 1 || this.type > 4) 
			{
				UpdaterException.check_data_exception(true);
			}
			this.id = 0;

			foreach (var s in dblink.Numbers)
			{
				this.value += s + "\t";
			}

			this.prokaryote_id = id;
		}

		public void execute_query(MySqlConnection connection)
		{
			string query = @"INSERT INTO prokaryote_schema.dblink(id, prokaryote_id, value, type) VALUES(@id,@prokaryote_id,@value, @type)";
			MySqlCommand cmd = connection.CreateCommand();
			cmd.CommandText = query;
			cmd.Parameters.AddWithValue("@id", this.id);
			cmd.Parameters.AddWithValue("@prokaryote_id", this.prokaryote_id);
			cmd.Parameters.AddWithValue("@value", this.value);
			cmd.Parameters.AddWithValue("@type", this.type);
			cmd.ExecuteNonQuery();
		}
	}
}

