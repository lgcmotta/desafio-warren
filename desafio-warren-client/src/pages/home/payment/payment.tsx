import React, { useState } from 'react';
import { Theme, makeStyles } from '@material-ui/core/styles';
import { Button, Paper, TextField, Typography } from '@material-ui/core';
import { PaymentButtonDiv, PaymentDiv, PaymentMoneyInputDiv, PaymentTyphographyDiv, PaymentInvoiceNumberDiv } from './payments.styles';
import MoneyInput from '../common/money-input';
import { ITransactionResponse } from 'models/transaction';
import { putAsync } from 'api';
import Notification from '../common/notification';
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


export const Payment: React.FC = () => {
    const [paymentValue, setPaymentValue] = useState('');
    
    const [invoiceNumber, setInvoiceNumber] = useState('');
    
    const [show, setShow] = useState(false);
    
    const [message, setMessage] =useState<string>('');
    
    const [severity, setSeverity] = useState<"error" | "success">('error');
    
    const classes = useStyles();
    
    const { id, currencySymbol } = useSelector(state => state.user)

    const handleTransfer = () => {
        const numberValue = parseFloat(paymentValue);

        async function putPayment(){
            await putAsync<ITransactionResponse>(`/api/v1/accounts/${id.toString()}/payment`, { value: numberValue, invoiceNumber: invoiceNumber })
            .then(response => {
                if(!response.failures.length){
                    setMessage(`${currencySymbol}${paymentValue} paid successfully!`);
                    setSeverity("success");
                    setShow(true);
                    setPaymentValue('');
                    setInvoiceNumber('');
                }
            })
            .catch(err => {
                const failuresString = [...err.response.data.failures.map(failure => failure.errorMessage)].join(',\n');
                setMessage(failuresString);
                setSeverity("error");
                setShow(true);
            })

            
        }

        putPayment();

    }

    return <PaymentDiv>
            <Paper className={classes.root}>
                <PaymentTyphographyDiv>
                    <Typography variant='h5'>Please enter the code or invoice number</Typography>
                </PaymentTyphographyDiv>
                <PaymentInvoiceNumberDiv>
                    <TextField variant="outlined" label="Invoice code or number" value={invoiceNumber} onChange={event => setInvoiceNumber(event.target.value)} fullWidth/>
                </PaymentInvoiceNumberDiv>
                <PaymentTyphographyDiv>
                    <Typography variant='h5'>What's the total amount for this invoice?</Typography>
                </PaymentTyphographyDiv>
                <PaymentMoneyInputDiv>
                    <MoneyInput onChange={setPaymentValue} value={paymentValue} />
                </PaymentMoneyInputDiv>
                <PaymentButtonDiv>
                    <Button variant="contained" color="primary" fullWidth onClick={handleTransfer}>Pay</Button>
                </PaymentButtonDiv>
            </Paper>
            <Notification show={show} setShow={setShow} message={message} severity={severity}/>
        </PaymentDiv>
}