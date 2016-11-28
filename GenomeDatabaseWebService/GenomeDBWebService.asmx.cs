using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Xml.Linq;
using GenomeDatabaseBusinessLayer;

[WebService(Namespace = "http://mquter.qut.edu.au/GenomeDBWebService")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class GenomeDBWebService : System.Web.Services.WebService
{
	#region Datbase content querying methods
	[WebMethod(Description = "Gets a list of prokaryotes and their respective class levels. Convert to Dictionary&ltString,String&gt")]
	public XmlDocument listProkaryotes()
	{
		return GDBL.listProkaryotes();
	}

	[WebMethod(Description = "Gets a list of prokaryotes and their project IDs. Convert to Dictionary&ltString,List&ltString&gt&gt")]
	public XmlDocument listPojects()
	{
		return GDBL.listProjects();
	}

	[WebMethod(Description = "Gets a list of prokaryotes and their journal details. Convert to Dictionary&ltString,List&ltString&gt&gt")]
	public XmlDocument listJournals()
	{
		return GDBL.listJournals();
	}

	[WebMethod(Description = "Gets a list of prokaryotes and their title details. Convert to Dictionary&ltString,List&ltString&gt&gt")]
	public XmlDocument listTitles()
	{
		return GDBL.listTitles();
	}

	[WebMethod(Description = "Gets a list of prokaryotes and their sequences. Convert to Dictionary&ltString,List&ltString&gt&gt")]
	public XmlDocument listSequences()
	{
		return GDBL.listSequences();
	}

	[WebMethod(Description = "Gets a list of prokaryotes and their genbankIDs including scaffolds. Convert to Dictionary&ltString,List&ltString&gt&gt")]
	public XmlDocument listGenbankIDs()
	{
		return GDBL.listGenbankIDs();
	}

	[WebMethod(Description = "Gets a list of genbankIDs with their definition. Convert to Dictionary&ltString,List&ltString&gt&gt")]
	public XmlDocument listDefinitions()
	{
		return GDBL.listDefinitions();
	}

	[WebMethod(Description = "Gets a list of genbankIDs with their ginumber. Convert to Dictionary&ltString,List&ltString&gt&gt")]
	public XmlDocument listGiNumbers()
	{
		return GDBL.listGiNumbers();
	}

	[WebMethod(Description = "Gets a list of genbankIDs with their accession.version. Convert to Dictionary&ltString,List&ltString&gt&gt")]
	public XmlDocument listAccessionVersions()
	{
		return GDBL.listAccessionVersions();
	}

	[WebMethod(Description = "Gets a list of genbankIDs with their division code. Convert to Dictionary&ltString,List&ltString&gt&gt")]
	public XmlDocument listDivisionCodes()
	{
		return GDBL.listDivisionCodes();
	}

	[WebMethod(Description = "Gets a list of genbankIDs with their molecule types. Convert to Dictionary&ltString,List&ltString&gt&gt")]
	public XmlDocument listMoleculeTypes()
	{
		return GDBL.listMoleculeTypes();
	}

	[WebMethod(Description = "Gets a list of genbankIDs with their strand topologies. Convert to Dictionary&ltString,List&ltString&gt&gt")]
	public XmlDocument listStrandTopologies()
	{
		return GDBL.listStrandTopologies();
	}

	[WebMethod(Description = "Gets a list of the Division Code Type that at least one sequence has. Convert to List&ltString&gt")]
	public List<string> listDivisionCodeAttributes()
	{
		return GDBL.listDivisionCodeAttributes();
	}

	[WebMethod(Description = "Gets a list of the Molecule Type that at least one sequence has. Convert to List&ltString&gt")]
	public List<string> listMoleculeCodeAttributes()
	{
		return GDBL.listMoleculeCodeAttributes();
	}

	[WebMethod(Description = "Gets a list of the Strand Type that at least one sequence has. Convert to List&ltString&gt")]
	public List<string> listStrandCodeAttributes()
	{
		return GDBL.listStrandCodeAttributes();
	}

	[WebMethod(Description = "Gets a list of the Strand Topology that at least one sequence has. Convert to List&ltString&gt")]
	public List<string> listStrandTopologyAttributes()
	{
		return GDBL.listStrandTopologyAttributes();
	}

	[WebMethod(Description = "Gets prokaryote name of given genbankID. Convert to String")]
	public string getProkaryote(string genBankID)
	{
		return GDBL.getProkaryote(genBankID);
	}

	[WebMethod(Description = "Gets prokaryote class level of given genbankID. Convert to String")]
	public string getProkaryoteClassLevel(string genBankID)
	{
		return GDBL.getProkaryoteClassLevel(genBankID);
	}

	[WebMethod(Description = "Gets the list of projectIDs of given genbankID. Convert to List&ltString&gt")]
	public List<string> getProjects(string genBankID)
	{
		return GDBL.getProjects(genBankID);
	}

	[WebMethod(Description = "Gets the list of journals of given genbankID. Convert to List&ltString&gt")]
	public List<string> getJournals(string genBankID)
	{
		return GDBL.getJournals(genBankID);
	}

	[WebMethod(Description = "Gets the list of titles of given genbankID. Convert to List&ltString&gt")]
	public List<string> getTitles(string genBankID)
	{
		return GDBL.getTitles(genBankID);
	}

	[WebMethod(Description = "Gets the sequence of given genbankID. Convert to String")]
	public string getSequence(string genBankID)
	{
		return GDBL.getSequence(genBankID);
	}

	[WebMethod(Description = "Gets the definition of given genbankID. Convert to List&ltString&gt")]
	public List<string> getDefinitions(string genbankID)
	{
		return GDBL.getDefinitions(genbankID);
	}

	[WebMethod(Description = "Gets the accession version of given genbankID. Convert to List&ltString&gt")]
	public List<string> getAccessionVersion(string genbankID)
	{
		return GDBL.getAccessionVersion(genbankID);
	}

	[WebMethod(Description = "Gets the ginumber of given genbankID. Convert to List&ltString&gt")]
	public List<string> getGiNumber(string genbankID)
	{
		return GDBL.getGiNumber(genbankID);
	}

	[WebMethod(Description = "Gets the molecule type of given genbankID. Convert to String")]
	public string getMoleculeType(string genbankID)
	{
		return GDBL.getMoleculeType(genbankID);
	}

	[WebMethod(Description = "Gets the strand type of given genbankID. Convert to String")]
	public string getStrandType(string genbankID)
	{
		return GDBL.getStrandType(genbankID);
	}

	[WebMethod(Description = "Gets the strand topology of given genbankID. Convert to String")]
	public string getStrandTopology(string genbankID)
	{
		return GDBL.getStrandTopology(genbankID);
	}

	[WebMethod(Description = "Gets the list of gene of type locus_tag of given accession_version. Convert to List&ltString&gt")]
	public List<string> getGenes(string accession_version)
	{
		return GDBL.getGenes(accession_version);
	}

	[WebMethod(Description = "Gets the list of protein_ids of given accession_version. Convert to List&ltString&gt")]
	public List<string> getProteinIDs(string accession_version)
	{
		return GDBL.getProteinIDs(accession_version);
	}

	[WebMethod(Description = "Gets the list of nucleotide locations of given accession_version. Convert to List&ltString&gt")]
	public List<string> listNucleotideLocations(string accession_version)
	{
		return GDBL.ListNucleotideLocations(accession_version);
	}

	[WebMethod(Description = "Gets the nucleotide sequence of given accession_version between given range. Convert to List&ltString&gt")]
	public string getNucleotideSequence(string accession_version,int start, int end)
	{
		return GDBL.getNucleotideSequence(accession_version,start.ToString(),end.ToString());
	}
	#endregion
}


