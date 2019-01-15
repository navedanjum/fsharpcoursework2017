(*

  Department of Software Science
  Tallinn University of Technology
  ------------------------------------

  Coursework 1: Basic operations on lists

  ------------------------------------
  Name: Ansari Navedanjum
  TUT Student ID: naansa@ttu.ee
  ------------------------------------


  Answer all the questions below.  You answers to questions should be
  correct F# code written after the question in comments. This file is an F#
  script file, it should be possible to load the whole file at
  once. If you can't then you have introduced a syntax error
  somewhere and your result will not be evaluated.

  This coursework will be graded.

  To submit the coursework you will be asked to
  
  1) Check out your  GIT repository
  from the server gitlab.cs.ttu.ee using instructions on page
  https://courses.cs.ttu.ee/pages/ITT8060

  2) Put your solution into a file coursework1/coursework1.fsx
  in the repository. Commit it and push it to the server!
  It is your responsibility to make sure you have pushed the solution
  to the repository!

  NB! It is very important to make sure you use the exact name using
  only small caps. Files submitted under wrong name may not get a grade.

  Also, use the exact function and identifier names with precise types as 
  specified in the question.

 
*)

// 1. Associate an identifier "myFirstList" with an empty list of type 'float list' (or list<float>).
let myFirstList = List.empty<float>

// 2. Write a function
// count 'a list -> int
// that will return a number of elements in a list. 
let list1 = [for a in 1..3 do yield! [ a .. a + 2 ] ]
printfn "%A" list1

let count list : int =
   List.length list

let res = count list1


// 3. Make a list of cantines available on TTU campus containing 4-tuples (quadruples).
// The identifier of the list should be "cantines". The 4-tuples should be of type string * string * int * int
// The elements of the 4-tuples should represent the following:
//   1) The name of the cantine
//   2) The building identifier
//   3) The closing hour as integer
//   4) The closing minutes as ingeger
 
let canteen  = [("Economics- and social science building canteen","SOC- building", 18, 30);
                ("Libary canteen","Akadeemia tee 1", 19, 00);
                ("Main building Deli cafe","U01 building", 16, 30);
                ("Main building Daily lunch restaurant","U01 building", 16, 30);
                ("U06 building canteen","U06 building", 16, 00);
                ("Natural Science building canteen","SCI building", 16, 00);
                ("ICT building canteen","Raja 15", 16,0);
                ("Sports building canteen","S01 building", 20, 0) ]

// Get length
printfn "Length : %i" canteen.Length
// Check if empty
printfn "Is Empty : %A" canteen.IsEmpty


// 4. Write a function currentlyOpen: int -> int -> (string * string * int * int) list -> string list
// that will return the building identifiers where cantines have not yet closed. The first argument is the
// current hours as integer, the second is the current minutes as integer, the third is the list of cantines 
// with the same type as you specified in previous question.
// Your solution should do the filtering explicitly.


 let currentlyOpen (hour:int) (min:int) (canlist: list<string* string * int * int>) = [for (a,b,c,d) in canlist do
                                                                                        if c > hour then yield b
                                                                                        elif (c = hour && d <= min) then yield b]

currentlyOpen 20 00 canteen


let currentlyOpen1 (hour:int) (min:int)  (canlist:(string * string * int * int)list) :list<string> =
    let mylist = List.skipWhile (fun (a,b,c,d) ->(hour > c)||(hour=c && min > d)) canlist;
    List.map(fun (a,b,c,d)->b) mylist

currentlyOpen1 16 00 canteen


// 5. Write a function currentlyOpen2: int -> int -> (string * string * int * int) list -> string list
// that behaves similarily to the currentlyOpen, but uses List.filter in its implementation.
let currentlyOpen2 (hour:int) (min:int)  (canlist:(string * string * int * int)list) :list<string>=   
    let mylist= List.filter (fun (a,b,c,d) ->(hour < c)||(hour=c&&min <= d)) canlist;
    List.map(fun (a,b,c,d)->b) mylist

currentlyOpen2 20 00 canteen

