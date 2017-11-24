import * as React from 'react'
import { Release } from '../../models'

interface ReleaseProps {
    release: Release
}
function Release(props: ReleaseProps) {
    const {release} = props
    return <div className="release">
        <div className="date">{release.releaseDate}</div>
        <h2>{ release.releaseVersion }</h2>

        <p className="authors">
            {release.authors.map(author => {
                return <span className="author" key={author}> { author }</span>
            })}
        </p>

        <ol>
            { release.workItems.map(item => {
                return <li key={item.id}>
                    <strong>{item.workItemString}</strong>
                    { item.description }
                </li>
            })}
        </ol>
    </div>
}

interface OwnProps {
    releases: Release[]
}

export default function Index(props: OwnProps){
    return <div className="container">
        <h1 className="header">Changelog</h1>
        {props.releases.map(release => {
            return <Release release={release} key={release.releaseVersion} />
        })}
    </div>
}