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


module VersionNumber = 
    type T = VersionNumber of int

    let create x = 
        match x with
        | _ when x < 0      -> Error(MustBeGreaterThanOrEqualTo 0)
        | _ when x > 100    -> Error(MustBeLessThanOrEqualTo 100)
        | _ -> Ok (VersionNumber x)
    
    let apply f (VersionNumber i) = f i


module ReleaseVersion =
    type T = {
                Major: VersionNumber.T
                Minor: VersionNumber.T
                Revision: VersionNumber.T
            }
    type T with 
        member this.isLowerThan (x:T) =
            match x with
            | _ when this.Major < x.Major       -> true
            | _ when this.Minor < x.Minor       -> true
            | _ when this.Revision < x.Revision -> true
            | _ -> false

        member this.isHigherThan (x:T) =
            match x with
            | _ when this.Major > x.Major       -> true
            | _ when this.Minor > x.Minor       -> true
            | _ when this.Revision > x.Revision -> true
            | _ -> false
 

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
