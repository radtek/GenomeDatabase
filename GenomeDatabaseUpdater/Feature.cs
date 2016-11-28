using System;
using Bio.IO.GenBank;

namespace GenomeDatabaseUpdater
{
	public class Feature
	{
		private long id {get;}
		private long metadata_id { get; }
		private string key_type { get; }
		private string label { get; }
		private string location_accession { get; }
		private string location_end { get; }
		private string location_start { get; }
		private int location_operator { get; }
		private string location_separator { get; }
		//private int sublocation { get; }
		//private int location_resolver { get; }

		public Feature(FeatureItem feature, long id)
		{

			if (feature == null || id < 1)
			{
				UpdaterException.check_data_exception(true);
			}
			this.location_operator = (int)feature.Location.Operator + 1;
			this.location_separator = feature.Location.Separator;
			this.id = 0;
			this.metadata_id = id;
			this.key_type = feature.Key;
			this.label = feature.Label;
			this.location_accession = feature.Location.Accession;
			this.location_end = feature.Location.EndData;
			this.location_start = feature.Location.StartData;


		}

		public string get_query()
		{
			string query = "INSERT INTO prokaryote_schema.feature(id, metadata_id, key_type, label, location_accession, location_end, location_start, location_operator, location_separator, sublocation, location_resolver)" +
				"VALUES('" + this.id + "','" + this.metadata_id + "','" + this.key_type + "','" + this.label + "','" + this.location_accession + "','" + this.location_end + "','" + this.location_start + "','" + this.location_operator + "','" + this.location_separator + "','" + 0 + "','" + 0 + "')";
			return query;
		}
	}
}
