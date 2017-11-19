export interface WorkItem {
    id: number,
    description: string,
    workItemString: string
}

export interface Release {
    releaseVersion: string,
    releaseDate: string,
    releaseAuthors: string[],
    workItems: WorkItem[]
}