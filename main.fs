namespace AOC2024

module Main =
  open System
  open System.IO

  let days = [
    Day01.run
    Day02.run
    Day03.run
    Day04.run
    Day05.run
    Day06.run
    Day07.run
    Day08.run
  ]

  let dayNames =
    days
    |> Seq.indexed
    |> Seq.map(fst)
    |> Seq.map(fun x -> x + 1)
    |> Seq.map(string)

  let input day env =
    match (day, env) with
      | (3, "test") -> [$"./input/day03-test.txt";$"./input/day03-test2.txt"]
      | _ -> [$"./input/day{day:D2}-{env}.txt"]

  let runDay num env =
    printfn "%s" env
    for input in (input num env) do
      days.[num-1] input ||> printfn "%d %d"


  [<EntryPoint>]
  let main argv =
    match System.Int32.TryParse argv.[0] with
    | true, day ->
      runDay day "test"
      runDay day "prod"
      0
    | _ ->
      printfn "Specify a valid Day to run"
      printfn "Either of: %s" (String.concat ", " dayNames)
      1