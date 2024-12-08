namespace AOC2024

module Day07 =
  open System
  open System.IO
  open System.Text.RegularExpressions

  let run inputFile =
    let lines = File.ReadLines inputFile

    let split s = Regex.Split(s, @":?\s+")
    let equations = lines |> Seq.map(split >> (Seq.map int64))

    let fitOperators numbers =
      let res = Seq.head numbers
      let nums = Seq.tail numbers
      let firstNum = Seq.head nums
      let restNums = Seq.tail nums
      let opCount = (Seq.length restNums)
      let combiCount = 1 <<< opCount
      let plus = (+)
      let mul a b = a * b
      let opSeq = seq {
        for c in 0 .. (combiCount-1) do
          [for o in 0 .. (opCount-1) ->  if (1<<<o &&& c) > 0 then plus else mul]
      }

      Seq.exists (fun ops ->
        let steps = Seq.zip ops restNums

        let result = Seq.fold (fun acc (op, num) -> op acc num) firstNum steps

        result = res
      ) opSeq
    let part1 = equations |> (Seq.filter fitOperators) |> Seq.map(Seq.head) |> Seq.sum

    (string part1, string 0)