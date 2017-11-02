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


module WorkItemId = 
    type T = WorkItemId of int
    val create: int -> Result<T, IntegerError>
    val apply: (int -> 'a) -> T -> 'a


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

    val create: DateTime -> Result<T, DateError>
    val apply: (DateTime -> 'a) -> T -> 'a
