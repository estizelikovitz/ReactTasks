import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Link, useHistory } from 'react-router-dom';


const SignUp = () => {

    const [user, setUser] = useState({name: '', email: '', passwordHash: '' });
    const history = useHistory();
    const onTextChange = (e) => {
        const newUser = { ...user };
        newUser[e.target.name] = e.target.value;
        setUser(newUser);
    }

    const onSubmitClick = async (e) => {
        e.preventDefault();
        await axios.post('/api/tasks/adduser', user);
        history.push('/login');

    }

    const { name, email, passwordHash } = user;

    return (
        <>
            <div>
                <br />
                <br />
                <br />
                <br />
                <div className="row" style={{ minHeight: 80 }}>
                    <div className="col-md-6 offset-md-3 card card-body bg-light">
                        <h3>Sign up for a new account</h3>
                        <form onSubmit={onSubmitClick}>
                            <input type="text" value={name} name="name" placeholder="Full Name" className="form-control" onChange={onTextChange} />
                            <br />
                            <input type="text" value={email} name="email" placeholder="Email" className="form-control" onChange={onTextChange} />
                            <br />
                            <input type="password" value={passwordHash} name="passwordHash" placeholder="Password" className="form-control" onChange={onTextChange} />
                            <br />
                            <button className="btn btn-primary" >Signup</button>
                        </form>
                    </div>
                </div>
            </div>
        </>
    )
}
export default SignUp;