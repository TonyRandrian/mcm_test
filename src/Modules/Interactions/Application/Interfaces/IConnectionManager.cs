namespace Mcm.Interactions.Application.Interfaces
{
    public interface IConnectionManager
    {
        void AddConnection(string teamMemberId, string connectionId);
        void RemoveConnection(string connectionId);
        IReadOnlyCollection<string> GetConnections(string teamMemberId);
    }
    
}