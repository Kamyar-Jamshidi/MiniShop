import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import { withRouter } from "react-router";
import axios from 'axios';

class Product extends Component {
    constructor(props) {
        super(props);

        this.state = {
            id: 0,
            title: '',
            categoryTitle: '',
            likes: 0,
            isTopRate: false,
            createOn: '',
            desc: '',
            loading: true,
            redirect: false,
            isLiked: false,

        }

        this.likeClickHandler = this.likeClickHandler.bind(this);
    }

    componentDidMount() {
        let id = this.props.match.params.id;
        { this.setState({ ...this.state, id: id }) };

        if (id != 0)
            axios.get('api/product/GetProduct?id=' + id)
                .then((res) => {
                    if (res.data.status === true) {
                        {
                            this.setState({
                                ...this.state,
                                id: res.data.data.id,
                                title: res.data.data.title,
                                categoryTitle: res.data.data.categoryTitle,
                                likes: res.data.data.likes,
                                isTopRate: res.data.data.isTopRate,
                                createOn: res.data.data.createOn,
                                desc: res.data.data.description
                            })
                        };
                    } else {
                        alert('Error in connect to server!');
                    }
                })
                .catch((err) => {
                    alert('Error in connect to server!');
                })
                .then(() => {
                    this.setState({ ...this.state, loading: false })
                });
        else
            this.setState({ ...this.state, redirect: true })
    }

    likeClickHandler(event) {
        event.preventDefault();

        axios.get('api/product/LikeProduct?id=' + this.state.id + '&like=' + !this.state.isLiked)
            .then((res) => {
                if (res.data.status === true) {
                    if (res.data.data === true)
                    {
                        this.setState({
                            ...this.state,
                            isLiked: !this.state.isLiked,
                            likes: this.state.isLiked ? this.state.likes - 1 : this.state.likes + 1
                        })
                    };
                } else {
                    alert('Error in connect to server!');
                }
            })
            .catch((err) => {
                alert('Error in connect to server!');
            })
            .then(() => {
                this.setState({ ...this.state, loading: false })
            });
    }

    render() {
        if (this.state.redirect)
            return <Redirect to="/" />

        return (
            <div className="card">
                <div className="card-body">
                    <h5 className="card-title">{this.state.title}</h5>
                    <p className="card-text">Category: {this.state.categoryTitle}</p>
                    <p className="card-text">Likes: {this.state.likes}</p>
                    <p className="card-text">Created on: {this.state.createOn}</p>
                    <p className="card-text">Top Product: {this.state.isTopRate ? "Yes" : "No"}</p>
                    <p className="card-text">Description: {this.state.desc}</p>
                    <a href="#" className="card-link" onClick={this.likeClickHandler}>{this.state.isLiked ? "Dislike" : "Like"}</a>
                </div>
            </div>
        );
    }
}

export default withRouter(Product)