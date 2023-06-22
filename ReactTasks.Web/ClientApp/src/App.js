import React, { Component } from 'react';
import { Route } from 'react-router';
import Home from './Home';
import Layout from './Layout';
import Login from './Login';
import Logout from './Logout';
import SignUp from './Signup';
import { AuthContextComponent } from './AuthContext';
import PrivateRoute from './PrivateRoute';




export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <AuthContextComponent>
                <Layout>
                    <Route exact path='/' component={Home} />
                    <Route exact path='/login' component={Login} />
                    <Route exact path='/logout' component={Logout} />
                    <Route exact path='/Signup' component={SignUp} />
                </Layout>
            </AuthContextComponent>
        );
    }
}