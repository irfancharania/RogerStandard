import * as React from 'react'
import { Release } from '../../models'
import ShowRelease from './Release'

interface OwnProps {
    releases: Release[]
}

export default function Index(props: OwnProps) {
    return <div className="container">
        <h1 className="header">Changelog</h1>
        {props.releases.map((release) => {
            return <ShowRelease release={release} key={release.releaseVersion} />
        })}
    </div>
}
