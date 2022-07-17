import axios from "axios";

const api = axios.create({
  baseURL:
    "https://auditmanagementservicegateway20220716005051.azurewebsites.net",
});

let token;
let quest = [];
let res1;
const ApiCaller = async (props) => {
  let url = props.url;

  if (url == "Auth") {
    const res = await api
      .post("/" + url, {
        UserName: props.username,
        Password: props.password,
      })
      .then((response) => {
        token = response.data;
        console.log(token);
        return response.status;
      });
    return res;
  }
  if (url == "AuditChecklist/Internal" || url == "AuditChecklist/SOX") {
    let quest = api
      .get("/" + url, {
        headers: {
          Authorization: "Bearer " + token,
          "Access-Control-Allow-Origin": "true",
        },
      })
      .then((res) => {
        console.log(res);
        if (res.status !== 200) {
          res.data = "Error";
        }
        return res.data;
      });
    return quest;
  }
  if (url == "AuditSeverity") {
    const data = {
      ProjectName: props.ProjectName,
      ProjectManagerName: props.ProjectManagerName,
      ApplicationOwnerName: props.ApplicationOwnerName,
      AuditDetail: {
        AuditType: props.AuditDetail.AuditType,
        AuditDate: props.AuditDetail.AuditDate,
        AuditQuestions: props.AuditDetail.AuditQuestions,
      },
    };
    const headers = {
      Authorization: "Bearer " + token,
      "Access-Control-Allow-Origin": "true",
    };
    res1 = await api
      .post("/" + url, data, {
        headers: headers,
      })
      .then((response) => {
        console.log(response);
        return response.data;
      });
    return res1;
  }
};

export default ApiCaller;
