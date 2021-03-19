import { getAsync } from 'api';
import React, { useEffect } from 'react';
import { AccountNameDiv, AccountPanelDiv, BalanceDiv } from './account-panel.styles';
import { IAccount } from 'models/account';
import { useHomeContext } from '../home.provider';
import { HomeActionType } from '../home.actions';
import { Paper, Typography } from '@material-ui/core';

export const AccountPanel: React.FC = () => {
    const { state, dispatch } = useHomeContext();
    
    useEffect(() => {
        async function getAccount(){
            const response = await getAsync<IAccount>('/api/v1/accounts/myself');
            dispatch({type: HomeActionType.UserAcquired, payload: response.payload });
        };
        getAccount();
    },[])

   const {user} = state;

    if(!user) return <></>

    return <AccountPanelDiv>
        <AccountNameDiv>
            <Paper elevation={3} style={{width:'50%', height: '100%', display:'flex', flexDirection:'column', alignItems: 'center',justifyContent: 'center', padding: '1%'}}>
                <Typography variant="h4" style={{fontWeight:200}}>Hello, {user.name}!</Typography>
            </Paper>
        </AccountNameDiv>
        <BalanceDiv>
            <Paper elevation={3} style={{width:'50%', height: '100%', display:'flex', flexDirection:'column', alignItems: 'center',justifyContent: 'center', padding: '1%'}}>
                <Typography variant="h6" style={{fontWeight:200}}>Your current balance is:</Typography>
                <Typography variant="h4" style={{fontWeight:200}}>{user.balance}</Typography>
            </Paper>
        </BalanceDiv>
    </AccountPanelDiv>
}