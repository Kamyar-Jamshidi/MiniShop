import React, { Component } from "react";
import axios from 'axios';
import { Link, Redirect } from 'react-router-dom';
import { connect } from 'react-redux';
import Spinner from '../../Spinner'

class Register extends Component {

    constructor(props) {
        super(props);

        this.state = {
            loading: false,
            firstName: '',
            firstNameError: '',
            lastName: '',
            lastNameError: '',
            username: '',
            usernameError: '',
            password: '',
            passwordError: '',
            confirmPassword: '',
            confirmPasswordError: '',
            errorMessage: ''
        }

        this.firstNameChangeHandler = this.firstNameChangeHandler.bind(this);
        this.lastNameChangeHandler = this.lastNameChangeHandler.bind(this);
        this.usernameChangeHandler = this.usernameChangeHandler.bind(this);
        this.passwordChangeHandler = this.passwordChangeHandler.bind(this);
        this.confirmPasswordChangeHandler = this.confirmPasswordChangeHandler.bind(this);
        this.signUpFormSubmitHandler = this.signUpFormSubmitHandler.bind(this);
    }

    firstNameChangeHandler(event) {
        this.setState({ ...this.state, firstName: event.target.value, firstNameError: '' }, () => {
            let errors = {};

            if (this.state.firstName === '') {
                errors.firstNameError = "First name field is requires.";
            } else if (this.state.firstName.length > 100) {
                errors.firstNameError = "Maximum length of first name field is 100 character.";
            }

            this.setState({ ...this.state, ...errors });
        });
    }

    lastNameChangeHandler(event) {
        this.setState({ ...this.state, lastName: event.target.value, lastNameError: '' }, () => {
            let errors = {};

            if (this.state.lastName === '') {
                errors.lastNameError = "Last name field is requires.";
            } else if (this.state.lastName.length > 100) {
                errors.lastNameError = "Maximum length of last name field is 100 character.";
            }

            this.setState({ ...this.state, ...errors });
        });
    }

    usernameChangeHandler(event) {
        this.setState({ ...this.state, username: event.target.value, usernameError: '' }, () => {
            let errors = {};

            if (this.state.username === '') {
                errors.usernameError = "Username field is requires.";
            } else if (this.state.username.length > 100) {
                errors.usernameError = "Maximum length of username field is 100 character.";
            }

            this.setState({ ...this.state, ...errors });
        });
    }

    passwordChangeHandler(event) {
        this.setState({ ...this.state, password: event.target.value, passwordError: '' }, () => {
            let errors = {};

            if (this.state.password === '') {
                errors.passwordError = "Password field is requires.";
            } else if (this.state.password.length < 6) {
                errors.passwordError = "Minimum length of password field is 6 character.";
            } else if (this.state.password.length > 15) {
                errors.passwordError = "Maximum length of password field is 15 character.";
            }

            this.setState({ ...this.state, ...errors });
        });
    }

    confirmPasswordChangeHandler(event) {
        this.setState({ ...this.state, confirmPassword: event.target.value, confirmPasswordError: '' }, () => {
            let errors = {};

            if (this.state.confirmPassword === '') {
                errors.confirmPasswordError = "Confirm password field is requires.";
            } else if (this.state.password !== this.state.confirmPassword) {
                errors.confirmPasswordError = "Confirm password' and 'password' do not match";
            }

            this.setState({ ...this.state, ...errors });
        });
    }

    signUpFormSubmitHandler(event) {
        event.preventDefault();

        if (this.state.firstNameError !== '' &&
            this.state.lastNameError !== '' &&
            this.state.usernameError !== '' &&
            this.state.passwordError !== '' &&
            this.state.confirmPasswordError !== '') {
            return;
        }

        this.setState({ ...this.state, loading: true });

        axios.post('api/account/RegisterUser', {
            firstName: this.state.firstName,
            lastName: this.state.lastName,
            username: this.state.username,
            password: this.state.password,
            confirmPassword: this.state.confirmPassword,
        })
            .then((res) => {
                if (res.data.status === true) {
                    if (res.data.data) {
                        alert('Congratulations. You have registered successfully. Please wait for the admin approval');
                    }
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
        if (this.props.token !== '')
            return <Redirect to="/admin" />

        return (
            <div className="container">
                <div className="row justify-content-center">
                    <div className="col-6" style={{ marginTop: "150px" }}>
                        <div className="card">
                            <div className="card-body">
                                <h5 className="card-title text-center">Sign up</h5>
                                <form onSubmit={this.signUpFormSubmitHandler}>
                                    {this.state.errorMessage ? <div className="alert alert-warning">
                                        {this.state.errorMessage}
                                    </div> : null}
                                    <div className="mb-3">
                                        <label className="form-label">First Name</label>
                                        <input type="text" className="form-control" placeholder="Your first name" value={this.firstName} onChange={this.firstNameChangeHandler} />
                                        <p className="text-danger">{this.state.firstNameError}</p>
                                    </div>
                                    <div className="mb-3">
                                        <label className="form-label">Last Name</label>
                                        <input type="text" className="form-control" placeholder="Your last name" value={this.lastName} onChange={this.lastNameChangeHandler} />
                                        <p className="text-danger">{this.state.lastNameError}</p>
                                    </div>
                                    <div className="mb-3">
                                        <label className="form-label">Username</label>
                                        <input type="text" className="form-control" placeholder="Your username" value={this.username} onChange={this.usernameChangeHandler} />
                                        <p className="text-danger">{this.state.usernameError}</p>
                                    </div>
                                    <div className="mb-3">
                                        <label className="form-label">Password</label>
                                        <input type="password" className="form-control" placeholder="Your password" value={this.password} onChange={this.passwordChangeHandler} />
                                        <p className="text-danger">{this.state.passwordError}</p>
                                    </div>
                                    <div className="mb-3">
                                        <label className="form-label">Confirm Password</label>
                                        <input type="password" className="form-control" placeholder="Confirm your password" value={this.confirmPassword} onChange={this.confirmPasswordChangeHandler} />
                                        <p className="text-danger">{this.state.confirmPasswordError}</p>
                                    </div>
                                    <div className="mb-3">
                                        {!this.state.loading ?
                                            <button className="btn btn-primary btn-block" type="submit">Sign in</button>
                                            : <Spinner />
                                        }
                                    </div>
                                </form>
                                <div className="mb-3 text-center">
                                    <Link to="/admin/login">Sign in to your account.</Link>
                                </div>
                            </div>
                        </div>
                        <br />
                        <br />
                    </div>
                </div>
            </div>
        );
    }
}

const mapStateToProps = state => {
    return {
        token: state.token
    };
};

const mapDispatchToProps = dispatch => {
    return {
        setData: (data) => dispatch({ type: 'SET_DATA', data })
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(Register)