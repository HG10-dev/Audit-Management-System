import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import "../styles/Signup.css";
import { Button } from "react-bootstrap";
import ApiCaller from "./ApiCaller";

function Signup() {
  const navigate = useNavigate();
  const initialValues = { username: "", email: "", password: "" };
  const [formValues, setFormValues] = useState(initialValues);
  const [formErrors, setFormErrors] = useState({});
  const [isSubmit, setIsSubmit] = useState(false);
  let stat;
  const [isAlertVisible, setIsAlertVisible] = useState(false);

  const handleAlert = () => {
    setIsAlertVisible(true);
    // setTimeout(() => {
    //   setIsAlertVisible(false);
    // }, 3000);
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormValues({ ...formValues, [name]: value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    const val = validate(formValues);

    setFormErrors(val);
    console.log(Object.keys(val).length);
    if (Object.keys(val).length == 0) {
      const data = {
        url: "Auth",
        username: formValues.username,
        password: formValues.password,
      };
      try {
        stat = await ApiCaller(data);
        console.log(stat);
        // setIsSubmit(true);
        // navigate("/types", { state: { name: formValues.username } });
      } catch (e) {
        handleAlert();
      }
      if (stat === 200) {
        setIsSubmit(true);
        navigate("/types", { state: { name: formValues.username } });
      }
      //   //setIsSubmit(true);
      // } else {
      //   console.log("else");
      //   handleAlert();
      // }
    }
  };
  useEffect(() => {
    console.log(formErrors);
    if (Object.keys(formErrors).length === 0 && isSubmit) {
      console.log(formValues);
    }
  }, [formErrors]);
  const validate = (values) => {
    const errors = {};
    if (!values.username) {
      errors.username = "Username is required!";
    }

    if (!values.password) {
      errors.password = "Password is required!";
    }
    console.log(errors);
    return errors;
  };

  return (
    <div className="container">
      {/* {Object.keys(formErrors).length === 0 && isSubmit ? (
        <div className="ui message success">Signed in successfully</div>
      ) : (
        <pre>{JSON.stringify(formValues, undefined, 2)}</pre>
      )} */}

      <form onSubmit={handleSubmit}>
        <h1 className="title">Login </h1>

        <div className="ui divider"></div>
        <div className="ui form">
          <div className="field">
            <label>Username</label>
            <br />
            <input
              type="text"
              name="username"
              placeholder="Username"
              value={formValues.username}
              onChange={handleChange}
            />
          </div>
          <p>{formErrors.username}</p>

          <div className="field">
            <label>Password</label>
            <br />
            <input
              type="password"
              name="password"
              placeholder="Password"
              value={formValues.password}
              onChange={handleChange}
            />
          </div>
          <p>{formErrors.password}</p>
          <div className="button-container">
            <button className="fluid ui button blue button-color">Login</button>
          </div>
          {isAlertVisible && <p>Invalid Username Or Password</p>}
        </div>
      </form>
    </div>
  );
}

export default Signup;
