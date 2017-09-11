module Changelog.Domain

open System
open Util.Types
open FSharpx.Collections


// TODO: constrain 10 to 100 characters
type Description = Description of string
// TODO: constrain 10 to 300 characters
type Author = Author of string


module VersionNumber = 
    type T = VersionNumber of int
    val create: int -> Result<T, IntegerError>


type WorkItemType = 
| Bug
| Feature
| Miscellaneous


type WorkItem = {
    Type: WorkItemType
    Description: Description
}


module Version =
    type T = {
                Major: VersionNumber.T
                Minor: VersionNumber.T
                Revision: VersionNumber.T
            }
    type T with 
        member isLowerThan: T -> bool
        member isHigherThan: T -> bool
  

type Release = {
    Version: Version
    Date: Date.PastDate
    Authors: NonEmptyList<Author>
    WorkItems: NonEmptyList<WorkItem>
}
