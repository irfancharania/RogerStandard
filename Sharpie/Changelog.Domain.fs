module Changelog.Domain

open System
open FSharpx.Collections
open Changelog.DomainPrimitiveTypes


// ============================== 
// Domain models
// ============================== 

type WorkItemType = 
| Bug
| Feature
| Miscellaneous


type WorkItem = {
    Type: WorkItemType
    Description: ReleaseDescription.T
}


type Release = {
    Version: Version
    Date: ReleaseDate.T
    Authors: NonEmptyList<ReleaseAuthor.T>
    WorkItems: NonEmptyList<WorkItem>
}
