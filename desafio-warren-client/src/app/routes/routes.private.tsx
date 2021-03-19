import { Pages } from 'pages';
import React, { lazy } from 'react';
import { Redirect, Route, Switch } from 'react-router-dom';

const LazyHome = lazy(() => import('../../pages/home'));

const PrivateRoutes: React.FC = () => {
    return <Switch>
        <Route exact path={Pages.Home} component={LazyHome}></Route>
        <Redirect to={Pages.Home} />
    </Switch>
}

export default PrivateRoutes;