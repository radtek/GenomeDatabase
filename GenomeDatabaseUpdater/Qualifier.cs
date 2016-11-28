using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace GenomeDatabaseUpdater
{
	public class Qualifier
	{
		private long id { get; }
		private long feature_id { get; }
		private string qualifier_key { get; }

		public Qualifier(string K, long id)
		{
			if (id < 1)
			{
				UpdaterException.check_data_exception(true);
			}
			this.id = 0;
			this.feature_id = id;
			this.qualifier_key = K;
		}


		public long execute_query(MySqlConnection connection)
		{
			string query = @"INSERT INTO prokaryote_schema.qualifier(id, feature_id, qualifier_key) VALUES(@id,@feature_id,@qualifier_key)";
			MySqlCommand cmd = connection.CreateCommand();
			cmd.CommandText = query;
			cmd.Parameters.AddWithValue("@id", this.id);
			cmd.Parameters.AddWithValue("@feature_id", this.feature_id);
			cmd.Parameters.AddWithValue("@qualifier_key", this.qualifier_key);
			cmd.ExecuteNonQuery();
			return cmd.LastInsertedId;
		}
	}
}

