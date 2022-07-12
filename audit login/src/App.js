
import './App.css';
import React,{useLink, useState} from 'react';
import ReactDOM from "react-dom";
import {Button} from 'react-bootstrap';
import {Link,Navigate,useNavigate} from 'react-router-dom'
import './pages/Types';


function App() {

  ///const navigate=useNavigate();
  const [errorMessage,setErrorMessage]=useState({});
  const [isSubmitted,setIsSubmitted ]= useState(false);

  const database = [
  {
    username: "user1",
    password: "pass1"
  },
  {
    username: "user2",
    password: "pass2"
  }
];
const errors = {
  uname: "invalid username",
  pass: "invalid password"
};
  //const navigate=useNavigate();
  const handleSubmit = (event) => {
    event.preventDefault();
    var[uname,pass]=document.forms[0];

    const userdata = database.find((user) => user.username===uname.value);
    if(userdata)
    {
      if(userdata.password!==pass.value)
      {
        setErrorMessage({name:"pass",message:errors.pass});
      }
      else{
        setIsSubmitted(true);
        Navigate('/pages/Types.js')
        
      }
    }
    else{
      setErrorMessage({name:"uname",message:errors.uname});
    }

  };

  const renderErrorMessage = (name) =>
    name === errorMessage.name && (
      <div className="error">{errorMessage.message}</div>
    );

    const renderForm = (
      <div className='form'>
        <form onSubmit={handleSubmit}>
          <div className='input-container'>
            <label>USERNAME</label>
            <input type="text" name='uname' required/>
            {renderErrorMessage("uname")}
          </div>
          <div className='input-container'>
            <label>PASSWORD</label>
            <input type="password" name='pass' required/>
            {renderErrorMessage("pass")}
          </div>
          <div className='button-container'>
            <Button as="input" type="submit" value="Submit" />
          </div>
        </form>
      </div>
    );
     return (
    <div className="app">
      <div className="login-form">
        <div className="title">Sign In</div>
        {isSubmitted ? <div>User is successfully logged in</div> : renderForm}
      </div>
    </div>
  );


}

export default App;
