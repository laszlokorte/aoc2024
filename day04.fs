namespace AOC2024

module Day04 =
  let run inputFile =
    let lines = System.IO.File.ReadLines inputFile
    let input = lines |> Seq.map Seq.toList |> Seq.toList |> array2D 

    let w, h = Array2D.length1 input, Array2D.length2 input

    let countFrom n = Seq.unfold (fun x -> Some (x, x+1)) n
    
    let part1 = 
        let pos = 
            seq {
                for x in 0 .. (w-4) do
                    for y in 0 .. (h - 4) do
                        yield Seq.zip 
                            (countFrom x |> Seq.take 4) 
                            (countFrom y |> Seq.take 4)
                        yield Seq.zip 
                            (Seq.rev (countFrom x |> Seq.take 4)) 
                            (countFrom y |> Seq.take 4)
                for x in 0 .. (w-4) do
                    for y in 0 .. (h-1) do
                        yield Seq.zip 
                            (countFrom x) 
                            (Seq.replicate 4 y)
                for x in 0 .. (w-1) do
                    for y in 0 .. (h - 4) do
                        yield Seq.zip 
                            (Seq.replicate 4 x) 
                            (countFrom y)
            }

        seq {
             for r in pos do
                yield! [r; Seq.rev r]
        }
        |> Seq.map (Seq.map (fun (x,y) -> Array2D.get input x y)) 
        |> Seq.filter (fun slice -> Seq.forall2 (=) slice "XMAS") 
        |> Seq.length


    let part2 = 
        let lookup = fun (x, y) -> Array2D.get input x y
        let isWord = fun sa -> Seq.forall2 (=) sa "MAS"

        seq {
            for x in 1 .. (w-2) do
                for y in 1 .. (h-2) do
                    let right = countFrom (x-1) |> Seq.take 3
                    let down = countFrom (y-1) |> Seq.take 3
                    let up = Seq.rev down
                    let left = Seq.rev right

                    let down_right = Seq.zip right down
                    let up_right = Seq.zip right up
                    let down_left = Seq.zip left down
                    let up_left = Seq.zip left up

                    for x in [down_right; up_left] do 
                        for y in [up_right; down_left] do
                            yield [x;y]
        }
        |> (Seq.map >> Seq.map >> Seq.map) lookup
        |> Seq.filter (Seq.forall isWord  )
        |> Seq.length


    part1, part2