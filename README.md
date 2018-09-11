# Console LFG
Console LFG is an interactive web application where users can create lobbies to "look-for-groups" to play with via Xbox or Playstation consoles.

## Motivation
A re-occuring issue in the gaming community is that some games require multiple-team members to progress in a certain in-game level or event. If you are a gamer with many friends, this shouldn't be an issue. But if your a gamer that doesn't have a huge friends list or friends that don't play the same game titles, it may be difficult to find players to play with. Despite Xbox having an in-console feature to "look-for-groups" to play with to solve this issue, Playstation lacks this aspect. This motivated the creation of Console LFG, a one-stop-shop for both Xbox and Playstation gamers that wish to match-make with other players. 

## Technology
- C#
- ASP.NET Core
- Entity Framework 
- HTML5
- CSS3
- Bootstrap
- jQuery
- Ajax

## Features
- Users may either search active lobbies to find players to play with or start up a lobby with their own specs. 
- When submitting a lobby form, users have the option to select different tags to attract certain types of gamers e.g.  #Casual, #Competitive, #Mic, #NoMic, #AdultsOnly, etc.
- Upon lobby creation, the creator provides his/her Xbox or Playstation gamertag. This gamertag is then linked for other users. When a user clicks the link, he/she will be redirected to either the Xbox or Playstation account page for a single-sign-on to be able to add the creator to their friendslist on their respective console. 
- Users may filter by game titles. This is implemented with AJAX calls.
- A user may only create one lobby at a time. 
- Lobbies expire after two hours if no action is taken to remove the lobby.  

## Contributors
- Damian Korzeniewski
