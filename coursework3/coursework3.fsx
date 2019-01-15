open System.Diagnostics.Eventing.Reader
open System.Security.AccessControl

(*

  ITT8060 -- Advanced Programming 2017
  Department of Computer Science
  Tallinn University of Technology
  ------------------------------------

  Coursework 3: Discriminated unions, higher order functions

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
  repository itt8060 under your name, into a file coursework3/coursework3.fsx by October 11, 2017.
  NB! The deadline has been extended!
  
  NB! Note that the solution has to be an F# script file!

  If the location, extension or name of the submission file or directory is incorrect it will not be graded.
*)

// 1. Create a type BibliographyItem data structure based on discriminated unions such that it supports data
// for bibliography items for
// * article (journal paper)
// * inproceedings (conference paper)
// * book
// * MSc thesis
// * Web page (misc)
// Use the specifications given at http://newton.ex.ac.uk/tex/pack/bibtex/btxdoc/node6.html
// You should support all mandatory and optional fields of each entry. The names of the fields should
// be the same as in the referenced web page. You should capitalize the names of constructors in 
// discriminated unions.


type article = { authorNames: string list 
                 title: string
                 journal: string
                 year : int
                 volume: int option   
                 number: int option
                 pageNumber: (int * int) option
                 month : int option
                 note : string option }


type inproceedings = { authorNames: string list 
                       title: string
                       booktitle: string
                       year:int
                       editor: string option list
                       volume: int option
                       number: int option
                       series: string option
                       pageNumber: (int * int) option
                       address: string option
                       month: int option
                       organization: string option
                       publisher:string option
                       note: string option }


type book = { authorNames: string option list
              editor: string option list
              title: string
              publisher: string
              year: int
              volume: int option
              number: int option
              series: int option
              address: string option
              edition: string option
              month: int option 
              note: string option }


type mscthesis = { authorNames:string list
                   title:string
                   school:string
                   year:int
                   type_:string option
                   address: string option
                   month: int option
                   note: string option }



type misc = { authorNames:string option list
              title: string option
              howpublished: string option
              month: int option 
              year: int option
              note: string option }


type BibliographyItem =
    | Article of article
    | Inproceedings of inproceedings
    | Book of book 
    | MscThesis of mscthesis
    | Misc of misc



// 2. Create a value bibliographyData : BibliographyItem list that contains
// at least 10 different publications on your favourite topic from http://dblp.uni-trier.de/ 
// and the MSc thesis databases of UT and TTÜ. At least one instance of every publication needs to be 
// Please note that you need not read the papers, just pick 10 papers that sound interesting to you from the database.

let arc1 = Article{ authorNames = ["Athmane,Abdallaoui" ; "Keddour,Lemrabet"] ;
                 title = "Mechanical impedance of a thin layer in asymmetric elasticity" ;
                 journal = "Applied Mathematics and Computation" ;
                 year = 2018 ;
                 volume = None ;
                 number = Some 316 ;
                 pageNumber = Some (467,479);
                 month = None ;
                 note = None }

let arc2 = Article{ authorNames = ["Aziz,Tariq " ; "Ketjoy,Nipon"] ;
                 title = "PV Penetration Limits in Low Voltage Networks and Voltage Variations" ;
                 journal = "IEEE Access" ;
                 year = 2017 ;
                 volume = Some 5;
                 number = None ;
                 pageNumber = Some (16784,16792);
                 month = None ;
                 note = None }

let conf1 = Inproceedings{ authorNames = ["Liu,Rey-Long"] ;
              title = "Identification of Biomedical Articles with Highly Related Core Contents" ;
                       booktitle = "ACIIDS" ;
                       year = 2017 ;
                       editor = [None] ;
                       volume = None ;
                       number = Some 1 ;
                       series = None ;
                       pageNumber = Some(217,226) ;
                       address = None ;
                       month = None ;
                       organization = None ;
                       publisher = None ;
                       note = None }



let conf2 = Inproceedings{ authorNames = ["Cozz,Vittoria" ; "Petrocchi,Marinella" ; "Spognardi,Angelo"] ;
              title = "A Matter of Words: NLP for Quality Evaluation of Wikipedia Medical Articles" ;
                       booktitle = "ICWE" ;
                       year = 2016 ;
                       editor = [None] ;
                       volume = None ;
                       number = None ;
                       series = None ;
                       pageNumber = Some(448,456) ;
                       address = None ;
                       month = None ;
                       organization = None ;
                       publisher = None ;
                       note = None }


let bk1 = Book{ authorNames = [Some "Valencia,Rafael" ; Some "Andrade-Cetto,Juan"] ;
              editor = [None] ;
              title = "Mapping, Planning and Exploration with Pose SLAM" ;
              publisher = "Springer" ; 
              year = 2018 ;
              volume = Some 3 ;
              number = None ;
              series = None ;
              address = None ;
              edition = None ;
              month = None ;
              note = None }

