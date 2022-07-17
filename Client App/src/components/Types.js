import React from "react";
import { useNavigate, useLocation } from "react-router-dom";
import { Button } from "react-bootstrap";
import "../styles/sam.css";
import ApiCaller from "./ApiCaller";

function Types() {
  const location = useLocation();
  const navigate = useNavigate();

  const handleInternal = async (event) => {
    event.preventDefault();

    const data = {
      url: "AuditChecklist/Internal",
    };
    const quest1 = await ApiCaller(data);
    if (quest1 === "Error") {
      navigate("/Error");
    } else {
      navigate("/internal", {
        state: {
          typeId: 0,
          questions: quest1,
          name: location.state.name,
        },
      });
    }
  };
  const handleSox = async (event) => {
    event.preventDefault();

    const data = {
      url: "AuditChecklist/SOX",
    };
    const quest2 = await ApiCaller(data);
    if (quest2 === "Error") {
      navigate("/Error");
    } else {
      navigate("/internal", {
        state: {
          typeId: 1,
          questions: quest2,
          name: location.state.name,
        },
      });
    }
  };
  return (
    <div className="jk">
      <div className="heading">
        <h2 className="caption">SELECT AN AUDIT TYPE</h2>
      </div>
      <div>
        <Button
          className="B1 card-btn"
          variant="warning"
          onClick={handleInternal}
        >
          INTERNAL
        </Button>
        <Button className="B2 card-btn" variant="warning" onClick={handleSox}>
          SOX
        </Button>
      </div>
    </div>
  );
}

export default Types;
