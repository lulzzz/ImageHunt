﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using ImageHunt.Model;
using ImageHunt.Model.Node;
using ImageHunt.Services;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using NFluent;
using TestUtilities;
using Xunit;
using Action = ImageHuntWebServiceClient.Action;

namespace ImageHuntTest.Services
{
    public class ActionServiceTest : ContextBasedTest
    {
      private ActionService _target;
      private ILogger<ActionService> _logger;

      public ActionServiceTest()
      {
        _logger = A.Fake<ILogger<ActionService>>();
        _target = new ActionService(_context, _logger);
      }
    [Fact]
    public async Task GetGameActionForGame()
    {
      // Arrange
      var teams = new List<Team> {new Team(), new Team(), new Team()};
      _context.Teams.AddRange(teams);
      _context.SaveChanges();
      var games = new List<Game> { new Game(), new Game() };
      _context.AddRange(games);
      _context.SaveChanges();
      var nodes = new List<Node>(){new FirstNode(){Latitude = 10.0001, Longitude = 15.0001},
       new ObjectNode(){Latitude = 12, Longitude = 16}, new LastNode()};
      _context.Nodes.AddRange(nodes);
      _context.SaveChanges();
      var gameActions = new List<GameAction>()
      {
        new GameAction()
        {
          Game = games[1],
          Action = Action.StartGame,
          DateOccured = DateTime.Now,
          Latitude = 10,
          Longitude = 15,
          Node = nodes[0],
          Team = teams[1]
        },
        new GameAction()
        {
          Game = games[1],
          Action = Action.VisitWaypoint,
          DateOccured = DateTime.Now,
          Latitude = 11,
          Longitude = 15.2,
          Node = nodes[1],
          Team = teams[1]
        },
        new GameAction()
        {
          Game = games[1],
          Action = Action.VisitWaypoint,
          DateOccured = DateTime.Now.AddMinutes(15),
          Latitude = 11.3,
          Longitude = 15.5,
          Team = teams[1]
        },
        new GameAction()
        {
          Game = games[0],
          Action = Action.VisitWaypoint,
          DateOccured = DateTime.Now,
          Latitude = 11,
          Longitude = 15.2,
          Node = nodes[1],
          Team = teams[1]
        },
      };
      _context.GameActions.AddRange(gameActions);
      _context.SaveChanges();
      // Act
      var results = await _target.GetGameActionsForGame(games[1].Id, 0, 10);
      // Assert
      Check.That(results).ContainsExactly(gameActions[0], gameActions[1], gameActions[2]);

      Check.That(results[2].Node).IsNull();
      Check.That(results[2].Delta).IsNaN();
      Check.That(results[0].Delta).IsEqualsWithDelta(15.6238, 0.001);
    }
    [Fact]
    public async Task GetGameActionForGame_Paginated()
    {
      // Arrange
      var teams = new List<Team> {new Team(), new Team(), new Team()};
      _context.Teams.AddRange(teams);
      _context.SaveChanges();
      var games = new List<Game> { new Game(), new Game() };
      _context.AddRange(games);
      _context.SaveChanges();
      var nodes = new List<Node>(){new FirstNode(){Latitude = 10.0001, Longitude = 15.0001},
       new ObjectNode(){Latitude = 12, Longitude = 16}, new LastNode()};
      _context.Nodes.AddRange(nodes);
      _context.SaveChanges();
      var gameActions = new List<GameAction>()
      {
        new GameAction()
        {
          Game = games[1],
          Action = Action.StartGame,
          DateOccured = DateTime.Now,
          Latitude = 10,
          Longitude = 15,
          Node = nodes[0],
          Team = teams[1]
        },
        new GameAction()
        {
          Game = games[1],
          Action = Action.VisitWaypoint,
          DateOccured = DateTime.Now,
          Latitude = 11,
          Longitude = 15.2,
          Node = nodes[1],
          Team = teams[1]
        },
        new GameAction()
        {
          Game = games[1],
          Action = Action.VisitWaypoint,
          DateOccured = DateTime.Now.AddMinutes(15),
          Latitude = 11.3,
          Longitude = 15.5,
          Team = teams[1]
        },
        new GameAction()
        {
          Game = games[0],
          Action = Action.VisitWaypoint,
          DateOccured = DateTime.Now,
          Latitude = 11,
          Longitude = 15.2,
          Node = nodes[1],
          Team = teams[1]
        },
      };
      _context.GameActions.AddRange(gameActions);
      _context.SaveChanges();
      // Act
      var results = await _target.GetGameActionsForGame(games[1].Id, 2, 2);
      // Assert
      Check.That(results).ContainsExactly(gameActions[2]);

      Check.That(results[0].Node).IsNull();
      Check.That(results[0].Delta).IsNaN();
      Check.That(results[0].Delta).IsEqualsWithDelta(15.6238, 0.001);
    }

