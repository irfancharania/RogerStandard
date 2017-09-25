module Changelog.DomainPrimitiveTypes

open System

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


module WorkItemDescription = 
    type T = WorkItemDescription of string
    val create: string -> Result<T, StringError>
    val apply: (string -> 'a) -> T -> 'a


module ReleaseAuthor = 
    type T = ReleaseAuthor of string
    val create: string -> Result<T, StringError>
    val apply: (string -> 'a) -> T -> 'a


module ReleaseDate =
    type T = ReleaseDate of DateTime

    val create: DateTime -> Result<DateTime, DateError>
    val apply: (DateTime -> 'a) -> T -> 'a
