namespace AOC2024

module Main =

  open System
  open System.IO

  [<EntryPoint>]
  let main argv =
    match argv.[0] with
       | "1" -> 
        (Day01.run "./input/day01-test.txt") ||> printfn "test: %d %d"
        (Day01.run "./input/day01-prod.txt") ||> printfn "prod %d %d"
       | "2" -> 
        (Day02.run "./input/day02-test.txt") ||> printfn "test: %d %d"
        (Day02.run "./input/day02-prod.txt") ||> printfn "prod: %d %d"
       | _ -> Console.WriteLine "Specify a Day to run"
    0