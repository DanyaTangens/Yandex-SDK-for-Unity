mergeInto(LibraryManager.library,
{
	GameplayReadyInternal: function() {
		if (ysdk !== null && ysdk.features.LoadingAPI !== undefined && ysdk.features.LoadingAPI !== null) {
			ysdk.features.LoadingAPI.ready();
			console.log('Game Ready');
		}
		else {
			console.error('Failed - Game Ready');
		}
	},
     	
    GameplayStartInternal: function () {
        if (ysdk !== null && ysdk.features !== undefined && ysdk.features.GameplayAPI !== undefined) {
            ysdk.features.GameplayAPI.start();
        }
        else {
            if (ysdk == null) console.error('Gameplay start rejected. The SDK is not initialized!');
            else console.error('Gameplay start undefined!');
        }
    },
    
    GameplayStopInternal: function () {
        if (ysdk !== null && ysdk.features !== undefined && ysdk.features.GameplayAPI !== undefined) {
            ysdk.features.GameplayAPI.stop();
        }
        else {
            if (ysdk == null) console.error('Gameplay stop rejected. The SDK is not initialized!');
            else console.error('Gameplay stop undefined!');
        }
    }
});
