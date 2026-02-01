EXTERNAL ChangeReputation(amount)
EXTERNAL EndEarly()
VAR closet = false

Welcome to my home little one
Are you in need of anything?
* [No miss, thank you for the shelter]
    ~ ChangeReputation(10)
    But of course, I wouldn't want you rusting out there
    Speaking of, let me get you a nice cup of oil for your gears
* [A drink would be nice]
    ~ ChangeReputation(-5)
    Ah of course, I can prepare some oil for you!
* [Id really like to go home actually]
    ~ ChangeReputation(-25)
    Oh dear I'd rather you'd not put yourself in danger, just stay here.
    I can give you some oil, would that be nice?
    * * [That would be lovely]
        ~ ChangeReputation(15)
        Ill get started on it right away!
    * * [I suppose....]
        ~ ChangeReputation(10)
        Alright! It will cheer you right up
    * * [I really want to go home]
        ~ ChangeReputation(-10)
        I insist you stay here, ill go make you that cup of oil
- I believe theres some messes I haven't cleaned around the house.
- If you would be so kind, I'd really appreciate it if you'd sweep them up before tea!
-> END