module Changelog.DomainPrimitiveTypes

open System

type DateError =
    | MustBeNewerThan of DateTime
    | MustBeOlderThan of DateTime

type StringError =
    | Missing
    | MustNotBeLongerThan of int
    | MustNotBeShorterThan of int
    | DoesntMatchPattern of string


type IntegerError =
    | Missing
    | MustBePositiveInteger
    | MustBeGreaterThan of int
    | MustBeGreaterThanOrEqualTo of int
    | MustBeLessThan of int
    | MustBeLessThanOrEqualTo of int


module ReleaseAuthor =
    type T = ReleaseAuthor of string

    let create (s:string) =
        match s with
        | _ when String.IsNullOrWhiteSpace s    -> Error StringError.Missing
        | _ when s.Length > 100                 -> Error (MustNotBeLongerThan 100)
        | _ -> Ok (ReleaseAuthor s)

    let apply f (ReleaseAuthor s) = f s


module ReleaseDescription =
    type T = ReleaseDescription of string

    let create (s:string) =
        match s with
        | _ when String.IsNullOrWhiteSpace s    -> Error StringError.Missing
        | _ when s.Length < 10                  -> Error (MustNotBeShorterThan 10)
        | _ when s.Length > 100                 -> Error (MustNotBeLongerThan 100)
        | _ -> Ok (ReleaseDescription s)

    let apply f (ReleaseDescription s) = f s


 module ReleaseDate =
    let minDate = new DateTime(2017,1,1)
    let maxDate = new DateTime(2057,1,1)
    let today = DateTime.Today

    type T = ReleaseDate of DateTime

    let create (dt:DateTime) = 
        match dt with
        | _ when dt > maxDate   -> Error (MustBeOlderThan maxDate)
        | _ when dt > today     -> Error (MustBeOlderThan today)        
        | _ when dt < minDate   -> Error (MustBeNewerThan minDate)
        | _ -> Ok(dt)


    let apply f (ReleaseDate dt) = f dt
