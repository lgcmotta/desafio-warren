import React from 'react';
import AccountPanel from './account-panel';
import { HomeProvider } from './home.provider';
import HubComponent from './hub-component';
import TopBar from './topbar';
import { TransactionsPanel } from './transactions-panel/transactions-panel';


export const Home: React.FC = () => {
    return <HomeProvider>
        <TopBar />
        <AccountPanel/>
        <TransactionsPanel />
        <HubComponent />
    </HomeProvider>
}

