module coursework7

(*
  ITT8060 -- Advanced Programming 2017
  Department of Computer Science
  Tallinn University of Technology
  ------------------------------------------------

  Coursework 7: Property based testing

  ------------------------------------------------
  Name: Navedanjum Ansari 
  Student ID: naansa@ttu.ee/172667IVSM
  ------------------------------------------------



*)


#if INTERACTIVE
#r "FsCheck"
#r "nunit.framework"
#r "FsCheck.NUnit"
#endif


open NUnit.Framework
open FsCheck.NUnit
open FsCheck
open FsCheck.Random
open NUnit.Framework


let rec isPalindrome (xs: int list) =
  match xs with
    | []        -> true
    | (x :: xs) -> match List.rev xs with
                     | []        -> true
                     | (y :: ys) -> x = y && isPalindrome ys ;;
 
 let toPalindrome (xs: int list) =
  let len       = List.length xs
  let suffixLen = len / 2
  let prefixLen = if 2 * suffixLen = len then suffixLen else suffixLen + 1
  let take n xs = Seq.toList (Seq.take n xs)
  take prefixLen xs @ List.rev (take suffixLen xs) ;;

  let onlyPalindromesList (xs:int list) = 
    Arb.generate<int list> 
    |> Gen.map (fun xs -> toPalindrome xs) ;;

  
  let palindromeIsEqualToItsReverseWithGen xs =
    Prop.forAll (Arb.fromGen (onlyPalindromesList xs)) 
                isPalindrome              ;;

let palindromeIsEqualToItsReverse xs = (xs = List.rev xs) ==> isPalindrome xs ;;

[<TestFixture>]
type Properties() =

    [<Property>]
    member this.twoListConcatenationOrderPreservation (xs: list<int>) (ys: list<int>) = 
                       List.rev xs @ List.rev ys = List.rev(ys @ xs) 

    [<Property>]
    member this.lengthOfListOfListsPreservedInConcatenation (xs:list<list<int>>) =
                       List.fold(fun acc y -> acc + List.length y)0 xs = List.length(List.concat xs)

    [<Property>]
    member this.palindromeIsEqualToItsReverseWithGen xs =
                    Prop.forAll (Arb.fromGen (onlyPalindromesList xs)) 
                                isPalindrome


     [<Property>]
      member this.palindromeIsEqualToItsReverseWithGenAndStats xs = 
                      palindromeIsEqualToItsReverseWithGen xs
                      |> Prop.trivial (List.length xs = 0)
                      |> Prop.collect (List.length xs)

 //Ignored two failing tests
 [<TestFixture>]
  type TestAttribute() =

     [<Test>]
     [<Ignore("Ignore a test")>]
     member this.palindromeIsEqualToItsReverse xs = (xs = List.rev xs) ==> isPalindrome xs
                 


     [<Test>]
     [<Ignore("Ignore a test")>]
     member this.palindromeIsEqualToItsReverseWithStats xs = 
                      palindromeIsEqualToItsReverse xs
                      |> Prop.trivial (List.length xs = 0)
                      |> Prop.collect (List.length xs) 
             
            

    
    
