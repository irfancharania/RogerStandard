module Changelog.Domain

open System
open Util.Types


// TODO: constrain 10 to 100 characters
type Description = Description of string
// TODO: constrain 10 to 300 characters
type Author = Author of string
// TODO: constrain 0 to 100
type VersionNumber = VersionNumber of int


type WorkItemType = 
| Bug
| Feature
| Miscellaneous

type WorkItem = {
    Type: WorkItemType
    Description: Description
}

type Version = {
    Major: VersionNumber
    Minor: VersionNumber
    Revision: VersionNumber
}

type Release = {
    Version: Version
    Date: PastDate
    Authors: list<Author>
    WorkItems: list<WorkItem>
}
