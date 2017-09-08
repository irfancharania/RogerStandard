module Changelog.Domain

open System


type ReleaseStatus = 
| Upcoming
| Released

type WorkItemType = 
| Bug
| Feature
| Miscellaneous

// TODO: constrain to 100 characters
type Description = Description of string
// TODO: constrain to 300 characters
type Author = Author of string

type Version = {
    Major: int
    Minor: int
    Revision: int
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
