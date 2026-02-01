EXTERNAL ChangeReputation(amount)
EXTERNAL EndEarly()
VAR closet = false

Did you sweep up all those messes?
*[yes ma'am]
    ->Dialogue
*[no ma'am]
    Please talk to me when you're done!
    ~ EndEarly()


===Dialogue===
Thank you so much for doing that dear!
Say, how would you say your joints feel?
*[A little creaky to be honest]
    ~ ChangeReputation(-5)
    Well hopefully this oil will help you feel better!
*[Nice and smooth]
    ~ ChangeReputation(20)
    Thats lovely! But they can always be smoother!
*[I haven't oiled them in years]
    ~ ChangeReputation(-20)
    Oh dear thats no good
- I set your oil on the coffee table, please take a drink!
Ill be here chopping up some bolts.
->END