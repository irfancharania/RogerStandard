module Util.Types

open System


type StringError =
    | Missing
    | MustNotBeLongerThan of int
    | DoesntMatchPattern of string


type IntegerError =
    | Missing
    | MustBePositiveInteger
    | MustBeGreaterThan of int
    | MustBeGreaterThanOrEqualTo of int
    | MustBeLessThan of int
    | MustBeLessThanOrEqualTo of int


/// http://www.taimila.com/blog/fsharp-pure-time-dependent-domain/
module Date =
    type PastDate = PastDate of DateTime
    type CurrentDate = CurrentDate of DateTime
    type FutureDate = FutureDate of DateTime

    type T = 
        | Past of PastDate
        | Current of CurrentDate
        | Future of FutureDate

    let create (dt: DateTime) =
        match dt with
        | dt when dt.Date = DateTime.Today -> Current (CurrentDate(dt))
        | dt when dt.Date < DateTime.Today -> Past (PastDate(dt))
        | _ -> Future (FutureDate(dt))

    let isToday = 
        function
        | Current _ -> true
        | _ -> false

    let isInFuture = 
        function
        | Future _ -> true
        | _ -> false

    let isInPast =
        function
        | Past _ -> true
        | _ -> false
