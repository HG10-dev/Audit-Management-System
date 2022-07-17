import { BrowserRouter, Routes, Route } from "react-router-dom";
import Signup from "./components/Signup";
import Types from "./components/Types";
import Internal from "./components/Internal";
import Result from "./components/Result";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Signup />} />
        <Route path="/types" element={<Types />} />
        <Route path="/internal" element={<Internal />} />
        <Route path="/result" element={<Result />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
