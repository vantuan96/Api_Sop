namespace SOP.ComService
{
    internal class CredentialModel
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public string Api { get; set; }
        public bool AutoLogin { get; set; }
    }
}