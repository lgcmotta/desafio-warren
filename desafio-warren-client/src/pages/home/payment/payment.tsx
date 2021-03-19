import React, { useState } from 'react';
import { Theme, makeStyles } from '@material-ui/core/styles';
import TextField from '@material-ui/core/TextField';
import { Button, Paper, Typography } from '@material-ui/core';
import { PaymentButtonDiv, PaymentDiv, PaymentMoneyInputDiv, PaymentTyphographyDiv, PaymentInvoiceNumberDiv } from './payments.styles';
import MoneyInput from '../common/money-input';
import { useSelector } from '../home.provider';
import { ITransactionResponse } from 'models/transaction';
import { putAsync } from 'api';
import ErrorAlert from '../common/error-alert';

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


export const Payment: React.FC = () => {
    const [transferValue, setTransferValue] = useState('');
    const [invoiceNumber, setInvoiceNumber] = useState('');
    const [show, setShow] = useState(false);
    const [failures, setFailures] =useState<string>('');
    const classes = useStyles();
    const { id } = useSelector(state => state.user)

    const handleTransfer = () => {
        const numberValue = parseFloat(transferValue);

        async function putPayment(){
            await putAsync<ITransactionResponse>(`/api/v1/accounts/${id.toString()}/payment`, { value: numberValue, invoiceNumber: invoiceNumber })
            .then(response => {
                if(!response.failures.length){
                    setTransferValue('');
                    setInvoiceNumber('');
                }
            })
            .catch(err => {
                const failuresString = [...err.response.data.failures.map(failure => failure.errorMessage)].join(',\n');
                setFailures(failuresString);
                setShow(true)
            })

            
        }

        putPayment();

    }

    return <PaymentDiv>
            <Paper className={classes.root}>
                <PaymentTyphographyDiv>
                    <Typography variant='h5'>Please, inform the invoice code or number</Typography>
                </PaymentTyphographyDiv>
                <PaymentInvoiceNumberDiv>
                    <TextField variant="outlined" label="Invoice code or number" value={invoiceNumber} onChange={event => setInvoiceNumber(event.target.value)} fullWidth/>
                </PaymentInvoiceNumberDiv>
                <PaymentTyphographyDiv>
                    <Typography variant='h5'>How much do you want to pay?</Typography>
                </PaymentTyphographyDiv>
                <PaymentMoneyInputDiv>
                    <MoneyInput onChange={setTransferValue} value={transferValue} />
                </PaymentMoneyInputDiv>
                <PaymentButtonDiv>
                    <Button variant="contained" color="primary" fullWidth onClick={handleTransfer}>Transfer</Button>
                </PaymentButtonDiv>
            </Paper>
            <ErrorAlert show={show} setShow={setShow} message={failures}/>
        </PaymentDiv>
}