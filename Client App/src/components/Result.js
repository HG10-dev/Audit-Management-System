import React from "react";
import { useLocation } from "react-router-dom";
import "../styles/result.css";

function Result() {
  const location = useLocation();

  const executionStatus = location.state.output.projectExecutionStatus;
  const actionDuration = location.state.output.remedialActionDuration;
  console.log(executionStatus);
  console.log(actionDuration);
  return (
    <div className="main">
      <div
        className="abcd"
        style={{
          background:
            executionStatus === 1
              ? "linear-gradient(to top right,  #4BE369,#4FDBA7)"
              : "linear-gradient(to top right,  #FF9D6F,#FE5366)",
        }}
      >
        <h2 className="ti1">RESULT</h2>
        <hr className="line" />
        {executionStatus === 1 ? (
          <h1 className="content">No action Needed</h1>
        ) : (
          <h1 className="content">
            Action to be taken in {actionDuration} weeks
          </h1>
        )}
      </div>
    </div>
  );
}

export default Result;
