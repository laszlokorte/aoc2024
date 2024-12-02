namespace AOC2024

module Day02 =
  open System.Text.RegularExpressions
  open System
  open System.IO

  let filterByIndex predicate seq =
    let tuple x y = (x, y)
    seq
    |> Seq.mapi tuple
    |> Seq.filter (fst >> predicate)
    |> Seq.map snd 

  let permuteLine line = 
    [0 .. (Seq.length line)] |> Seq.map(fun i -> filterByIndex (fun ii -> ii <> i) line)
  

  let run inputFile =
    let lines = System.IO.File.ReadLines inputFile
    let input = [for l in lines do [for c in Regex.Split(l, @"\s+") do int c]]
    
    let inRange n = Seq.contains n [1 .. 3]
    let neg x = -x 
    let pairDelta = function 
      | [|a; b|] -> a - b 
      | _ -> 0

    let stepSizeMatch s = (Seq.forall inRange s) || (Seq.forall (neg>>inRange) s)
    let getStepSize lines = lines |> Seq.map(Seq.windowed 2) |> Seq.map(Seq.map(pairDelta))

    let part1 = 
      input |> getStepSize |> Seq.filter(stepSizeMatch) |> Seq.length

    let part2 = 
      input 
      |> Seq.map permuteLine 
      |> Seq.map(getStepSize)  
      |> Seq.filter(Seq.exists(stepSizeMatch))
      |> Seq.length


    part1, part2