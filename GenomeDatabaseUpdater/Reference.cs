using System;
using Bio.IO.GenBank;
using MySql.Data.MySqlClient;

namespace GenomeDatabaseUpdater
{
	public class Reference
	{
		private long id { get; }
		private long prokaryote_id { get; }
		private string authors { get; }
		private string consortium { get; }
		private string journal { get; }
		private string location { get; }
		private string medline { get; }
		private string pub_med { get; }
		private string remark { get; }
		private string title { get; }

		public Reference(CitationReference reference, long id)
		{
			if (id < 1)
			{
				UpdaterException.check_data_exception(true);
			}
			this.id = 0;
			this.prokaryote_id = id;
			this.authors = reference.Authors;
			this.consortium = reference.Consortiums;
			this.journal = reference.Journal;
			this.location = reference.Location;
			this.medline = reference.Medline;
			this.pub_med = reference.PubMed;
			this.remark = reference.Remarks;
			this.title = reference.Title;
		}

		public void execute_query(MySqlConnection connection)
		{
			string query = @"INSERT INTO prokaryote_schema.reference(id, prokaryote_id, authors, consortium, journal, location, medline, pub_med, remark, title) VALUES(@id,@prokaryote_id,@authors,@consortium,@journal,@location,@medline,@pub_med,@remark,@title)";
			MySqlCommand cmd = connection.CreateCommand();
			cmd.CommandText = query;
			cmd.Parameters.AddWithValue("@id", this.id);
			cmd.Parameters.AddWithValue("@prokaryote_id", this.prokaryote_id);
			cmd.Parameters.AddWithValue("@authors", this.authors);
			cmd.Parameters.AddWithValue("@consortium", this.consortium);
			cmd.Parameters.AddWithValue("@journal", this.journal);
			cmd.Parameters.AddWithValue("@location", this.location);
			cmd.Parameters.AddWithValue("@medline", this.medline);
			cmd.Parameters.AddWithValue("@pub_med", this.pub_med);
			cmd.Parameters.AddWithValue("@remark", this.remark);
			cmd.Parameters.AddWithValue("@title", this.title);
			cmd.ExecuteNonQuery();
		}
	}
}

