using System;
namespace GenomeDatabaseUpdater
{
	public static class UpdaterException
	{
		public static void check_data_exception(bool value)
		{
			if (value)
			{
				throw new Exception("Unexpected error in data or Database cannot handle data length");
			}
		}
	}
}

