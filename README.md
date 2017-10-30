# TheMoon (Server closed)
- AWS-chatbot challenge 2017. 
- Unity &amp; AWS Lex , Lambda..etc
## What Is **The Moon**?
- Simple, but new type game.
- You can chat with game-bot(called NPC) Sara and Alex.
- You made an emergency landing on the moon. Talk to Sara and Alex.

## Why is this special?
1. It's easy to implement the ChatBot through Amazon Lex and make it easy to play in the game.
2. You can manage the ChatBot separately from the game program.
3. You can talk directly to the person(NPC,Bot..) in the game.
4. Short term development (this project is designed to be completed a week before the end of the week! :))
5. New possibilities


## Development tools 
- Unity Engine(it can build -> Windows, Linux, WebGL, Android, Ios ..etc)
- AWS Lex , Lambda Web consoles

## How does it work?
1. In Unity , use the AWS Lambda sdk
2. Make a lambda function that could send the chatting dialog data to the AWS LEX. 
3. Develope the GameBot using Lex console. And just connect it.
4. Again in Unity, it can be receive the reply data from lex through the lambda function.

Unity ----> Lambda ----> Lex ---> Lambda for Lex ----> Lex ----> Lambda ---> Unity


_Oh, Very Simple!_

## Here You Can Meet Games Bots
- [http://ec2-34-227-224-117.compute-1.amazonaws.com/Alex.html] - Game Bot Alex (Sorry! Server closed)
- [http://ec2-34-227-224-117.compute-1.amazonaws.com/Sara.html] - Game Bot Sara (Server closed)

