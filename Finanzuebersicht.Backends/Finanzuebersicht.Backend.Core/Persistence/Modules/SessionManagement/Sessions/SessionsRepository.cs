﻿using Finanzuebersicht.Backend.Core.Contract.Persistence.Modules.SessionManagement.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Finanzuebersicht.Backend.Core.Persistence.Modules.SessionManagement.Sessions
{
    internal class SessionsRepository : ISessionsRepository
    {
        private readonly PersistenceDbContext context;

        public SessionsRepository(PersistenceDbContext context)
        {
            this.context = context;
        }

        public void CreateSession(IDbSession session)
        {
            this.context.Sessions.Add(DbSession.ToEfSession(session));
            this.context.SaveChanges();
        }

        public void DeleteExpiredSessions(DateTime now)
        {
            var sessionsToDelete = this.context.Sessions.Where(session => session.ExpiresOn <= now);
            this.context.Sessions.RemoveRange(sessionsToDelete);
            this.context.SaveChanges();
        }

        public void DeleteSession(string token)
        {
            var sessionToRemove = this.context.Sessions.Find(token);
            this.context.Sessions.Remove(sessionToRemove);
            this.context.SaveChanges();
        }

        public void DeleteSessionsOfEmailUser(Guid emailUserId)
        {
            var sessionsToDelete = this.context.Sessions
                .Where(session => session.EmailUserId == emailUserId);
            this.context.Sessions.RemoveRange(sessionsToDelete);
            this.context.SaveChanges();
        }

        public IDbSession GetSession(string token)
        {
            var session = this.context.Sessions
                .Where(s => s.Token == token)
                .FirstOrDefault();

            return DbSession.FromEfSession(session);
        }

        public List<IDbSession> GetSessionsOfEmailUser(Guid emailUserId)
        {
            return this.context.Sessions
                .Where(session => session.EmailUserId == emailUserId)
                .Select(session => DbSession.FromEfSession(session))
                .ToList();
        }
    }
}