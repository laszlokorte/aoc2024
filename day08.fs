namespace AOC2024

module Day08 =
  open System
  open System.IO

  let run inputFile =
    let lines = File.ReadLines inputFile

    let maxY = lines |> Seq.length
    let maxX = lines |> Seq.head |> Seq.length

    let antennas =
      lines
      |> Seq.map Seq.indexed
      |> Seq.indexed
      |> Seq.map (fun (y, chars) -> chars |> Seq.map (fun (x, c) -> (c, (x,y))) )
      |> Seq.concat
      |> Seq.filter (fun (c, pos) -> c <> '.')
      |> Seq.groupBy (fun (c, pos) -> c)
      |> Seq.map (fun (c, poses) -> (c, poses |> Seq.map snd |> Array.ofSeq))
      |> Map.ofSeq

    let freqs = Map.keys antennas

    let isInRange (a, b) = a >= 0 && b >= 0 && a < maxX && b < maxY

    let part1 =
      let antinodes =
        freqs
        |> Seq.map (fun f -> Map.find f antennas)
        |> Seq.map (fun poses ->
          poses |> Seq.map (fun (xa, ya) ->
            poses
            |> Seq.map (fun (xb, yb) -> (xa + xa - xb, ya + ya - yb))
            |> Seq.filter (fun (a,b) -> a <> xa || b <> ya))
          )
        |> Seq.concat
        |> Seq.concat
        |> Seq.filter isInRange
        |> Set.ofSeq

      Set.count antinodes

    let part2 =
      let antinodes =
        freqs
        |> Seq.map (fun f -> Map.find f antennas)
        |> Seq.map (fun poses ->
          poses |> Seq.map (fun (xa, ya) ->
            poses
            |> Seq.map (fun (xb, yb) -> (xa - xb, ya - yb))
            |> Seq.filter (fun (a,b) -> a <> 0 || b <> 0)
            |> Seq.map (fun (dx, dy) ->
              Seq.unfold (fun (x,y) ->
                if isInRange (x, y) then
                  Some ((x,y), (x + dx,y + dy))
                else
                  None
              ) (xa, ya)
            )
            |> Seq.concat)
          )
        |> Seq.concat
        |> Seq.concat
        |> Seq.filter isInRange
        |> Set.ofSeq

      Set.count antinodes

    // for y in 0..(maxY-1) do
    //   for x in 0..(maxY-1) do
    //     if Set.contains (x,y) antinodes then
    //       Console.Write '#'
    //     else
    //       Console.Write '.'
    //   Console.Write '\n'


    (string part1, string part2)
