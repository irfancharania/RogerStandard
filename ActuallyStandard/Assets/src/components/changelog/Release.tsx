import * as React from 'react'
import { Release } from '../../models'

interface OwnProps {
    release: Release
}

export default function Release(props: OwnProps) {
    const { release } = props
    return <div className="release">
        <div className="date">{release.releaseDate}</div>
        <h2>{release.releaseVersion}</h2>

        <p className="authors">
            {release.authors.map((author) => {
                return <span className="author" key={author}> {author}</span>
            })}
        </p>

        <ol>
            {release.workItems.map((item) => {
                return <li key={item.id}>
                    <strong>{item.workItemString}</strong>
                    {item.description}
                </li>
            })}
        </ol>
    </div>
}
