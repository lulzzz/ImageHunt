﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Components.DictionaryAdapter;
using FakeItEasy;
using ImageHunt.Model;
using ImageHunt.Services;
using Microsoft.Extensions.Logging;
using NFluent;
using TestUtilities;
using Xunit;

namespace ImageHuntTest.Services
{
  public class TeamServiceTest : ContextBasedTest
  {
    private TeamService _target;
    private ILogger<TeamService> _logger;

    public TeamServiceTest()
    {
      _logger = A.Fake<ILogger<TeamService>>();
      _target = new TeamService(_context, _logger);
    }
    [Fact]
    public void CreateTeam()
    {
      // Arrange
      var games = new List<Game>() { new Game(), new Game() };
      _context.Games.AddRange(games);
      _context.SaveChanges();
      var team = new Team() { Name = "Team1" };
      // Act
      _target.CreateTeam(games[1].Id, team);
      // Assert
      Check.That(games[1].Teams.First()).IsEqualTo(team);
      Check.That(team.Id).IsNotZero();
    }

    [Fact]
    public void DeleteTeam()
    {
      // Arrange
      var teams = new List<Team>() { new Team(), new Team(), new Team() };
      _context.Teams.AddRange(teams);
      _context.SaveChanges();
      // Act
      _target.DeleteTeam(teams[1]);
      // Assert
      Check.That(_context.Teams).ContainsExactly(teams[0], teams[2]);
    }

    [Fact]
    public void GetTeams()
    {
      // Arrange
      var teams = new List<Team>() { new Team(), new Team(), new Team() };
      var games = new List<Game>() { new Game() { Teams = teams }, new Game() };
      _context.Games.AddRange(games);
      _context.SaveChanges();
      // Act
      var result = _target.GetTeams(games[0].Id);
      // Assert
      Check.That(result).ContainsExactly(teams);
    }

    [Fact]
    public void GetTeamByName()
    {
      // Arrange
      var teams = new List<Team>() { new Team() { Name = "Team1" }, new Team() { Name = "Team2" }, new Team() { Name = "Team3" } };
      _context.Teams.AddRange(teams);
      _context.SaveChanges();
      // Act
      var team = _target.GetTeamByName("Team2");
      // Assert
      Check.That(team).IsEqualTo(teams[1]);
    }
    [Fact]
    public void GetTeamById()
    {
      // Arrange
      var teams = new List<Team>() { new Team() { Name = "Team1" }, new Team() { Name = "Team2" }, new Team() { Name = "Team3" } };
      _context.Teams.AddRange(teams);
      _context.SaveChanges();
      // Act
      var team = _target.GetTeamById(teams[1].Id);
      // Assert
      Check.That(team).IsEqualTo(teams[1]);
    }

    [Fact]
    public void AddMemberToTeam()
    {
      // Arrange
      var teams = new List<Team>() { new Team(), new Team(), new Team() };
      _context.Teams.AddRange(teams);
      _context.SaveChanges();
      var players = new List<Player>() { new Player(), new Player(), new Player() };
      // Act
      _target.AddMemberToTeam(teams[1], players);
      // Assert
      Check.That(teams[1].Players).ContainsExactly(players);
      Check.That(_context.Players).ContainsExactly(players);
    }

    [Fact]
    public void DelMemberToTeam()
    {
      // Arrange
      var players = new List<Player>(){new Player(), new Player(), new Player()};
      _context.Players.AddRange(players);
       var teams = new List<Team>()
            {
                new Team(),
                new Team(),
                new Team()
            };
      _context.Teams.AddRange(teams);
      var teamPlayers = players.Select(p => new TeamPlayer() {Team = teams[0], Player = p});
      teams[0].TeamPlayers = teamPlayers.ToList();

      _context.SaveChanges();
      // Act
      _target.DelMemberToTeam(teams[0], players[1]);
      // Assert
      Check.That(teams[0].TeamPlayers).HasSize(2);
    }


    [Fact]
    public void RemovePlayer()
    {
      // Arrange
      var players = new List<Player>() { new Player() { ChatLogin = "toto1" }, new Player() { ChatLogin = "toto2" }, new Player() { ChatLogin = "Toto3" } };
      var teams = new List<Team>(){new Team()
        {
          Name = "Team1"
        },

        new Team(){Name = "Team2"}

      };

      _context.Players.AddRange(players);
      _context.Teams.AddRange(teams);

      var teamPlayers = new List<TeamPlayer>
      {
        new TeamPlayer() {Team = teams[0], Player = players[0]},
        new TeamPlayer() {Team = teams[0], Player = players[2]},
        new TeamPlayer() {Team = teams[1], Player = players[1]},

      };
      teams[0].TeamPlayers = new List<TeamPlayer>(){ teamPlayers[0] , teamPlayers[1] };
      teams[1].TeamPlayers = new List<TeamPlayer>(){ teamPlayers[2] };
      _context.SaveChanges();
      // Act
      _target.DelMemberToTeam(teams[0], players[2]);
      // Assert
      Check.That(teams[0].Players).HasSize(1);
    }

    [Fact]
    public void GetTeamsForPlayer()
    {
      // Arrange
      var players = new List<Player>() { new Player() { ChatLogin = "toto1" }, new Player() { ChatLogin = "toto2" }, new Player() { ChatLogin = "Toto3" } };
      var teams = new List<Team>(){new Team()
        {
          Name = "Team1"
        },

        new Team(){Name = "Team2"}

      };

      _context.Players.AddRange(players);
      _context.Teams.AddRange(teams);

      var teamPlayers = new List<TeamPlayer>
      {
        new TeamPlayer() {Team = teams[0], Player = players[0]},
        new TeamPlayer() {Team = teams[0], Player = players[2]},
        new TeamPlayer() {Team = teams[1], Player = players[1]},

      };
      teams[0].TeamPlayers = new List<TeamPlayer>() { teamPlayers[0], teamPlayers[1] };
      teams[1].TeamPlayers = new List<TeamPlayer>() { teamPlayers[2] };
      _context.SaveChanges();
      // Act
      var result = _target.GetTeamsForPlayer(players[1]);
      // Assert
      Check.That(result).Contains(teams[1]);
    }
  }
}
