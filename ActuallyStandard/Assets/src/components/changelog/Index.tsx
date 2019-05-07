import * as React from 'react'
import { connect } from 'react-redux'
import AlertsContainer from '../../containers/AlertsContainer'
import { t } from '../../i18n'
import { Release } from '../../models'
import { DispatchFn } from '../../store'
import ShowRelease from './Release'

interface OwnProps {
    releases: Release[]
    addAlert: () => void
}

export default function Index(props: OwnProps) {
    return <div className="container">
        <AlertsContainer />
        <h1 className="header">Changelog</h1>
        {props.releases.map((release) => {
            return <ShowRelease release={release} key={release.releaseVersion} />
        })}
        <button onClick={() => props.addAlert()}> Add Alert </button>

    </div>
}
