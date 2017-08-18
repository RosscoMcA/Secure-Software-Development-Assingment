
from tkinter import *

#Adds the main body of GUI
root = Tk()

LblOne = Label(root, text="Test", bg="red",fg="white")

LblOne.pack()

LblTwo = Label(root, text="Test2", bg="green", fg="black")

#Width remains consistent
LblTwo.pack(fill=X)

LblThree = Label(root, text="three", bg="white", fg="black")
#height remains consistent
LblThree.pack(side=LEFT, fill=Y)


#Maintains lifetime of window
root.mainloop()




