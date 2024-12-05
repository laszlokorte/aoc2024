namespace AOC2024

module Day05 =
  open System.Text.RegularExpressions
  open System
  open System.IO
  open System.Text.RegularExpressions

  let run inputFile =
    let lines = File.ReadAllText inputFile

    let blocks = lines.Split("\n\n")
    let rules = blocks.[0].Split("\n") |> Seq.map(_.Split('|')) |> (Seq.map (Array.map int)) |> Set.ofSeq
    let prints = blocks.[1].Split("\n") |> Seq.map(_.Split(',')) |>(Seq.map (Array.map int))

    let isValidByRule (list : int array) =
      let len = Array.length list
      seq {
        for l in 0 .. (len-1) do
          for r in l .. (len-1) do
            yield [|list.[r]; list.[l]|]
      }
      |> Seq.exists(fun a -> Set.contains a rules)
      |> not

    let compareByRule (ia, a) (ib, b) =
      if Set.contains [|a;b|] rules then
         1
      else
        if Set.contains [|b;a|] rules then
           -1
        else
          compare ia ib

    let fixOrder s =
      s
      |> Seq.indexed
      |> Seq.sortWith compareByRule
      |> Seq.map snd
      |> Array.ofSeq

    let part1 =
      prints
      |> Seq.filter(isValidByRule)
      |> Seq.map (fun l -> l.[Seq.length(l) / 2])
      |> Seq.sum

    let part2 =
      prints
      |> Seq.filter (isValidByRule >> not)
      |> Seq.map fixOrder
      |> Seq.map (fun l -> l.[Seq.length(l) / 2])
      |> Seq.sum


    part1, part2