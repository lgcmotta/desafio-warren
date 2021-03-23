import React, { useState } from 'react';
import MoneyInput from '../common/money-input';
import Notification from '../common/notification';
import { ITransactionResponse } from 'models/transaction';
import { putAsync } from 'api';
import { Theme, makeStyles } from '@material-ui/core/styles';
import { Button, Paper, Typography } from '@material-ui/core';
import { WithdrawButtonDiv, WithdrawDiv, WithdrawMoneyInputDiv, WithdrawTyphographyDiv } from './withdraw.styles';
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


export const Withdraw: React.FC = () => {
    const [value, setValue] = useState('');
    
    const [show, setShow] = useState(false);
    
    const [message, setMessage] =useState<string>('');
    
    const [severity, setSeverity] = useState<"error" | "success">('error');
    
    const classes = useStyles();
    
    const { id, currencySymbol } = useSelector(state => state.user)

    const handleWithdraw = () => {
        const numberValue = parseFloat(value);

        async function putWithdraw(){
            await putAsync<ITransactionResponse>(`/api/v1/accounts/${id.toString()}/withdraw`, { value: numberValue })
            .then(response => {
                if(!response.failures.length){
                    setMessage(`${currencySymbol}${value} withdrawn successfully!`);
                    setSeverity("success");
                    setShow(true);
                    setValue('');
                }
            })
            .catch(err => {
                const failuresString = [...err.response.data.failures.map(failure => failure.errorMessage)].join(',\n');
                setMessage(failuresString);
                setSeverity("error");
                setShow(true);
            })

            
        }

        putWithdraw();

    }

    return <WithdrawDiv>
            <Paper className={classes.root}>
                <WithdrawTyphographyDiv>
                    <Typography variant='h5'>How much do you want to withdraw?</Typography>
                </WithdrawTyphographyDiv>
                <WithdrawMoneyInputDiv>
                    <MoneyInput onChange={setValue} value={value} />
                </WithdrawMoneyInputDiv>
                <WithdrawButtonDiv>
                    <Button variant="contained" color="primary" fullWidth onClick={handleWithdraw}>Withdraw</Button>
                </WithdrawButtonDiv>
            </Paper>
            <Notification show={show} setShow={setShow} message={message} severity={severity}/>
        </WithdrawDiv>
}