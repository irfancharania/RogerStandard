import * as im from 'immutable'
import * as React from 'react'
import { connect } from 'react-redux'
import { option } from '../Option'
import { Message } from '../reducers/alerts'
import { AppState, DispatchFn } from '../store'

function Alert({ message, clear }: { message: Message, clear: () => void }) {
    return <div className={`alert alert-${message.type} alert-dismissible fade show`} role="alert">
        <button type="button" className="close" data-dismiss="alert" aria-label="Close" onClick={clear}>
            <span aria-hidden="true">&times;</span>
        </button>
        <strong>{message.title}</strong> {message.body}
    </div>
}

interface ReduxProps {
    messages: im.Map<string, Message>
}

interface ReduxHandlers {
    removeAlert: (messageId: string) => void
}

type Props = ReduxProps & ReduxHandlers
class AlertsContainer extends React.PureComponent<Props, {}> {
    componentWillReceiveProps(nextProps: Props) {
        const nextKeys = nextProps.messages.keySeq().toSet()
        const currentKeys = this.props.messages.keySeq().toSet()
        nextKeys.subtract(currentKeys).forEach((k) => {
            const timeout = option(k)
                .map((key) => nextProps.messages.get(key))
                .map((m) => m.timeout)
                .orElse(5000)

            if (k) {
                setTimeout(() => nextProps.removeAlert(k), timeout)
            }
        })
    }
    render() {
        return <div className="alerts-container">
            {this.props.messages.valueSeq().toArray().map((msg) => {
                return <Alert key={msg.id} message={msg} clear={() => this.props.removeAlert(msg.id)} />
            })}
        </div>
    }
}

export default connect(
    (state: AppState): ReduxProps => ({
        messages: state.alerts.messages
    }),
    (dispatch: DispatchFn): ReduxHandlers => ({
        removeAlert: (messageId) => {
            dispatch({ type: 'alerts/remove', messageId })
        }
    })
)(AlertsContainer)
