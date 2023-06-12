using System.ComponentModel;
using System.Runtime.Serialization;

namespace VoteApp.Database.Candidate;

public enum CandidateStatus
{
    Empty,
    Moderation,
    Approve,
    Decline,
}