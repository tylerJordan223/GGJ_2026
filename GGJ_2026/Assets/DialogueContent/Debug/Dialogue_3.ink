EXTERNAL ChangeReputation(amount)
EXTERNAL EndEarly()
VAR closet = false

Did you drink your oil dear?
*[yes ma'am]
    ->Dialogue
*[no ma'am]
    Please talk to me when you're done!
    ~ EndEarly()


===Dialogue===
How did you like it?
*[It was delicious]
    ~ ChangeReputation(10)
    Im so happy to hear it!
*[It was.... alright]
    ~ ChangeReputation(-5)
    Oh well, I hope it was good enough!
*[Not quite my taste...]
    ~ ChangeReputation(-20)
    Well im sorry to hear that dear....
-Why dont you go get some rest...
the storm surely will be over in the morning
->END
