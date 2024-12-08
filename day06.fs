namespace AOC2024

module Day06 =
  open System
  open System.IO

  let findIndex2D (pred) (array: 'T[,]) : (int * int)  =
    let rows = Array2D.length1 array
    let cols = Array2D.length2 array

    seq {
        for row in 0 .. rows - 1 do for col in 0 .. cols - 1 -> (row, col)
    }
    |> Seq.filter(fun (row, col) -> pred array.[row, col])
    |> Seq.head

  type Direction = Up | Down | Right | Left

  let delta dir =
    match dir with
    | Up -> (-1,0)
    | Right -> (0,1)
    | Down -> (1,0)
    | Left -> (0,-1)

  let clockwise dir =
    match dir with
    | Up -> Right
    | Right -> Down
    | Down -> Left
    | Left -> Up

  let run inputFile =
    let lines = File.ReadLines inputFile

    let startMarker = [|('v', Down);('<', Left);('>', Right);('^', Up)|] |> Map.ofArray
    let map = lines |> array2D
    let (height, width) = (Array2D.length1 map), (Array2D.length2 map)
    let obstacles = map |> Array2D.map ((=) '#')
    let startPos = findIndex2D (fun e -> Map.containsKey e startMarker) map
    let startDir = Map.find (map |> Array2D.get <|| startPos) startMarker

    let step obs (dir, (x,y)) =
      let (dx, dy) = delta dir
      let newPos = (x+dx, y+dy)
      let (nx, ny) = newPos

      if nx < 0 || ny < 0 || nx >= height || ny >= width then
        None
      elif obs |> Array2D.get <|| newPos then
        Some (((x,y), dir), (clockwise dir, (x,y)))
      else
        Some (((x,y), dir), (dir, newPos))


    let walk obs : ((int * int) * Direction) seq = Seq.unfold (step obs) (startDir, startPos)
    let trace acc (pos, _dir) = Set.add pos acc
    let visited = Seq.fold trace (Set.singleton startPos) (walk obstacles)
    let part1 = visited |> Set.count |> (+) 1


    let part2 =
      let possibleObstacles = Set.remove startPos visited
      let detectLoop acc step =
        Set.add step acc

      possibleObstacles
      |> Set.filter (fun (x,y) ->
        obstacles[x,y] <- true
        let loop =
          Seq.scan detectLoop (Set.singleton (startPos, startDir)) (walk obstacles)
          |> Seq.pairwise
          |> Seq.skip 1
          |> Seq.exists (fun (a, b) -> Set.count a = Set.count b)

        obstacles[x,y] <- false
        loop)
      |> Set.count

    part1, part2
