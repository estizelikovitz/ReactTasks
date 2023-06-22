import axios from 'axios';
import React, { useEffect, useState, useRef } from 'react';
import { useHistory } from 'react-router-dom';
import { useAuthContext } from './AuthContext';
import { HubConnectionBuilder } from '@microsoft/signalr';


const Home = () => {
    const history = useHistory();
    const { user } = useAuthContext();
    const [taskTitle, setTaskTitle] = useState('');
    const [tasks, setTasks] = useState([]);

    const connectionRef = useRef(null);



    useEffect(() => {
            const connectToHub = async () => {
            const connection = new HubConnectionBuilder().withUrl("/taskshub").build();
            await connection.start();
                connectionRef.current = connection;

                //connection.invoke("GetAll");

                connection.on('refreshTasks', tasks => setTasks(tasks));

        }
        connectToHub();

    }, []);
    
    const onChooseClick = async task => {
        await connectionRef.current.invoke('choose', task);
    }

    const onDoneClick = async (task) => {
        await connectionRef.current.invoke('completed', { task });
    }



    const onAddClick = async () => {
        await connectionRef.current.invoke('newTask', taskTitle);
        setTaskTitle('');
        await console.log(tasks);

    }

    return <>
        <div className="row">
            <div className="col-md-10">
                <input type="text" className="form-control" placeholder="Task Title" value={taskTitle} onChange={e => setTaskTitle(e.target.value)} />
            </div>
            <div className="col-md-2">
                <button className="btn btn-primary w-100" onClick={onAddClick}>Add Task</button>
            </div>
        </div>

        <table className="table table-hover table-striped table-bordered mt-3">
            <thead>
                <tr>
                    <th>Title</th>
                    <th>Status</th>
                </tr>
            </thead>
            <tbody>
                {tasks.map(task => {
                    return <tr key={task.id}>
                        <td>{task.title}</td>
                        <td>{!task.beingDoneBy && <button className="btn btn-dark" onClick={()=>onChooseClick(task)}>I'm doing this one!</button>}
                            {!!task.beingDoneBy && task.beingDoneBy == user.id && <button className="btn btn-success" onClick={()=>onDoneClick(task)}>I'm done!</button>}
                            {!!task.beingDoneBy && task.beingDoneBy != user.id && <button className="btn btn-warning" disabled={true} >{task.userName}is doing this</button>}
                        </td>
                    </tr>
                })}
            </tbody>
        </table>
    </>
}

export default Home;