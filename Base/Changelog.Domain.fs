module Changelog.Domain

open System


type ReleaseStatus = 
| Upcoming
| Released

type WorkItemType = 
| Bug
| Feature
| Miscellaneous

// TODO: constrain 10 to 100 characters
type Description = Description of string
// TODO: constrain 10 to 300 characters
type Author = Author of string
// TODO: constrain 0 to 100
type VersionNumber = VersionNumber of int

type Version = {
    Major: VersionNumber
    Minor: VersionNumber
    Revision: VersionNumber
}

type WorkItem = {
    Type: WorkItemType
    Description: Description
}

type Release = {
    Version: Version
    Date: DateTime
    Status: ReleaseStatus
    Authors: list<Author>
    WorkItems: list<WorkItem>
}
