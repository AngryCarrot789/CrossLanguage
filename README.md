# CrossLanguage
A program for calling functions between (at the moment) C# and Java, nowhere near done yet

the c# bit uses a ton of generics and it still doesnt work... yet

it doesnt really "call" functions.... more like it serialises the parameters, and sends the function name and serialised parameters 
over a serial port (or network...) and the receiver contains a map of functions key'd to the function name, and it calls functions like that.
