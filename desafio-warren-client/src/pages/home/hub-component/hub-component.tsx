import React, { memo, useEffect } from 'react';
import { createHubConnection } from '../../../api/hub/hub-provider';
import { useHomeContext } from '../home.provider';
import { Guid } from 'guid-typescript';
import { HomeActionType } from '../home.actions';
import { useSelector } from 'pages/home/home.provider';

const SignalRHubComponent: React.FC = () => {
    
    const { dispatch} = useHomeContext();

    const {id} = useSelector(state => state.user);
    
    useEffect(() => {
        if(!id) return;

        const hubConnection = createHubConnection();

        hubConnection.on('AccountBalanceChanged', (accountBalance) => {
            dispatch({type: HomeActionType.AccountBalanceChanged, payload: accountBalance });
        })

        hubConnection.start().then(a => {
            sendAccountInfoToHub(hubConnection, id);
        });

        hubConnection.onreconnected(() => sendAccountInfoToHub(hubConnection, id));

        function sendAccountInfoToHub(hubConnection:signalR.HubConnection, id: Guid | string) {
            if (hubConnection.connectionId)
                hubConnection.send('AppendAccountToList', id.toString(), hubConnection.connectionId);
        } 
    }, [id])

    return <></>
}
 
export const HubComponent = memo(SignalRHubComponent);