import React, { Component } from 'react';
import { Route, Switch } from 'react-router';
import BackLayout from './backend/components/BackLayout';
import Login from './backend/containers/Login';
import Register from './backend/containers/Register';
import FrontLayout from './frontend/components/FrontLayout';

export default class App extends Component {
    render() {
        return (
            <Switch>
                <Route path='/admin/login' render={() => <Login />} />
                <Route path='/admin/register' render={() => <Register />} />
                <Route path='/admin' render={() => <BackLayout />} />
                <Route path='/' render={() => <FrontLayout />} />
            </Switch>
        );
    }
}
