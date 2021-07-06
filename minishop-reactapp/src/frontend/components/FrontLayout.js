import React, { Component } from 'react';
import { Route, Switch, Link } from 'react-router-dom';
import Products from '../containers/Products'
import Product from '../containers/Product'

export default class Layout extends Component {

    render() {
        return (
            <React.Fragment>
                <div className="row">
                    <div className="col">
                        <nav className="navbar navbar-expand-lg navbar-light bg-light">
                            <div className="container-fluid">
                                <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                                    <span className="navbar-toggler-icon"></span>
                                </button>
                                <div className="collapse navbar-collapse" id="navbarSupportedContent">
                                    <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                                        <li className="nav-item">
                                            <Link className="nav-link" to="/Products/New">New Products</Link>
                                        </li>
                                        <li className="nav-item">
                                            <Link className="nav-link" to="/Products/Top">Top Products</Link>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </nav>
                    </div>
                </div>
                <div className="container-fluid">
                    <div className="row">
                        <div className="col">
                            <Switch>
                                <Route key="New" path='/Products/New' render={() => <Products type="New" />} />
                                <Route key="Top" path='/Products/Top' render={() => <Products type="Top" />} />
                                <Route path='/Product/:id' render={() => <Product />} />
                                <Route key="Home" path='/' render={() => <Products type="New" />} />
                            </Switch>
                        </div>
                    </div>
                </div>
            </React.Fragment>
        );
    }
}
