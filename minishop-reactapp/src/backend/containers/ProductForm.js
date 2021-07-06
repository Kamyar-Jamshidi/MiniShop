import React, { Component } from "react";
import axios from 'axios';
import { Redirect } from 'react-router-dom';
import { withRouter } from "react-router";
import Spinner from '../../Spinner'

class ProductForm extends Component {

    constructor(props) {
        super(props);

        this.state = {
            id: 0,
            loading: true,
            title: '',
            titleError: '',
            desc: '',
            descError: '',
            isTopRate: false,
            categoryId: 0,
            categories: [],
            errorMessage: '',
            redirect: false
        }

        this.titlechangeHandler = this.titlechangeHandler.bind(this);
        this.descchangeHandler = this.descchangeHandler.bind(this);
        this.categoryChangeHandler = this.categoryChangeHandler.bind(this);
        this.topRateChangeHandler = this.topRateChangeHandler.bind(this);
        this.productFormSubmitHandler = this.productFormSubmitHandler.bind(this);
    }

    componentDidMount() {
        axios.post('api/product/GetCategories')
            .then((res) => {
                if (res.data.status === true) {
                    if (res.data.data.length > 0) {

                        {
                            this.setState({
                                ...this.state,
                                categories: res.data.data,
                                categoryId: this.state.categoryId === 0 ? res.data.data[0].id : this.state.categoryId,
                                loading: false,
                            })
                        };

                    } else {
                        alert('There is no category!');

                        {
                            this.setState({
                                ...this.state,
                                redirect: true
                            })
                        };
                    }
                } else {
                    alert(res.data.errorMessage);
                }
            })
            .catch((err) => {
                alert('Error in connect to server!');
            })
            .then(() => { });

        let id = this.props.match.params.id;
        { this.setState({ ...this.state, id: id }) };
        if (id != 0)
            axios.get('api/product/GetProduct?id=' + id)
                .then((res) => {
                    { this.setState({ ...this.state, loading: true }) };

                    if (res.data.status === true) {
                        {
                            this.setState({
                                ...this.state,
                                title: res.data.data.title,
                                desc: res.data.data.description,
                                isTopRate: res.data.data.isTopRate,
                                categoryId: res.data.data.categoryId,
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

    descchangeHandler(event) {
        this.setState({ ...this.state, desc: event.target.value, descError: '' }, () => {
            let descError = '';

            if (this.state.desc === '')
                descError = "Description field is requires.";

            this.setState({ ...this.state, descError: descError });
        });
    }

    categoryChangeHandler(event) {
        this.setState({ ...this.state, categoryId: event.target.value });
    }

    topRateChangeHandler(event) {
        this.setState({ ...this.state, isTopRate: event.target.checked });
    }

    productFormSubmitHandler(event) {
        event.preventDefault();

        if (this.state.titleError !== '' &&
            this.state.descError !== '') {
            return;
        }

        this.setState({ ...this.state, loading: true });

        axios.post('api/product/productForm', {
            id: parseInt(this.state.id),
            title: this.state.title,
            description: this.state.desc,
            isTopRate: this.state.isTopRate,
            categoryId: parseInt(this.state.categoryId),
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
            return <Redirect to="/admin/products" />

        return (
            this.state.loading ? <Spinner /> :
                <div className="card">
                    <div className="card-body">
                        <h5 className="card-title text-center">Product Form</h5>
                        <form onSubmit={this.productFormSubmitHandler}>
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
                                <label className="form-label">Category</label>
                                <select value={this.state.categoryId} onChange={this.categoryChangeHandler} className="form-control">
                                    {this.state.categories.map((item) => {
                                        return (<option key={item.id} value={item.id}>{item.title}</option>);
                                    })}
                                </select>
                            </div>
                            <div className="mb-3">
                                <label className="form-label">Description</label>
                                <textarea type="text" className="form-control" placeholder="Description" value={this.state.desc} onChange={this.descchangeHandler}></textarea>
                                <p className="text-danger">{this.state.descError}</p>
                            </div>
                            <div className="mb-3">
                                <div className="form-check">
                                    <input className="form-check-input" type="checkbox" checked={this.state.isTopRate} onChange={this.topRateChangeHandler} />
                                    <label className="form-check-label">Top-Rate</label>
                                </div>
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

export default withRouter(ProductForm)