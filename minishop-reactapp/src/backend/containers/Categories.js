import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';
import Spinner from '../../Spinner'

export default class Categories extends Component {

    constructor(props) {
        super(props);

        this.state = {
            categoryList: [],
            loading: true
        }
    }

    componentDidMount() {
        axios.post('api/product/GetCategories')
            .then((res) => {
                if (res.data.status === true) {
                    if (res.data.data) {
                        this.setState({ ...this.state, categoryList: res.data.data })
                    }
                } else {
                    alert(res.data.errorMessage);
                }
            })
            .catch((err) => {
                alert('Error in connect to server!');
            })
            .then(() => { this.setState({ ...this.state, loading: false }) });
    }

    render() {
        return (
            <React.Fragment>
                <h3>Categories</h3>
                <hr />
                {this.state.loading ? <Spinner /> :
                    <table className="table">
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">Title</th>
                                <th scope="col">Created on</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            {this.state.categoryList.map((item, i) => {
                                return (<tr key={item.id}>
                                    <th scope="row">{i + 1}</th>
                                    <td>{item.title}</td>
                                    <td>{item.createOn}</td>
                                    <td>
                                        <Link className="btn btn-primary" to={'/admin/categories/' + item.id + '/categoryForm/'}>Edit</Link>
                                    </td>
                                </tr>)
                            })}
                        </tbody>
                    </table>}
                <Link className="btn btn-primary" to="/admin/categories/0/categoryForm/">New Category</Link>
            </React.Fragment>
        );
    }
}
