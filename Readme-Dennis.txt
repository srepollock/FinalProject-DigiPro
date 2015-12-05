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
	- Filtering works. When selecting a large amount of data, and then selecting filter, you will however duplicate the original data
	- The filtered data (yes it DOES WORK) will be 'doubled' (as in the sound is kept while filtering new sound and both saved ontop of each other) but STILL FILTERED

When Compiling (Please read!)
--------------

One issue I found when compiling my code on a new computer with a fresh install of Visual Studio 2015 with C#/Visual Basic installed is that:
- 'SignTool.exe' cannot be found

To Fix:
- Open Programs and Features for your computer
- Select (right click) 'Microsoft Visual Stuido [version]' and click 'Change'
- And select 'ClickOnce Publishing Tools' for installation
