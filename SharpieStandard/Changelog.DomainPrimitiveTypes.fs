module Changelog.DomainPrimitiveTypes

open System

type GuidError =
    | Missing

type DateError =
    | MustBeNewerThan of DateTime
    | MustBeOlderThan of DateTime

type StringError =
    | Missing
    | MustNotBeLongerThan of int
    | MustNotBeShorterThan of int

type IntegerError =
    | Missing
    | MustBePositiveInteger
    | MustBeGreaterThan of int
    | MustBeGreaterThanOrEqualTo of int
    | MustBeLessThan of int
    | MustBeLessThanOrEqualTo of int


module WorkItemId = 
    type T = WorkItemId of int

    let create (i:int) =
        if i < 1 then
            Error (MustBePositiveInteger)
        else 
            Ok (WorkItemId i)

    let apply f (WorkItemId i) = f i


module WorkItemDescription =
    type T = WorkItemDescription of string

    let create (s:string) =
        match s with
        | _ when String.IsNullOrWhiteSpace s    -> Error StringError.Missing
        | _ when s.Length < 10                  -> Error (MustNotBeShorterThan 10)
        | _ when s.Length > 100                 -> Error (MustNotBeLongerThan 100)
        | _ -> Ok (WorkItemDescription s)

    let apply f (WorkItemDescription s) = f s


module ReleaseAuthor =
    type T = ReleaseAuthor of string

    let create (s:string) =
        match s with
        | _ when String.IsNullOrWhiteSpace s    -> Error StringError.Missing
        | _ when s.Length > 100                 -> Error (MustNotBeLongerThan 100)
        | _ -> Ok (ReleaseAuthor s)

    let apply f (ReleaseAuthor s) = f s


 module ReleaseDate =
    let minDate = DateTime(2017,1,1)
    let tomorrow = DateTime.UtcNow.Date.AddDays(1.0)

    type T = ReleaseDate of DateTime

    let create (dt:DateTime) = 
        match dt with
        | _ when dt > tomorrow  -> Error (MustBeOlderThan tomorrow)        
        | _ when dt < minDate   -> Error (MustBeNewerThan minDate)
        | _ -> Ok(ReleaseDate dt)


    let apply f (ReleaseDate dt) = f dt


type ReleaseId = ReleaseId of Guid
type RecordVersion = RecordVersion of byte[]

