using System;
using System.Collections.Generic;
using System.Xml;
using GenomeDatabaseDatabaseLayer;

namespace GenomeDatabaseBusinessLayer
{
	public static class GDBL
	{
		#region Database content Querying methods

		/// <summary>
		/// This method returns the list of prokaryotes and its respective class levels.
		/// Throws an exception if any error accessing the database occurs.
		/// </summary>
		public static XmlDocument listProkaryotes()
		{
			XmlDocument doc = new XmlDocument();
			doc.AppendChild(dictionaryToXML(GDDL.listProkaryotes(), doc));
			return doc;
		}

		/// <summary>
		/// This method returns the list of prokaryotes and its respective project IDs.
		/// Throws an exception of any error accessing the database occurs.
		/// </summary>
		public static XmlDocument listProjects()
		{
			XmlDocument doc = new XmlDocument();
			doc.AppendChild(dictionaryToXML(GDDL.listDblinks(), doc));
			return doc;
		}

		/// <summary>
		/// This method returns the list of prokaryotes with its journal details.
		/// Throws an exception of any error accessing the database occurs.
		/// </summary>
		public static XmlDocument listJournals()
		{
			XmlDocument doc = new XmlDocument();
			doc.AppendChild(dictionaryToXML(GDDL.listJournals(),doc));
			return doc;
		}

		/// <summary>
		/// This method returns the list of prokaryotes with its title details.
		/// Throws an exception of any error accessing the database occurs.
		/// </summary>
		public static XmlDocument listTitles()
		{
			XmlDocument doc = new XmlDocument();
			doc.AppendChild(dictionaryToXML(GDDL.listTitles(), doc));
			return doc;
		}

		/// <summary>
		/// This mehtod returns the list of prokaryotes with its all the sequences including scaffolds.
		/// Throws an exception of any error accessing the database occurs.
		/// </summary>
		/// <returns>The sequences.</returns>
		public static XmlDocument listSequences()
		{
			XmlDocument doc = new XmlDocument();
			doc.AppendChild(dictionaryToXML(GDDL.listSequences(), doc));
			return doc;
		}

		/// <summary>
		/// This method returns the list of prokaryotes with its scaffolds genbankIDs.
		/// Throws an exception of any error accessing the database occurs.
		/// </summary>
		public static XmlDocument listGenbankIDs()
		{
			XmlDocument doc = new XmlDocument();
			doc.AppendChild(dictionaryToXML(GDDL.listGenbankIDs(),doc));
			return doc;
		}

		/// <summary>
		/// This method returns the list of genbankID with its definition.
		/// Throws an exception of any error accessing the database occurs.
		/// </summary>
		public static XmlDocument listDefinitions()
		{
			XmlDocument doc = new XmlDocument();
			doc.AppendChild(dictionaryToXML(GDDL.listDefinitions(), doc));
			return doc;
		}

		/// <summary>
		/// This method returns the list of genbankID with its corresponding ginumbers.
		/// Throws an exception of any error accessing the database occurs.
		/// </summary>
		public static XmlDocument listGiNumbers()
		{
			XmlDocument doc = new XmlDocument();
			doc.AppendChild(dictionaryToXML(GDDL.listGiNumbers(), doc));
			return doc;
		}

		/// <summary>
		/// This method returns the list of genbankID with its corresponding accession.verison.
		/// Throws an exception of any error accessing the database occurs.
		/// </summary>
		public static XmlDocument listAccessionVersions()
		{
			XmlDocument doc = new XmlDocument();
			doc.AppendChild(dictionaryToXML(GDDL.listAccessionVersions(),doc));
			return doc;
		}

		/// <summary>
		/// This method returns the list of genbankID with its corresponding divisioncode.
		/// Throws an exception of any error accession the database occurs.
		/// </summary>
		public static XmlDocument listDivisionCodes()
		{
			XmlDocument doc = new XmlDocument();
			doc.AppendChild(dictionaryToXML(GDDL.listDivisionCodes(), doc));
			return doc;
		}

		/// <summary>
		/// This method returns the list of genbankID with its corresponding molecule types.
		/// Throws an exception of any error accessing the database occurs.
		/// </summary>
		public static XmlDocument listMoleculeTypes()
		{
			XmlDocument doc = new XmlDocument();
			doc.AppendChild(dictionaryToXML(GDDL.listMoleculeTypes(), doc));
			return doc;
		}

		/// <summary>
		/// This method returns the list of genbankID with its corresponding molecule stand topology.
		/// Throws an exception of any error accessing the database occurs.
		/// </summary>
		public static XmlDocument listStrandTopologies()
		{
			XmlDocument doc = new XmlDocument();
			doc.AppendChild(dictionaryToXML(GDDL.listStrandTopologies(), doc));
			return doc;
		}

		/// <summary>
		/// This method returns the Lists of the division code attributes.
		/// Throws an exception of any error accesing the database occurs.
		/// </summary>
		public static List<string> listDivisionCodeAttributes()
		{
			return GDDL.listDivisionCodeAttributes();
		}

		/// <summary>
		/// This method returns the Lists of the molecule code attributes.
		/// Throws an exception of any error accessing the datbase occurs.
		/// </summary>
		public static List<string> listMoleculeCodeAttributes()
		{
			return GDDL.listMoleculeCodeAttributes();
		}

		/// <summary>
		/// This method returns the Lists of the strand code attributes.
		/// Throws an exception of any error accessing the datbase occurs.
		/// </summary>
		public static List<string> listStrandCodeAttributes()
		{
			return GDDL.listStrandCodeAttributes();
		}

		/// <summary>
		/// This method returns the Lists of the strand topology attributes.
		/// Throws an exception of any error accessing the datbase occurs.
		/// </summary>
		public static List<string> listStrandTopologyAttributes()
		{
			return GDDL.listStrandTopologyAttributes();
		}
		#endregion

