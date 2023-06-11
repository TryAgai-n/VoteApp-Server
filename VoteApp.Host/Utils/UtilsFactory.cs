using VoteApp.Host.Utils.DocumentUtils;
using VoteApp.Host.Utils.User;

namespace VoteApp.Host.Utils;

internal class UtilsFactory : IUtilsFactory
{
    public IUserUtils UserUtils { get; set; }
    public IDocumentUtils DocumentUtils { get; set; }
    
    
    public UtilsFactory(
        IUserUtils userUtils,
        IDocumentUtils documentUtils)
    {
        UserUtils = userUtils;
        DocumentUtils = documentUtils;
    }
}