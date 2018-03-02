import * as React from 'react'
import { t } from '../../i18n'
import { Release } from '../../models'
import ShowRelease from './Release'

interface OwnProps {
    releases: Release[]
}

export default function Index(props: OwnProps) {
    return <div className="container">
        <h1>{t('hello')}</h1>
        <h1 className="header">Changelog</h1>
        {props.releases.map((release) => {
            return <ShowRelease release={release} key={release.releaseVersion} />
        })}
    </div>
}
