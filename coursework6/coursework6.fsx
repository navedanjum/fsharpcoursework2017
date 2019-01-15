open System

(*

  ITT8060 -- Advanced Programming 2017
  Department of Computer Science
  Tallinn University of Technology
  ------------------------------------------------

  Coursework 6: Tail recursion

  ------------------------------------------------
  Name: Navedanjum Ansari 
  Student ID: naansa@ttu.ee
              172667IVSM	
  ------------------------------------------------


  Answer the questions below. You answers to the questions should be correct F#
  code written after the question. This file is an F# script file; it should be
  possible to load the whole file at once. If you can't, then you have
  introduced a syntax error somewhere.

  This coursework will be graded.

  Commit and push your script part of the solution to the repository as file
  coursework6.fsx in directory coursework6.

  The deadline for completing the above procedure is Friday, November 10, 2017.

  We will consider the submission to be the latest version of the appropriate
  files in the appropriate directory before the deadline of a particular
  coursework.

*)

(*
  Task 1:

  Write a function minInList : float list -> float that returns the minimum element
  in the given list. Make sure your implementation uses tail recursion.
*)

 let rec minInList (li:list<float>) : float =
    match li with
    | [] -> failwith "Empty List"
    | [x] -> x
    | x::xs -> min x (minInList xs) ;;

//Test
let list1 = [1.0;2.0;3.0;4.0;-4.0;-3.0;-2.0;-1.0]
let list2 = [9.0]  
minInList list1
minInList list2

(*
  Task 2:

  Write a function swapElementsInList : 'a list -> 'a list that swaps the 1st
  element with the second, the 3rd with the 4th, etc. Make sure your
  implementation uses tail recursion.
*)

let rec swapElementsInList (li: list<'a>):list<'a> = 
    match li with
    | [] -> failwith "Empty List"
    | [x;y] -> [y;x]
    | [x] -> [x]
    | x1::x2::xs -> x2::x1::(swapElementsInList xs) ;;

//Test
let list3 = ["Hello";"Hi";"Welcome";"Bye"]  
swapElementsInList list1
swapElementsInList list3



(*
  Task 3:

  Below you find the definition of a type Tree of leaf-labeled trees. Write a
  function minInTree : float Tree -> float that returns the minimum label in the
  given tree. Use continuation-passing style in your implementation.
*)

type 'a Tree =
  | Leaf   of 'a
  | Branch of 'a Tree * 'a Tree

let rec minInTree (t:Tree<float>)  =
    match t with
    | Leaf(n) ->  n
    | Branch(left, right) -> 
        min (minInTree left) (minInTree right) ;;

//Test
minInTree (Branch(Branch(Leaf(12.0),Leaf(13.0)),Branch(Leaf(1.9),Leaf(12.6))))


(* 
  Task 4:

  Write a function minInTree' : int Tree -> int that returns the minimum label
  in the given tree, like the function minInTree from Task 3 does. Use
  continuation-passing style in combination with accumulation in your
  implementation.
*)

let minInTree' (t:Tree<int>): int =
    let rec cont (t:Tree<int>) (c) =
        match t with
        | Leaf(n) -> c n
        | Branch(left, right) -> 
             cont left (fun f1  ->
                cont right (fun f2  ->
                  c(min f1 f2 ))) 
    cont t id ;;

//Test
minInTree' (Branch(Branch(Leaf(0),Leaf(1)),Branch(Leaf(1),Leaf(-12)))) 
