using MonoMac.Security;
using MonoMac.Foundation;

namespace Virgil.Sync.CLI.Monomac
{
    public static class KeychainAccess
	{
		// Update to the name of your service
		private const string ServiceName = "Virgil.Sync.CLI.Monomac";

		public static bool GetPassword(string username, out string password)
		{
			SecRecord searchRecord;
			var record = FetchRecord(username, out searchRecord);
