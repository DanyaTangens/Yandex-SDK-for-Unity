mergeInto(LibraryManager.library,
{
	InitPlayerInternal: function ()
	{
		var returnStr = playerData;
		var bufferSize = lengthBytesUTF8(returnStr) + 1;
		var buffer = _malloc(bufferSize);
		stringToUTF8(returnStr, buffer, bufferSize);
		
		return buffer;
	},
	
	OpenAuthDialogInternal: function ()
	{
		OpenAuthDialog();
	},
	
	RequestAuthInternal: function (sendback) {
        InitPlayer(sendback);
    }
});
