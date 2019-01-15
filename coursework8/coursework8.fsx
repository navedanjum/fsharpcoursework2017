(*
  ITT8060 -- Advanced Programming 2017
  Department of Computer Science
  Tallinn University of Technology
  ------------------------------------------------------------------------------

  Coursework 8: Sequences and computation expressions

  ------------------------------------------------------------------------------
  Name: Navedanjum Ansari
  Student ID: naansa@ttu.ee/172667IVSM
  ------------------------------------------------------------------------------


  Answer the questions below. You answers to the questions should be correct F#
  code written after the question. This file is an F# script file; it should be
  possible to load the whole file at once. If you can't, then you have
  introduced a syntax error somewhere.

  This coursework will be graded.

  Commit and push your script part of the solution to the repository as file
  coursework8.fsx in directory coursework8.


  The deadline for completing the above procedure is Friday, December 1, 2017.

  We will consider the submission to be the latest version of the appropriate
  files in the appropriate directory before the deadline of a particular
  coursework.

*)

(*
  Task 1:

  Define a sequence
  
  evenOnes : int seq
  
  that contains integer representation of numbers for which the binary
  represenation contains an even number of ones in ascending order.
  
  e.g.   0 -> 0
        11 -> 3
       101 -> 5 
       110 -> 6
etc
*)


//Reference: Brian Kernighan’s Algorithm
//http://www.geeksforgeeks.org/count-set-bits-in-an-integer/

let int2binary (num:int) =
   let mutable b = num
   let mutable count = 0
   while (b <>  0) do
       b <- b &&& (b- 1)
       count <- count + 1
   count

let evenOnes = Seq.initInfinite (fun x -> x) |> Seq.filter(fun x -> ((int2binary x)%2=0)) 


(*
  Task 2:

  Define a function fourthRoot : float -> float option that returns Some x if x
  is the 4th root of the argument, and None if the argument has no 4th root. In
  your implementation, use the squareRoot function from the lecture and
  computation expressions for the option type as defined in the lecture.
*)

type OptionBuilder () =
   member this.Bind (opt, f) = Option.bind f opt
   member this.Return x      = Some x

let option = OptionBuilder()

let squareRoot x =
    if x >= 0.0 then Some (sqrt x) else None

let fourthRoot y = 
    option{
        let! res1 = squareRoot y
        let! res2 = squareRoot res1
        return res2
    }

//Test
//fourthRoot 16.00


(*
  Task 3:

  A function from a type 'env to a type 'a can be seen as a computation that
  computes a value of type 'a based on an environment of type 'env. We call such
  a computation a reader computation, since compared to ordinary computations,
  it can read the given environment. Below you find the following:

    • the definition of a builder that lets you express reader computations
      using computation expressions

    • the definition of a reader computation ask : 'env -> 'env that returns the
      environment

    • the definition of a function runReader : ('env -> 'a) -> 'env -> 'a that
      runs a reader computation on a given environment

    • the definition of a type Expr of arithmetic expressions

  Implement a function eval : Expr -> Map<string, int> -> int that evaluates
  an expression using an environment which maps identifiers to values. Use
  computation expressions for reader computations in your implementation. Note
  that partially applying eval to just an expression will yield a function of
  type map <string, int> -> int, which can be considered a reader computation.
  This observation is the key to using computation expressions.
*)

type ReaderBuilder () =
  member this.Bind   (reader, f) = fun env -> f (reader env) env
  member this.Return x           = fun _   -> x

let reader = new ReaderBuilder ()

let ask = id
let runReader = (<|)


type Expr =
  | Const of int
  | Ident of string
  | Sum   of Expr * Expr
  | Diff  of Expr * Expr
  | Prod  of Expr * Expr
  | Let   of string * Expr * Expr


//Answer


let rec evalP (expr) env  =
   match expr with
    | Sum(exp1,exp2) -> evalP exp1 env + evalP exp2 env
    | Prod(exp1,exp2) -> evalP exp1 env * evalP exp2 env
    | Diff(exp1,exp2) -> evalP exp1 env - evalP exp2 env
    | Let(var,right,main_expr) -> let a1 = evalP right env
                                  let env1 = Map.add var a1 env
                                  evalP main_expr env1
    | Ident(var) -> Map.find var env
    | Const(n) -> n   ;;
  
let eval syntaxTree = 
    reader {
        let! x = evalP syntaxTree
        return x
    }

//let a = 5 in (a + 1) * 6 ;;
let expr = Let("a", Const 6, Prod(Sum(Ident("a"),Const 1) , Const 6)) 
let mymap = Map.empty
ask(eval expr mymap)