        [Fact]
        public void GetGameActionCountForGame()
        {
            // Arrange
            var teams = new List<Team> { new Team(), new Team(), new Team() };
            _context.Teams.AddRange(teams);
            _context.SaveChanges();
            var games = new List<Game> { new Game(), new Game() };
            _context.AddRange(games);
            _context.SaveChanges();
            var nodes = new List<Node>(){new FirstNode(){Latitude = 10.0001, Longitude = 15.0001},
                new ObjectNode(){Latitude = 12, Longitude = 16}, new LastNode()};
            _context.Nodes.AddRange(nodes);
            _context.SaveChanges();
            var gameActions = new List<GameAction>()
            {
                new GameAction()
                {
                    Game = games[1],
                    Action = Action.StartGame,
                    DateOccured = DateTime.Now,
                    Latitude = 10,
                    Longitude = 15,
                    Node = nodes[0],
                    Team = teams[1]
                },
                new GameAction()
                {
                    Game = games[1],
                    Action = Action.VisitWaypoint,
                    DateOccured = DateTime.Now,
                    Latitude = 11,
                    Longitude = 15.2,
                    Node = nodes[1],
                    Team = teams[1]
                },
                new GameAction()
                {
                    Game = games[1],
                    Action = Action.VisitWaypoint,
                    DateOccured = DateTime.Now.AddMinutes(15),
                    Latitude = 11.3,
                    Longitude = 15.5,
                    Team = teams[1]
                },
                new GameAction()
                {
                    Game = games[0],
                    Action = Action.VisitWaypoint,
                    DateOccured = DateTime.Now,
                    Latitude = 11,
                    Longitude = 15.2,
                    Node = nodes[1],
                    Team = teams[1]
                },
            };
            _context.GameActions.AddRange(gameActions);
            _context.SaveChanges();
            // Act
            var result = _target.GetGameActionCountForGame(games[1].Id);
            // Assert
            Check.That(result).Equals(3);
        }
        [Fact]
    public void GetGameAction()
    {
      // Arrange
      var teams = new List<Team> { new Team(), new Team(), new Team() };
      _context.Teams.AddRange(teams);
      _context.SaveChanges();
      var games = new List<Game> { new Game(), new Game() { Name = "Toto" } };
      _context.AddRange(games);
      _context.SaveChanges();
      var nodes = new List<Node>(){new FirstNode(){Latitude = 10.0001, Longitude = 15.0001},
        new ObjectNode(){Latitude = 12, Longitude = 16}, new LastNode()};
      _context.Nodes.AddRange(nodes);
      _context.SaveChanges();
      var gameActions = new List<GameAction>()
      {
        new GameAction()
        {
          Game = games[1],
          Action = Action.StartGame,
          DateOccured = DateTime.Now,
          Latitude = 10,
          Longitude = 15,
          Node = nodes[0],
          Team = teams[1]
        },
        new GameAction()
        {
          Game = games[1],
          Action = Action.VisitWaypoint,
          DateOccured = DateTime.Now,
          Latitude = 11,
          Longitude = 15.2,
          Node = nodes[1],
          Team = teams[1]
        },
        new GameAction()
        {
          Game = games[1],
          Action = Action.VisitWaypoint,
          DateOccured = DateTime.Now.AddMinutes(15),
          Latitude = 11.3,
          Longitude = 15.5,
          Team = teams[1]
        },
        new GameAction()
        {
          Game = games[0],
          Action = Action.VisitWaypoint,
          DateOccured = DateTime.Now,
          Latitude = 11,
          Longitude = 15.2,
          Node = nodes[1],
          Team = teams[1]
        },
      };
      _context.GameActions.AddRange(gameActions);
      _context.SaveChanges();
      // Act
      var result = _target.GetGameAction(gameActions[1].Id);
      // Assert
      Check.That(result.Action).Equals(Action.VisitWaypoint);
      Check.That(result.Game.Name).Equals(games[1].Name);
      Check.That(result.Delta).IsEqualsWithDelta(141447.769119081, 0.001);
    }

