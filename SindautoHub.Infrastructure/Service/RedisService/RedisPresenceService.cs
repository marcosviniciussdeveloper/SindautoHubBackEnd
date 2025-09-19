using StackExchange.Redis;
using SindautoHub.Application.Interface;
using SindautoHub.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using Pipelines.Sockets.Unofficial;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Infrastructure.Service.RedisService
{
    public class RedisPresenceService : IPresenceService
    {
        private readonly IDatabase _db;
        private static readonly TimeSpan PresenceTtl = TimeSpan.FromSeconds(90);

        public RedisPresenceService(IConnectionMultiplexer mux)
        {
            _db = mux.GetDatabase();
        }

        private static string UserKey(Guid id) => $"user:presence:{id}";

       private static string SectorUsersKey(Guid id ) => $"sector:onlineUsers:{id}";
        private static string SectorKey(Guid id) => $"sector:onlineCount:{id}";

        public async Task SetOnlineAsync(Guid userId, Guid sectorId)
        {
            await _db.SetAddAsync(SectorUsersKey(sectorId), userId.ToString());
            await _db.StringSetAsync(UserKey(userId), PresenceStatus.Online.ToString(), PresenceTtl);
            await _db.StringIncrementAsync(SectorKey(sectorId));
        }

        public async Task SetOfflineAsync(Guid userId, Guid sectorId)
        {
            await _db.SetRemoveAsync(SectorUsersKey(sectorId), userId.ToString());
            await _db.StringSetAsync(UserKey(userId), PresenceStatus.Offline.ToString(), TimeSpan.FromMinutes(5));
            await _db.StringDecrementAsync(SectorKey(sectorId));
        }

        public Task PingAsync(Guid userId) =>
            _db.KeyExpireAsync(UserKey(userId), PresenceTtl);

        public Task SetAusenteAsync(Guid userId) =>
            _db.StringSetAsync(UserKey(userId), PresenceStatus.Ausente.ToString(), PresenceTtl);

        public async Task<PresenceStatus?> GetStatusAsync(Guid userId)
        {
            var val = await _db.StringGetAsync(UserKey(userId));
            if (!val.HasValue) return null;
            return Enum.TryParse<PresenceStatus>(val!, out var status) ? status : null;
        }

        public async Task<int> GetOnlineCountAsync(Guid sectorId)
        {
            var val = await _db.StringGetAsync(SectorKey(sectorId));
            return val.HasValue && int.TryParse(val!, out var count) ? count : 0;
        }

  
        public async Task<bool> IsUserOnlineAsync(Guid userId)
        {
            var status = await _db.StringGetAsync(UserKey(userId));
            return status.HasValue && status == PresenceStatus.Online.ToString();
        }

        public async Task<List<Guid>> GetOnlineUsersBySectAsync(Guid sectorId)
        {
            var redisValues = await _db.SetMembersAsync(SectorUsersKey(sectorId));

            var userIds = new List<Guid>();

            foreach (var value in redisValues)
            {
                if (Guid.TryParse(value, out var userId))
                {
                    // Verifica se ainda está online
                    var status = await _db.StringGetAsync(UserKey(userId));
                    if (status == PresenceStatus.Online.ToString())
                    {
                        userIds.Add(userId);
                    }
                }
            }

            return userIds;
        }
    }
}
