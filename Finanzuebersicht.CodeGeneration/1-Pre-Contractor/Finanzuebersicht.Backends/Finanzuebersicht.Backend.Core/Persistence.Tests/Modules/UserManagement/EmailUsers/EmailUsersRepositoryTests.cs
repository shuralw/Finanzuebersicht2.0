﻿using Finanzuebersicht.Backend.Core.Contract.Persistence.Modules.UserManagement.EmailUsers;
using Finanzuebersicht.Backend.Core.Persistence.Modules.UserManagement.EmailUsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Finanzuebersicht.Backend.Core.Persistence.Tests.Modules.UserManagement.EmailUsers
{
    [TestClass]
    public class EmailUsersRepositoryTests
    {
        [TestMethod]
        public void CreateEmailUserAndGetEmailUserTest()
        {
            // Arrange
            InMemoryRepositories repos = new InMemoryRepositories();
            EmailUsersRepository emailUsersRepository = repos.GetEmailUsersRepository();
            IDbEmailUser emailUserToAdd = DbEmailUserMocking.Create();

            // Act
            emailUsersRepository.CreateEmailUser(emailUserToAdd);
            IDbEmailUser emailUser = emailUsersRepository.GetEmailUser(emailUserToAdd.Id);

            // Assert
            Assert.AreEqual(emailUserToAdd.Id, emailUser.Id);
            Assert.AreEqual(emailUserToAdd.Email, emailUser.Email);
            Assert.AreEqual(emailUserToAdd.PasswordHash, emailUser.PasswordHash);
            Assert.AreEqual(emailUserToAdd.PasswordSalt, emailUser.PasswordSalt);
        }

        [TestMethod]
        public void UpdateEmailUserTest()
        {
            // Arrange
            InMemoryRepositories repos = new InMemoryRepositories();
            EmailUsersRepository emailUsersRepository = repos.GetEmailUsersRepository();
            IDbEmailUser emailUserToAdd = DbEmailUserMocking.Create();
            emailUsersRepository.CreateEmailUser(emailUserToAdd);

            // Act
            emailUserToAdd.PasswordHash = Guid.NewGuid().ToString();
            emailUserToAdd.PasswordSalt = Guid.NewGuid().ToString();
            emailUsersRepository.UpdateEmailUser(emailUserToAdd);

            // Assert
            IDbEmailUser emailUser = emailUsersRepository.GetEmailUser(emailUserToAdd.Id);
            Assert.AreEqual(emailUserToAdd.Id, emailUser.Id);
            Assert.AreEqual(emailUserToAdd.Email, emailUser.Email);
            Assert.AreEqual(emailUserToAdd.PasswordHash, emailUser.PasswordHash);
            Assert.AreEqual(emailUserToAdd.PasswordSalt, emailUser.PasswordSalt);
        }

        [TestMethod]
        public void GetEmailUserByMailTest()
        {
            // Arrange
            InMemoryRepositories repos = new InMemoryRepositories();
            EmailUsersRepository emailUsersRepository = repos.GetEmailUsersRepository();
            IDbEmailUser emailUserToAdd = DbEmailUserMocking.Create();
            emailUsersRepository.CreateEmailUser(emailUserToAdd);

            // Act
            IDbEmailUser emailUser = emailUsersRepository.GetEmailUser(emailUserToAdd.Email);

            // Assert
            Assert.AreEqual(emailUserToAdd.Id, emailUser.Id);
            Assert.AreEqual(emailUserToAdd.Email, emailUser.Email);
            Assert.AreEqual(emailUserToAdd.PasswordHash, emailUser.PasswordHash);
            Assert.AreEqual(emailUserToAdd.PasswordSalt, emailUser.PasswordSalt);
        }

        [TestMethod]
        public void GetEmailUserListTest()
        {
            // Arrange
            InMemoryRepositories repos = new InMemoryRepositories();
            EmailUsersRepository emailUsersRepository = repos.GetEmailUsersRepository();
            IDbEmailUser emailUserToAdd = DbEmailUserMocking.Create();
            emailUsersRepository.CreateEmailUser(emailUserToAdd);

            // Act
            List<IDbEmailUser> emailUsers = emailUsersRepository.GetEmailUsers().ToList();

            // Assert
            Assert.AreEqual(1, emailUsers.Count);
            Assert.AreEqual(emailUserToAdd.Id, emailUsers[0].Id);
            Assert.AreEqual(emailUserToAdd.Email, emailUsers[0].Email);
            Assert.AreEqual(emailUserToAdd.PasswordHash, emailUsers[0].PasswordHash);
            Assert.AreEqual(emailUserToAdd.PasswordSalt, emailUsers[0].PasswordSalt);
        }

        [TestMethod]
        public void CreatePermissionsAndUpdatePermissionsAndGetPermissions()
        {
            // Arrange
            InMemoryRepositories repos = new InMemoryRepositories();
            EmailUsersRepository emailUsersRepository = repos.GetEmailUsersRepository();
            IDbEmailUser emailUserToAdd = DbEmailUserMocking.Create();
            emailUsersRepository.CreateEmailUser(emailUserToAdd);

            // Act
            IDbEmailUser emailUserToUpdate = emailUsersRepository.GetEmailUser(emailUserToAdd.Id);
            emailUserToUpdate.Email = "new-mail@example.org";
            emailUsersRepository.UpdateEmailUser(emailUserToUpdate);

            // Assert
            IDbEmailUser emailUser = emailUsersRepository.GetEmailUser(emailUserToAdd.Id);
            Assert.AreEqual(emailUserToUpdate.Id, emailUser.Id);
            Assert.AreEqual(emailUserToUpdate.Email, emailUser.Email);
            Assert.AreEqual(emailUserToUpdate.PasswordHash, emailUser.PasswordHash);
            Assert.AreEqual(emailUserToUpdate.PasswordSalt, emailUser.PasswordSalt);
        }

        [TestMethod]
        public void DeletePermissionsTest()
        {
            // Arrange
            InMemoryRepositories repos = new InMemoryRepositories();
            EmailUsersRepository emailUsersRepository = repos.GetEmailUsersRepository();
            IDbEmailUser emailUserToAdd = DbEmailUserMocking.Create();
            emailUsersRepository.CreateEmailUser(emailUserToAdd);

            // Act
            emailUsersRepository.DeleteEmailUser(emailUserToAdd.Id);

            // Assert
            IDbEmailUser emailUser = emailUsersRepository.GetEmailUser(emailUserToAdd.Id);
            Assert.IsNull(emailUser);
        }
    }
}