import React, { useState } from 'react';
import { AppBar, Tabs, Tab,  makeStyles, Theme, Box } from '@material-ui/core';
import { TransactionsPanelDiv } from './transactions-panel.styles';
import { Deposit } from '../deposit/deposit';
import TabPanel from './tab-panel';
import Transfer from '../transfer';
import Payment from '../payment';
import Withdraw from '../withdraw';
import TransactionsTable from '../transactions-table';

const useStyles = makeStyles((theme: Theme) => ({
    root: {
      flexGrow: 1,
      backgroundColor: theme.palette.background.paper,
      marginTop: '10px'
    },
    appBar: {
        display:'flex',
        flexDirection:'row',
        alignItems:'center',
        justifyContent:'center',
    }
  }));

export const TransactionsPanel:React.FC  = () => {
    const classes = useStyles();
    const [value, setValue] = useState(0);
    return <TransactionsPanelDiv className={classes.root}>
            <AppBar className={classes.appBar} position='static'>
                <Tabs value={value} onChange={(event, value) => setValue(value)}>
                    <Tab label="Deposit"/>
                    <Tab label="Transfer"/>
                    <Tab label="Payment"/>
                    <Tab label="Withdraw"/>
                    <Tab label="My Transactions"/>
                </Tabs>
            </AppBar>
            <TabPanel index={0} value={value}>
                <Deposit/>
            </TabPanel>
            <TabPanel index={1} value={value}>
                <Transfer/>
            </TabPanel>
            <TabPanel index={2} value={value}>
                <Payment/>
            </TabPanel>
            <TabPanel index={3} value={value}>
                <Withdraw/>
            </TabPanel>
            <TabPanel index={4} value={value}>
                <TransactionsTable/>
            </TabPanel>
    </TransactionsPanelDiv>;
}

