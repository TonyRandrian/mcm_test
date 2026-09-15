using System.Collections.Concurrent;
using Mcm.Interactions.Application.Interfaces;

namespace Mcm.Interactions.Application.Features.Notifications
{
    public class ConnectionManager
        : IConnectionManager
    {
        private readonly ConcurrentDictionary<string, HashSet<string>> _users = new();
        private readonly ConcurrentDictionary<string, string> _connections = new();
        private readonly object _lock = new();

        public void AddConnection(string teamMemberId, string connectionId)
        {
            lock (_lock)
            {
                if (!_users.TryGetValue(teamMemberId, out var connections))
                {
                    connections = new HashSet<string>();
                    _users[teamMemberId] = connections;
                }

                connections.Add(connectionId);
                _connections[connectionId] = teamMemberId;
            }
        }

        public void RemoveConnection(string connectionId)
        {
            lock (_lock)
            {
                if (!_connections.TryRemove(connectionId, out var teamMemberId))
                    return;

                if (_users.TryGetValue(teamMemberId, out var connections))
                {
                    connections.Remove(connectionId);

                    if (connections.Count == 0)
                        _users.TryRemove(teamMemberId, out _);
                }
            }
        }

        public IReadOnlyCollection<string> GetConnections(string teamMemberId)
        {
            if (_users.TryGetValue(teamMemberId, out var connections))
                return connections.ToList();

            return Array.Empty<string>();
        }
    }
}