import React, { useEffect, useState } from "react";
import "../styles/Internal.css";
import Progressbar from "react-js-progressbar";
import { Button, Form } from "react-bootstrap";
import { useLocation, useNavigate } from "react-router-dom";
import ApiCaller from "./ApiCaller";

function Internal() {
  const location = useLocation();
  const navigate = useNavigate();
  const [questions, setQuestions] = useState([]);
  const [showScore, setShowScore] = useState(true);

  const [ansList, setAnsList] = useState([]);
  const [proj, setProj] = useState("");
  let ans;
  let d;
  const [index, setIndex] = useState(0);
  const scor = {};
  const name = location.state.name;
  const typeId = location.state.typeId;
  const current = new Date();
  const date = `${current.getMonth()}/${current.getDate()}/${current.getFullYear()}`;
  let outs;

  const handleAnswerOptionClick = async (event) => {
    const a = event.target.value;
    console.log(a);

    setAnsList((ansList) => {
      ans = [...ansList, { Id: index + 1, Ans: event.target.value }];
      //console.log(updated);
      return ans;
    });
    console.log(ans);
    // setAnsList((ansList) => [
    //   ...ansList,
    //   { Id: index + 1, Ans: event.target.value },
    // ]);
    //console.log(ansList);
    if (index < rows.length - 1) {
      setIndex(index + 1);
    } else {
      outs = await submitauth();
      navigate("/result", { state: { output: outs } });
    }
  };

  const handleProj = (event) => {
    event.preventDefault();

    setShowScore(false);
  };

  const submitauth = async () => {
    const data = {
      url: "AuditSeverity",
      ProjectName: proj,
      ProjectManagerName: name,
      ApplicationOwnerName: name,
      AuditDetail: {
        AuditType: typeId,
        AuditDate: date,
        AuditQuestions: ans,
      },
    };
    console.log(data);
    d = await ApiCaller(data);
    return d;
  };

  const questionss = location.state.questions;
  const rows = questionss.map((que) => {
    return <div>{que.question}</div>;
  });

  return (
    <div className="internal">
      <>
        {showScore ? (
          <div className="pro">
            <form onSubmit={handleProj}>
              <label className="lab">Project Name</label>

              <input
                type="text"
                className="pro-name"
                value={proj}
                onChange={(e) => setProj(e.target.value)}
                required
              ></input>

              <button className="button-color">Submit</button>
            </form>
          </div>
        ) : (
          <div>
            <div className="question-section">
              <br />
              <div className="question-count">
                <Progressbar
                  input={(index / questionss.length) * 100}
                  pathWidth={10}
                  pathColor={["#56ab2f", "#311bbf"]}
                  trailWidth={20}
                  size={100}
                  shape={"full circle"}
                  trailColor="#363636"
                  textStyle={{ fill: "red", fontSize: "100px" }}
                  customText={index + 1}
                ></Progressbar>
              </div>
              <div className="question-text">{rows[index]}</div>
            </div>
            <div className="answer-section">
              <button
                className="internal-btn"
                value="Yes"
                onClick={handleAnswerOptionClick}
              >
                Yes
              </button>
              <button
                className="internal-btn"
                value="No"
                onClick={handleAnswerOptionClick}
              >
                No
              </button>
            </div>
          </div>
        )}
      </>
    </div>
  );
}

export default Internal;
