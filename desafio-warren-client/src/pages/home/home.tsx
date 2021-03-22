import { useAccount, useMsal } from '@azure/msal-react';
import { api } from 'api/api';
import { azureConfiguration } from 'api/authentication';
import React, { useEffect, useState } from 'react';
import AccountPanel from './account-panel';
import { HomeProvider } from './home.provider';
import HubComponent from './hub-component';
import TopBar from './topbar';
import { TransactionsPanel } from './transactions-panel/transactions-panel';


export const Home: React.FC = () => {
    const [isAuthenticating, setIsAuthenticating] = useState(true);

    const { instance, accounts, inProgress } = useMsal();

    const account = useAccount(accounts[0] || {});

    useEffect(() => {
        if(!account) return;

        const scopes = [ azureConfiguration.getMSALScopes() ];

        instance.acquireTokenSilent({
            scopes,
            account,
        }).then(response => {
            const { accessToken } = response;

            api.defaults.headers.common.Authorization = `Bearer ${accessToken}`;

            api.defaults.headers.common.Accept = 'application/json';

            setIsAuthenticating(false);
        })
    }, [])

    return !isAuthenticating || inProgress !== 'none' ? (
        <HomeProvider>
            <TopBar />
            <AccountPanel/>
            <TransactionsPanel />
            <HubComponent />
        </HomeProvider>
    ) : (
        <div/>
    );
}

