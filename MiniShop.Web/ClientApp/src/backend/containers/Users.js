import React, { Component } from 'react';
import axios from 'axios';
import { connect } from 'react-redux';
import Spinner from '../../Spinner'

class Users extends Component {

    constructor(props) {
        super(props);

        this.state = {
            userList: [],
            loading: true
        }

        this.approveUser = this.approveUser.bind(this);
    }

    componentDidMount() {
        axios.post('api/account/GetAllUsers', {
            token: this.props.token
        })
            .then((res) => {
                if (res.data.status === true) {
                    if (res.data.data) {
                        var list = res.data.data.map((item) => { return { ...item, loading: false } });
                        this.setState({ ...this.state, userList: list })
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

    approveUser(token) {
        let userList = this.state.userList;
        var index = userList.findIndex(x => x.token === token);
        userList[index].loading = true;
        this.setState({ ...this.state, userList: userList });

        axios.post('api/account/approveUser', {
            token: this.props.token,
            userToken: token
        })
            .then((res) => {
                if (res.data.status === true &&
                    res.data.data === true) {
                    userList[index].isApproved = true;
                    this.setState({ ...this.state, userList: userList });
                } else {
                    alert(res.data.errorMessage);
                }
            })
            .catch((err) => {
                alert('Error in connect to server!');
            })
            .then(() => {
                userList[index].loading = false;
                this.setState({ ...this.state, userList: userList });
            });
    }

    render() {
        return (
            <React.Fragment>
                <h3>Usres</h3>
                <hr />
                {this.state.loading ? <Spinner /> :
                    <table className="table">
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">First Name</th>
                                <th scope="col">Last Name</th>
                                <th scope="col">Username</th>
                                <th scope="col">Status</th>
                            </tr>
                        </thead>
                        <tbody>
                            {this.state.userList.map((item, i) => {
                                return (<tr key={item.token}>
                                    <th scope="row">{i + 1}</th>
                                    <td>{item.firstName}</td>
                                    <td>{item.lastName}</td>
                                    <td>{item.username}</td>
                                    <td>{!item.isApproved ?
                                        !item.loading ?
                                            <button className="btn btn-sm btn-primary" type="button" onClick={() => this.approveUser(item.token)}>Approve</button>
                                            : <Spinner align={"left"} />
                                        : <span>Approved</span>}</td>
                                </tr>)
                            })}
                        </tbody>
                    </table>}
            </React.Fragment>
        );
    }
}

const mapStateToProps = state => {
    return {
        token: state.token
    };
};

export default connect(mapStateToProps)(Users)