let bk2 = Book{ authorNames = [Some "Dourish,Paul"] ;
              editor = [None] ;
              title = "The Stuff of Bits: An Essay on the Materialities of Information" ;
              publisher = "MIT Press"; 
              year = 2017 ;
              volume = None ;
              number = None ;
              series = None ;
              address = None ;
              edition = None ;
              month = None ;
              note = None }
                                

let thesis1 = MscThesis{ authorNames = ["Chang, Chii"];
                           title = "Indie Fog" ;
                           school = "University of Tartu" ;
                           year = 2012 ;
                           type_ = Some "Masters" ;
                           address = Some "Tartu,Estonia" ;
                           month = None;
                           note = None}   
                           

let thesis2 = MscThesis{ authorNames = ["Jakovits, Pelle"];
                           title = "Large Scale Data Processing" ;
                           school = "University of Tartu" ;
                           year = 2015 ;
                           type_ = Some "Masters" ;
                           address = Some "Tartu,Estonia" ;
                           month = None;
                           note = None }  

let misc1 = Misc{ authorNames = [Some "Parmar, Jyotsana"] ;
                   title = Some "Semantic Web Prefetching Using Semantic Relatedness between Web pages";
                   howpublished = Some "Website" ;
                   month = None ;
                   year = Some 2012 ;
                   note = None }

let misc2 = Misc{ authorNames = [Some "Rachael Barlow"] ;
                   title = Some "An Analysis of a Data Website Redesign at a Small Liberal Arts College";
                   howpublished = Some "IASST Conference" ;
                   month = None ;
                   year = Some 2012 ;
                   note = None }


let BibliographyData:BibliographyItem list = [arc1; arc2; conf1; conf2; bk1; bk2; thesis1; thesis2; misc1; misc2]


// 3. Create a function formatInACMReferenceStyle : BibliographyItem -> string that will format the bibliography items
// using the reference style specified here: http://www.acm.org/publications/authors/reference-formatting

let formatInACMReferenceStyle(btem1:BibliographyItem) : string =
    match btem1 with    
        | Article(btem1) -> btem1.authorNames
                            |> String.concat "," 
        | Inproceedings(btem1) -> btem1.title
        | Book(btem1) -> btem1.publisher
        | MscThesis(btem1) -> btem1.school
        | Misc(btem1) -> "Optional"
       
    
//Test
formatInACMReferenceStyle BibliographyData.[1]


// 4. Write a function compareByAuthorYear : BibliographyItem -> BibliographyItem -> int that will compare the authors and year of the
// bibliography item in the same way as specified in coursework2.
//This function is to know the type of discriminated union and extract year out of it

//Answer: Here I have created three other function to acheive the results yearExtractor function to extract the year
//        authorExtractor to extract the string list of authors and finally reused compareLists function from previous coursework
//        to find out if two list of authors are same if they are same then compareByAuthorYear which is our final function.

let yearExtractor(btem:BibliographyItem) =
    match btem with   
        | Article(btem) -> btem.year
        | Inproceedings(btem) -> btem.year
        | Book(btem) -> btem.year
        | MscThesis(btem) -> btem.year
        | Misc(btem) -> match btem.year with 
                           |None -> 0
                           |Some(x) -> x  ;;

//This function is to know the type of discriminated union and extract author strings out of it
let authorExtractor(btem:BibliographyItem) =
    match btem with   
        | Article(btem) -> btem.authorNames
        | Inproceedings(btem) -> btem.authorNames
        | Book(btem) -> match btem.authorNames with 
                           |[None] -> []
                           |[Some "str"] -> ["str"] 
                           |_ -> []

        | MscThesis(btem) -> btem.authorNames
        | Misc(btem) -> match btem.authorNames with 
                           |[None] -> []
                           |[Some "str"] -> ["str"] 
                           | _ -> []   ;;

// Resuing string list comparator function from previous coursework 
let rec compareLists (list1 :  list<string>) (list2:  list<string>):int =
    match list1, list2 with
    | [],[] -> 0
    | head::tail, head2::tail2 when compare head head2 = 0 -> compareLists tail tail2
    | head::tail, head2::tail2 when compare head head2 > 0 -> 1
    | _ -> -1

let compareAuthorsYears (btem3:BibliographyItem)  (btem4:BibliographyItem) :int =
    //compare by year if authors are same
    let y1 = yearExtractor(btem3)
    let y2 = yearExtractor(btem3)
    let s1:string list = authorExtractor(btem3)
    let s2:string list = authorExtractor(btem4)
    let res = compareLists s1 s2
    match res with 
       | 0 -> compare y1 y2 
       | 1 -> 1
       | _ -> -1  ;;

