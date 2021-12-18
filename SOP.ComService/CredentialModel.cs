using System.Collections.Generic;

namespace SOP.ComService
{
    public class CredentialModel
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public string Api { get; set; }
        public bool AutoLogin { get; set; }
    }
}