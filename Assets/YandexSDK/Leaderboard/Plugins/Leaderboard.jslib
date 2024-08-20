mergeInto(LibraryManager.library,
{
	SetLeaderboardScoreInternal: function (name, score)
	{
		SetLeaderboardScore(UTF8ToString(name), score);
	},
	
	GetLeaderboardScoresInternal: function (name, maxPlayers, quantityTop, quantityAround, photoSize, auth)
	{
		GetLeaderboardScores(UTF8ToString(name), maxPlayers, quantityTop, quantityAround, UTF8ToString(photoSize), auth);
	}
});