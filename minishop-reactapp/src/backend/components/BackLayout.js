import React, { Component } from 'react';
import { Route, Switch } from 'react-router';
import { Link, Redirect } from 'react-router-dom';
import { connect } from 'react-redux';
import Products from '../containers/Products';
import ProductForm from '../containers/ProductForm';
import Users from '../containers/Users';
import Categories from '../containers/Categories';
import CategoryForm from '../containers/CategoryForm';

class BackLayout extends Component {

    constructor(props) {
        super(props);

        this.logoutHandler = this.logoutHandler.bind(this);
    }

    logoutHandler(event) {
        event.preventDefault();

        this.props.unsetData();
    }

    render() {
        if (this.props.firstName === '')
            return <Redirect to="/admin/login" />

        return (
            <div className="container-fluid" style={{ marginTop: "50px" }}>
                <div className="row">
                    <div className="col-lg-2 col-sm-3">
                        <div className="card">
                            <div className="card-body">
                                <h5 className="card-title">Welcome {this.props.firstName}</h5>
                                <ul className="nav flex-column">
                                    <li className="nav-item">
                                        <Link className="nav-link" to="/admin/products">Products</Link>
                                    </li>
                                    <li className="nav-item">
                                        <Link className="nav-link" to="/admin/categories">Categories</Link>
                                    </li>
                                    {this.props.isSuperAdmin ?
                                        <li className="nav-item">
                                            <Link className="nav-link" to="/admin/users">Users</Link>
                                        </li>
                                        : null}
                                    <li className="nav-item">
                                        <a className="nav-link" href="#" onClick={this.logoutHandler}>Sign out</a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div className="col-lg-10 col-sm-9">
                        <Switch>
                            <Route path='/admin/products/:id/productForm/'><ProductForm /></Route>
                            <Route path='/admin/products' render={() => <Products />} />
                            <Route path='/admin/categories/:id/categoryForm/'><CategoryForm /></Route>
                            <Route path='/admin/categories' render={() => <Categories />} />
                            <Route path='/admin/users' render={() => <Users />} />
                            <Route path='/admin' render={() => <Products />} />
                        </Switch>
                    </div>
                </div>
            </div>
        );
    }
}

const mapStateToProps = state => {
    return {
        firstName: state.firstName,
        lastName: state.lastName,
        isSuperAdmin: state.isSuperAdmin,
    };
};

const mapDispatchToProps = dispatch => {
    return {
        unsetData: () => dispatch({ type: 'UNSET_DATA' })
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(BackLayout)
