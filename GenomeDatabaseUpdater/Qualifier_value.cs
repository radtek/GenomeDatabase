using System;
using MySql.Data.MySqlClient;

namespace GenomeDatabaseUpdater
{
	public class Qualifier_value
	{
		private long id { get; }
		private long qualifier_id { get; }
		private string qualifier_values { get; }

		public Qualifier_value(string V, long id)
		{
			if (id < 1)
			{
				UpdaterException.check_data_exception(true);
			}
			this.id = 0;
			this.qualifier_id = id;
			this.qualifier_values = V;
		}

		public void execute_query(MySqlConnection connection)
		{
			string query = @"INSERT INTO prokaryote_schema.qualifier_value(id, qualifier_id, qualifier_values) VALUES(@id,@qualifier_id,@qualifier_values)";
			MySqlCommand cmd = connection.CreateCommand();
			cmd.CommandText = query;
			cmd.Parameters.AddWithValue("@id", this.id);
			cmd.Parameters.AddWithValue("@qualifier_id", this.qualifier_id);
			cmd.Parameters.AddWithValue("@qualifier_values", this.qualifier_values);
			cmd.ExecuteNonQuery();
		}
	}
}

