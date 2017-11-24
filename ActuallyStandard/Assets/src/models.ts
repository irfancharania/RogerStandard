export interface WorkItem {
    id: number,
    description: string,
    workItemString: string
}

export interface Release {
    releaseVersion: string,
    releaseDate: string,
    authors: string[],
    workItems: WorkItem[]
}