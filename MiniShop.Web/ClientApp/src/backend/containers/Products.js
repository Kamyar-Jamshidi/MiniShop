import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';
import { connect } from 'react-redux';
import Spinner from '../../Spinner'

class Products extends Component {

    constructor(props) {
        super(props);

        this.state = {
            productList: [],
            loading: true
        }

        this.changeProductStatus = this.changeProductStatus.bind(this);
    }

    componentDidMount() {
        axios.post('api/product/GetAllProducts', {
            token: this.props.token
        })
            .then((res) => {
                if (res.data.status === true) {
                    if (res.data.data) {
                        var list = res.data.data.map((item) => { return { ...item, loading: false } });
                        this.setState({ ...this.state, productList: list })
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

    changeProductStatus(id) {
        let productList = this.state.productList;
        var index = productList.findIndex(x => x.id === id);
        productList[index].loading = true;
        this.setState({ ...this.state, productList: productList });

        axios.post('api/product/ChangeProductStatus', {
            token: this.props.token,
            productId: id
        })
            .then((res) => {
                if (res.data.status === true &&
                    res.data.data === true) {
                    productList[index].isApproved = !productList[index].isApproved;
                    this.setState({ ...this.state, productList: productList });
                } else {
                    alert(res.data.errorMessage);
                }
            })
            .catch((err) => {
                alert('Error in connect to server!');
            })
            .then(() => {
                productList[index].loading = false;
                this.setState({ ...this.state, productList: productList });
            });
    }

    render() {
        return (
            <React.Fragment>
                <h3>Products</h3>
                <hr />
                {this.state.loading ? <Spinner /> :
                    <table className="table">
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">Title</th>
                                <th scope="col">Category</th>
                                <th scope="col">Likes</th>
                                <th scope="col">Top-Rate</th>
                                <th scope="col">Created on</th>
                                <th scope="col">Status</th>
                                <th scope="col"></th>
                            </tr>
                        </thead>
                        <tbody>
                            {this.state.productList.map((item, i) => {
                                return (<tr key={item.id}>
                                    <th scope="row">{i + 1}</th>
                                    <td>{item.title}</td>
                                    <td>{item.categoryTitle}</td>
                                    <td>{item.likes}</td>
                                    <td>{item.isTopRate? "Yes": "No"}</td>
                                    <td>{item.createOn}</td>
                                    <td>{this.props.isSuperAdmin ? item.loading ?
                                        <Spinner align={"left"} /> :
                                        item.isApproved ?
                                            <button className="btn btn-sm btn-warning" type="button" onClick={() => this.changeProductStatus(item.id)}>Disable</button> :
                                            <button className="btn btn-sm btn-primary" type="button" onClick={() => this.changeProductStatus(item.id)}>Enable</button> :
                                        item.isApproved ? "Enable" : "Disable"}
                                    </td>
                                    <td>
                                        <Link className="btn btn-primary" to={'/admin/products/' + item.id + '/productForm/'}>Edit</Link>
                                    </td>
                                </tr>)
                            })}
                        </tbody>
                    </table>}
                <Link className="btn btn-primary" to="/admin/products/0/productForm/">New Product</Link>
            </React.Fragment>
        );
    }
}

const mapStateToProps = state => {
    return {
        token: state.token,
        isSuperAdmin: state.isSuperAdmin
    };
};

export default connect(mapStateToProps)(Products)
