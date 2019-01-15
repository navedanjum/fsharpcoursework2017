open System


(*

  ITT8060 -- Advanced Programming 2017
  Department of Computer Science
  Tallinn University of Technology
  ------------------------------------

  Coursework 2: Operations on lists and tuples, recursion, combination of functions

  ------------------------------------
  Name: Ansari Navedanjum
  TUT Student ID: naansa@ttu.ee
  ------------------------------------


  Answer the questions below.  You answers to all questions should be
  correct F# code written after the question. This file is an F#
  script file, it should be possible to load the whole file at
  once. If you can't then you have introduced a syntax error
  somewhere.

  This coursework will be graded. It has to be submitted to the https://gitlab.cs.ttu.ee
  repository itt8060 under your name, into a file coursework2/coursework2.fsx by September 29, 2017.
  
  NB! Note that the solution has to be an F# script file!

  If the location, extension or name of the submission file or directory is incorrect it will not be graded.
*)

// 1. Create a type BibliographyItem that has the following structure:
// string list * string * int * (int * int)
// The meaning of the tuple elements is as follows:
// * The first field represents the list of author names where each name is in the format
//   "Lastname, Firstname1 Firstname2" (i.e. listing all first names after comma)
// * The second field represents the title of the publication
// * The third field represents the year of publication
// * The fourth field represents a pair containing the starting page number and ending page number of the paper.

type BibliographyItem = string list * string * int * (int * int)

// 2. Create a value bibliographyData : BibliographyItem list that contains
// at least 10 different publications on your favourite topic from http://dblp.uni-trier.de/ 
// Please note that you need not read the papers, just pick 10 papers that sound interesting to you from the database.

let BibliographyData:BibliographyItem list = [
  (["Gnatchuk, Hanna"], "A bibliography of quantitative studies on sound symbolism", 2015 , (86,90))
  (["Kelih , Emmerich" ; "Grzybek ,Peter"],"Bibliography: Glottometrics 1-30", 2015, (89,102))
  (["Vovk, Vladimir"; "Gammerman, Vladimir"], "Alexey Chervonenkis's bibliography: introductory comments", 2015, (2051,2066))
  (["Gültepe, Yasemin"], "Querying Bibliography Data Based on Linked Data", 2015, (1014,1020))
  (["Deriche, Mohamed"; "Qureshi, Muhammad Ali"],"A bibliography of pixel-based blind image forgery detection techniques", 2015, (46,74))
  (["Yang, Bo"; "Ma, Kun"], "A simple scheme for bibliography acquisition using DOI content negotiation proxy", 2014, (806,824))
  (["Sureka, Ashish"; "Nitish, Mittal"; "Agarwal,Swati"], "Minority ethnic groups in computer science research: what is the bibliography data telling us?", 2017, (5,15))
  (["Pankowski, Tadeusz"],"Exploring Ontology-Enhanced Bibliography Databases Using Faceted Search", 2017, (27,39))
  (["Purdy, Robert";"Zygmunt, Jan"], "Adolf Lindenbaum: Notes on his Life, with Bibliography and Selected References", 2014, (285,320))
  (["Nowinski, Aleksander"], "Current Bibliography Research Information Systems in Poland", 2014 , (174,178))
  ]
   


// 3. Make a function compareLists : string list -> string list -> int that takes two string lists and
// returns 
// * Less than zero in case the first list precedes the second in the sort order;
// * Zero in case the first list and second list occur at the same position in the sort order;
// * Greater than zero in case the first list follows the second list in the sort order;
// You are encouraged to use String.Compare to compare individual strings. If the first authors are the same
// then the precedence should be determined by the next author.
// A missing author can be considered to be equivalent to an empty string.
// Please note that your implementation should be recursive over the input lists.

let rec compareLists (list1 :  list<string>) (list2:  list<string>):int =
    match list1, list2 with
    | [],[] -> 0
    | head::tail, head2::tail2 when compare head head2 = 0 -> compareLists tail tail2
    | head::tail, head2::tail2 when compare head head2 > 0 -> 1
    | _ -> -1  ;;

//Test
let list1 : string list = ["a"; "b"; "d"]
let list2 : string list = ["a"; "b"; "c"]
let list3 = []
compareLists list1 list2



// 4. Make a function
// compareAuthors : BibliographyItem -> BibliographyItem -> int
// that takes two instances of bibliography items and compares them according to the authors.
// Use solution from task 3.

let compareAuthors (btem1:BibliographyItem)  (btem2:BibliographyItem) :int =
    let (a,_,_,_) = btem1
    let (b,_,_,_) = btem2
    compareLists a b  ;;

//Test
compareAuthors BibliographyData.[0] BibliographyData.[0]


// 5. Make a function
// compareAuthorsYears : BibliographyItem -> BibliographyItem -> int
// that takes two instances of bibliography items and compares them according to the authors and if the authors are 
// the same then according to years.


let compareAuthorsYears (btem3:BibliographyItem)  (btem4:BibliographyItem) :int =
    let (_,_,y1,_) = btem3
    let (_,_,y2,_) = btem4
    let res = compareAuthors btem3 btem4 
    match res with 
       | 0 -> compare y1 y2 
       | 1 -> 1
       | _ -> -1  ;;

//Test
compareAuthorsYears BibliographyData.[1] BibliographyData.[0]



// 6. Make a function 
// sortBibliographyByYear : BibliographyItem list -> BibliographyItem list
// That returns a bibliography sorted according to the year in ascending order

let sortBibliographyByYear (blist1:list<BibliographyItem>) : list<BibliographyItem> =
    List.sortBy (fun (_,_,y,_) -> y) blist1 ;;

//Test
sortBibliographyByYear BibliographyData


// 7. Make a function 
// sortBibliographyByAuthorYear : BibliographyItem list -> BibliographyItem list
// That returns a bibliography sorted according to the authors and year in ascending order

let sortBibliographyByAuthorYear (blist2:list<BibliographyItem>) : list<BibliographyItem> =
    List.sortBy (fun (a,_,y,_) -> a,y) blist2 ;;

//Test
sortBibliographyByAuthorYear BibliographyData

// 8. Make a function
// groupByYear : BibliographyItem list -> BibliographyItem list list
// where the return list contains lists of bibliography items published in the same year.

let groupByYear (blist3:list<BibliographyItem>) : list< list<BibliographyItem> > =
   let thrd (_, _, c,_) = c
   blist3
   |> List.groupBy thrd
   |> Seq.map (fun (key, values) -> (values))
   |> Seq.toList ;;

//Test
groupByYear BibliographyData
        

// 9. Make a function
// commaSeparatedList : BibliographyItem list -> string
// That will return a comma separated string representation of the data.
// Use function composition operator "<<" in your implementation.  

let commaSeparatedList (blist4:list<BibliographyItem>):string = 
    let strReplace (str:string) = str.Replace( ")(" , "," )
    (strReplace << List.fold (fun str x -> str + x.ToString())"" )blist4 

///Test
commaSeparatedList BibliographyData