//Test 
compareAuthorsYears BibliographyData.[0] BibliographyData.[1]
compareAuthorsYears BibliographyData.[1] BibliographyData.[0]
compareAuthorsYears BibliographyData.[2] BibliographyData.[2]


// 5. Write a function orderBibliography: (BibliographyItem -> BibliographyItem -> int) -> BibliographyItem list -> BibliographyItem list
// That will order the list of bibliography items according to the given comparison function. 

// 6. Write a function formatBibliographyItems : (BibliographyItem -> string) -> BibliographyItem list -> string list that will take
// a formatting function and a bibliography list and produce a string list that contains formatted bibliography.

// 7. Write a function getNumberedBibliography : BibliographyItem list -> string
// that contains a numbered bibliography where each bibliography item is preceded with a sequence number surrounded
// by square brackets [] and ends with a newline character '\n'.
// The implementation should involve List.fold or List.foldBack function, whichever you deem appropriate.

let getNumberedBibliography (btem: BibliographyItem list)  =
    let i = 1
    btem
    |> List.fold (fun str x  -> str + sprintf"[%i]"i + x.ToString() + "\n" )""

//Test
getNumberedBibliography BibliographyData

// 8. Create 5 appropriate functions to create BibliographyItem data instances. Please note that 
// it is up to you to define the internal data structure. The following functions will be used for generating data in your
// format.
(* 
createArticle :
  author:string list ->
    title:string ->
      journal:string ->
        year:int ->
          volume:int option ->
            number:int option ->
              (int * int) option ->
                month:int option ->
                  note:string option -> BibliographyItem
*)

let createArticle (author:string list)(title:string)(journal:string)(year:int)(volume:int option)(number:int option)(pageNumber: (int*int) option)
                  (month:int option) (note:string option):BibliographyItem = 
    let arc = 
        Article{ authorNames = author;
        title = title ;
        journal = journal ;
        year = year ;
        volume = volume;
        number = number ;
        pageNumber = pageNumber ;
        month = month ;
        note =  note }
    arc    

(*
createBook :
  author:string option list ->
    editor:string option list ->
      title:string -> 
        publisher:string ->
          year:int ->
            volume:int option ->
              number:int option ->
                series:int option ->
                  address:string option ->
                    edition:string option ->
                      month:int option ->
                        note:string option -> BibliographyItem
*)

let createBook (author:string option list)(editor:string option list)(title:string)(publisher:string)(year:int)(volume:int option)(number:int option)
               (series: int option)(address:string option)(edition:string option)(month:int option) (note:string option):BibliographyItem = 
    let bk = 
        Book{ authorNames = author;
        editor = editor ;
        title = title ;
        publisher = publisher ;
        year = year ;
        volume = volume;
        number = number ;
        series = series ;
        address = address ;
        edition = edition ;
        month = month ;
        note =  note }
    bk    
     

(*
createInProceedings :
  author:string list->
    title:string ->
      booktitle:string ->
        year:int ->
          editor:string option list ->
            volume:int option ->
              number:int option ->
                series:string option ->
                  (int * int) option ->
                    address:string option ->
                      month:int option ->
                        organization:string option ->
                          publisher:string option ->
                            note:string option -> BibliographyItem
*)

let createInProceedings (author:string list)(title:string)(booktitle:string)(year:int)(editor:string option list)(volume:int option)(number:int option)
               (series: string option)(pageNumer: (int*int) option )(address:string option)(month:int option) 
               (organization:string option)(publisher:string option)(note:string option):BibliographyItem = 
    let conf = 
        Inproceedings{ authorNames = author;
        title = title ;
        booktitle = booktitle;
        year = year ;
        editor = editor;
        volume = volume;
        number = number ;
        series = series ;
        pageNumber = pageNumer ;
        address = address ;
        month = month ;
        organization = organization;
        publisher = publisher;
        note =  note }
    conf   
     


(*
createMScThesis :
  author:string list ->
    title:string ->
      school:string ->
        year:int ->
          type_:string option ->
            address:string option ->
              month:int option ->
                note:string option -> BibliographyItem

*)

let createMScThesis (author:string list)(title:string)(school:string)(year:int)(type_:string option)(address:string option)(month:int option) (note:string option):BibliographyItem = 
    let thesis = 
        MscThesis{ authorNames = author;
        title = title ;
        school = school ;
        year = year ;
        type_ = type_ ;
        address = address ;
        month = month ;
        note =  note }
    thesis 


(*
createMisc :
  author:string option list ->
    title:string option ->
      howpublished:string option ->
        month:int option ->
          year:int option ->
            note:string option -> BibliographyItem

*)

let createMisc (author:string option list)(title:string option)(howpublished:string option)(month:int option)(year:int option)
               (note:string option):BibliographyItem = 
    let misc = 
        Misc{ authorNames = author;
        title = title ;
        howpublished = howpublished ;
        month = month;
        year = year ;
        note =  note }
    misc  
