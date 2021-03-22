import React, { useEffect, useState } from 'react';
import MaterialTable from 'material-table';
import { TransactionsTableDiv } from './transactions-table.styles';
import { useSelector } from '../home.provider';
import { ITransaction } from 'models/transaction';
import { getAsync } from 'api';
import DateComponent from '../common/date-component';
import AccountDetails from './account-details';


export const TransactionsTable: React.FC = () => {
    const user = useSelector(state => state.user);

    const {id, currency, currencySymbol } = user;
    
    const [transactions, setTransactions] = useState<ITransaction[]>([]);
    
    useEffect(() => {
        async function getAccountTransactions():Promise<void>{
            const response = await getAsync<ITransaction[]>(`/api/v1/accounts/${id.toString()}/transactions`);
    
            setTransactions(response.payload);
        }
        getAccountTransactions();

    }, [])

    const data = transactions.map(transaction => {
        return {
            id: transaction.id,
            occurrence: transaction.occurrence,
            transactionType: transaction.transactionType,
            tax: transaction.transactionType === 'Earnings' ? `${transaction.earningsTaxPerDay}%` : '',
            before: `${currencySymbol}${transaction.balanceBeforeTransaction.toFixed(2)}`,
            credit: transaction.transactionValue > 0 
                ? `${currencySymbol}${transaction.transactionValue.toFixed(2)}` 
                : '',
            debit: transaction.transactionValue < 0 
                ? `-${currencySymbol}${(transaction.transactionValue * -1).toFixed(2)}`
                : '',
            after: `${currencySymbol}${transaction.balanceAfterTransaction.toFixed(2)}`
        }
    })

    const format = currency === 'USD' ? 'MM-DD-YYYY hh:mm:ss' : 'DD/MM/YYYY hh:mm:ss';

    return <TransactionsTableDiv>
                <AccountDetails {...user}/>
                <MaterialTable 
                            title="Transactions"
                            options={{maxBodyHeight: '350px', minBodyHeight: '350px'}}
                            columns={[
                                {title: 'Transaction Identifier', field: 'id', cellStyle: {
                                    width: 250,
                                    maxWidth: 350
                                },
                                headerStyle: {
                                    width: 350,
                                    maxWidth: 350
                                }},
                                {title: 'Occurence', field: 'occurrence', render: row => <span><DateComponent date={row.occurrence} format={format} /></span>},
                                {title: 'Transaction', field: 'transactionType'},
                                {title: 'Tax', field: 'tax'},
                                {title: 'Balance Before', field: 'before'},
                                {title: 'Credit', field: 'credit'},
                                {title: 'Debit', field: 'debit' },
                                {title: 'Balance After', field: 'after'}
                            ]} 
                            data={data}>

                </MaterialTable>
        </TransactionsTableDiv>
}