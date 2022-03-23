I developed a simple but efficient solution for the presented problem. The main difficulty was to simulate the matrix that should be searched, so instead of trying to do that literally I thought of it as two arrays: one having all the inputs entered horizontally separated by empty spaces and another having all the characters concatenated vertically as if they were concatenated columns. Then I could wander through the list searching for the words entered for the word steam. I made some validations to follow the instructions on the given document, like putting a limitation of characters for the rows entered for the matrix, so all the rows would have the same length. I also ordered the results by the most repeated words and counted the found words just once.
This is the data I made for a successfully use of the application. Feel free to try it with any other combination of matrix and steam words.
A	C	H	I	L	L	C
C	O	L	D	S	F	O
K	L	R	X	N	Z	L
E	D	U	C	O	L	D
W	I	N	D	W	X	E
Y	A	C	H	I	L	L
K	R	X	A	Y	R	Z

The input data for the matrix should be the following:
achillc
coldsfo
klrxnzl
educold
windwxe
yachill
krxayrz

The input data for the word steam should be the following:
chill
cold
wind
snow

The output data should be the following:
cold – chill – wind – snow  
