import React, { Component } from "react";
import axios from 'axios';
import { Redirect } from 'react-router-dom';
import { withRouter } from "react-router";
import Spinner from '../../Spinner'

class CategoryForm extends Component {

    constructor(props) {
        super(props);

        this.state = {
            id: 0,
            loading: true,
            title: '',
            titleError: '',
            errorMessage: '',
            redirect: false
        }

        this.titlechangeHandler = this.titlechangeHandler.bind(this);
        this.categoryFormSubmitHandler = this.categoryFormSubmitHandler.bind(this);
    }

    componentDidMount() {
        let id = this.props.match.params.id;
        { this.setState({ ...this.state, id: id }) };
        if (id != 0){
            axios.get('api/product/GetCategory?id=' + id)
                .then((res) => {
                    if (res.data.status === true) {
                        {
                            this.setState({
                                ...this.state,
                                title: res.data.data
                            })
                        };
                    } else {
                        alert(res.data.errorMessage);
                    }
                })
                .catch((err) => {
                    alert('Error in connect to server!');
                })
                .then(() => {
                    { this.setState({ ...this.state, loading: false }) };
                });
            }else{
                { this.setState({ ...this.state, loading: false }) };
            }
    }

    titlechangeHandler(event) {
        this.setState({ ...this.state, title: event.target.value, titleError: '' }, () => {
            let titleError = '';

            if (this.state.title === '') {
                titleError = "Title field is requires.";
            } else if (this.state.title.length > 100) {
                titleError = "Maximum length of title field is 100 character.";
            }

            this.setState({ ...this.state, titleError: titleError });
        });
    }

    categoryFormSubmitHandler(event) {
        event.preventDefault();

        if (this.state.titleError !== '') {
            return;
        }

        this.setState({ ...this.state, loading: true });

        axios.post('api/product/categoryForm', {
            id: parseInt(this.state.id),
            title: this.state.title
        })
            .then((res) => {
                if (res.data.status === true) {
                    this.setState({ ...this.state, redirect: true });
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
        if (this.state.redirect)
            return <Redirect to="/admin/categories" />

        return (
            this.state.loading ? <Spinner /> :
                <div className="card">
                    <div className="card-body">
                        <h5 className="card-title text-center">Category Form</h5>
                        <form onSubmit={this.categoryFormSubmitHandler}>
                            {this.state.errorMessage ?
                                <div className="alert alert-warning">
                                    {this.state.errorMessage}
                                </div> :
                                null}

                            <div className="mb-3">
                                <label className="form-label">Title</label>
                                <input type="text" className="form-control" placeholder="Title" value={this.state.title} onChange={this.titlechangeHandler} />
                                <p className="text-danger">{this.state.titleError}</p>
                            </div>
                            <div className="mb-3">
                                {!this.state.loading ?
                                    <button className="btn btn-primary" type="submit">Save</button>
                                    : <Spinner />
                                }
                            </div>
                        </form>
                    </div>
                </div>
        );
    }
}

export default withRouter(CategoryForm)