﻿using Finanzuebersicht.Backend.Core.Contract.Logic.LogicResults;
using Finanzuebersicht.Backend.Core.Contract.Logic.Modules.SessionManagement.Sessions;
using Finanzuebersicht.Backend.Core.Contract.Logic.Tools.Identifier;
using Finanzuebersicht.Backend.Core.Contract.Logic.Tools.Time;
using Finanzuebersicht.Backend.Core.Contract.Persistence.Modules.SessionManagement.Sessions;
using Finanzuebersicht.Backend.Core.Logic.LogicResults;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace Finanzuebersicht.Backend.Core.Logic.Modules.SessionManagement.Sessions
{
    internal class SessionsCrudLogic : ISessionsCrudLogic
    {
        private readonly ISessionsRepository sessionsRepository;

        private readonly IDateTimeProvider dateTimeProvider;
        private readonly ISHA256TokenGenerator sha256TokenGenerator;
        private readonly ILogger<SessionsCrudLogic> logger;

        private readonly SessionExpirationOptions options;

        public SessionsCrudLogic(
            ISessionsRepository sessionsRepository,
            ISHA256TokenGenerator sha256TokenGenerator,
            IDateTimeProvider dateTimeProvider,
            ILogger<SessionsCrudLogic> logger,
            IOptions<SessionExpirationOptions> options)
        {
            this.sessionsRepository = sessionsRepository;

            this.sha256TokenGenerator = sha256TokenGenerator;
            this.dateTimeProvider = dateTimeProvider;
            this.logger = logger;

            this.options = options.Value;
        }

        public string CreateSessionForEmailUser(Guid emailUserId, string name)
        {
            string sessionToken = this.CreateSession(emailUserId, name);
            this.logger.LogDebug("Session für Email-User erstellt");
            return sessionToken;
        }

        public ILogicResult<ISession> GetSessionFromToken(string token)
        {
            IDbSession dbSession = this.sessionsRepository.GetSession(token);
            if (dbSession == null || dbSession.ExpiresOn <= this.dateTimeProvider.Now())
            {
                this.logger.LogDebug("Session konnte nicht gefunden werden.");
                return LogicResult<ISession>.NotFound("Session konnte nicht gefunden werden.");
            }

            this.logger.LogDebug("Session wurde geladen");
            return LogicResult<ISession>.Ok(Session.FromDbSession(dbSession));
        }

        public ILogicResult TerminateSession(string token)
        {
            IDbSession sessionToDelete = this.sessionsRepository.GetSession(token);
            if (sessionToDelete == null)
            {
                this.logger.LogDebug("Session konnte nicht gefunden werden");
                return LogicResult.NotFound("Session konnte nicht gefunden werden.");
            }

            this.sessionsRepository.DeleteSession(token);

            this.logger.LogDebug("Session wurde gelöscht");
            return LogicResult.Ok();
        }

        private string CreateSession(Guid? emailUserId, string name)
        {
            var dbSession = new DbSession()
            {
                Token = this.sha256TokenGenerator.Generate(),
                Name = name,
                ExpiresOn = this.dateTimeProvider.Now().AddMinutes(this.options.ExpirationTimeInMinutes),
                EmailUserId = emailUserId,
            };
            this.sessionsRepository.CreateSession(dbSession);

            return dbSession.Token;
        }
    }
}