namespace AOC2024

module Day03 =
  open System.Text.RegularExpressions
  open System
  open System.IO
  open System.Text.RegularExpressions

  let mulA = Regex(@"(?:mul\((?<a>\d{1,3}),(?<b>\d{1,3})\))", RegexOptions.Compiled)
  let mulB = Regex(@"(?:mul\((?<a>\d{1,3}),(?<b>\d{1,3})\)|(?<do>do\(\))|(?<dont>don\'t\(\)))", RegexOptions.Compiled)

  let run inputFile =
    let lines = File.ReadAllText inputFile      

    let part1 = 
      mulA.Matches(lines)
      |> Seq.map(fun m -> int(m.Groups["a"].Value) * int(m.Groups["b"].Value))
      |> Seq.sum

    let part2 = 
      let folder((enabled, acc): bool * int) (ins: Match) : bool * int = 
        if enabled && ins.Groups["a"].Success then 
          true, acc + int(ins.Groups["a"].Value) * int(ins.Groups["b"].Value)
        else if ins.Groups["dont"].Success then
          false, acc
        else if ins.Groups["do"].Success then
          true, acc
        else
          enabled, acc
      
      Seq.fold folder (true, 0) (mulB.Matches(lines)) |> snd

    part1, part2