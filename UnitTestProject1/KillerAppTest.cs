using System;
using System.Collections.Generic;
using System.ComponentModel;
using Data.Context;
using Data.Context.SQL;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace TestKillerApp
{
    [TestClass]
    public class KillerAppTest
    {
        private PlayerLogic _playerLogic;
        private Player _player;
        
        [TestInitialize]
        public void TestInitialize()
        {
            _playerLogic = new PlayerLogic(new PlayerMemoryContext());
            _player = new Player
            {
                PlayerId = 1,
                Username = "UnitPLayer",
                Password = "1234",
                Role = "player",
                PlayerLevel = 0,
                Experience = 0,
                SkillPoints = 5,
                Money = 100,
                Income = 1,
                Energy = 100,
                EnergyRegen = 5,
                RefillableEnergy = 100,
                LastTimeEnergyRefilled = DateTime.Now,
                MaxEnergy = 100,
                RealName = "Unit Player",
                Country = "Netherlands",
                City = "Eindhoven"
            };
        }
        
        /* Integration Testing */
        // Login
        [TestMethod]
        public void Login_CorrextUsernameAndPassword_PlayerLogsIn()
        {
            // Arrange
            
            // Act
            Player player = _playerLogic.Login(_player.Username, _player.Password);

            // Assert
            Assert.IsNotNull(player, "Logging in failed!");
        }
        // Haal lijst op met Hacks op basis van PlayerLevel
        [TestMethod]
        public void GetListHacks_MinimalLevelIsGoodEnough_ListHacksComesBack()
        {
            // Arrange

            // Act
            List<Hack> hacks = _playerLogic.GetAvailableHacks(_player.PlayerLevel);

            // Assert
            Assert.AreEqual(hacks.Count, 1, "Hacks don't match!");
        }
        // Heeft Player genoeg energy voor geselecteerde hack
        [TestMethod]
        public void HasEnoughEnergyForHack_PlayerHasEnoughEnergy_HasEnoughEnergy()
        {
            // Arrange

            // Act
            bool hasEnoughEnergy = _playerLogic.HasEnoughEnergy(1, _player.PlayerId);

            // Assert
            Assert.IsTrue(hasEnoughEnergy, "Not enough energy!");
        }
        // Heeft speler genoeg skillpunten om skill up te graden
        // Is skill al gemaxed
        // Skill Point uitgeven
        [TestMethod]
        public void UpgradeSkill_PlayerHasEnoughSKillPoints_HasEnoughSkillPointsForUpgrade()
        {
            // Arrange

            // Act
            bool hasEnoughSkillPoints = _playerLogic.UpgradeSkill(3, _player.PlayerId);

            // Assert
            Assert.IsTrue(hasEnoughSkillPoints, "Not enough skill points!");
        }
        // IsUsernameTaken
        [TestMethod]
        public void IsUsernameTaken_UsernameIsNotTaken_NewUsername()
        {
            // Arrange

            // Act
            bool isUsernameTaken = _playerLogic.IsUsernameTaken("UnitPLayer2");

            // Assert
            Assert.IsTrue(isUsernameTaken, "Username is taken!");
        }
    }
}
