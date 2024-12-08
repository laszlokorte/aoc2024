namespace AOC2024

module Day01 =

  open System.Text.RegularExpressions


  let run inputFile =
    let lines = System.IO.File.ReadLines inputFile
    let input = [for l in lines do [for c in Regex.Split(l, @"\s+") do int c]]
    let leftColumn = [for row in input do row[0]]
    let rightColumn = [for row in input do row[1]]

    let part1 =
      List.zip (List.sort leftColumn) (List.sort rightColumn)
      |> List.map (fun (a,b) -> a - b)
      |> List.map abs
      |> List.sum

    let part2 =
      let count v =
        let counts = rightColumn |> Seq.countBy id |> Map.ofSeq
        Map.tryFind v counts |> Option.defaultValue 0
      leftColumn |> List.map(fun n -> n * count n) |> List.sum

    (string part1), (string part2)
