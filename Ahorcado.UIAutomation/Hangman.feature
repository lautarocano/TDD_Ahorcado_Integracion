Feature: Hangman
	In order to play the game
	As a player
	I want to guess a word and know if I won or not

@mytag
Scenario: Lose the game
	Given I have entered Ahorcado as the wordToGuess
	When I enter X as the typedLetter five times
	Then I should be told that I lost

Scenario: Hit a letter
	Given I have entered Hola as the wordToGuess
	When I enter A as the typedLetter one time
	Then I should be told that I hit the letter

Scenario: Risk a number
	Given I have entered Computadora as the wordToGuess
	When I enter 4 as the typedLetter one time
	Then It should tell me that the letter is invalid

Scenario: Hit a Secret Word
	Given I have entered Teclado as the wordToGuess
	When I enter Teclado as the typedLetter
	Then I should be told that I win

Scenario: Insert a non-alphabetic Secret Word
	Given I have entered 123 as the wordToGuess
	Then It should tell me that the word is invalid