		#region specific information Querying methods
		/// <summary>
		/// This method returns the name of prokaryote of provided genBankID.
		/// Throws an exception of any error accessing the database occurs.
		/// </summary>
		/// <param name="genbankID">Genbank identifier.</param>
		public static string getProkaryote(string genbankID)
		{
			string prokaryote = GDDL.getProkaryote(genbankID);
			if (prokaryote == null)
				throw new Exception("No match found");
			return prokaryote;
		}

		public static string getProkaryoteClassLevel(string genbankID)
		{
			string prokaryote_class_level = GDDL.getProkaryoteClassLevel(genbankID);
			if (prokaryote_class_level == null)
				throw new Exception("No match found");
			return prokaryote_class_level;
		}

		public static List<string> getProjects(string genbankID)
		{
			List<string> project = GDDL.getProjects(genbankID);
			if (project.Count < 1)
				throw new Exception("No match found");
			return project;
		}


		public static List<string> getJournals(string genbankID)
		{
			List<string> journal = GDDL.getJournals(genbankID);
			if (journal.Count < 1)
				throw new Exception("No match found");
			return journal;
		}

		public static List<string> getTitles(string genbankID)
		{
			List<string> title = GDDL.getTitles(genbankID);
			if (title.Count < 1)
				throw new Exception("No match found");
			return title;
		}

		public static string getSequence(string genbankID)
		{
			string sequence = GDDL.getSequence(genbankID);
			if (sequence == null)
				throw new Exception("No match found");
			return sequence;
		}

		public static List<string> getDefinitions(string genbankID)
		{
			List<string> definition = GDDL.getDefinitions(genbankID);
			if (definition.Count < 1)
				throw new Exception("No match found");
			return definition;
		}

		public static List<string> getAccessionVersion(string genbankID)
		{
			List<string> accession_version = GDDL.getAccessionVersion(genbankID);
			if (accession_version.Count < 1)
				throw new Exception("No match found");
			return accession_version;
		}

		public static List<string> getGiNumber(string genbankID)
		{
			List<string> ginumber = GDDL.getGiNumber(genbankID);
			if (ginumber.Count < 1)
				throw new Exception("No match found");
			return ginumber;
		}

		public static string getMoleculeType(string genbankID)
		{
			string molecule = GDDL.getMoleculeType(genbankID);
			if (molecule == null)
				throw new Exception("No match found");
			return molecule;
		}

		public static string getStrandType(string genbankID)
		{
			string strand = GDDL.getStrandType(genbankID);
			if (strand == null)
				throw new Exception("No match found");
			return strand;
		}

		public static string getStrandTopology(string genbankID)
		{
			string strand_topology = GDDL.getStrandTopology(genbankID);
			if (strand_topology == null)
				throw new Exception("No match found");
			return strand_topology;
		}

		public static List<string> getGenes(string accession_version)
		{
			List<string> genes = GDDL.getGenes(accession_version);
			if (genes.Count < 1)
				throw new Exception("No match found");
			return genes;
		}

		public static List<string> getProteinIDs(string accession_version)
		{
			List<string> protein_ids = GDDL.getProteinIDs(accession_version);
			if (protein_ids.Count < 1)
				throw new Exception("No match found");
			return protein_ids;
		}
		public static List<string> ListNucleotideLocations(string accession_version)
		{
			List<string> nucleotide_locations = GDDL.ListNucleotideSequenceLocations(accession_version);
			if (nucleotide_locations.Count < 1)
				throw new Exception("No match found");
			return nucleotide_locations;
		}

		public static string getNucleotideSequence(string accession_version, string start, string end)
		{
			string nucleotide_sequence = GDDL.getNucleotideSequence(accession_version,start,end);
			if (nucleotide_sequence == null)
				throw new Exception("No match found");
			return nucleotide_sequence;
		}
		#endregion

		#region helper functions
		private static XmlElement dictionaryToXML(Dictionary<string, string> dict, XmlDocument doc)
		{
			XmlElement dictionary = doc.CreateElement("Dictionary");
			dictionary.Attributes.Append(doc.CreateAttribute("KeyType"));
			dictionary.Attributes.Append(doc.CreateAttribute("ValueType"));
			dictionary.Attributes["KeyType"].Value = "string";
			dictionary.Attributes["ValueType"].Value = "string";

			foreach (var k in dict.Keys)
			{
				XmlElement entry = doc.CreateElement("Entry");
				XmlElement key = doc.CreateElement("Key");
				XmlElement val = doc.CreateElement("Value");
				key.InnerText = k;
				val.InnerText = dict[k];
				entry.AppendChild(key);
				entry.AppendChild(val);
				dictionary.AppendChild(entry);
			}
			return dictionary;
		}

		public static XmlElement dictionaryToXML(Dictionary<string, List<string>> dict, XmlDocument doc)
		{
			XmlElement dictionary = doc.CreateElement("Dictionary");
			dictionary.Attributes.Append(doc.CreateAttribute("KeyType"));
			dictionary.Attributes.Append(doc.CreateAttribute("ValueType"));
			dictionary.Attributes["KeyType"].Value = "string";
			dictionary.Attributes["ValueType"].Value = "string";

			foreach (String k in dict.Keys)
			{
				XmlElement entry = doc.CreateElement("Entry");
				XmlElement key = doc.CreateElement("Key");
				key.InnerText = k;
				entry.AppendChild(key);
				foreach (String v in dict[k])
				{
					XmlElement val = doc.CreateElement("Value");
					val.InnerText = v;
					entry.AppendChild(val);
				}
				dictionary.AppendChild(entry);
			}
			return dictionary;
		}
		#endregion
	}
}

