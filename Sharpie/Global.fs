namespace global

/// Functions that work with Results
module Result =
    /// given a function wrapped in a result
    /// and a value wrapped in a result
    /// apply the function to the value only if both are Success
    let apply fResult xResult =
        match (fResult,xResult) with
            | Ok f, Ok x                -> Ok (f x)
            | Error err1, Ok x          -> Error err1
            | Ok f, Error err2          -> Error err2
            | Error err1, Error err2    -> Error (err1 @ err2)

    /// Lift a three parameter function to use Result parameters
    let lift3 f x1 x2 x3 = 
        let (<!>) = Result.map
        let (<*>) = apply
        f <!> x1 <*> x2 <*> x3

