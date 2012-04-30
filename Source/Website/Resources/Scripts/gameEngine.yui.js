/* --------------------------------------------------- *\
	Namespaces
\* --------------------------------------------------- */

ttt = {};
ttt.input = {};
ttt.game = {};


/* ------------------------------------------- *\
	Input
\* ------------------------------------------- */

ttt.input.touchclickEvent = ('createTouch' in document) ? 'touchend' : 'click';


/* --------------------------------------------------- *\
	Game Controller
\* --------------------------------------------------- */

ttt.game.Controller = function () {
	var _settings;
	var _elements;

	this.init = function (options, elements) {
		_settings = $.extend(options, {
		});
		_elements = elements;

		_elements.boardPositionDisplays.bind(ttt.input.touchclickEvent, function () {
			chooseBoardPosition($(this));
		});
	};

	function chooseBoardPosition(selectedElement) {
		var id = selectedElement.attr("id");
		performMoveCheck(id);
	}

	function performMoveCheck(positionId) {
		var gameId = _settings.gameId;
		var column = positionId.substring(0, 1);
		var row = positionId.substring(1, 2);
		var data = {
			GameId: gameId,
			Owner: "Human",
			SelectedColumn: column,
			SelectedRow: row
		};
		$.get(_settings.urls.performMove, data, function (result) {
			processMoveResponse(result);
		});
	}

	function processMoveResponse(data) {
		_elements.messageContainer.removeClass("error");
		_elements.messageContainer.html("Good move, try another.");
		displayMoveWarnings(data.MoveWarnings);
		var gameMoves = data.GameMoves;
		drawMoves(gameMoves);

		console.log(data);

		if (data.IsGameOver) {
			if (data.IsComputerWinner) {
				endGame("Game Over! The computer has won!");
			}
			if (data.IsPlayerWinner) {
				endGame("Oh Noes!!! You beat the game! That really wasn't supposed to be possible.");
			}
			if (data.IsGameDraw) {
				endGame("Game over! The computer has placed you in a draw match.");
			}
		}
	}

	function endGame(message) {
		_elements.boardPositionDisplays.unbind();
		_elements.messageContainer.html(message);
		_elements.messageContainer.addClass("gameOver");
		_elements.startNewGame.removeClass("noDisplay");
	}

	function drawMoves(gameMoves) {
		for (var i = 0; i < gameMoves.length; i++) {
			var move = gameMoves[i];
			var position = move.Position;
			var owner = move.Owner;
			var element = $("#" + position.Column + position.Row);
			element.removeClass("human").removeClass("computer");
			element.addClass(owner.toLowerCase());
			if (owner === "Human") {
				element.html("O");
			} else {
				element.html("X");
			}
		}
	}

	function displayMoveWarnings(moveWarnings) {
		if (moveWarnings.length === 0) { return; }
		var warnings = "";
		for (var i = 0; i < moveWarnings.length; i++) {
			warnings += "<div>" + moveWarnings[i].Message + "</div>";
		}
		_elements.messageContainer.html(warnings);
		_elements.messageContainer.addClass("error");
	}
};