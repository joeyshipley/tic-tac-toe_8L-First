using System.Collections.Generic;
using System.Linq;
using TTT.Application.Models;
using TTT.Application.Models.Factories;
using TTT.Application.Repositories;
using TTT.Application.Request;
using TTT.Domain;
using TTT.Domain.Entities;
using TTT.Domain.Factories;
using TTT.Domain.GameLogic.Processes;
using TTT.Domain.GameLogic.Specifications;
using TTT.Domain.GameLogic.Validators;

namespace TTT.Application.Services
{
	public class GameService : IGameService
	{
		private readonly IGameFactory _gameFactory;
		private readonly IModelFactory _modelFactory;
		private readonly IGameRepository _gameRepository;
		private readonly IGameStatusSpecification _gameStatusSpecification;
		private readonly IGameAlgorithms _gameAlgorithms;
		private readonly IMoveValidator _moveValidator;
		private readonly IGameMoveAssigner _gameMoveAssigner;
		private readonly IGameStatusAssigner _gameStatusAssigner;

		public GameService(IGameFactory gameFactory, 
			IModelFactory modelFactory, 
			IGameRepository gameRepository, 
			IGameStatusSpecification gameStatusSpecification,
			IGameAlgorithms gameAlgorithms,
			IMoveValidator moveValidator,
			IGameMoveAssigner gameMoveAssigner,
			IGameStatusAssigner gameStatusAssigner)
		{
			_gameFactory = gameFactory;
			_modelFactory = modelFactory;
			_gameRepository = gameRepository;
			_gameStatusSpecification = gameStatusSpecification;
			_gameAlgorithms = gameAlgorithms;
			_moveValidator = moveValidator;
			_gameMoveAssigner = gameMoveAssigner;
			_gameStatusAssigner = gameStatusAssigner;
		}

		public GameModel New()
		{
			var game = _gameFactory.CreateNew(_gameRepository.Save);
			return _modelFactory.CreateFrom(game);
		}

		public GameModel PerformMove(PerformMoveRequest request)
		{
			var game = _gameRepository.Get(request.GameId);
			var boardPosition = BoardPosition.CreateFrom(request.SelectedColumn, request.SelectedRow);
	
			var moveWarningsFromPlayersChoice = _moveValidator.ValidateMove(game, Enums.PlayerType.Human, boardPosition);
			if(moveWarningsFromPlayersChoice.Any())
				return getModelForInvalidPlayerChoice(game, moveWarningsFromPlayersChoice);

			assignPlayersChoiceToTheGame(game, boardPosition);

			assignComputersChoiceToTheGame(game);

			_gameStatusAssigner.AssignGameStatus(game, _gameRepository.Save);

			var model = _modelFactory.CreateFrom(game);
			return model;
		}
		
		private GameModel getModelForInvalidPlayerChoice(Game game, IList<ValidationError> moveWarnings)
		{
			return _modelFactory.CreateFrom(game, moveWarnings);
		}

		private void assignPlayersChoiceToTheGame(Game game, BoardPosition boardPosition)
		{
			_gameMoveAssigner.AssignMove(game, Enums.PlayerType.Human, boardPosition, _gameRepository.Save);
		}

		private void assignComputersChoiceToTheGame(Game game)
		{
			var playerWonGameFromMostRecentMove = determineIfPlayerHasWonGameFromMostRecentMove(game);
			if (playerWonGameFromMostRecentMove) 
				return;

			var computerMove = _gameAlgorithms.DetermineNextMove(game);
			_gameMoveAssigner.AssignMove(game, computerMove, _gameRepository.Save);
		}
		
		private bool determineIfPlayerHasWonGameFromMostRecentMove(Game game)
		{
			return _gameStatusSpecification.IsGameOver(game);
		}
	}
}