import { Snackbar } from '@material-ui/core';
import React from 'react';
import MuiAlert, { AlertProps } from '@material-ui/lab/Alert';

interface IErroAlertProps {
    show: boolean;
    setShow: React.Dispatch<boolean>;
    message: string;
}

function Alert(props: AlertProps) {
    return <MuiAlert elevation={6} variant="filled" {...props} />;
  }


export const ErrorAlert: React.FC<IErroAlertProps> = (props: IErroAlertProps) => {
    const { show, setShow, message} = props;

    const handleClose = (event?: React.SyntheticEvent, reason?: string) => {
        if (reason === 'clickaway') {
          return;
        }
    
        setShow(false);
      };

    return <Snackbar open={show} autoHideDuration={6000} onClose={handleClose} anchorOrigin={{ vertical:'top', horizontal:'right' }}>
                <Alert onClose={handleClose} severity="error">
                {message}
                </Alert>
        </Snackbar>
}