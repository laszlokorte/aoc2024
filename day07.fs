namespace AOC2024

module Day07 =
  open System
  open System.IO

  let run inputFile =
    let lines = File.ReadAllText inputFile

    (0,0)