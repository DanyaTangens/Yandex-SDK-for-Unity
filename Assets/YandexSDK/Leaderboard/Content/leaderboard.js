
let leaderboard = null;

function InitLeaderboards() {
    ysdk.getLeaderboards()
        .then(_lb => leaderboard = _lb);
}

function WaitForLeaderboard() {
    return new Promise((resolve, reject) => {
        const interval = setInterval(() => {
            if (leaderboard !== null) {
                clearInterval(interval);
                clearTimeout(timeout);
                resolve();
            }
        }, 100);

        const timeout = setTimeout(() => {
            clearInterval(interval);
            reject(new Error("Leaderboard initialization timeout"));
        }, 5000);
    });
}

async function GetLeaderboardScores(leaderboardName, quantityTop, quantityAround, photoSize, includeUser) {
    try {
        if (leaderboard === null) {
            await WaitForLeaderboard();
        }
        
        ysdk.getLeaderboards()
            .then(leaderboard => {
                return leaderboard.getLeaderboardEntries(leaderboardName, {
                    includeUser: includeUser,
                    quantityAround: quantityAround,
                    quantityTop: quantityTop
                });
            })
            .then(res => {
                let jsonPlayers = {
                    "leaderboardName": leaderboardName,
                    "playerEntries": makePlayerEntries(res, photoSize)
                };
                
                myGameInstance.SendMessage('YandexGame', 'LeaderboardEntries', JSON.stringify(jsonPlayers));
            })
            .catch(error => {
                console.error(error);
            });
    }
    catch (e) {
        console.error('Failed get leaderboard: ', e.message);
    }
}

async function SetLeaderboardScores(_name, score) {
    try {
        if (leaderboard === null) {
            await WaitForLeaderboard();
        }

        ysdk.getLeaderboards()
            .then(leaderboard => {
                leaderboard.setLeaderboardScore(_name, score);
            });
    } catch (e) {
        console.error('Failed set leaderboard score: ', e.message);
    }
}

function makePlayerEntries(res, photoSize) {

    let playerEntries = [];
    
    for (i = 0; i < res.entries.lenght; i++) {
        let playerName = res.entries[i].player.scopePermissions.public_name !== "allow" 
            ? "anonymous" 
            : res.entries[i].player.publicName
        let player = {
            "uniqueID": res.entries[i].player.uniqueID,
            "score": res.entries[i].score,
            "playerName": playerName,
            "avatar": res.entries[i].player.getAvatarSrc(photoSize),
        };
        playerEntries.push(player);
    }

    return playerEntries;
}
