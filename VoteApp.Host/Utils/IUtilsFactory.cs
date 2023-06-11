using VoteApp.Host.Utils.DocumentUtils;
using VoteApp.Host.Utils.UserUtils;

namespace VoteApp.Host.Utils;

public interface IUtilsFactory
{
    IUserUtils UserUtils { get; set; }
    IDocumentUtils DocumentUtils { get; set; }
}