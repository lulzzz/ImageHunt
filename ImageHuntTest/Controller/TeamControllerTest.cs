﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using FakeItEasy;
using ImageHunt.Controllers;
using ImageHunt.Model;
using ImageHunt.Model.Node;
using ImageHunt.Services;
using ImageHuntWebServiceClient.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NFluent;
using Xunit;

namespace ImageHuntTest.Controller
{
  [Collection("AutomapperFixture")]
  public class TeamControllerTest
    {
        private ITeamService _teamService;
        private TeamController _target;
      private IPlayerService _playerService;
      private IImageService _imageService;

      public TeamControllerTest()
        {
            _teamService = A.Fake<ITeamService>();
          _playerService = A.Fake<IPlayerService>();
          _imageService = A.Fake<IImageService>();
            _target = new TeamController(_teamService, _playerService, _imageService);
        }

      [Fact]
      public void CreateTeam()
      {
        // Arrange
        var team = new Team() {Name = "Team"};
        
        // Act
        _target.CreateTeam(1, team);
        // Assert
        A.CallTo(() => _teamService.CreateTeam(1, team)).MustHaveHappened();
      }

      [Fact]
      public void DeleteTeam()
      {
        // Arrange
        
        // Act
        _target.DeleteTeam(1);
        // Assert
        A.CallTo(() => _teamService.GetTeamById(1)).MustHaveHappened();
        A.CallTo(() => _teamService.DeleteTeam(A<Team>._)).MustHaveHappened();
      }

      [Fact]
      public void GetTeams()
      {
        // Arrange
        
        // Act
        var result = _target.GetTeams(1);

        // Assert
        Check.That(result).IsInstanceOf<OkObjectResult>();
        A.CallTo(() => _teamService.GetTeams(1)).MustHaveHappened();
      }
        [Fact]
        public void Get()
        {
            // Arrange
            
            // Act
            var result = _target.GetTeam(1);
            // Assert
        }

        [Fact]
        public void AddPlayer()
        {
            // Arrange
            var team = new Team();
            A.CallTo(() => _teamService.GetTeamById(A<int>._)).Returns(team);
          A.CallTo(() => _playerService.GetPlayerByChatId(A<string>._)).Throws<InvalidOperationException>();
            var player = new Player(){Name = "toto", ChatLogin = "Toro"};
            // Act
            _target.AddPlayer(1, player);
            // Assert
            A.CallTo(() => _teamService.AddMemberToTeam(team, A<List<Player>>._)).MustHaveHappened();
        }
        [Fact]
        public void AddPlayer_PlayerAlreadyExist()
        {
            // Arrange
            var team = new Team();
            A.CallTo(() => _teamService.GetTeamById(A<int>._)).Returns(team);
            var player = new Player(){Name = "toto", ChatLogin = "Toro"};
            // Act
            _target.AddPlayer(1, player);
            // Assert
            A.CallTo(() => _playerService.JoinTeam(1, A<int>._)).MustHaveHappened();
        }

      [Fact]
      public void RemovePlayer()
      {
        // Arrange
        
        // Act
        var result = _target.RemovePlayer(1, 1);
        // Assert
        A.CallTo(() => _teamService.DelMemberToTeam(A<Team>._, A<Player>._)).MustHaveHappened();
      }
      [Fact]
      public void GetPlayerDetails()
      {
        // Arrange
        string playerLogin = "Toto";
        var player = new Player(){};
        A.CallTo(() => _playerService.GetPlayerByChatId(playerLogin)).Returns(player);
        // Act
        _target.GetPlayer(playerLogin);
        // Assert
        A.CallTo(() => _playerService.GetPlayerByChatId(playerLogin)).MustHaveHappened();
      }

      [Fact]
      public void GetTeamOfPlayer()
      {
        // Arrange
        // Act
        var result = _target.GetTeamsOfPlayer("toto");
        // Assert
        Check.That(result).IsInstanceOf<OkObjectResult>();
        A.CallTo(() => _playerService.GetPlayerByChatId("toto")).MustHaveHappened();
        A.CallTo(() => _teamService.GetTeamsForPlayer(A<Player>._)).MustHaveHappened();
      }
      [Fact]
      public void StartPlayer()
      {
        // Arrange
        // Act
        var result = _target.StartTeam(1, 1);
        // Assert
        Check.That(result).IsInstanceOf<OkObjectResult>();
        var nextNode = ((OkObjectResult)result).Value;
        Check.That(nextNode).InheritsFrom<Node>();
        A.CallTo(() => _teamService.StartGame(A<int>._, A<int>._)).MustHaveHappened();
      }

      [Fact]
      public void NextNodeForPlayer()
      {
        // Arrange

        // Act
        var result = _target.NextNodeForPlayer(1);
        // Assert
        Check.That(result).InheritsFrom<IActionResult>();
        var nextNode = ((OkObjectResult)result).Value;
        Check.That(nextNode).InheritsFrom<Node>();

        A.CallTo(() => _teamService.NextNodeForTeam(1, A<double>._, A<double>._)).MustHaveHappened();
      }

      [Fact]
      public void UploadImage()
      {
        // Arrange
        var formFile = A.Fake<IFormFile>();
        var uploadImageRequest = new UploadImageRequest(){FormFile = formFile, GameId = 1, TeamId = 1, Longitude = 15, Latitude = 15};

        // Act
        var result = _target.UploadImage(uploadImageRequest);
        // Assert
        Check.That(result).InheritsFrom<IActionResult>();
        A.CallTo(() => _teamService.UploadImage(A<int>._, A<int>._, A<double>._, A<double>._, A<byte[]>._, A<string>._))
          .MustHaveHappened();
      }
      [Fact]
      public void UploadImageWithTitle()
      {
        // Arrange
        var formFile = A.Fake<IFormFile>();
        var uploadImageRequest = new UploadImageRequest(){FormFile = formFile, GameId = 1, TeamId = 1, Longitude = 15, Latitude = 15, ImageName = "3"};

        // Act
        var result = _target.UploadImage(uploadImageRequest);
        // Assert
        Check.That(result).InheritsFrom<IActionResult>();
        A.CallTo(() => _teamService.UploadImage(uploadImageRequest.GameId, uploadImageRequest.TeamId, uploadImageRequest.Latitude, uploadImageRequest.Longitude, A<byte[]>._, uploadImageRequest.ImageName))
          .MustHaveHappened();
      }
      [Fact]
      public void UploadImageGeoTagged()
      {
        // Arrange
        var formFile = A.Fake<IFormFile>();
        var resourcseStream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("ImageHuntTest.TestData.IMG_20170920_180905.jpg");
        A.CallTo(() => formFile.OpenReadStream()).Returns(resourcseStream);
        A.CallTo(() => _imageService.ExtractLocationFromImage(A<Picture>._)).Returns((15.6, 85.1));
        var uploadImageRequest = new UploadImageRequest(){FormFile = formFile, GameId = 1, TeamId = 1, Longitude = 0, Latitude = 0, ImageName = "3"};

        // Act
        var result = _target.UploadImage(uploadImageRequest);
        // Assert
        Check.That(result).InheritsFrom<IActionResult>();
        A.CallTo(() => _teamService.UploadImage(uploadImageRequest.GameId, uploadImageRequest.TeamId, 15.6, 85.1, A<byte[]>._, uploadImageRequest.ImageName))
          .MustHaveHappened();
      }

  }

}
