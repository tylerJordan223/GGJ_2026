EXTERNAL ChangeReputation(amount)
EXTERNAL EndEarly()

Line of Dialogue 1
Line of Dialogue 2
* [Choice 1]
    ~ ChangeReputation(25)
    thats good
* [Choice 2]
    ~ ChangeReputation(50)
    thats lovely
* [Choice 3]
    ~ ChangeReputation(-25)
    thats horrible
* [Leave Dialogue]
    alright goodbye
    ~ EndEarly()
    -> END
-More dialogue here
