import * as React from 'react'
import { connect } from 'react-redux'
import Index from '../../components/changelog/Index'
import { Release } from '../../models'
import { DispatchFn } from '../../store'

async function fetchReleases(): Promise<Release[]> {
    const response = await fetch('/api/v1/changelog')
    const releases = await response.json()
    return releases
}

interface OwnState {
    releases: Release[]
}

interface DispatchProps {
    addAlert: () => void
}

class IndexContainer extends React.PureComponent<DispatchProps, OwnState> {
    componentWillMount() {
        this.setState({ releases: [] })

        fetchReleases().then((releases) => {
            this.setState((state) => ({ ...state, releases }))
        })
    }

    render() {
        return <Index addAlert={this.props.addAlert} {...this.state} />
    }
}

export default connect(
    null,
    (dispatch: DispatchFn): DispatchProps => ({
        addAlert: () => {
            dispatch({
                type: 'alerts/add',
                message: {
                    id: '',
                    title: 'Test Message',
                    body: 'Everything went ok.',
                    timeout: 1000,
                    type: 'warning'
                }
            })
        }
    })
)(IndexContainer)