      [Fact]
      public void AddGameAction()
      {
        // Arrange
        var games = new List<Game> {new Game(), new Game()};
        _context.Games.AddRange(games);
        var teams = new List<Team> { new Team(), new Team(), new Team() };
        _context.Teams.AddRange(teams);
        _context.SaveChanges();
        var gameAction = new GameAction(){Game = games[1], Team = teams[1]};
        // Act
        _target.AddGameAction(gameAction);
        // Assert
        Check.That(_context.GameActions).HasSize(1).And.Contains(gameAction);
      }

      [Fact]
      public void Validate_Validate()
      {
        // Arrange
        var gameActions = new List<GameAction> {new GameAction(),
            new GameAction() {IsValidated = false, Node = new PictureNode{Points = 15}}};
        _context.GameActions.AddRange(gameActions);
        _context.SaveChanges();
        var admins = new List<Admin> {new Admin(), new Admin()};
        _context.Admins.AddRange(admins);
        _context.SaveChanges();
        // Act
        _target.Validate(gameActions[1].Id, admins[1].Id);
        // Assert
        Check.That(gameActions.Extracting("IsValidated")).Contains(false, true);
        Check.That(gameActions.Extracting("IsReviewed")).Contains(false, true);
          Check.That(gameActions.Extracting("PointsEarned")).Contains(0, 15);
      }

      [Fact]
      public void Validate_With_Node()
      {
        // Arrange
        var nodes = new List<Node> {new TimerNode() {Points = 15}, new ObjectNode(){Points = 20}};
        _context.Nodes.AddRange(nodes);
        var gameActions = new List<GameAction> { new GameAction(), new GameAction() { Node = nodes[1]} };
        _context.GameActions.AddRange(gameActions);
        var admins = new List<Admin> { new Admin(), new Admin(){Role = Role.Validator} };
        _context.Admins.AddRange(admins);
        _context.SaveChanges();
        // Act
        _target.Validate(gameActions[1].Id, admins[1].Id);
        // Assert
        Check.That(gameActions.Extracting("PointsEarned")).Contains(0, 20);
      }
      [Fact]
      public void Validate_InValidate()
      {
        // Arrange
        var gameActions = new List<GameAction> {new GameAction(),
            new GameAction() {IsValidated = true, PointsEarned = 15,
                Node = new PictureNode(){Points = 15}}};
        _context.GameActions.AddRange(gameActions);
        _context.SaveChanges();
      var admins = new List<Admin> { new Admin(), new Admin() };
        _context.Admins.AddRange(admins);
        _context.SaveChanges();
        // Act
      _target.Validate(gameActions[1].Id, admins[1].Id);
        // Assert
        Check.That(gameActions.Extracting("IsValidated")).ContainsExactly(false, false);
        Check.That(gameActions.Extracting("IsReviewed")).ContainsExactly(false, true);
          Check.That(gameActions.Extracting("PointsEarned")).ContainsExactly(0, 0);
      }

        [Fact]
      public void Validate_without_Reviewer()
      {
      // Arrange
        var gameActions = new List<GameAction> { new GameAction(), new GameAction() { IsValidated = false } };
        _context.GameActions.AddRange(gameActions);
        _context.SaveChanges();
        // Act
        Check.ThatCode(()=> _target.Validate(gameActions[1].Id, 0)).Throws<InvalidOperationException>();
        // Assert
      }

