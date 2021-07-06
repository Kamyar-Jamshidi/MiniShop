import React, { Component } from "react";
import axios from 'axios';
import { Link, Redirect } from 'react-router-dom';
import { connect } from 'react-redux';
import Spinner from '../../Spinner'

class Login extends Component {

    constructor(props) {
        super(props);

        this.state = {
            loading: false,
            username: '',
            usernameError: '',
            password: '',
            passwordError: '',
            errorMessage: ''
        }

        this.usernameChangeHandler = this.usernameChangeHandler.bind(this);
        this.passwordChangeHandler = this.passwordChangeHandler.bind(this);
        this.signInFormSubmitHandler = this.signInFormSubmitHandler.bind(this);
    }

    usernameChangeHandler(event) {
        this.setState({ ...this.state, username: event.target.value, usernameError: '' }, () => {
            let errors = {};

            if (this.state.username === '') {
                errors.usernameError = "Username field is requires.";
            }

            this.setState({ ...this.state, ...errors });
        });
    }

    passwordChangeHandler(event) {
        this.setState({ ...this.state, password: event.target.value, passwordError: '' }, () => {
            let errors = {};

            if (this.state.password === '') {
                errors.passwordError = "Password field is requires.";
            }

            this.setState({ ...this.state, ...errors });
        });
    }

    signInFormSubmitHandler(event) {
        event.preventDefault();

        if (this.state.usernameError !== '' &&
            this.state.passwordError !== '') {
            return;
        }

        this.setState({ ...this.state, loading: true });

        axios.post('api/account/CheckAuthData', {
            username: this.state.username,
            password: this.state.password,
        })
            .then((res) => {
                if (res.data.status === true) {

                    axios.defaults.headers.common['Authorization'] ='Bearer ' + res.data.data.token;

                    this.props.setData({
                        firstName: res.data.data.firstName,
                        lastName: res.data.data.lastName,
                        isSuperAdmin: res.data.data.isSuperAdmin
                    });
                } else {
                    this.setState({ ...this.state, errorMessage: res.data.errorMessage });
                }
            })
            .catch((err) => {
                alert('Error in connect to server!');
            })
            .then(() => { this.setState({ ...this.state, loading: false }) });
    }

    render() {
        if (this.props.firstName !== '')
            return <Redirect to="/admin" />

        return (
            <div className="container">
                <div className="row justify-content-center">
                    <div className="col-6" style={{ marginTop: "150px" }}>
                        <div className="card">
                            <div className="card-body">
                                <h5 className="card-title text-center">Sign in</h5>
                                <form onSubmit={this.signInFormSubmitHandler}>
                                    {this.state.errorMessage ? <div className="alert alert-warning">
                                        {this.state.errorMessage}
                                    </div> : null}
                                    <div className="mb-3">
                                        <label className="form-label">Usename</label>
                                        <input type="text" className="form-control" placeholder="Your username" value={this.username} onChange={this.usernameChangeHandler} />
                                        <p className="text-danger">{this.state.usernameError}</p>
                                    </div>
                                    <div className="mb-3">
                                        <label className="form-label">Password</label>
                                        <input type="password" className="form-control" placeholder="Your password" value={this.password} onChange={this.passwordChangeHandler} />
                                        <p className="text-danger">{this.state.passwordError}</p>
                                    </div>
                                    <div className="mb-3">
                                        {!this.state.loading ?
                                            <button className="btn btn-primary btn-block" type="submit">Sign in</button>
                                            : <Spinner />
                                        }
                                    </div>
                                </form>
                                <div className="mb-3 text-center">
                                    <Link to="/admin/register">Create an account.</Link>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

const mapStateToProps = state => {
    return {
        firstName: state.firstName
    };
};

const mapDispatchToProps = dispatch => {
    return {
        setData: (data) => dispatch({ type: 'SET_DATA', data })
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(Login)