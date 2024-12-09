namespace AOC2024

module Day07 =
  open System
  open System.IO
  open System.Text.RegularExpressions
  open System

  let rec cartesianProduct n array =
    if n = 1 then
        array |> Array.map (fun x -> [|x|])
    else
        array
        |> Array.collect (fun x ->
            cartesianProduct (n - 1) array
            |> Array.map (fun subArray -> Array.append [|x|] subArray))


  let run inputFile =
    let lines = File.ReadLines inputFile

    let split s = Regex.Split(s, @":?\s+")
    let equations = lines |> Seq.map(split >> (Seq.map int64))


    let plus = (+)
    let mul a b = a * b
    let concatDigits (x: int64) (y: int64) : int64 =
      let rec countDigits (n: int64) =
          if n = 0L then 0 else 1 + countDigits (n / 10L)

      let powerOfTen = pown 10L (countDigits y)
      (x * powerOfTen) + y

    let fitOperators ops numbers =
      let numOps = Array.length ops
      let res = Seq.head numbers
      let nums = Seq.tail numbers
      let firstNum = Seq.head nums
      let restNums = Seq.tail nums
      let opCount = (Seq.length restNums)
      let opSeq = cartesianProduct  opCount ops

      Seq.exists (fun ops ->
        let steps = Seq.zip ops restNums

        let result = Seq.fold (fun acc (op, num) -> op acc num) firstNum steps

        result = res
      ) opSeq
    let part1 = equations |> (Seq.filter (fitOperators [|plus;mul|])) |> Seq.map(Seq.head) |> Seq.sum
    let part2 = equations |> (Seq.filter (fitOperators [|plus;mul;concatDigits|])) |> Seq.map(Seq.head) |> Seq.sum

    (string part1, string part2)