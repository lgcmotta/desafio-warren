import React, { useState } from 'react';
import { Theme, makeStyles } from '@material-ui/core/styles';
import { Button, Paper, Typography } from '@material-ui/core';
import { DepositButtonDiv, DepositDiv, DepositMoneyInputDiv, DepositTyphographyDiv } from './deposit.styles';
import MoneyInput from '../common/money-input';
import { useSelector } from '../home.provider';
import { ITransactionResponse } from 'models/transaction';
import { postAsync } from 'api';
import Notification from '../common/notification';



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


export const Deposit: React.FC = () => {
    const [value, setValue] = useState('');
    
    const classes = useStyles();
    
    const { id, currencySymbol } = useSelector(state => state.user)
    
    const [show, setShow] = useState(false);
    
    const [message, setMessage] =useState<string>('');
    
    const [severity, setSeverity] = useState<"error" | "success">('error');
    
    const handleDeposit = () => {
        const numberValue = parseFloat(value);

        async function postDeposit(){
            await postAsync<ITransactionResponse>(`/api/v1/accounts/${id.toString()}/deposit`, { value: numberValue }).then(response => {
                if(!response.failures.length){
                    setMessage(`${currencySymbol}${value} deposited  successfully!`);
                    setSeverity("success");
                    setShow(true);
                    setValue('');
                }
            }).catch(err => {
                const failuresString = [...err.response.data.failures.map(failure => failure.errorMessage)].join(',\n');
                setMessage(failuresString);
                setSeverity("error");
                setShow(true);
            });
            
        }

        postDeposit();

    }

    return <DepositDiv>
            <Paper className={classes.root}>
                <DepositTyphographyDiv>
                    <Typography variant='h5'>How much do you want to deposit?</Typography>
                </DepositTyphographyDiv>
                <DepositMoneyInputDiv>
                    <MoneyInput onChange={setValue} value={value} />
                </DepositMoneyInputDiv>
                <DepositButtonDiv>
                    <Button variant="contained" color="primary" fullWidth onClick={handleDeposit}>Deposit</Button>
                </DepositButtonDiv>
            </Paper>'
            <Notification show={show} setShow={setShow} message={message} severity={severity}/>
        </DepositDiv>
}