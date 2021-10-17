# WordlistGenerator
Smart words generator

It can generate smart words to recover passwords,
applying rules to filter words like sequencial repetition or normal repetition chars.

This program can generate specific wordlists and have checkpoint restore system to continue
generation on future.

### BUILD TUTORIAL ###
1 - Install Visual Studio 2019 or most recent version
2 - Start Visual Studio
3 - Click on clone repository
4 - Click on github and login in your account
5 - Set repository and clone
6 - When project is open click on Build and Build Solution, than this you can run the program

### USE EXAMPLE ###
1 - Open CMD and program folder
2 - Run command: Wordlistgenerator newSession --maxSequencialNormal 2 --minSize 1 --maxSize 12 --charColl abAB12@!

You can run "Wordlistgenerator help" to for view all arguments options
