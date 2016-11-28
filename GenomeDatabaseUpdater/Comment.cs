using System;
using Bio.IO.GenBank;
using MySql.Data.MySqlClient;

namespace GenomeDatabaseUpdater
{
	public class Comment
	{
		private long id { get; }
		private long prokaryote_id {get;}
		private string value {get;}

		public Comment(string comment, long id)
		{
			if (id < 1)
			{
				UpdaterException.check_data_exception(true);
			}
			this.id = 0;
			this.value = comment;
			this.prokaryote_id = id;
		}

		public void execute_query(MySqlConnection connection)
		{
			string query = @"INSERT INTO prokaryote_schema.comment(id, prokaryote_id, value) VALUES(@id,@prokaryote_id,@value)";
			MySqlCommand cmd = connection.CreateCommand();
			cmd.CommandText = query;
			cmd.Parameters.AddWithValue("@id", this.id);
			cmd.Parameters.AddWithValue("@prokaryote_id", this.prokaryote_id);
			cmd.Parameters.AddWithValue("@value", this.value);
			cmd.ExecuteNonQuery();
		}
	}
}

