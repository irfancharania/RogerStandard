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


module ReleaseDescription = 
    type T = ReleaseDescription of string
    val create: string -> Result<T, StringError>
    val apply: (string -> 'a) -> T -> 'a


module ReleaseAuthor = 
    type T = ReleaseAuthor of string
    val create: string -> Result<T, StringError>
    val apply: (string -> 'a) -> T -> 'a


module VersionNumber = 
    type T = VersionNumber of int
    val create: int -> Result<T, IntegerError>
    val apply: (int -> 'a) -> T -> 'a


module ReleaseVersion =
    type T = {
                Major: VersionNumber.T
                Minor: VersionNumber.T
                Revision: VersionNumber.T
            }
            with 
                member isLowerThan: T -> bool
                member isHigherThan: T -> bool

module ReleaseDate =
    type T = ReleaseDate of DateTime

    val create: DateTime -> Result<DateTime, DateError>
    val apply: (DateTime -> 'a) -> T -> 'a