        [Fact]
        public void GetScoreForGame()
        {
            // Arrange
            var games = new List<Game> {new Game()};
            _context.Games.AddRange(games);
            var teams = new List<Team> {new Team(), new Team()};
            _context.Teams.AddRange(teams);
            var gameActions = new List<GameAction>
            {
                new GameAction {Game = games[0], Team = teams[0], IsValidated = true, PointsEarned = 15},
                new GameAction {Game = games[0], Team = teams[1], IsValidated = true, PointsEarned = 15},
                new GameAction {Game = games[0], Team = teams[0], IsValidated = true, PointsEarned = 15},
                new GameAction {Game = games[0], Team = teams[1], IsValidated = false, PointsEarned = 15},
                new GameAction {Game = games[0], Team = teams[1], IsValidated = true, PointsEarned = 15},
                new GameAction {Game = games[0], Team = teams[1], IsValidated = true, PointsEarned = 15},
                new GameAction {Game = games[0], Team = teams[1], IsValidated = false, PointsEarned = 15},
            };
            _context.GameActions.AddRange(gameActions);
            _context.SaveChanges();
            // Act
            var result = _target.GetScoresForGame(gameActions[1].Game.Id);
            // Assert
            var expectedScores = new List<Score>
            {
                new Score(){Team = gameActions[0].Team, Points = 30},
                new Score(){Team = gameActions[1].Team, Points = 45},
            };
            var list = result.ToList();
            Check.That(result.Extracting("Points")).ContainsExactly(expectedScores.Extracting("Points"));
            Check.That(result.Extracting("Team")).ContainsExactly(expectedScores.Extracting("Team"));
        }

        [Fact]
        public void GetTeamsPositionsForGame()
        {
            // Arrange
            var games = new List<Game>() { new Game(), new Game() };
            _context.Games.AddRange(games);
            _context.SaveChanges();
            var teams = new List<Team>(){new Team(), new Team(), new Team()};
            _context.Teams.AddRange(teams);
            _context.SaveChanges();

            var gameActions = new List<GameAction>
            {
                new GameAction() {Action = Action.StartGame, Game = games[1], Team = teams[0]},
                new GameAction() {Action = Action.SubmitPosition, Game = games[1], Team = teams[0], Latitude = 48.8915138244629, Longitude = 2.23110699653625},
                new GameAction() {Action = Action.SubmitPosition, Game = games[0], Team = teams[0], Latitude = 48.8915138244629, Longitude = 2.23110699653625},
                new GameAction() {Action = Action.SubmitPosition, Game = games[1], Team = teams[1], Latitude = 48.891513824, Longitude = 2.2311068653625},
                new GameAction() {Action = Action.SubmitPosition, Game = games[1], Team = teams[0], Latitude = 48.891519824, Longitude = 2.211068653625},
                new GameAction() {Action = Action.SubmitPosition, Game = games[1], Team = teams[1], Latitude = 48.891593824, Longitude = 2.231106853625},
                new GameAction() {Action = Action.SubmitPosition, Game = games[1], Team = teams[0], Latitude = 48.891569824, Longitude = 2.2110653625},
                new GameAction() {Action = Action.SubmitPosition, Game = games[1], Team = teams[1], Latitude = 48.894593824, Longitude = 2.23116853625},
                new GameAction() {Action = Action.SubmitPosition, Game = games[1], Team = teams[0], Latitude = 48.8915679824, Longitude = 2.27653625},
            };
            _context.GameActions.AddRange(gameActions);
            _context.SaveChanges();
            // Act
            var results = _target.GetTeamsPositionsForGame(games[1].Id);
            // Assert
            Check.That(results.Extracting("Key")).Contains(teams[0], teams[1]);
            Check.That(results.First().Value).HasSize(4);
            Check.That(results.Last().Value).HasSize(3);
        }
  }
}
