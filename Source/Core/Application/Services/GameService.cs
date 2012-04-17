using System.Collections.Generic;
using TTT.Core.Application.Factories;
using TTT.Core.Application.Repositories;
using TTT.Core.Application.Request;
using TTT.Core.Domain;
using TTT.Core.Domain.Entities;
using TTT.Core.Domain.Factories;
using TTT.Core.Domain.Models;
using TTT.Core.Domain.Processes;
using TTT.Core.Domain.Specifications;

namespace TTT.Core.Application.Services
{
	public class GameService : IGameService
	{
		private readonly IGameFactory _gameFactory;
		private readonly IModelFactory _modelFactory;
		private readonly IGameRepository _gameRepository;
		private readonly IGameSpecifications _gameSpecifications;
		private readonly IGameAlgorithms _gameAlgorithms;

		public GameService(IGameFactory gameFactory, 
			IModelFactory modelFactory, 
			IGameRepository gameRepository, 
			IGameSpecifications gameSpecifications,
			IGameAlgorithms gameAlgorithms)
		{
			_gameFactory = gameFactory;
			_modelFactory = modelFactory;
			_gameRepository = gameRepository;
			_gameSpecifications = gameSpecifications;
			_gameAlgorithms = gameAlgorithms;
		}

		public GameModel New()
		{
			var game = _gameFactory.CreateNew();
			_gameRepository.Save(game);
			var model = _modelFactory.CreateFrom(game);
			return model;
		}

		public GameModel PerformMove(PerformMoveRequest request)
		{
			var game = _gameRepository.Get(request.GameId);
			var boardPosition = BoardPosition.CreateFrom(request.SelectedColumn, request.SelectedRow);
			var isMoveLegitimate = _gameSpecifications.IsMoveLegitimate(game, Enums.PlayerType.Human, boardPosition);
			if(!isMoveLegitimate)
			{
				var moveWarnings = new List<ValidationError> 
				{ 
					new ValidationError { Type = "InvalidMove", Message = "Sorry this move is not legal, try another move to keep playing." }
				};
				return _modelFactory.CreateFrom(game, moveWarnings);
			}

			var playerMove = _gameFactory.CreateFrom(Enums.PlayerType.Human, boardPosition);
			game.AddMove(playerMove);

			if(!_gameSpecifications.IsGameOver(game)) 
			{
				var computerMove = _gameAlgorithms.DetermineNextMove(game);
				game.AddMove(computerMove);
			}

			var isGameOver = _gameSpecifications.IsGameOver(game);
			game.IsGameOver = isGameOver;

			var model = _modelFactory.CreateFrom(game);

			var isComputerWinner = isGameOver && _gameSpecifications.IsComputerWinner(game);
			model.IsComputerWinner = isComputerWinner;

			var isPlayerWinner = isGameOver && _gameSpecifications.IsPlayerWinner(game);
			model.IsPlayerWinner = isPlayerWinner;

			var isGameDraw = isGameOver && !isComputerWinner && !isPlayerWinner;
			model.IsGameDraw = isGameDraw;

			_gameRepository.Save(game);

			return model;
		}
	}
}