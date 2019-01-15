(*

  ITT8060 -- Advanced Programming 2017
  Department of Computer Science
  Tallinn University of Technology
  ------------------------------------------------------------------------------

  Coursework 9: Asynchronous and reactive programming

  ------------------------------------------------------------------------------
  Name: Navedanjum Ansari
  Student ID: 172667IVSM
  ------------------------------------------------------------------------------


  Answer the questions below. You answers to the questions should be correct F#
  code written after the question. This file is an F# script file; it should be
  possible to load the whole file at once. If you can't, then you have
  introduced a syntax error somewhere.

  This coursework will be graded.

  Commit and push your script part of the solution to the repository as file
  coursework9.fsx in directory coursework9.

  Please do not upload DLL-s. Just include a readme.txt file containing the
  dependencies required (additional DLLs), if they are required.

  The deadline for completing the above procedure is Friday, December 8, 2017.

  We will consider the submission to be the latest version of the appropriate
  files in the appropriate directory before the deadline of a particular
  coursework.

*)

(*
  Task 1:

  Write a function downloadParallel : (string * string) list -> Async<string []> that takes
  a list of name and URLs pairs and downloads the resources referenced by these URLs in
  parallel. Use the function fetchAsync from the lecture in your
  implementation.
*)

open System.Net
open System.IO
open Microsoft.FSharp.Control.CommonExtensions


let readToEndAsync (reader : StreamReader) =
    Async.AwaitTask (reader.ReadToEndAsync())

let downloadParallel (nm, url:string) =
    async {  
        let request = HttpWebRequest.Create(url)
        let! response = request.AsyncGetResponse()
        use response = response
        let stream = response.GetResponseStream()
        use reader = new StreamReader (stream)
        let! text = readToEndAsync reader 
        return text
        }   


(*
  Task 2:

  Write a function downloadSemiParallel : (string * string) list -> Async<string []> that
  takes a list of URLs and downloads the resources referenced by these URLs.
  Resources from URLs with the same domain name shall be downloaded
  sequentially, but otherwise, parallelism shall be used. The order of the
  resources in the resulting array can be chosen by you.
*)





(*
  Task 3:

  Write an event stream additions : IObservable<string*string> that emits an event
  everytime a file is created in the current directory. Each such event shall
  carry the name of the created file and the creation time as string tupled together.

  Furthermore, write an event stream removals : IObservable<string*string> that emits
  an event everytime a file is removed from the current directory. Each such
  event shall carry the name of the removed file and the deletion time as string.
*)

let w = new FileSystemWatcher((@"."), EnableRaisingEvents = true)

let isVisible (eventArgs : FileSystemEventArgs) =
  let hidden = FileAttributes.Hidden
  (File.GetAttributes(eventArgs.FullPath) &&& hidden) <> hidden

let reactCallbackC () = w.Created.Add (fun eventArgs -> 
   if not(isVisible eventArgs) then
     printfn "%s\n created to \n %s" (eventArgs.FullPath) (System.DateTime.Now.ToString())
)
let reactCallbackD () = w.Deleted.Add (fun eventArgs -> 
   if isVisible eventArgs then
     printfn "%s\n deleted to \n %s" (eventArgs.FullPath) (System.DateTime.Now.ToString())
)



(*
  Task 4:

  Below you find the definition of a type Change whose values represent changes
  to a directory. Use the event streams from Task 3 to define an event stream
  changes : IObservable<Change> of all file additions and removals in the
  current directory.
*)

type Change =
  | Addition of string*string
  | Removal  of string*string

let eventData = w.Created
let changes = 
    Observable.merge (eventData|> Observable.map (fun e  ->Addition (e.Name,System.DateTime.Now.ToString()))) (eventData |> Observable.map (fun e ->Removal (e.Name,System.DateTime.Now.ToString())))
                  

(*
  Task 5:

  Use the event stream changes from Task 4 to define an event stream
  turnover : IObservable<int> that tells at every file addition or removal how
  much the number of files in this directory has increased since the beginning
  (with negative numbers signifying a decrease). For example, if two files are
  added and one file is removed afterwards, there should be three events, that
  carry the numbers 1, 2, and 1, respectively.
*)


let turnover = match eventData with 
    |w.Created -> Observable.map (fun e  ->Addition (e.Name,System.DateTime.Now.ToString()))
               |> Observable.scan (fun count _ -> count + 1) 0)
    |w.Deleted -> Observable.map (fun e ->Removal (e.Name,System.DateTime.Now.ToString()))
               |>  Observable.scan (fun count _ -> count - 1) 0)
    |_ -> 0


