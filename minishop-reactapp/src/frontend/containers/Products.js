import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';
import Spinner from '../../Spinner';

class Products extends Component {

    constructor(props) {
        super(props);

        this.state = {
            productList: [],
            loading: true,
            name: 'New'
        }
    }

    componentDidMount() {
        let action = 'GetNewProducts';
        if (this.props.type &&
            this.props.type.toLowerCase() === 'top') {
            action = 'GetTopProducts';
            this.setState({ ...this.state, name: 'Top' })
        }

        axios.post('api/product/' + action)
            .then((res) => {
                try {
                    if (res.data.status === true) {
                        if (res.data.data) {
                            this.setState({ ...this.state, productList: res.data.data });
                        }
                    } else {
                        alert('Error in connect to server!');
                    }
                }
                catch(e) {
                    console.log(e);
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
        return (
            <React.Fragment>
                <h3>{this.state.name} Products</h3>
                <hr />
                {this.state.loading ? <Spinner /> :
                    <ul className="list-group">
                        {this.state.productList.map((item) => { return (
                            <li key={item.id} className="list-group-item">
                                <Link to={'/Product/' + item.id}>{item.title}</Link>
                            </li>
                        )})}
                    </ul>}
            </React.Fragment>
        )
    }
}

export default Products