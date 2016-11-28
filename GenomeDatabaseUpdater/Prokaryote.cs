using System;
using Bio.IO.GenBank;
using MySql.Data.MySqlClient;

namespace GenomeDatabaseUpdater
{
	public class Prokaryote
	{
		private long id {get;}
		private string common_name { get; }
		private string class_level { get; }
		private string genus { get; }
		private string species { get; }

		public Prokaryote(SequenceSource source)
		{
			this.id = 0;
			this.common_name = source.CommonName;
			this.class_level = source.Organism.ClassLevels;
			this.genus = source.Organism.Genus;
			this.species = source.Organism.Species;
		}

		public long execute_query(MySqlConnection connection)
		{
			string query = @"INSERT INTO prokaryote_schema.prokaryote(id, common_name, class_level, genus, species) VALUES(@id,@common_name,@class_level,@genus,@species)";
			MySqlCommand cmd = connection.CreateCommand();
			cmd.CommandText = query;
			cmd.Parameters.AddWithValue("@id", this.id);
			cmd.Parameters.AddWithValue("@common_name", this.common_name);
			cmd.Parameters.AddWithValue("@class_level", this.class_level);
			cmd.Parameters.AddWithValue("@genus",this.genus);
			cmd.Parameters.AddWithValue("@species", this.species);
			cmd.ExecuteNonQuery();
			return cmd.LastInsertedId;
		}
	}
}

