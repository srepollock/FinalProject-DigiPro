Changes since Demo
------------------
- Threading DFT
	- Max of 4 threads
-Playing/Recording
	- Can play and record audio through DLL imports of C functions
	Known issues:
		- Cannot stop playing
		- Can press play button again while playing
			- Pressing multiple times will crash the program
- Cut/Copy/Paste/Delete
	- All now work inside the program, and you can cut/copy between different files
- Filtering*
	- Filtering an audio file works, however entire data will be 'doubled' (as in the sound is kept while filtering new sound and both saved ontop of each other) but STILL FILTERED
