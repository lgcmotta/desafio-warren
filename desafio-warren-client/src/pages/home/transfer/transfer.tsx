import React, { useState } from 'react';
import MoneyInput from '../common/money-input';
import AvailableAccounts from './available-accounts';
import Notification from '../common/notification';
import { Theme, makeStyles } from '@material-ui/core/styles';
import { Button, Paper, Typography } from '@material-ui/core';
import { TransferButtonDiv, TransferDiv, TransferMoneyInputDiv, TransferTyphographyDiv } from './transfer.styles';
import { ITransactionResponse } from 'models/transaction';
import { putAsync } from 'api';
import { useSelector } from 'app/app.context';


const useStyles = makeStyles((theme: Theme) => ({
    root: {
      flexGrow: 1,
      backgroundColor: theme.palette.background.paper,
      marginTop: '10px',
      height: '100%',
      width: '100%',
      minHeight:'500px',
      display: 'flex',
      flexDirection:'column',
      justifyContent: 'flex-start',
      alignItems: 'center'
    }
  }));


export const Transfer: React.FC = () => {
    const [transferValue, setTransferValue] = useState('');
    
    const [accountNumber, setAccountNumber] = useState('');
    
    const [show, setShow] = useState(false);
    
    const [message, setMessage] =useState<string>('');
    
    const [severity, setSeverity] = useState<"error" | "success">('error');
    
    const classes = useStyles();
    
    const { id, currencySymbol } = useSelector(state => state.user)

    if(!id) return <></>

    const handleTransfer = () => {
        const numberValue = parseFloat(transferValue);

        async function putTransfer(){
            await putAsync<ITransactionResponse>(`/api/v1/accounts/${id.toString()}/transfer`, { value: numberValue, destinationAccount: accountNumber })
                .then(response => {
                    if(!response.failures.length){
                        setMessage(`${currencySymbol}${transferValue} transferred successfully!`);
                        setSeverity("success");
                        setShow(true);
                        setTransferValue('');
                        setAccountNumber('');
                    }
                }).catch(err => {
                    const failuresString = [...err.response.data.failures.map(failure => failure.errorMessage)].join(',\n');
                    setMessage(failuresString);
                    setSeverity("error");
                    setShow(true);
                })

            
        }

        putTransfer();

    }

    return <TransferDiv>
            <Paper className={classes.root}>
                <TransferTyphographyDiv>
                    <Typography variant='h5'>To whom do you want to transfer?</Typography>
                </TransferTyphographyDiv>
                <TransferTyphographyDiv>
                    <AvailableAccounts accountNumber={accountNumber} onSelectedAccountChange={setAccountNumber}/>
                </TransferTyphographyDiv>
                <TransferTyphographyDiv>
                    <Typography variant='h5'>How much do you want to transfer?</Typography>
                </TransferTyphographyDiv>
                <TransferMoneyInputDiv>
                    <MoneyInput onChange={setTransferValue} value={transferValue} />
                </TransferMoneyInputDiv>
                <TransferButtonDiv>
                    <Button variant="contained" color="primary" fullWidth onClick={handleTransfer}>Transfer</Button>
                </TransferButtonDiv>
            </Paper>
            <Notification show={show} setShow={setShow} message={message} severity={severity}/>
        </TransferDiv>
}